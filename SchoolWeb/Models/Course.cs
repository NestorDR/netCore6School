using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SchoolWeb.Helpers;

namespace SchoolWeb.Models
{
    public class Course
    {
        /*
         * System.ComponentModel.DataAnnotations Namespace Documentation
         * Visit https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations?view=net-6.0
         */
        
        // To create constructor type "ctor and Tab twice"
        public Course()
        {
            this.Students = new HashSet<Student>();
            this.Teachers = new HashSet<Teacher>();
        }

        // To create properties type "prop and Tab twice"
        // Scalar properties
        [Key]           // Data Annotation Attribute to set primary key    
        public int Id { get; set; }

        [Column(TypeName = "nvarchar")]
        [MaxLength(100)]                                        // MaxLength is used for the EF to decide how large to make a string value field when it creates the DB.
        [Required(ErrorMessage = "Name is required.")]
        [StringLengthAttributeHelper(minimumLength: 3, maximumLength: 100)]   // StringLengthAttributeHelper extends StringLength base data annotation and will be used for validation of user input.
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Created on")]
        [ReadOnly(true)]
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        [Display(Name = "Last update")]
        [ReadOnly(true)]
        public DateTime? UpdatedOn { get; set; } = null;

        // Collection Navigation property: is a property of generic collection of an entity type
        public virtual ICollection<Student> Students { get; set; }      // Many-to-Many relationship
        public virtual ICollection<Teacher> Teachers { get; set; }      // Many-to-Many relationship
    }
}

