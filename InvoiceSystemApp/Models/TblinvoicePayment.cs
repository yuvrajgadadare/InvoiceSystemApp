using System;
using System.Collections.Generic;

namespace InvoiceSystemApp.Models;

public partial class TblinvoicePayment
{
    public int PaymentId { get; set; }

    public int InvoiceId { get; set; }

    public double PaymentAmount { get; set; }

    public string PaymentMode { get; set; } = null!;

    public string? PaymentDescription { get; set; }

    public DateTime? PaymentDate { get; set; }

    public virtual TblinvoiceDetail Invoice { get; set; } = null!;
}
