CREATE OR REPLACE PROCEDURE add_skill_to_course(
    _course_id INT,
    _skill_id INT
)
LANGUAGE plpgsql
AS $$
BEGIN
    IF NOT EXISTS (SELECT 1 FROM "CourseSkills" WHERE "CourseId" = _course_id AND "SkillId" = _skill_id) THEN
        INSERT INTO "CourseSkills" ("CourseId", "SkillId")
        VALUES (_course_id, _skill_id);
    END IF;
END;
$$;

CALL add_skill_to_course(1, 5);