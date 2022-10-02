namespace invoice_project.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string adderss { get; set; }
        public string phone { get; set; }
        public virtual List<Invoice>? invoices { get; set; }
    }
}
