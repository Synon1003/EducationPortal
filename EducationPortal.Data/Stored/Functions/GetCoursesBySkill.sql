CREATE OR REPLACE FUNCTION get_courses_by_skill(_skill_name TEXT)
RETURNS TABLE (
    course_id INT,
    course_name VARCHAR(50),
    course_description VARCHAR(250)
) 
LANGUAGE plpgsql
AS $$
BEGIN
    RETURN QUERY
    SELECT c."Id", c."Name", c."Description"
    FROM "Courses" c
    JOIN "CourseSkills" cs ON c."Id" = cs."CourseId" -- EF Core default naming
    JOIN "Skills" s ON cs."SkillId" = s."Id"
    WHERE s."Name" ILIKE '%' || _skill_name || '%'; -- ILIKE is case-insensitive
END;
$$;

SELECT * FROM get_courses_by_skill('C#');

-- SELECT pg_get_functiondef('get_courses_by_skill'::regproc);

-- var courses = await _context.Courses
--    .FromSqlRaw("SELECT * FROM get_courses_by_skill({0})", "C#")
--    .ToListAsync();