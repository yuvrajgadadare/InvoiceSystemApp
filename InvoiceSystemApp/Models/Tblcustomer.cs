using System;
using System.Collections.Generic;

namespace InvoiceSystemApp.Models;

public partial class Tblcustomer
{
    public int CustomerId { get; set; }

    public string CustomerName { get; set; } = null!;

    public string? EmailAddress { get; set; }

    public string? MobileNumber { get; set; }

    public string? City { get; set; }

    public virtual ICollection<TblinvoiceDetail> TblinvoiceDetails { get; set; } = new List<TblinvoiceDetail>();
}
