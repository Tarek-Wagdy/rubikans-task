using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace invoice_project.Models
{
    public class InvoiceItem
    {
       
        [Column(Order =0),Key,ForeignKey("Invoice")]
        public int invoice_id { get; set; }
        [Column(Order =1),Key,ForeignKey("Item")]
        public int item_id { get; set; }
        public int quantity { get; set; }
        public virtual Invoice Invoice { get; set; }
        public virtual Item Item { get; set; }
    }
}
