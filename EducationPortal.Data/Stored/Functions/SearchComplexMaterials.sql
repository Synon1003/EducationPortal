CREATE OR REPLACE FUNCTION search_complex_materials(_min_value INT)
RETURNS TABLE (material_id INT, material_title VARCHAR, type_info TEXT) 
LANGUAGE plpgsql
AS $$
BEGIN
    RETURN QUERY
    SELECT m."Id", m."Title", 
           (m."Type" || ': ' || 
            COALESCE(m."Duration"::text, m."Pages"::text) || ' units') as type_info
    FROM "Materials" m
    WHERE (m."Type" = 'Video' AND m."Duration" >= _min_value)
       OR (m."Type" = 'Publication' AND m."Pages" >= _min_value);
END;
$$;

SELECT * FROM search_complex_materials(500);