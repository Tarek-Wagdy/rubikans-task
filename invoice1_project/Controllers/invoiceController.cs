using invoice_project.Models;
using invoice1_project.Repositories;
using invoice1_project.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace invoice1_project.Controllers
{
    public class invoiceController : Controller
    {
        private readonly IinvoiceRepository iinvoiceRepository;
        Entity context;
        public invoiceController(IinvoiceRepository iinvoiceRepository, Entity context)
        {
            this.iinvoiceRepository = iinvoiceRepository;
            this.context = context;
        }
        [HttpGet]
        public IActionResult New(int itemId)
        {
            HttpContext.Session.Clear();
            ViewBag.customers = context.Customer.ToList();
            ViewBag.stores = context.Store.ToList();
            ViewBag.items = context.Item.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult New(Invoice invoice)
        {
            float totalPrice = 0;
            float tax = 0.0f;
            List<InvoiceItem> invoiceItems = GetFromSession<List<InvoiceItem>>("items");
            if(invoiceItems==null)
            {
                
                ViewBag.customers = context.Customer.ToList();
                ViewBag.stores = context.Store.ToList();
                ViewBag.items = context.Item.ToList();
                return View("New");

            }
            else
            {
                foreach (InvoiceItem item in invoiceItems)
                {
                    totalPrice += item.Item.price * item.quantity;
                }
                tax = totalPrice * 0.14f;
                invoice.totalCost = totalPrice+tax;
                invoice.tax = tax;
                iinvoiceRepository.insert(invoice);
                int invoiceId = invoice.Id;
                //insert invoice items
                foreach (InvoiceItem item in invoiceItems)
                {
                    InvoiceItem invoiceItem = new InvoiceItem() { invoice_id = invoiceId, item_id = item.item_id, quantity = item.quantity };
                    context.InvoiceItem.Add(invoiceItem);
                    context.SaveChanges();
                }
            }
            


            //clear session 
            HttpContext.Session.Clear();

            return RedirectToAction("New", "invoice");

        }
        [HttpGet]
        public TaxAndCostViewModel GetTotalCost()
        {
            float totalTax = 0;
            float totalPrice = 0;
            float InvoiceTotalCost = 0;
            TaxAndCostViewModel costs = new TaxAndCostViewModel();
            List<InvoiceItem> invoiceItems = GetFromSession<List<InvoiceItem>>("items");
            if (invoiceItems != null)
            {
                foreach (InvoiceItem item in invoiceItems)
                {
                    totalTax += (item.Item.price * item.quantity * 0.14f);
                    totalPrice +=(item.Item.price * item.quantity);
                    
                }
                InvoiceTotalCost = totalPrice+totalTax;
                costs.Cost = totalPrice;
                costs.Tax = totalTax;
                costs.NetCost = InvoiceTotalCost;
                return costs;
            }
            return null;
        }

        [HttpPost]
        public Item Add(int itemId, int quantity)
        {
            Item item = context.Item.FirstOrDefault(item => item.Id == itemId);
            List<InvoiceItem> items = GetFromSession<List<InvoiceItem>>("items") ?? new List<InvoiceItem>();
            if (item != null)
            {
                items.Add(new InvoiceItem() { Item = item, item_id = itemId, quantity = quantity });
                AddToSession("items", items);
            }
            return item;
        }

        private void AddToSession(string key, object value)
        {
            HttpContext.Session.SetString(key, JsonConvert.SerializeObject(value, Formatting.None,
                        new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        }));
        }

        public T GetFromSession<T>(string key)
        {
            var value = HttpContext.Session.GetString(key);
            return value == null ? default : JsonConvert.DeserializeObject<T>(value);
        }
    }


}
