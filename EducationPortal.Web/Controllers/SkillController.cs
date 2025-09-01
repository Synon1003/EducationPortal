using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using EducationPortal.Web.Models;
using Microsoft.AspNetCore.Authorization;
using EducationPortal.Application.Services.Interfaces;

namespace EducationPortal.Web.Controllers;

[Authorize]
public class SkillController : Controller
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public SkillController(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    public IActionResult AddSkillToViewModel(CourseCreateViewModel model)
    {
        model.Skills.Add(new SkillCreateViewModel());
        return PartialView("_CreateSkillsListPartial", model.Skills);
    }

    public IActionResult RemoveSkillFromViewModel(CourseCreateViewModel model, int idx)
    {
        if (idx >= 0 && idx < model.Skills.Count)
            model.Skills.RemoveAt(idx);

        return PartialView("_CreateSkillsListPartial", model.Skills);
    }

    public async Task<IActionResult> LoadSkillToViewModel(CourseCreateViewModel model, int skillId, string skillName)
    {
        if (!model.LoadedSkills.Any(v => v.Id == skillId))
        {
            var skill = await _userService.GetSkillByIdAsync(skillId);

            if (skill is not null && skill.Name == skillName)
                model.LoadedSkills.Add(_mapper.Map<SkillViewModel>(skill));
        }

        return PartialView("_LoadSkillsPartial", model.LoadedSkills);
    }

    public IActionResult UnloadSkillFromViewModel(CourseCreateViewModel model, int idx)
    {
        if (idx >= 0 && idx < model.LoadedSkills.Count)
            model.LoadedSkills.RemoveAt(idx);

        return PartialView("_LoadSkillsPartial", model.LoadedSkills);
    }
}
