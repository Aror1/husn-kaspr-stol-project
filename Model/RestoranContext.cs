using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace ApiTeamProject.Model;

public partial class RestoranContext : DbContext
{
    public RestoranContext()
    {
    }

    public RestoranContext(DbContextOptions<RestoranContext> options)
        : base(options)
    {
    }

    public virtual DbSet<MenuCategory> MenuCategories { get; set; }

    public virtual DbSet<MenuItem> MenuItems { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;database=Restoran;user=root;port=3309", Microsoft.EntityFrameworkCore.ServerVersion.Parse("5.7.39-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_unicode_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<MenuCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("menu_categories");

            entity.HasIndex(e => e.Name, "name").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<MenuItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("menu_items");

            entity.HasIndex(e => e.CategoryId, "category_id");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.CategoryId)
                .HasColumnType("int(11)")
                .HasColumnName("category_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(255)
                .HasColumnName("image_url");
            entity.Property(e => e.IsAvailable)
                .HasDefaultValueSql("'1'")
                .HasColumnName("is_available");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Price)
                .HasPrecision(10, 2)
                .HasColumnName("price");

            entity.HasOne(d => d.Category).WithMany(p => p.MenuItems)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("menu_items_ibfk_1");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("orders");

            entity.HasIndex(e => e.ConfirmedByWaiterId, "confirmed_by_waiter_id");

            entity.HasIndex(e => e.Status, "orders_status_statusid_fk");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.ConfirmedByWaiterId)
                .HasColumnType("int(11)")
                .HasColumnName("confirmed_by_waiter_id");
            entity.Property(e => e.Created)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("created");
            entity.Property(e => e.DeliveredAt)
                .HasColumnType("datetime")
                .HasColumnName("delivered_at");
            entity.Property(e => e.Status)
                .HasColumnType("int(11)")
                .HasColumnName("status");
            entity.Property(e => e.TableId)
                .HasColumnType("int(11)")
                .HasColumnName("table_id");
            entity.Property(e => e.TotalAmount)
                .HasPrecision(10, 2)
                .HasDefaultValueSql("'0.00'")
                .HasColumnName("total_amount");

            entity.HasOne(d => d.ConfirmedByWaiter).WithMany(p => p.Orders)
                .HasForeignKey(d => d.ConfirmedByWaiterId)
                .HasConstraintName("orders_ibfk_2");

            entity.HasOne(d => d.StatusNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.Status)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("orders_status_statusid_fk");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("order_items");

            entity.HasIndex(e => e.MenuItemId, "menu_item_id");

            entity.HasIndex(e => e.OrderId, "order_id");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.MenuItemId)
                .HasColumnType("int(11)")
                .HasColumnName("menu_item_id");
            entity.Property(e => e.Notes)
                .HasColumnType("text")
                .HasColumnName("notes");
            entity.Property(e => e.OrderId)
                .HasColumnType("int(11)")
                .HasColumnName("order_id");
            entity.Property(e => e.PriceAtOrder)
                .HasPrecision(10, 2)
                .HasColumnName("price_at_order");
            entity.Property(e => e.Quantity)
                .HasDefaultValueSql("'1'")
                .HasColumnType("int(11)")
                .HasColumnName("quantity");

            entity.HasOne(d => d.MenuItem).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.MenuItemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("order_items_ibfk_2");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("order_items_ibfk_1");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.Statusid).HasName("PRIMARY");

            entity.ToTable("status");

            entity.Property(e => e.Statusid)
                .HasColumnType("int(11)")
                .HasColumnName("statusid");
            entity.Property(e => e.StatusName)
                .HasMaxLength(255)
                .HasColumnName("status_name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("users");

            entity.HasIndex(e => e.Username, "username").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.FullName)
                .HasMaxLength(100)
                .HasColumnName("full_name");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("'1'")
                .HasColumnName("is_active");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .HasColumnName("password_hash");
            entity.Property(e => e.Role)
                .HasColumnType("enum('waiter','director')")
                .HasColumnName("role");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
