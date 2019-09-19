CREATE PROCEDURE spListProductId
@Id UNIQUEIDENTIFIER
AS
SELECT
Id
,Title
,Description
,Image
,Price
,QuantityOnHand
FROM Product WHERE Id = @Id