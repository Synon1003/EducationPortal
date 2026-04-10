CREATE TABLE "GlobalStats" (
    "StatName" TEXT PRIMARY KEY,
    "StatValue" INT DEFAULT 0
);

INSERT INTO "GlobalStats" ("StatName", "StatValue") VALUES ('TotalEnrollments', 0);

CREATE TABLE "SystemEvents" (
    "Id" SERIAL PRIMARY KEY,
    "EventTime" TIMESTAMP DEFAULT NOW(),
    "Operation" TEXT, -- INSERT or DELETE
    "Message" TEXT,
    "UserId" UUID
);

CREATE OR REPLACE FUNCTION maintain_enrollment_count()
RETURNS TRIGGER AS $$
BEGIN
    IF (TG_OP = 'INSERT') THEN
        UPDATE "GlobalStats" SET "StatValue" = "StatValue" + 1 WHERE "StatName" = 'TotalEnrollments';
        
        INSERT INTO "SystemEvents" ("Operation", "Message", "UserId")
        VALUES ('INSERT', 'User enrolled in course', NEW."UserId");

    ELSIF (TG_OP = 'DELETE') THEN
        UPDATE "GlobalStats" SET "StatValue" = "StatValue" - 1 WHERE "StatName" = 'TotalEnrollments';
        
        INSERT INTO "SystemEvents" ("Operation", "Message", "UserId")
        VALUES ('DELETE', 'User unenrolled from course', OLD."UserId");
    END IF;

    RETURN NULL; -- AFTER triggers can return NULL
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER trg_enrollment_audit
AFTER INSERT OR DELETE ON "UserCourses"
FOR EACH ROW
EXECUTE FUNCTION maintain_enrollment_count();

--
SELECT * FROM public."GlobalStats"
ORDER BY "StatName" ASC 