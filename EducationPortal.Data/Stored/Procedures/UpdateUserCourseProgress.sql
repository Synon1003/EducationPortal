CREATE OR REPLACE PROCEDURE update_user_course_progress(
    _user_id UUID,
    _course_id INT,
    _new_progress INT
)
LANGUAGE plpgsql
AS $$
BEGIN
    -- 1. Update the progress percentage
    UPDATE "UserCourses"
    SET "ProgressPercentage" = _new_progress
    WHERE "UserId" = _user_id AND "CourseId" = _course_id;

    -- 2. If course is completed, boost associated skills
    IF _new_progress = 100 THEN
        -- Insert or Update Skill Level
        INSERT INTO "UserSkills" ("UserId", "SkillId", "Level")
        SELECT _user_id, cs."SkillId", 1
        FROM "CourseSkills" cs
        WHERE cs."CourseId" = _course_id
        ON CONFLICT ("UserId", "SkillId") 
        DO UPDATE SET "Level" = "UserSkills"."Level" + 1;
        
        RAISE NOTICE 'Course completed! Skills boosted for user %', _user_id;
    END IF;
END;
$$;

CALL update_user_course_progress('2fc3ecef-00ee-4aa3-f194-08dde5627abe', 2, 100);