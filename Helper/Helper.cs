using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebApplication7.Helper
{
    public class ProjectAPI
    {
        public HttpClient initial()
        {
            var Client = new HttpClient();
            Client.BaseAddress = new Uri("http://localhost:64288");
            return Client;
        }
    }
}
