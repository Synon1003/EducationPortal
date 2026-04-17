CREATE TRIGGER trg_reward_user_completion
ON dbo.UserCourses
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    IF UPDATE(ProgressPercentage)
    BEGIN
        UPDATE u
        SET u.Theme = 'cyberpunk'
        FROM dbo.AspNetUsers u
        INNER JOIN inserted i ON u.Id = i.UserId
        INNER JOIN deleted d ON i.UserId = d.UserId AND i.CourseId = d.CourseId
        WHERE i.ProgressPercentage = 100
          AND d.ProgressPercentage < 100;
    END
END;
GO