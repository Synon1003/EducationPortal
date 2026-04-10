CREATE OR REPLACE FUNCTION generate_user_skill_rankings()
RETURNS TABLE (ranking_log TEXT) 
LANGUAGE plpgsql
AS $$
DECLARE
    -- 1. Declare the Cursor
    user_cursor CURSOR FOR 
        SELECT "Id", "FirstName", "LastName" FROM "AspNetUsers";
    
    -- Variables to hold the fetched data
    _user_id UUID;
    _f_name TEXT;
    _l_name TEXT;
    _top_skill TEXT;
    _level INT;
BEGIN
    -- 2. Open the Cursor
    OPEN user_cursor;

    LOOP
        -- 3. Fetch the next row into variables
        FETCH user_cursor INTO _user_id, _f_name, _l_name;
        
        -- 4. Exit the loop when no more rows are found
        EXIT WHEN NOT FOUND;

        -- Perform logic for the current user
        SELECT s."Name", us."Level" 
        INTO _top_skill, _level
        FROM "UserSkills" us
        JOIN "Skills" s ON us."SkillId" = s."Id"
        WHERE us."UserId" = _user_id
        ORDER BY us."Level" DESC
        LIMIT 1;

        -- Prepare the return row
        IF _top_skill IS NOT NULL THEN
            ranking_log := _f_name || ' ' || _l_name || ' is a Level ' || _level || ' expert in ' || _top_skill;
        ELSE
            ranking_log := _f_name || ' ' || _l_name || ' has not acquired any skills yet.';
        END IF;

        RETURN NEXT; -- Push the row to the output table
    END LOOP;

    -- 5. Close the Cursor
    CLOSE user_cursor;
END;
$$;