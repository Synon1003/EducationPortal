CREATE OR ALTER VIEW view_database_relationships AS
SELECT 
    obj.name AS TableName, 
    col.name AS ColumnName, 
    reftbl.name AS ForeignTableName, 
    refcol.name AS ForeignColumnName
FROM sys.foreign_key_columns fkc
INNER JOIN sys.objects obj ON fkc.parent_object_id = obj.object_id
INNER JOIN sys.columns col ON fkc.parent_object_id = col.object_id AND fkc.parent_column_id = col.column_id
INNER JOIN sys.objects reftbl ON fkc.referenced_object_id = reftbl.object_id
INNER JOIN sys.columns refcol ON fkc.referenced_object_id = refcol.object_id AND fkc.referenced_column_id = refcol.column_id;
GO

-- To query it, you must use the aliases you defined above:
SELECT * FROM view_database_relationships;

-- Note the CamelCase to match your AS aliases
SELECT * FROM view_database_relationships 
WHERE TableName = 'Courses' OR ForeignTableName = 'Courses';