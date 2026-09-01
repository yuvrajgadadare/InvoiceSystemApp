using System;
using System.Collections.Generic;

namespace InvoiceSystemApp.Models;

public partial class TblinvoiceDetail
{
    public int InvoiceId { get; set; }

    public int CustomerId { get; set; }

    public DateOnly InvoiceDate { get; set; }

    public double? InvoiceAmount { get; set; }

    public virtual Tblcustomer Customer { get; set; } = null!;

    public virtual ICollection<TblinvoicePayment> TblinvoicePayments { get; set; } = new List<TblinvoicePayment>();

    public virtual ICollection<TblinvoiceProduct> TblinvoiceProducts { get; set; } = new List<TblinvoiceProduct>();
}
