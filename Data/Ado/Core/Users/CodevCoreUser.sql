CREATE LOGIN [coreuser] WITH PASSWORD=N'%CORE_DATABASE_PASSWORD%', DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
GO

CREATE USER [coreuser] FOR LOGIN [coreuser];
GO

EXECUTE sp_addrolemember @rolename = N'db_owner', @membername = N'coreuser';
GO