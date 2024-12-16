using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acudir.Test.Core.Domain.Entities
{
    public class CreatePersonRequestDto
    {

        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "The Name field can only contain letters and spaces.")]
        [StringLength(50, MinimumLength = 2)]
        public string Name { get; set; }

        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "The Name field can only contain letters and spaces.")]
        [StringLength(50, MinimumLength = 2)]
        public string LastName { get; set; }

        [Range(18, 99, ErrorMessage = "Age must be between 18 and 99.")]
        public int Age { get; set; }

        [StringLength(100, ErrorMessage = "The Address cannot exceed 100 characters.")]
        public string Address { get; set; }

        [RegularExpression(@"^\+?[0-9\s\-]{7,15}$", ErrorMessage = "The Phone number is invalid.")]
        [StringLength(15, ErrorMessage = "The Phone number cannot exceed 15 characters.")]
        public string Phone { get; set; }
    }
}
