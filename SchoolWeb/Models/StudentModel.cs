using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CommonLibrary.Extensions;
using CommonLibrary.Helpers;

namespace SchoolWeb.Models;

public class StudentModel
{
    /*
     * System.ComponentModel.DataAnnotations Namespace Documentation
     * Visit https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations?view=net-6.0
     */
        
    // To create properties type "prop and Tab twice"

    // Scalar properties
    [Key]           // Data Annotation Attribute to set primary key    
    public int Id { get; set; }
        
    [Column(TypeName = "nvarchar")]
    // Data Annotation Attribute "Name" to set Label and Error messages
    // Data Annotation Attribute "Prompt to set Placeholder
    [Display(Name = "Full name", Prompt = "Enter the student full name")]
    [MaxLength(100)]                                            // MaxLength is used for the EF to decide how large to make a string value field when it creates the DB.
    [Required(ErrorMessage = "Name is required.")]
    [StringLengthAttributeHelper(minimumLength: 3, maximumLength: 100)]       // StringLengthAttributeHelper extends StringLength base data annotation and will be used for validation of user input.
    public string Name { get; set; } = string.Empty;
        
    [Column(TypeName = "nvarchar")]
    [RegularExpression(@"^[F|M|X]$", ErrorMessage = "Only uppercase F, M or X are allowed.")]
    [Required(ErrorMessage = "Gender is required.")]
    [StringLength(1)]
    public string Gender { get; set; } = "";

    [Column(TypeName = "date")]
    [Display(Name = "Birthday")]
    public DateTime? DateOfBirth { get; set; } = null;
        
    [NotMapped]
    public int? Age => DateOfBirth.ElapsedYearsUntil(DateTime.Today);
        
    [Column(TypeName = "decimal(5, 2)")]
    public decimal? Height { get; set; }
        
    [Column(TypeName = "real")] 
    public float? Weight { get; set; }

    [DataType(DataType.EmailAddress)]
    [Display(Name = "E-Mail")]
    [DisplayFormat(ConvertEmptyStringToNull = true)]
    [EmailAddress(ErrorMessage = "Invalid Email.")]         // Redounds with RegularExpression, but it is only as an example
    [MaxLength(100)]
    [RegularExpression(@"^(([A-Za-z0-9]+_+)|([A-Za-z0-9]+\-+)|([A-Za-z0-9]+\.+)|([A-Za-z0-9]+\++))*[A-Za-z0-9]+@((\w+\-*)|(\w+\.))*\w+\.[a-zA-Z]{2,6}$",
        ErrorMessage = "Email appears to be invalid.")]
    public string Email { get; set; } = "";
        
    [Display(Name = "Created on")]
    [ReadOnly(true)]
    public DateTime CreatedOn { get; set; } = DateTime.Now;
    [Display(Name = "Last update")]
    [ReadOnly(true)]
    public DateTime? UpdatedOn { get; set; } = null;

    // Fully defined One-to-Many relationship at both ends (dependen entity is Student, and principal entity is Grade)
    // Visit: topic "Convention 4" in
    //        https://www.entityframeworktutorial.net/efcore/one-to-many-conventions-entity-framework-core.aspx
    //        https://www.entityframeworktutorial.net/code-first/configure-one-to-many-relationship-in-code-first.aspx#conventions-for-one-to-many-ef6
    public int? GradeId { get; set; }
        
    // Reference Navigation properties: is a property of another entity type
    public virtual GradeModel Grade { get; set; } = new();          // One-to-Many relationship
    public virtual StudentAddressModel Address { get; set; } = new();    // One-to-One Relationship

    // Collection Navigation property: is a property of generic collection of an entity type
    public virtual ICollection<CourseModel> Courses { get; set; } = new HashSet<CourseModel>(); // Many-to-Many relationship
}