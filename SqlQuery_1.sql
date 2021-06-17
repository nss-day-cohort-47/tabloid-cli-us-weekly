USE [master]

IF db_id('TabloidCLI') IS NULl
BEGIN
    CREATE DATABASE [TabloidCLI]
END;
GO

use [TabloidCLI]
go
SELECT t.Id, t.Name 
                                        FROM Post p 
                                        join PostTag pt on pt.PostId = p.Id
                                        join Tag t on pt.TagId = t.Id
                                        Where p.Id =1
