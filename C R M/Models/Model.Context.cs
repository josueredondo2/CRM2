﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace C_R_M.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class CRMEntities : DbContext
    {
        public CRMEntities()
            : base("name=CRMEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Canton> Canton { get; set; }
        public virtual DbSet<Contacto> Contacto { get; set; }
        public virtual DbSet<Correo> Correo { get; set; }
        public virtual DbSet<Cuenta> Cuenta { get; set; }
        public virtual DbSet<Distrito> Distrito { get; set; }
        public virtual DbSet<Empresa> Empresa { get; set; }
        public virtual DbSet<EstadodeCuenta> EstadodeCuenta { get; set; }
        public virtual DbSet<Marketing> Marketing { get; set; }
        public virtual DbSet<MedioPublicitario> MedioPublicitario { get; set; }
        public virtual DbSet<Movimiento> Movimiento { get; set; }
        public virtual DbSet<Producto> Producto { get; set; }
        public virtual DbSet<Provincia> Provincia { get; set; }
        public virtual DbSet<Publicidad> Publicidad { get; set; }
        public virtual DbSet<Recordar> Recordar { get; set; }
        public virtual DbSet<Recordatorio> Recordatorio { get; set; }
        public virtual DbSet<Rol> Rol { get; set; }
        public virtual DbSet<ServicioEmpresa> ServicioEmpresa { get; set; }
        public virtual DbSet<Telefono> Telefono { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
    }
}
