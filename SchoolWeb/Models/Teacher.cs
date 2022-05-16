using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SchoolWeb.Enums;
using SchoolWeb.Helper;

namespace SchoolWeb.Models
{
    /*
     * Teacher class maps to a database table.
     * This class:
     *      must be included as a DbSet<TEntity> type property in the .\Data\AppDbContext class.
     *      become an entity in Entity Framework when it are included as DbSet<TEntity> property in the context class
     */
    public class Teacher
    {
        /*
         * System.ComponentModel.DataAnnotations Namespace Documentation
         * Visit https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations?view=net-6.0
         */

        // To create constructor type "ctor and Tab twice"
        public Teacher()
        {
            this.Courses = new HashSet<Course>();
        }

        // To create properties type "prop and Tab twice"
        // Scalar properties
        [Key]           // Data Annotation Attribute to set primary key
        public int Id { get; set; }

        [Column(TypeName = "nvarchar")]
        // Data Annotation Attribute "Name" to set Label and Error messages
        // Data Annotation Attribute "Prompt to set Placeholder
        [Display(Name = "Full name", Prompt = "Enter the teacher full name")]
        [MaxLength(100)]                                        // MaxLength is used for the EF to decide how large to make a string value field when it creates the DB.
        [Required(ErrorMessage = "Name is required.")]
        [StringLengthAttributeHelper(100, MinimumLength = 3)]   // StringLengthAttributeHelper extends StringLength base data annotation and will be used for validation of user input.
        public string Name { get; set; } = String.Empty;

        [Column(TypeName = "nvarchar")]
        [RegularExpression(@"^[F|M|X]$", ErrorMessage = "Only uppercase F, M or X are allowed.")]
        [Required(ErrorMessage = "Gender is required")]
        [StringLength(1)]
        public string Gender { get; set; } = "";

        [Column(TypeName = "date")]
        // Show date format without the time part
        [DataType(DataType.Date)]
        [Display(Name = "Birthday")]
        public DateTime? DateOfBirth { get; set; } = null;
        
        [NotMapped]
        public int? Age => DateOfBirth.ElapsedYearsUntil(DateTime.Today);
        
        [Column(TypeName = "tinyint")]
        [Display(Name = "Teaching Mode")]
        [Required(ErrorMessage = "Teaching Mode is required.")]
        public TeachingMode TeachingMode { get; set; } = TeachingMode.Unspecified;
        
        [NotMapped]
        public string TeachingModeDescription => TeachingMode.GetFriendlyText(TeachingMode.ToString());

        [Display(Name = "In activity")]
        public bool Active { get; set; } = true;
        
        [NotMapped]
        public string ActiveAsYesNo => Active ? "Yes" : "No";

        [DataType(DataType.EmailAddress)]
        [Display(Name = "E-Mail")]
        [DisplayFormat(ConvertEmptyStringToNull = true)]
        [EmailAddress(ErrorMessage = "Invalid Email.")]         // Redunda with RegularExpression, but it is only as an example
        [MaxLength(100)]
        [RegularExpression(@"^(([A-Za-z0-9]+_+)|([A-Za-z0-9]+\-+)|([A-Za-z0-9]+\.+)|([A-Za-z0-9]+\++))*[A-Za-z0-9]+@((\w+\-*)|(\w+\.))*\w+\.[a-zA-Z]{2,6}$",
                           ErrorMessage = "Email appears to be invalid.")]
        [Required(AllowEmptyStrings = true)]
        public string Email { get; set; } = "";

        [Display(Name = "Created on")]
        [ReadOnly(true)]
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        [Display(Name = "Last update")]
        [DisplayFormat(ConvertEmptyStringToNull = true, NullDisplayText = "Never")]
        [ReadOnly(true)]
        public DateTime? UpdatedOn { get; set; } = null;

        // Collection Navigation property: is a property of generic collection of an entity type
        public virtual ICollection<Course> Courses { get; set; }    // Many-to-Many relationship
    }
}
