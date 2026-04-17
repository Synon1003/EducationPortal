CREATE TRIGGER trg_validate_material
ON dbo.Materials
INSTEAD OF INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO dbo.Materials (Title, Type, Duration, CourseId, /*others*/)
    SELECT 
        TRIM(i.Title),
        i.Type,
        CASE 
            WHEN i.Type = 'Video' AND i.Duration < 0 THEN 0 
            ELSE i.Duration 
        END,
        i.CourseId
    FROM inserted i;
END;
GO