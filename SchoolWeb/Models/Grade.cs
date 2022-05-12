using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SchoolWeb.Helper;

namespace SchoolWeb.Models
{
    public class Grade
    {
        // To create properties type "prop and Tab twice"
        
        // Scalar properties
        [Key]           // Data Annotation Attribute to set primary key    
        public int Id { get; set; }
        [Column(TypeName = "nvarchar")]
        [MaxLength(100)]                                // MaxLength is used for the EF to decide how large to make a string value field when it creates the DB.
        [Required(ErrorMessage = "Name is required.")]
        [StringLengthCustom(100, MinimumLength = 3)]    // StringLength is a data annotation that will be used for validation of user input.
        public string Name { get; set; } = String.Empty;
        public string Section { get; set; } = String.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.Now;

        // Collection Navigation property: is a property of generic collection of an entity type
        public virtual ICollection<Student> Students { get; set; }      // One-to-Many relationship
    }
}
