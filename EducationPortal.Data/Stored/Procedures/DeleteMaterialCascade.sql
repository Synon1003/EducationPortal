CREATE OR REPLACE PROCEDURE delete_material_cascade(_material_id INT)
LANGUAGE plpgsql
AS $$
DECLARE
    _material_exists BOOLEAN;
BEGIN
    -- Check if it exists
    SELECT EXISTS(SELECT 1 FROM "Materials" WHERE "Id" = _material_id) INTO _material_exists;
    
    IF NOT _material_exists THEN
        RAISE NOTICE 'Material with ID % not found.', _material_id;
        RETURN;
    END IF;

    -- Delete from Join Tables (EF Core default names)
    DELETE FROM "CourseMaterials" WHERE "MaterialId" = _material_id;
    DELETE FROM "UserMaterials" WHERE "MaterialId" = _material_id;

    -- Delete the actual material (works for Article, Video, or Publication)
    DELETE FROM "Materials" WHERE "Id" = _material_id;

    RAISE NOTICE 'Material % and all its associations deleted.', _material_id;
    
    -- Transaction is automatically committed at the end of the procedure
END;
$$;

CALL delete_material_cascade(2);