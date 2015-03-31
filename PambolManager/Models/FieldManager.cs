using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PambolManager.Models
{
    public class FieldManager : IdentityUser
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Name { get; set; }
        public string FieldName { get; set; }
    }
}