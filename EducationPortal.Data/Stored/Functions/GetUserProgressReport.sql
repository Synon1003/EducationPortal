CREATE OR REPLACE FUNCTION get_user_progress_report(_user_id UUID)
RETURNS TABLE (
    full_name TEXT,
    courses_enrolled BIGINT,
    courses_completed BIGINT,
    avg_progress NUMERIC,
    primary_skill VARCHAR
) 
LANGUAGE plpgsql
AS $$
BEGIN
    RETURN QUERY
    SELECT 
        (u."FirstName" || ' ' || u."LastName") as full_name,
        COUNT(uc."Id") as courses_enrolled,
        COUNT(uc."Id") FILTER (WHERE uc."ProgressPercentage" = 100) as courses_completed,
        ROUND(AVG(uc."ProgressPercentage"), 2) as avg_progress,
        COALESCE((
            SELECT s."Name" 
            FROM "UserSkills" us
            JOIN "Skills" s ON us."SkillId" = s."Id"
            WHERE us."UserId" = _user_id
            ORDER BY us."Level" DESC, s."Name" ASC
            LIMIT 1
        ), 'None') as primary_skill
    FROM "AspNetUsers" u -- Identity default table name
    LEFT JOIN "UserCourses" uc ON u."Id" = uc."UserId"
    WHERE u."Id" = _user_id
    GROUP BY u."Id", u."FirstName", u."LastName";
END;
$$;

SELECT * FROM get_user_progress_report('2fc3ecef-00ee-4aa3-f194-08dde5627abe');