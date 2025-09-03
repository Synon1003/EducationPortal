SELECT TOP (10)
  [Id], [Name], [Description]
FROM [EducationPortalDb].[dbo].[Courses];

SELECT TOP (100)
  [Id], [Title], [Type]
FROM [EducationPortalDb].[dbo].[Materials];

DELETE FROM [EducationPortalDb].[dbo].[Materials]
WHERE [Type] = '';

SELECT TOP (10)
  [Id], [Name]
FROM [EducationPortalDb].[dbo].[Skills];

SELECT TOP (10)
  *
FROM [EducationPortalDb].[dbo].[CourseMaterials];

SELECT TOP (10)
  *
FROM [EducationPortalDb].[dbo].[CourseSkills];

SELECT TOP (10)
  *
FROM [EducationPortalDb].[dbo].[UserSkills];