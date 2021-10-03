// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//window onload
var ci,ri;
jQuery(document).ready(function() {


    drag();
    

    jQuery("#table_data td")
        .mouseover(function () {
            ci = jQuery(this).parent().children().index(this);
            ri = jQuery(this).parent().parent().children().index(this.parentNode);
        })
        .click(function () {
        var ri2 = jQuery(this).parent().parent().children().index(this.parentNode);
        var table = document.getElementById("table_data");
        document.getElementById("ProjectId").value = table.rows[ri2].cells[0].innerText;
        document.getElementById("title").value = table.rows[ri2].cells[1].innerText;
        document.getElementById("owner").value = table.rows[ri2].cells[2].innerText;
        document.getElementById("duedate").value = table.rows[ri2].cells[3].innerText;

        })
        .droppable({
            accept: ".dragitem",
            drop: function (event, ui) {
                var table = document.getElementById("table_data");
                var ci_ = jQuery(this).parent().children().index(this);
                var ri_ = jQuery(this).parent().parent().children().index(this.parentNode);

                if (ri_!=ri || ci_ != ci) {
                    table.rows[ri_].cells[ci_].innerHTML += "<div class='dragitem'>" + ui.draggable[0].innerText + "</div>";
                    jQuery(ui.draggable).remove();
                    drag();
                }
                

            }
        })

    jQuery("#thisgroup").change(function () {
        var obj = document.getElementById("thisgroup");
        document.getElementById("title").value = obj.options[obj.selectedIndex].text;
        document.getElementById("owner").value = obj.options[obj.selectedIndex].value;

    });


});

function drag() {
    jQuery(".dragitem").draggable({
        revert: true,
        start: function () {
            this.style.zIndex = 5;
        }
    })
}

