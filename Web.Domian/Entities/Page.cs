using Web.Domian.Entities.Base;

namespace Web.Domian.Entities
{
    public class Page :AuditableBaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Tag { get; set; }=string.Empty; //Tagline
        public virtual ICollection<PageContent>? Contents { get; set; }
    }
}
