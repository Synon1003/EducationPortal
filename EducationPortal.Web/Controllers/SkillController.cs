using Microsoft.AspNetCore.Mvc;
using EducationPortal.Web.Models;
using Microsoft.AspNetCore.Authorization;

namespace EducationPortal.Web.Controllers;

[Authorize]
public class SkillController : Controller
{
    public IActionResult AddSkillToViewModel(CourseCreateViewModel model)
    {
        model.Skills.Add(new SkillCreateViewModel());
        return PartialView("_CreateSkillsListPartial", model.Skills);
    }

    public IActionResult RemoveSkillFromViewModel(CourseCreateViewModel model, int index)
    {
        if (index >= 0 && index < model.Skills.Count)
            model.Skills.RemoveAt(index);

        return PartialView("_CreateSkillsListPartial", model.Skills);
    }
}
