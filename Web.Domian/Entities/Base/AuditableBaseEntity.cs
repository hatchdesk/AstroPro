namespace Web.Domian.Entities.Base
{
        public abstract class AuditableBaseEntity : BaseEntity
        {
            public DateTime CreatedOn { get; set; } = DateTime.Now;
            public DateTime UpdatedOn { get; set; } = DateTime.Now;
            public bool IsActive { get; set; } = true;
        }
        public abstract class DeletableBaseEntity : AuditableBaseEntity
        {
            public DateTime DeletedOn { get; set; } = DateTime.Now;
        }
    }

