using System.Threading.Tasks;

namespace JobListingApp.AppDataAccess.Repository.Interfaces
{
    public interface ICrudRepo
    {
        Task<bool> Add<T>(T entity);
        Task<bool> Delete<T>(T entity);
        Task<bool> Update<T>(T entity);
        Task<bool> SaveChanges();
        Task<int> RowCount();
    }
}
/* public List<UserRole> UserRoles { get; set; } = new List<UserRole>();

       public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
       {
           foreach (EntityEntry<AuditableEntity> entry in ChangeTracker.Entries<AuditableEntity>())
           {
               if (entry.State == EntityState.Added)
               {
                   entry.Entity.Created = DateTime.Now;
                   //entry.Entity.CreatedBy = _currentUserDA.UserId;
               }
               else if (entry.State == EntityState.Modified)
               {
                   entry.Entity.LastModified = DateTime.Now;
                   //entry.Entity.LastModifiedBy = _currentUserDA.UserId;
               }
           }
           return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
       }
       */
