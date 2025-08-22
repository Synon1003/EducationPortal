using System.ComponentModel.DataAnnotations;

namespace EducationPortal.Web.Models;

public class HomeViewModel
{
    public string Header { get; set; }

    public string WelcomeText { get; set; }

    [Required]
    [MaxLength(40)]
    public string InputText { get; set; }
}
