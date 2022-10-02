using invoice_project.Models;

namespace invoice1_project.Repositories
{
    public interface IinvoiceRepository
    {
        List<Invoice> getAll();
        Invoice getByid(int id);
        void insert (Invoice invoice);
        void update (int id,Invoice invoice);
        void delete (int id);

    }
}
