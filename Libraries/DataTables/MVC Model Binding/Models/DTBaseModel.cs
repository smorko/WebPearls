using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebPearls.DataTables.Models
{
    public class DTBaseModel
    {
        public List<string> GetAllPropertyValues()
        {
            var values = new List<string>();
            foreach (var pi in this.GetType().GetProperties())
            {
                values.Add(pi.GetValue(this, null).ToString());
            }
            return values;
        }
        
        public List<string> GetAllPropertyNames()
        {
            var values = new List<string>();
            foreach (var pi in this.GetType().GetProperties())
            {
                values.Add(pi.Name);
            }
            return values;
        }

        public string GetDTColumns()
        {
            var fields = this.GetAllPropertyNames();
            var cols = "";
            foreach (var fld in fields)
            {
                cols += string.Format(@"
                        {{ ""data"": ""{0}"" }},"
                , fld);
            }
            return cols;
        }

        public string GetOrderByColumnName(int colPos)
        {
            var fields = this.GetAllPropertyNames();
            return fields.ElementAt(colPos);
        }
    }
}