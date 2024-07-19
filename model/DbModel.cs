using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApiClient.model
{
    public class DbModel
    {
        public int id { get; set; }
        public string question { get; set; }
        public string answer { get; set; }
        public double value { get; set; }
        
    }
}
