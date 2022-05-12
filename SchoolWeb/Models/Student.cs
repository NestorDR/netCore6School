using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SchoolWeb.Helper;

namespace SchoolWeb.Models
{
    public class Student
    {
        // To create properties type "prop and Tab twice"
        
        // Scalar properties
        [Key]           // Data Annotation Attribute to set primary key    
        public int Id { get; set; }
        [Column(TypeName = "nvarchar")]
        [Display(Prompt = "Enter the student full name")]   // Data Annotation Attribute to set Placeholder
        [MaxLength(100)]                                    // MaxLength is used for the EF to decide how large to make a string value field when it creates the DB.
        [Required(ErrorMessage = "Name is required.")]
        [StringLengthCustom(100, MinimumLength = 3)]        // StringLength is a data annotation that will be used for validation of user input.
        public string Name { get; set; } = string.Empty;
        public DateTime? DateOfBirth { get; set; }
        [NotMapped]
        public int? Age { get; set; }
        [Column(TypeName = "decimal(5, 2)")]
        public decimal? Height { get; set; }
        [Column(TypeName = "real")] 
        public float? Weight { get; set; }
        [Column(TypeName = "varbinary(max)")]
        public byte[] RowVersion { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;

        // Fully defined One-to-Many relationship at both ends (dependen entity is Student, and principal entity is Grade)
        // Visit: topic "Convention 4" in
        //        https://www.entityframeworktutorial.net/efcore/one-to-many-conventions-entity-framework-core.aspx
        //        https://www.entityframeworktutorial.net/code-first/configure-one-to-many-relationship-in-code-first.aspx#conventions-for-one-to-many-ef6
        public int? GradeId { get; set; }
        
        // Reference Navigation properties: is a property of another entity type
        public virtual Grade Grade { get; set; }                    // One-to-Many relationship
        public virtual StudentAddress Address { get; set; }         // One-to-One Relationship

        // Collection Navigation property: is a property of generic collection of an entity type
        public virtual ICollection<Course> Courses { get; set; }    // Many-to-Many relationship
    }
}
