using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiraloExercise.SimListResponseModels
{
    public class Simable
    {
        public int id { get; set; }
        public string code { get; set; }
        public string package_id { get; set; }
        public string currency { get; set; }
        public int quantity { get; set; }
        public string type { get; set; }
        public string description { get; set; }
        public string esim_type { get; set; }
        public string validity { get; set; }
        public string package { get; set; }
        public string data { get; set; }
        public string price { get; set; }
        public string created_at { get; set; }
    }
}
