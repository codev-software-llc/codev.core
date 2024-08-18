-- ****************************************************************************
-- Script to populate the tables with predefined data.
-- ****************************************************************************
DELETE FROM [core].[EnumTypes] WHERE [Name] = 'Test'

-- ****************************************************************************
-- Populate the Core Flags (this bridges the use of flags across the
-- C# and SQL side of things.
-- ****************************************************************************
DECLARE @DateNow DATETIME = CURRENT_TIMESTAMP;

INSERT INTO [core].[EnumTypes] ([IsActive], [Flags], [DateCreated], [DateModified], [IsFlag], [IsBig], [Application], [Name], [EnumKey], [Value], [Comment]) VALUES(1, 0, @DateNow, @DateNow, 1, 1, 'Test', 'MerchantCardFlags'        , 'None'   ,  0, '')
INSERT INTO [core].[EnumTypes] ([IsActive], [Flags], [DateCreated], [DateModified], [IsFlag], [IsBig], [Application], [Name], [EnumKey], [Value], [Comment]) VALUES(1, 0, @DateNow, @DateNow, 1, 1, 'Test', 'MerchantCustomerFlags'    , 'None'   ,  0, '')
INSERT INTO [core].[EnumTypes] ([IsActive], [Flags], [DateCreated], [DateModified], [IsFlag], [IsBig], [Application], [Name], [EnumKey], [Value], [Comment]) VALUES(1, 0, @DateNow, @DateNow, 1, 1, 'Test', 'MerchantPlanFlags'        , 'None'   ,  0, '')
INSERT INTO [core].[EnumTypes] ([IsActive], [Flags], [DateCreated], [DateModified], [IsFlag], [IsBig], [Application], [Name], [EnumKey], [Value], [Comment]) VALUES(1, 0, @DateNow, @DateNow, 1, 1, 'Test', 'MerchantSubscriptionFlags', 'None'   ,  0, '')
INSERT INTO [core].[EnumTypes] ([IsActive], [Flags], [DateCreated], [DateModified], [IsFlag], [IsBig], [Application], [Name], [EnumKey], [Value], [Comment]) VALUES(1, 0, @DateNow, @DateNow, 1, 1, 'Test', 'MerchantTokenFlags'       , 'None'   ,  0, '')
INSERT INTO [core].[EnumTypes] ([IsActive], [Flags], [DateCreated], [DateModified], [IsFlag], [IsBig], [Application], [Name], [EnumKey], [Value], [Comment]) VALUES(1, 0, @DateNow, @DateNow, 1, 1, 'Test', 'MerchantTransactionFlags' , 'Success',  0, '')
INSERT INTO [core].[EnumTypes] ([IsActive], [Flags], [DateCreated], [DateModified], [IsFlag], [IsBig], [Application], [Name], [EnumKey], [Value], [Comment]) VALUES(1, 0, @DateNow, @DateNow, 1, 1, 'Test', 'MerchantTransactionFlags' , 'Failure',  1, '')