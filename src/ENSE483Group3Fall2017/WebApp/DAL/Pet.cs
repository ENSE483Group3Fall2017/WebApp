using System.ComponentModel.DataAnnotations;

namespace WebApp.DAL
{
    public class Pet
    {
        [Key]
        public string BeaconID { get; set; }

        public string Name { get; set; }

        public PetKind Kind { get; set; }

        public PetStatus Status { get; set;}

        public string Description { get; set; }
    }

    public enum PetKind
    {
        Dog = 1,
        Cat = 2,
        Other = 100
    }

    public enum PetStatus
    {

        Owned = 1,
        Lost = 2
    }
}