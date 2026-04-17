CREATE PROCEDURE dbo.enroll_user_in_course
    @UserId UNIQUEIDENTIFIER,
    @CourseId INT
AS
BEGIN
    IF NOT EXISTS (SELECT 1 FROM UserCourses WHERE UserId = @UserId AND CourseId = @CourseId)
    BEGIN
        INSERT INTO UserCourses (UserId, CourseId, ProgressPercentage)
        VALUES (@UserId, @CourseId, 0);
        
        PRINT 'User successfully enrolled.';
    END
    ELSE
    BEGIN
        PRINT 'User was already enrolled.';
    END
END;
GO