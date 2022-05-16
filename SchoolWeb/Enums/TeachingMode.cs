using System.ComponentModel.DataAnnotations;

namespace SchoolWeb.Enums
{
    public enum TeachingMode : ushort
    {
        [Display(Name = "Not specified yet")]       // Alternatively [Description("Not specified yet")]
        Unspecified,
        [Display(Name = "Classroom")]               // Alternatively [Description("Classroom")]
        ClassRoom,
        [Display(Name = "Online")]                  // Alternatively [Description("Online")]
        Online,
        [Display(Name = "Live online")]             // Alternatively [Description("Live online")]
        LiveOnline,
        [Display(Name = "Personalized teaching")]   // Alternatively [Description("Personalized teaching")]
        PersonalTraining,
        [Display(Name = "Mix of modes")]            // Alternatively [Description("A mix of the above")]
        Hybrid = 255
    }
}
