
namespace PPI.Platform.Core.Entities
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Core.Objects;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Threading.Tasks;
    using PPI.Platform.Core.Attributes;
    using PPI.Platform.Core.Utility;
    public partial class PlatformEntities : DbContext
    {
  
        public override int SaveChanges()
        {
            var contextAdapter = ((IObjectContextAdapter)this);
            contextAdapter.ObjectContext.DetectChanges(); //force this. Sometimes entity state needs a handle jiggle
            var pendingEntities = contextAdapter.ObjectContext.ObjectStateManager
                .GetObjectStateEntries(EntityState.Added | EntityState.Modified)
                .Where(en => !en.IsRelationship).ToList();
            foreach (var entry in pendingEntities) //Encrypt all pending changes
                EncryptEntity(entry.Entity);
            int result = base.SaveChanges();
            foreach (var entry in pendingEntities) //Decrypt updated entities for continued use
                DecryptEntity(entry.Entity);
            return result;
        }
        public override async Task<int> SaveChangesAsync(System.Threading.CancellationToken cancellationToken)
        {

            var contextAdapter = ((IObjectContextAdapter)this);
            contextAdapter.ObjectContext.DetectChanges(); //force this. Sometimes entity state needs a handle jiggle
            var pendingEntities = contextAdapter.ObjectContext.ObjectStateManager
                .GetObjectStateEntries(EntityState.Added | EntityState.Modified)
                .Where(en => !en.IsRelationship).ToList();

            foreach (var entry in pendingEntities) //Encrypt all pending changes

                EncryptEntity(entry.Entity);

            var result = await base.SaveChangesAsync(cancellationToken);

            foreach (var entry in pendingEntities) //Decrypt updated entities for continued use

                DecryptEntity(entry.Entity);

            return result;
        }
        void ObjectMaterialized(object sender, ObjectMaterializedEventArgs e)
        {

            DecryptEntity(e.Entity);

        }

        private void EncryptEntity(object entity)
        {
            //Get all the properties that are encryptable and encrypt them            
            var encryptedProperties = entity.GetType().GetProperties()
                .Where(prop => Attribute.IsDefined(prop, typeof(SecureAttribute)));
            foreach (var property in encryptedProperties)
            {
                string value = property.GetValue(entity) as string;
                if (!String.IsNullOrEmpty(value))
                {
                    string encryptedValue = MachineKeyWrapper.ProtectUrlSafeString(value, "fields");
                    property.SetValue(entity, encryptedValue);
                }
            }
        }

        private void DecryptEntity(object entity)
        {
            //Get all the properties that are encryptable and decyrpt them
            
            var encryptedProperties = entity.GetType().GetProperties()
                .Where(prop => Attribute.IsDefined(prop, typeof(SecureAttribute)));
            foreach (var property in encryptedProperties)
            {
                string encryptedValue = property.GetValue(entity) as string;
                if (!String.IsNullOrEmpty(encryptedValue))
                {
                    string value = MachineKeyWrapper.UnprotectUrlSafeString(encryptedValue, "fields");
                    //EncryptionService.Decrypt(encryptedValue);
                    this.Entry(entity).Property(property.Name).OriginalValue = value;
                    this.Entry(entity).Property(property.Name).IsModified = false;
                }
            }
        }

    }
}
