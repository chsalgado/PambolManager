using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace PambolManager.Domain.Entities.Core
{
    public class EntitiesContext : IdentityDbContext<IdentityUser>
    {
        public EntitiesContext()
            : base("PambolManagerContext")
        {

        }
    }
}
