
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    public class Person
    {
        [Key]
        public Guid PersonId { get; set; }
        [StringLength(40)] //nvarchar(40)

        public string? PersonName { get; set; }

        public DateTime? DateOfBirth { get; set; }

        [StringLength(10)] //nvarchar(40)
        public string? Gender { get; set; }
        public Guid? CountryID { get; set; }
        [StringLength(200)] //nvarchar(40)

        public string? Address { get; set; }
        public bool ReceiveNewsLetters { get; set; }

        [StringLength(40)] //nvarchar(40)
        public string? Email { get; set; }
        [ForeignKey("CountryID")]
        public virtual Country? Country { get; set; }
        [StringLength(8)]
        public string? TIN { get; set; }
    }
}
