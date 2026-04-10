CREATE OR REPLACE FUNCTION validate_material_data()
RETURNS TRIGGER AS $$
BEGIN
    NEW."Title" := TRIM(NEW."Title");

    IF NEW."Discriminator" = 'Video' AND NEW."Duration" < 0 THEN
        NEW."Duration" := 0;
    END IF;

    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER trg_validate_material
BEFORE INSERT OR UPDATE ON "Materials"
FOR EACH ROW
EXECUTE FUNCTION validate_material_data();