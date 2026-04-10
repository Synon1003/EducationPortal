CREATE OR REPLACE FUNCTION get_course_stats(_course_id INT)
RETURNS TABLE (
    total_materials BIGINT,
    total_skills BIGINT
) 
LANGUAGE plpgsql
AS $$
BEGIN
    RETURN QUERY
    SELECT 
        (SELECT COUNT(*) FROM "CourseMaterials" WHERE "CourseId" = _course_id),
        (SELECT COUNT(*) FROM "CourseSkills" WHERE "CourseId" = _course_id);
END;
$$;

SELECT * FROM get_course_stats(1);