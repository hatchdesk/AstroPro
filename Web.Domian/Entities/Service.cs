using System.ComponentModel.DataAnnotations;
using Web.Domian.Entities.Base;

namespace Web.Domian.Entities
{
    public  class Service : AuditableBaseEntity
    {
        public string ? Icon { get; set; }

        [MaxLength(500)]
        public string Title { get; set; }= string.Empty;
        public string Content { get; set; } = string.Empty;
    }
}
