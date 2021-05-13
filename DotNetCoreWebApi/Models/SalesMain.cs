using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreWebApi.Models
{
    public class SalesMain
    {
        public SalesMain()
        {
            SalesItems = new List<SalesSub>();
            DeletedOrderItemIDs = new List<int>();
        }
        
        public int SalesMainID { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime SalesDate { get; set; }

        [Required]
        public double TotalAmount { get; set; }
        [NotMapped]
        public List<int> DeletedOrderItemIDs { get; set; }

        public virtual List<SalesSub> SalesItems { get; set; }

    }
}
