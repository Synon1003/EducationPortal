using System.ComponentModel.DataAnnotations;

namespace EducationPortal.Web.Models;

public class ListPublicationsViewModel
{
    public List<PublicationViewModel> Publications { get; set; } = [];
}

public class PublicationViewModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Authors { get; set; }
    public int Pages { get; set; }
    public string Format { get; set; }
    public int PublicationYear { get; set; }
    public int MaterialId { get; set; }
}

public class PublicationCreateViewModel
{
    [Required]
    [StringLength(100)]
    public string Title { get; set; }

    [Required]
    public string Authors { get; set; }

    [Required]
    public int Pages { get; set; }

    [Required]
    [StringLength(20)]
    public string Format { get; set; }

    [Required]
    public int PublicationYear { get; set; }
}
