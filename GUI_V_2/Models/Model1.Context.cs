﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class POSEntities : DbContext
    {
        public POSEntities()
            : base("name=POSEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Almacen> Almacens { get; set; }
        public virtual DbSet<Categoria> Categorias { get; set; }
        public virtual DbSet<Factura> Facturas { get; set; }
        public virtual DbSet<FacturaDetalle> FacturaDetalles { get; set; }
        public virtual DbSet<Producto> Productos { get; set; }
        public virtual DbSet<Recibo> Reciboes { get; set; }
        public virtual DbSet<Contacto> Contactoes { get; set; }
        public virtual DbSet<UnidadMedida> UnidadMedidas { get; set; }
        public virtual DbSet<TipoFactura> TipoFacturas { get; set; }
    }
}
