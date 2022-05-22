using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolWeb.Models
{
    public class StudentAddress
    {
        /*
         * System.ComponentModel.DataAnnotations Namespace Documentation
         * Visit https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations?view=net-6.0
         */

        // To create properties type "prop and Tab twice"
        // Scalar properties
        [Key]                   // Data Annotation Attribute to set primary key        
        public int Id { get; set; }

        [Column(TypeName = "nvarchar")]
        [Display(Name="Main address", Prompt = "Enter the main address of the student")]
        [MaxLength(100)]        // MaxLength is used for the EF to decide how large to make a string value field when it creates the DB.
        [StringLength(100)]     // StringLength is used for validation of user input.
        public string Address1 { get; set; } = string.Empty;

        [Column(TypeName = "nvarchar")]
        [Display(Name= "Alternative address", Prompt = "Enter the alternative address of the student")]
        [MaxLength(100)]        // MaxLength is used for the EF to decide how large to make a string value field when it creates the DB.
        [StringLength(100)]     // StringLength is used for validation of user input.
        public string Address2 { get; set; } = string.Empty;

        [Column(TypeName = "nvarchar")]
        [Display(Prompt = "Enter the city where live the student")]
        [MaxLength(80)]         // MaxLength is used for the EF to decide how large to make a string value field when it creates the DB.
        [StringLength(80)]      // StringLength is used for validation of user input.
        public string City { get; set; } = string.Empty;

        [Column(TypeName = "smallint")]
        [DataType(DataType.PostalCode)]
        [Display(Name = "Zip Code", Prompt = "Enter the zip code of the city")]
        [Range(1000, 9999, ErrorMessage = "Zip code must be between 1000 and 9999.")]
        public int? Zipcode { get; set; }

        [Column(TypeName = "nvarchar")]
        [Display(Prompt = "Enter the state of the city")]
        [MaxLength(80)]         // MaxLength is used for the EF to decide how large to make a string value field when it creates the DB.
        [StringLength(80)]      // StringLength is used for validation of user input.
        public string State { get; set; } = string.Empty;
        
        [Column(TypeName = "nvarchar")]
        [Display(Prompt = "Enter the country of the city")]
        [MaxLength(80)]         // MaxLength is used for the EF to decide how large to make a string value field when it creates the DB.
        [Required(ErrorMessage = "Country is requiered.")]
        [StringLength(80)]      // StringLength is used for validation of user input.
        public string Country { get; set; } = string.Empty;

        [Display(Name = "Created on")]
        [ReadOnly(true)]
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        [Display(Name = "Last update")]
        [ReadOnly(true)]
        public DateTime? UpdatedOn { get; set; } = null;

        // Fully defined One-to-One relationship at both ends (dependen entity is StudentAddress, and principal entity is Student)
        public int StudentId { get; set; }
        // Reference Navigation property: is a property of another entity type
        public virtual Student Student { get; set; }            // One-to-One Relationship
    }
}
