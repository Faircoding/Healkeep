
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spGetAllRoles]
AS
BEGIN
	SELECT Id,Name
	FROM [AspNetRoles]
END

