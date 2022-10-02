﻿using System.ComponentModel.DataAnnotations.Schema;

namespace invoice_project.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public int totalCost { get; set; }
        public float tax { get; set; }
        public int discount { get; set; }
        public DateTime invoice_date { get; set; }
        [ForeignKey("customer")]
        public int customer_id { get; set; }
        [ForeignKey("Store")]
        public int store_id { get; set; }
        public virtual Customer? customer { get; set; }
        public virtual Store? Store { get; set; }
        //public virtual List<Item>? Items { get; set; }
        public Invoice()
        {
            invoice_date=DateTime.Now;
        }
    }
}
