//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GUI_V_2.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Factura
    {
        public int Id { get; set; }
        public Nullable<int> IdCliente { get; set; }
        public Nullable<int> IdTipo { get; set; }
        public Nullable<decimal> Total_Facturado { get; set; }
        public Nullable<decimal> Total_Pagado { get; set; }
        public Nullable<decimal> Total_Descuento { get; set; }
        public Nullable<decimal> Fecha_Facturacion { get; set; }
        public Nullable<decimal> Fecha_Vencimiento { get; set; }
    }
}