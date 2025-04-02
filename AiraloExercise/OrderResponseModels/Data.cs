using Microsoft.VisualStudio.CodeCoverage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiraloExercise.OrderResponseModels
{
    public class Data
    {
        public int id { get; set; }
        public string code { get; set; }
        public string currency { get; set; }
        public string package_id { get; set; }
        public int quantity { get; set; }
        public string type { get; set; }
        public string description { get; set; }
        public string esim_type { get; set; }
        public int validity { get; set; }
        public string package { get; set; }
        public string data { get; set; }
        public double price { get; set; }
        public string created_at { get; set; }
        public List<Sim> sims { get; set; }
    }
}
