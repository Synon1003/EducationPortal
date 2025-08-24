namespace EducationPortal.Web.Models;

public class ListProfileMaterialViewModel
{
    public List<VideoViewModel> Videos { get; set; } = [];
    public List<PublicationViewModel> Publications { get; set; } = [];
    public List<ArticleViewModel> Articles { get; set; } = [];
}

// var videos = await _materialService.GetVideosWithMaterialByCourseIdAsync(courseId: id);

// var publications = await _materialService.GetPublicationsWithMaterialByCourseIdAsync(courseId: id);

// var articles = await _materialService.GetArticlesWithMaterialByCourseIdAsync(courseId: id);

// var listMaterialsViewModel = new ListProfileMaterialViewModel
// {
//     CourseId = id,
//     CourseName = course.Name,
//     Videos = _mapper.Map<List<VideoViewModel>>(videos),
//     Publications = _mapper.Map<List<PublicationViewModel>>(publications),
//     Articles = _mapper.Map<List<ArticleViewModel>>(articles)
// };