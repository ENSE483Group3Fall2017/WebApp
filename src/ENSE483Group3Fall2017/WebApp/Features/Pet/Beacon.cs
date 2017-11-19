using System.ComponentModel.DataAnnotations;

namespace WebApp.Features.Pet
{
    public class Beacon
    {
        [Required(AllowEmptyStrings = false)]
        [StringLength(maximumLength: 5, MinimumLength = 5)]
        public string Minor { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(maximumLength: 5, MinimumLength = 5)]
        public string Major { get; set; }

        public override string ToString() => $"{Minor}-{Major}";
    }
}
