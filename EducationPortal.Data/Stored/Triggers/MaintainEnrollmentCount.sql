CREATE TRIGGER trg_maintain_enrollment_count
ON UserCourses
AFTER INSERT, DELETE
AS
BEGIN
    SET NOCOUNT ON;

    -- Handle Inserts
    IF EXISTS (SELECT * FROM inserted) AND NOT EXISTS (SELECT * FROM deleted)
    BEGIN
        UPDATE GlobalStats 
        SET StatValue = StatValue + 1 
        WHERE StatName = 'TotalEnrollments';
    END

    -- Handle Deletes
    IF EXISTS (SELECT * FROM deleted) AND NOT EXISTS (SELECT * FROM inserted)
    BEGIN
        UPDATE GlobalStats 
        SET StatValue = StatValue - 1 
        WHERE StatName = 'TotalEnrollments';
    END
END;