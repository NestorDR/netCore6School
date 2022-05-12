using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolWeb.Models
{
    public class StudentAddress
    {
        // To create properties type "prop and Tab twice"

        // Scalar properties
        [Key]                   // Data Annotation Attribute to set primary key        
        public int Id { get; set; }
        [Column(TypeName = "nvarchar")]
        [MaxLength(100)]        // MaxLength is used for the EF to decide how large to make a string value field when it creates the DB.
        [StringLength(100)]     // StringLength is a data annotation that will be used for validation of user input.
        public string Address1 { get; set; } = string.Empty;
        [Column(TypeName = "nvarchar")]
        [MaxLength(100)]        // MaxLength is used for the EF to decide how large to make a string value field when it creates the DB.
        [StringLength(100)]     // StringLength is a data annotation that will be used for validation of user input.
        public string Address2 { get; set; } = string.Empty;
        [Column(TypeName = "nvarchar")]
        [MaxLength(80)]         // MaxLength is used for the EF to decide how large to make a string value field when it creates the DB.
        [StringLength(80)]      // StringLength is a data annotation that will be used for validation of user input.        public string State { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        [Range(1000, 9999, ErrorMessage = "Zip code must be between 1000 and 9999.")]
        public int? Zipcode { get; set; }
        [Column(TypeName = "nvarchar")]
        [MaxLength(80)]         // MaxLength is used for the EF to decide how large to make a string value field when it creates the DB.
        [StringLength(80)]      // StringLength is a data annotation that will be used for validation of user input.        public string State { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.Now;

        // Fully defined One-to-One relationship at both ends (dependen entity is StudentAddress, and principal entity is Student)
        public int StudentId { get; set; }
        // Reference Navigation property: is a property of another entity type
        public virtual Student Student { get; set; }            // One-to-One Relationship
    }
}
