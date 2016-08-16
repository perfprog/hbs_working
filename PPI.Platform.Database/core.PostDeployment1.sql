/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
/*first deployment */
/*
:r .\core\Data\core.Culture.data.sql
:r .\core\Data\core.GenericType.data.sql
:r .\core\Data\core.Resource.data.sql
:r .\core\Data\core.GenericTypeValue.data.sql
:r .\core\Data\core.ResourceValue.data.sql
:r .\core\Data\dbo.AspNetRoles.data.sql
:r .\core\Data\dbo.AspNetUsers.data.sql
:r .\core\Data\dbo.AspNetUserRoles.data.sql
*/

-- Email Send type lookups
--:r .\core\Data\EmailTypes.sql