using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WebApplication7.Helper;
using WebApplication7.Models;

namespace WebApplication7.Controllers
{
    public class HomeController : Controller
    {
        ProjectAPI _api = new ProjectAPI();
        public async Task<IActionResult> Index()
        {
            ViewBag.ProjectId = 0;
            ViewBag.name = "-";
            ViewBag.owner = "-";
            ViewBag.duedate = DateTime.Now.AddDays(0).ToString("dd-MM-yyyy");

            //ViewBag.Groups = GetGroups().Select(x => new SelectListItem()
            //{
            //    Text = x.text,
            //    Value = x.id
            //}) ;

            Project list = new Project();
            List<Project> project = new List<Project>();

            HttpClient client = _api.initial();
            HttpResponseMessage res = await client.GetAsync("api/project/get-all-projects");
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                project = JsonConvert.DeserializeObject<List<Project>>(result);
                foreach (var o in project)
                {
                    list.project.Add(new Project
                    {
                        title = o.title,
                        owner = o.owner,
                        duedate = o.duedate,
                        ProjectId = o.ProjectId
                    });


                }
            }

            List<Project> model = list.project.ToList();


            ViewBag.Groups = model.Select(x => new SelectListItem()
            {
                Text = x.title,
                Value = x.owner
            });


            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        private List<Project> GetGroups()
        {
            return new List<Project>()
            {
                new Project(){id="All", text = "All" },
                new Project(){id="1", text = "A" },
                new Project(){id="2", text = "B" },
                new Project(){id="3", text = "C" }
            };
        }

        public async Task<ActionResult<Project>> Save(Project model)
        {
            HttpClient client = _api.initial();
            var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var action = "api/project/update-project-by-id/" + model.ProjectId;
            if (model.ProjectId == 0)
            {
                action = "api/project/add-project";
            }
            HttpResponseMessage res = await client.PostAsync(action, content).ConfigureAwait(false);
            res.EnsureSuccessStatusCode();
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
            }

            return RedirectToAction("Index", "Home");

        }

        public async Task<ActionResult<Project>> DeleteData(Project model)
        {
            HttpClient client = _api.initial();
            var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var action = "api/project/delete-project-by-id/" + model.ProjectId;
            HttpResponseMessage res = await client.PostAsync(action, content).ConfigureAwait(false);
            res.EnsureSuccessStatusCode();
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
            }

            return RedirectToAction("Index", "Home");

        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
