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
        return PartialView("_DetailsVideoPartial", _mapper.Map<VideoViewModel>(video));
    }

    public async Task<ActionResult<PublicationViewModel>> PublicationDetails(int id)
    {
        var publication = await _materialService.GetPublicationByMaterialIdAsync(materialId: id);
        return PartialView("_DetailsPublicationPartial", _mapper.Map<PublicationViewModel>(publication));
    }

    public async Task<ActionResult<ArticleViewModel>> ArticleDetails(int id)
    {
        var article = await _materialService.GetArticleByMaterialIdAsync(materialId: id);
        return PartialView("_DetailsArticlePartial", _mapper.Map<ArticleViewModel>(article));
    }


    public IActionResult AddVideoToViewModel(CourseCreateViewModel model)
    {
        model.Videos.Add(new VideoCreateViewModel());
        return PartialView("_CreateVideosListPartial", model.Videos);
    }

    public IActionResult RemoveVideoFromViewModel(CourseCreateViewModel model, int idx)
    {
        if (idx >= 0 && idx < model.Videos.Count)
            model.Videos.RemoveAt(idx);

        return PartialView("_CreateVideosListPartial", model.Videos);
    }


    public IActionResult AddPublicationToViewModel(CourseCreateViewModel model)
    {
        model.Publications.Add(new PublicationCreateViewModel());
        return PartialView("_CreatePublicationsListPartial", model.Publications);
    }

    public IActionResult RemovePublicationFromViewModel(CourseCreateViewModel model, int idx)
    {
        if (idx >= 0 && idx < model.Publications.Count)
            model.Publications.RemoveAt(idx);

        return PartialView("_CreatePublicationsListPartial", model.Publications);
    }


    public IActionResult AddArticleToViewModel(CourseCreateViewModel model)
    {
        model.Articles.Add(new ArticleCreateViewModel());
        return PartialView("_CreateArticlesListPartial", model.Articles);
    }

    public IActionResult RemoveArticleFromViewModel(CourseCreateViewModel model, int idx)
    {
        if (idx >= 0 && idx < model.Articles.Count)
            model.Articles.RemoveAt(idx);

        return PartialView("_CreateArticlesListPartial", model.Articles);
    }


    public async Task<IActionResult> LoadVideoToViewModel(CourseCreateViewModel model, int videoId, string title)
    {
        if (!model.LoadedVideos.Any(v => v.Id == videoId))
        {
            var video = await _materialService.GetVideoByMaterialIdAsync(videoId);

            if (video is not null && video.Title == title)
                model.LoadedVideos.Add(_mapper.Map<VideoViewModel>(video));
        }

        return PartialView("_LoadVideosPartial", model.LoadedVideos);
    }

    public IActionResult UnloadVideoFromViewModel(CourseCreateViewModel model, int idx)
    {
        if (idx >= 0 && idx < model.LoadedVideos.Count)
            model.LoadedVideos.RemoveAt(idx);

        return PartialView("_LoadVideosPartial", model.LoadedVideos);
    }

    public async Task<IActionResult> LoadPublicationToViewModel(CourseCreateViewModel model, int publicationId, string title)
    {
        if (!model.LoadedPublications.Any(v => v.Id == publicationId))
        {
            var publication = await _materialService.GetPublicationByMaterialIdAsync(publicationId);

            if (publication is not null && publication.Title == title)
                model.LoadedPublications.Add(_mapper.Map<PublicationViewModel>(publication));
        }

        return PartialView("_LoadPublicationsPartial", model.LoadedPublications);
    }

    public IActionResult UnloadPublicationFromViewModel(CourseCreateViewModel model, int idx)
    {
        if (idx >= 0 && idx < model.LoadedPublications.Count)
            model.LoadedPublications.RemoveAt(idx);

        return PartialView("_LoadPublicationsPartial", model.LoadedPublications);
    }

    public async Task<IActionResult> LoadArticleToViewModel(CourseCreateViewModel model, int articleId, string title)
    {
        if (!model.LoadedArticles.Any(v => v.Id == articleId))
        {
            var article = await _materialService.GetArticleByMaterialIdAsync(articleId);

            if (article is not null && article.Title == title)
                model.LoadedArticles.Add(_mapper.Map<ArticleViewModel>(article));
        }

        return PartialView("_LoadArticlesPartial", model.LoadedArticles);
    }

    public IActionResult UnloadArticleFromViewModel(CourseCreateViewModel model, int idx)
    {
        if (idx >= 0 && idx < model.LoadedArticles.Count)
            model.LoadedArticles.RemoveAt(idx);

        return PartialView("_LoadArticlesPartial", model.LoadedArticles);
    }
}
