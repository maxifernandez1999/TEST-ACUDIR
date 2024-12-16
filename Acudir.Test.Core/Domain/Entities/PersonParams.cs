using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acudir.Test.Core.Domain.Entities
{
    public class PersonParams
    {
        public int? Id { get; set; }

        public string? Name { get; set; }

        public string? LastName { get; set; }

        public int? Age { get; set; }

        public string? Address { get; set; }

        public string? Phone { get; set; }
    }
}
