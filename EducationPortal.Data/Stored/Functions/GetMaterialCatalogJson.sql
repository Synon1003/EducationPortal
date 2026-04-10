CREATE OR REPLACE FUNCTION get_material_catalog_json()
RETURNS TABLE (
    id INT,
    title VARCHAR,
    material_type VARCHAR,
    details JSONB
) 
LANGUAGE plpgsql
AS $$
BEGIN
    RETURN QUERY
    SELECT 
        m."Id", 
        m."Title", 
        m."Type",
        CASE 
            WHEN m."Type" = 'Video' THEN 
                jsonb_build_object('Duration', m."Duration", 'Quality', m."Quality")
            WHEN m."Type" = 'Article' THEN 
                jsonb_build_object('Published', m."PublicationDate", 'Link', m."ResourceLink")
            WHEN m."Type" = 'Publication' THEN 
                jsonb_build_object('Authors', m."Authors", 'Year', m."PublicationYear", 'Pages', m."Pages")
            ELSE '{}'::jsonb
        END as details
    FROM "Materials" m
    ORDER BY m."Title";
END;
$$;

SELECT * FROM get_material_catalog_json();