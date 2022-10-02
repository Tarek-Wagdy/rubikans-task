namespace invoice_project.Models
{
    public class Store
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string address { get; set; }
        public virtual List<Invoice>? Invoices { get; set; }
    }
}
