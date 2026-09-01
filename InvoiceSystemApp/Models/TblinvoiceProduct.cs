using System;
using System.Collections.Generic;

namespace InvoiceSystemApp.Models;

public partial class TblinvoiceProduct
{
    public int InvoiceProductId { get; set; }

    public int InvoiceId { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public virtual TblinvoiceDetail Invoice { get; set; } = null!;

    public virtual Tblproduct Product { get; set; } = null!;
}
