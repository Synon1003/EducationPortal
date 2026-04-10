CREATE OR REPLACE FUNCTION audit_course_syllabuses()
RETURNS TABLE (syllabus_line TEXT) 
LANGUAGE plpgsql
AS $$
DECLARE
    -- Main cursor for all courses
    curs_courses CURSOR FOR 
        SELECT "Id", "Name" FROM "Courses";

    -- Parameterized cursor for materials
    curs_materials CURSOR (_cid INT) FOR 
        SELECT m."Title", m."Type" 
        FROM "Materials" m
        JOIN "CourseMaterials" cm ON m."Id" = cm."MaterialId"
        WHERE cm."CourseId" = _cid;

    _course_id INT;
    _course_name TEXT;
    _mat_title TEXT;
    _mat_type TEXT;
BEGIN
    OPEN curs_courses;
    LOOP
        FETCH curs_courses INTO _course_id, _course_name;
        EXIT WHEN NOT FOUND;

        syllabus_line := '--- COURSE: ' || UPPER(_course_name) || ' ---';
        RETURN NEXT;

        OPEN curs_materials(_course_id);
        LOOP
            FETCH curs_materials INTO _mat_title, _mat_type;
            EXIT WHEN NOT FOUND;

            syllabus_line := '   -> [' || _mat_type || '] ' || _mat_title;
            RETURN NEXT;
        END LOOP;
        CLOSE curs_materials;

    END LOOP;
    CLOSE curs_courses;
END;
$$;

SELECT * FROM audit_course_syllabuses();