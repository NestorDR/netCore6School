using System.ComponentModel.DataAnnotations;

namespace SchoolWeb.Enums
{
    public enum TeachingMode
    {
        [Display(Name = "Not specified yet")]       // Alternatively [Description("Not specified yet")]
        Unspecified,
        [Display(Name = "Online")]                  // Alternatively [Description("Online")]
        Online,
        [Display(Name = "Classroom")]               // Alternatively [Description("Classroom")]
        ClassRoom,
        [Display(Name = "Live online")]             // Alternatively [Description("Live online")]
        LiveOnline,
        [Display(Name = "Mix of modes")]            // Alternatively [Description("A mix of the above")]
        Hybrid
    }
}
