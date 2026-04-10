CREATE OR REPLACE FUNCTION reward_user_completion()
RETURNS TRIGGER AS $$
BEGIN
    IF NEW."ProgressPercentage" = 100 AND OLD."ProgressPercentage" < 100 THEN
        UPDATE "AspNetUsers"
        SET "Theme" = 'cyberpunk'
        WHERE "Id" = NEW."UserId";
    END IF;
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER trg_completion_reward
AFTER UPDATE ON "UserCourses"
FOR EACH ROW
EXECUTE FUNCTION reward_user_completion();