namespace invoice_project.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int price { get; set; }
        
        public string unit { get; set; }
        //public virtual List<Invoice>? Invoices { get; set; }
    }
}
