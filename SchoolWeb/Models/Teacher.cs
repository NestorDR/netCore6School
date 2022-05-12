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
        // To create properties type "prop and Tab twice"

        /*
         * System.ComponentModel.DataAnnotations Namespace Documentation
         * Visit https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations?view=net-6.0
         */

        // Scalar properties
        [Key]           // Data Annotation Attribute to set primary key
        public int Id { get; set; }
        [Column(TypeName = "nvarchar")]
        // Data Annotation Attribute "Name" to set Label and Error messages
        // Data Annotation Attribute "Prompt to set Placeholder
        [Display(Name="Full name", Prompt = "Enter the teacher full name")]   
        [MaxLength(100)]                                    // MaxLength is used for the EF to decide how large to make a string value field when it creates the DB.
        [Required(ErrorMessage = "Name is required.")]
        [StringLengthCustom(100, MinimumLength = 3)]        // StringLength is a data annotation that will be used for validation of user input.
        public string Name { get; set; } = String.Empty;
        [Column(TypeName = "tinyint")]
        // Data Annotation Attribute "Name" to set Label and Error messages
        [Display(Name = "Teaching Mode")]
        [Required(ErrorMessage = "Teaching Mode is required.")]
        public TeachingMode TeachingMode { get; set; } = TeachingMode.Unspecified;
        [NotMapped]
        public string TeachingModeDescription => TeachingMode.GetFriendlyText(TeachingMode.ToString());
        public DateTime CreatedOn { get; set; } = DateTime.Now;
    }
}
