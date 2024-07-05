using System.ComponentModel.DataAnnotations;
using Web.Domian.Entities.Base;

namespace Web.Domian.Entities
{
    public class Account : AuditableBaseEntity
    { 
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
