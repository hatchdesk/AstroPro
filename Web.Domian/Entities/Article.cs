using System.ComponentModel.DataAnnotations;
using Web.Domian.Entities.Base;
using Web.Domian.Enums;

namespace Web.Domian.Entities
{
    public class Article : AuditableBaseEntity
    {
        [MaxLength(500)]
        public string Title { get; set; } = string.Empty;
        public string ? Content { get; set; }
        public bool IsPublished { get; set; }
        public ArticleCategory?  Category { get; set; }

       
    }

}