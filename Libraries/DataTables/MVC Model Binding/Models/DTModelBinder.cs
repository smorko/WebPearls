using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebPearls.DataTables.Models
{
    public class DTModelBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            base.BindModel(controllerContext, bindingContext);
            var request = controllerContext.HttpContext.Request;
            // Retrieve request data
            var draw = Convert.ToInt32(request["draw"]);
            var start = Convert.ToInt32(request["start"]);
            var length = Convert.ToInt32(request["length"]);
            
            // Add some extra search parameters
            var extra_search1 = request["extra_search1"] != null ? request["extra_search1"] : "";
            var extra_search2 = request["extra_search2"] != null ? request["extra_search2"] : "";
            var extra_search3 = request["extra_search3"] != null ? request["extra_search3"] : "";
            // Search
            var search = new DTSearch
            {
                Value = request["search[value]"],
                Regex = Convert.ToBoolean(request["search[regex]"])
            };
            
            // Order
            var o = 0;
            var order = new List<DTOrder>();
            while (request["order[" + o + "][column]"] != null)
            {
                order.Add(new DTOrder
                {
                    Column = Convert.ToInt32(request["order[" + o + "][column]"]),
                    Dir = request["order[" + o + "][dir]"]
                });
                o++;
            }
            
            // Columns
            var c = 0;
            var columns = new List<DTColumn>();
            while (request["columns[" + c + "][name]"] != null)
            {
                columns.Add(new DTColumn
                {
                    Data = request["columns[" + c + "][data]"],
                    Name = request["columns[" + c + "][name]"],
                    Orderable = Convert.ToBoolean(request["columns[" + c + "][orderable]"]),
                    Searchable = Convert.ToBoolean(request["columns[" + c + "][searchable]"]),
                    Search = new DTSearch
                    {
                        Value = request["columns[" + c + "][search][value]"],
                        Regex = Convert.ToBoolean(request["columns[" + c + "][search][regex]"])
                    }
                });
                c++;
            }

            return new DTParameterModel
            {
                Draw = draw,
                Start = start,
                Length = length,
                Search = search,
                Order = order,
                Columns = columns,
                // Add some extra search parameters
                ExtraSearch1 = extra_search1,
                ExtraSearch2 = extra_search2,
                ExtraSearch3 = extra_search3,
            };
        }
    }
}