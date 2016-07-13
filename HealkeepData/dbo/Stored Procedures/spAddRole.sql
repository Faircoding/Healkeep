

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spAddRole]
  --@Id NVARCHAR(128),
  @Name nvarchar(256)
AS
BEGIN
	INSERT INTO [AspNetRoles] (Id,Name)
	VALUES (
			CASE (select max(cast(Id as bigint)) from [dbo].[AspNetRoles])
				WHEN NULL THEN '1'
				ELSE (select cast(max(cast(Id as bigint)+1) as nvarchar) from [dbo].[AspNetRoles])
			END
			--@Id
			,@Name)
	
END


