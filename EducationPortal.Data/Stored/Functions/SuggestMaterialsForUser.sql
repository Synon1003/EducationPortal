CREATE OR REPLACE FUNCTION suggest_materials_for_user(_user_id UUID)
RETURNS TABLE (
    material_id INT,
    material_title VARCHAR,
    material_type VARCHAR,
    reason_course_name VARCHAR
) 
LANGUAGE plpgsql
AS $$
BEGIN
    RETURN QUERY
    SELECT DISTINCT m."Id", m."Title", m."Type", c."Name"
    FROM "Materials" m
    JOIN "CourseMaterials" cm ON m."Id" = cm."MaterialId"
    JOIN "Courses" c ON cm."CourseId" = c."Id"
    JOIN "UserCourses" uc ON c."Id" = uc."CourseId"
    WHERE uc."UserId" = _user_id
      -- Exclude materials the user already has
      AND NOT EXISTS (
          SELECT 1 FROM "UserMaterials" um 
          WHERE um."UserId" = _user_id AND um."MaterialId" = m."Id"
      )
    ORDER BY c."Name";
END;
$$;

SELECT * FROM suggest_materials_for_user('2fc3ecef-00ee-4aa3-f194-08dde5627abe');