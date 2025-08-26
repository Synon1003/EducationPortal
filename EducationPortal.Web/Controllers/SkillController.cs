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
        return PartialView("_CreateSkillsListPartial", model);
    }

    public IActionResult RemoveSkillFromViewModel(CourseCreateViewModel model, int idx)
    {
        if (idx >= 0 && idx < model.Skills.Count)
            model.Skills.RemoveAt(idx);

        return PartialView("_CreateSkillsListPartial", model);
    }
}
