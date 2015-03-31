using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;

namespace PambolManager.Domain.Entities
{
    public class FieldManager : IdentityUser
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string FieldName { get; set; }

        // 1:N relationship to Tournaments 
        public virtual ICollection<Tournament> Tournaments { get; set; }
    }
}
