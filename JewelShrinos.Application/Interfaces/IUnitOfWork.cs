using JewelShrinos.Core.Entities;
using System.Linq.Expressions;

namespace JewelShrinos.Core.Interfaces
{
    /// <summary>
    /// Unit of Work Pattern
    /// Gestiona transacciones entre múltiples repositorios
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Product> Products { get; }
        IRepository<Category> Categories { get; }
        IRepository<Material> Materials { get; }
        IRepository<Supplier> Suppliers { get; }
        IRepository<Inventory> Inventory { get; }
        IRepository<InventoryMovement> InventoryMovements { get; }
        IRepository<Purchase> Purchases { get; }
        IRepository<PurchaseDetail> PurchaseDetails { get; }
        IRepository<Customer> Customers { get; }
        IRepository<Sale> Sales { get; }
        IRepository<SaleDetail> SaleDetails { get; }
        IRepository<Return> Returns { get; }
        IRepository<ReturnDetail> ReturnDetails { get; }
        IRepository<User> Users { get; }
        IRepository<SunatIntegration> SunatIntegrations { get; }
        IRepository<Configuration> Configurations { get; }
 
        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();
    }
}