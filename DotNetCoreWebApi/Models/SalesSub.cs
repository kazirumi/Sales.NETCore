using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreWebApi.Models
{
    public class SalesSub
    {
        public int SalesSubID { get; set; }

        
        [ForeignKey("SalesMainID")]
        public int SalesMainID { get; set; }

        public SalesMain salesMain { get; set; }

       
        [ForeignKey("ItemID")]
        public int ItemID { get; set; }

        public Item Item { get; set; }

        [Required]
        public int ItemQuantity { get; set; }

        [Required]
        public double TotalPrice { get; set; }
    }
}
