using System.ComponentModel.DataAnnotations;

namespace OSL_B2.Inventory.Entities.Entities
{
    public class AppUser
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public long IdentityUserId { get; set; }
    }
}
