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
    
    public partial class TipoFactura
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TipoFactura()
        {
            this.Facturas = new HashSet<Factura>();
        }
    
        public int Id { get; set; }
        public string Codigo { get; set; }
        public Nullable<int> Secuencia { get; set; }
        public string Grupo { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Factura> Facturas { get; set; }
    }
}
