using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace TravelExperts.Models
{
    public class PackageViewModel : IValidatableObject
    {
        public Package Package { get; set; }

        public IEnumerable<Product> Products { get; set; }
        public int[] SelectedProducts { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext ctx)
        {
            if (SelectedProducts == null)
            {
                yield return new ValidationResult(
                  "Please select at least one Area.",
                  new[] { nameof(SelectedProducts) });
            }
        }

    }
}
