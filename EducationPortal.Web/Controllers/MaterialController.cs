using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using EducationPortal.Web.Models;
using Microsoft.AspNetCore.Authorization;
using EducationPortal.Application.Services.Interfaces;

namespace EducationPortal.Web.Controllers;

[Authorize]
public class MaterialController : Controller
{
    private readonly IMaterialService _materialService;
    private readonly IMapper _mapper;

    public MaterialController(IMaterialService materialService, IMapper mapper)
    {
        _materialService = materialService;
        _mapper = mapper;
    }

    public async Task<ActionResult<VideoViewModel>> VideoDetails(int id)
    {
        var video = await _materialService.GetVideoByMaterialIdAsync(materialId: id);
        return PartialView("_VideoDetailsPartial", _mapper.Map<VideoViewModel>(video));
    }

    public async Task<ActionResult<PublicationViewModel>> PublicationDetails(int id)
    {
        var publication = await _materialService.GetPublicationByMaterialIdAsync(materialId: id);
        return PartialView("_PublicationDetailsPartial", _mapper.Map<PublicationViewModel>(publication));
    }

    public async Task<ActionResult<ArticleViewModel>> ArticleDetails(int id)
    {
        var article = await _materialService.GetArticleByMaterialIdAsync(materialId: id);
        return PartialView("_ArticleDetailsPartial", _mapper.Map<ArticleViewModel>(article));
    }
}
