CREATE PROCEDURE spListCustomer
AS
SELECT [Id]
, CONCAT([FirstName],' ',[LastName]) AS [Name]
, [Document]
, [Email] FROM Customer WHERE IsDeleted = 0