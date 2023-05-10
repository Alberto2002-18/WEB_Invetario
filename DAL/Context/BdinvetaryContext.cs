using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MODEL;

namespace DAL.Context;

public partial class BdinvetaryContext : DbContext
{
    public BdinvetaryContext()
    {
    }

    public BdinvetaryContext(DbContextOptions<BdinvetaryContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Categoria> Categoria { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Compra> Compras { get; set; }

    public virtual DbSet<Compradetalle> Compradetalles { get; set; }

    public virtual DbSet<Menu> Menus { get; set; }

    public virtual DbSet<Menurole> Menuroles { get; set; }

    public virtual DbSet<Negocio> Negocios { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Proveedor> Proveedors { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Tipodocumento> Tipodocumentos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Usuariorole> Usuarioroles { get; set; }

    public virtual DbSet<Ventadetalle> Ventadetalles { get; set; }

    public virtual DbSet<Venta> Venta { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.IdCategoria).HasName("PK__CATEGORI__A3C02A1087013B1A");

            entity.ToTable("CATEGORIA");

            entity.Property(e => e.EsActivo).HasDefaultValueSql("((1))");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdCliente).HasName("PK__CLIENTE__D5946642F264B50A");

            entity.ToTable("CLIENTE");

            entity.Property(e => e.Cedula).HasMaxLength(50);
            entity.Property(e => e.Correo).HasMaxLength(250);
            entity.Property(e => e.Nombre).HasMaxLength(250);
            entity.Property(e => e.Telefono).HasMaxLength(50);

            entity.HasOne(d => d.IdTipoDocumentoNavigation).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.IdTipoDocumento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CLIENTE__IdTipoD__5BE2A6F2");
        });

        modelBuilder.Entity<Compra>(entity =>
        {
            entity.HasKey(e => e.IdCompra).HasName("PK__COMPRA__0A5CDB5C910A08BE");

            entity.ToTable("COMPRA");

            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.Impuesto).HasColumnType("money");
            entity.Property(e => e.Total).HasColumnType("money");

            entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.Compras)
                .HasForeignKey(d => d.IdProveedor)
                .HasConstraintName("FK__COMPRA__IdProvee__6383C8BA");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Compras)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK__COMPRA__IdUsuari__6477ECF3");
        });

        modelBuilder.Entity<Compradetalle>(entity =>
        {
            entity.HasKey(e => e.IdCompraDetalle).HasName("PK__COMPRADE__A1B840C5F272F281");

            entity.ToTable("COMPRADETALLE");

            entity.Property(e => e.Precio).HasColumnType("money");

            entity.HasOne(d => d.IdCompraNavigation).WithMany(p => p.Compradetalles)
                .HasForeignKey(d => d.IdCompra)
                .HasConstraintName("FK__COMPRADET__IdCom__628FA481");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.Compradetalles)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("FK__COMPRADET__IdPro__619B8048");
        });

        modelBuilder.Entity<Menu>(entity =>
        {
            entity.HasKey(e => e.IdMenu).HasName("PK__MENU__4D7EA8E11A52CBDF");

            entity.ToTable("MENU");

            entity.Property(e => e.Icono).HasMaxLength(50);
            entity.Property(e => e.Nombre).HasMaxLength(250);
        });

        modelBuilder.Entity<Menurole>(entity =>
        {
            entity.HasKey(e => e.IdMenuRole).HasName("PK__MENUROLE__17EFF1040779BED1");

            entity.ToTable("MENUROLE");

            entity.HasOne(d => d.IdMenuNavigation).WithMany(p => p.Menuroles)
                .HasForeignKey(d => d.IdMenu)
                .HasConstraintName("FK_MenuRoles_Menu");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Menuroles)
                .HasForeignKey(d => d.IdRol)
                .HasConstraintName("FK_MenuRoles_Roles");
        });

        modelBuilder.Entity<Negocio>(entity =>
        {
            entity.HasKey(e => e.IdNegocio).HasName("PK__NEGOCIO__750B6A556F05CA6A");

            entity.ToTable("NEGOCIO");

            entity.Property(e => e.Correo).HasMaxLength(100);
            entity.Property(e => e.Ruc).HasMaxLength(20);
            entity.Property(e => e.Telefono).HasMaxLength(100);
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.IdProducto).HasName("PK__PRODUCTO__098892103B176650");

            entity.ToTable("PRODUCTO");

            entity.Property(e => e.EsActivo).HasDefaultValueSql("((1))");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.Nombre).HasMaxLength(250);
            entity.Property(e => e.Precio).HasColumnType("money");

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdCategoria)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PRODUCTO__IdCate__5EBF139D");

            entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdProveedor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PRODUCTO__IdProv__5FB337D6");
        });

        modelBuilder.Entity<Proveedor>(entity =>
        {
            entity.HasKey(e => e.IdProveedor).HasName("PK__PROVEEDO__E8B631AFEDA9B202");

            entity.ToTable("PROVEEDOR");

            entity.Property(e => e.Cedula).HasMaxLength(50);
            entity.Property(e => e.Correo).HasMaxLength(250);
            entity.Property(e => e.Nombre).HasMaxLength(250);
            entity.Property(e => e.Telefono).HasMaxLength(50);

            entity.HasOne(d => d.IdTipoDocumentoNavigation).WithMany(p => p.Proveedors)
                .HasForeignKey(d => d.IdTipoDocumento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PROVEEDOR__IdTip__60A75C0F");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PK__ROLES__2A49584C63ADE208");

            entity.ToTable("ROLES");

            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<Tipodocumento>(entity =>
        {
            entity.HasKey(e => e.IdTipoDocumento).HasName("PK__TIPODOCU__3AB3332F8F44403F");

            entity.ToTable("TIPODOCUMENTO");

            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__USUARIO__5B65BF97DD5EA359");

            entity.ToTable("USUARIO");

            entity.Property(e => e.EsActivo).HasDefaultValueSql("((1))");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.NombreUsuario).HasMaxLength(250);
        });

        modelBuilder.Entity<Usuariorole>(entity =>
        {
            entity.HasKey(e => e.IdUsuarioRol).HasName("PK__USUARIOR__6806BF4A93212B9C");

            entity.ToTable("USUARIOROLES");

            entity.HasOne(d => d.IdNegocioNavigation).WithMany(p => p.Usuarioroles)
                .HasForeignKey(d => d.IdNegocio)
                .HasConstraintName("FK__USUARIORO__IdNeg__693CA210");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Usuarioroles)
                .HasForeignKey(d => d.IdRol)
                .HasConstraintName("FK__USUARIORO__IdRol__6A30C649");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Usuarioroles)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK__USUARIORO__IdUsu__6B24EA82");
        });

        modelBuilder.Entity<Ventadetalle>(entity =>
        {
            entity.HasKey(e => e.IdVentaDetalle).HasName("PK__VENTADET__2787211DE5D8A43E");

            entity.ToTable("VENTADETALLE");

            entity.Property(e => e.Precio).HasColumnType("money");
            entity.Property(e => e.Total).HasColumnType("money");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.Ventadetalles)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("FK__VENTADETA__IdPro__656C112C");

            entity.HasOne(d => d.IdVentaNavigation).WithMany(p => p.Ventadetalles)
                .HasForeignKey(d => d.IdVenta)
                .HasConstraintName("FK__VENTADETA__IdVen__66603565");
        });

        modelBuilder.Entity<Venta>(entity =>
        {
            entity.HasKey(e => e.IdVenta).HasName("PK__VENTA__BC1240BD38BB349B");

            entity.ToTable("VENTA");

            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.Impuesto).HasColumnType("money");
            entity.Property(e => e.NumeroDocumento).HasMaxLength(50);
            entity.Property(e => e.TipoPago)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Total).HasColumnType("money");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Venta)
                .HasForeignKey(d => d.IdCliente)
                .HasConstraintName("FK__VENTA__IdCliente__6754599E");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Venta)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK__VENTA__IdUsuario__68487DD7");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
