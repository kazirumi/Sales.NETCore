using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreWebApi.Models
{
    public class Item
    {
        public int ItemID { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string ItemName { get; set; }
        public double ItemRate { get; set; }
    }
}
