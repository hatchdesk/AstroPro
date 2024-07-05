using System.ComponentModel.DataAnnotations.Schema;
using Web.Domian.Entities.Base;

namespace Web.Domian.Entities
{
    public class PageContent :AuditableBaseEntity
    {
        public string Tag { get; set; } = string.Empty;
        public string ? Content { get; set; }
        public int PageId { get; set; }
        //public string BackgroundColor { get; set; } = string.Empty;

        [ForeignKey("PageId")]
        public virtual Page? Page { get; set; }
    }
}
