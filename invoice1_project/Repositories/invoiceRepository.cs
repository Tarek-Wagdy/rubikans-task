using invoice_project.Models;
using Microsoft.EntityFrameworkCore;

namespace invoice1_project.Repositories
{
    public class invoiceRepository : IinvoiceRepository
    {
        Entity context;
        public invoiceRepository(Entity context)
        {
            this.context = context;
        }

       

        public List<Invoice> getAll()
        {
            List<Invoice> invoices = context.Invoice.Include(e=>e.customer).ToList();
            return invoices;
        }

        public Invoice getByid(int id)
        {
            Invoice invoice = context.Invoice.Include(e => e.customer)
                .Include(e => e.Store).Where(e => e.Id == id).First();
            return invoice;
        }

        public void insert(Invoice invoice)
        {
            context.Invoice.Add(invoice);
            context.SaveChanges();
        }

        public void update(int id, Invoice invoice)
        {
            Invoice old_invoice =getByid(id);
            old_invoice.customer_id=invoice.customer_id;
            old_invoice.store_id=invoice.store_id;
            old_invoice.totalCost=invoice.totalCost;
            old_invoice.discount=invoice.discount;
            context.SaveChanges();
        }
        public void delete(int id)
        {
            Invoice invoice = getByid(id);
            context.Invoice.Remove(invoice);
            context.SaveChanges();
        }
    }
}
