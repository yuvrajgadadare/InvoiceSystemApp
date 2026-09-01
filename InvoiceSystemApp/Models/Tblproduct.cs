using System;
using System.Collections.Generic;

namespace InvoiceSystemApp.Models;

public partial class Tblproduct
{
    public int ProductId { get; set; }

    public string ProductName { get; set; } = null!;

    public double Rate { get; set; }

    public double Gst { get; set; }

    public double StockQuantity { get; set; }

    public virtual ICollection<TblinvoiceProduct> TblinvoiceProducts { get; set; } = new List<TblinvoiceProduct>();
}
