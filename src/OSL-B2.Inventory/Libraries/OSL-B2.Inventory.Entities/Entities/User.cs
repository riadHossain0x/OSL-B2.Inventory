using System.ComponentModel.DataAnnotations;

namespace OSL_B2.Inventory.Entities.Entities
{
    public class User
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public long IdentityUserId { get; set; }
    }
}
