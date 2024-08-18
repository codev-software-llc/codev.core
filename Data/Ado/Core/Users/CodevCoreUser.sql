CREATE LOGIN [codevcoreuser] WITH PASSWORD=N'%CORE_DATABASE_PASSWORD%', DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
GO

CREATE USER [codevcoreuser] FOR LOGIN [codevcoreuser];
GO

EXECUTE sp_addrolemember @rolename = N'db_owner', @membername = N'codevcoreuser';
GO