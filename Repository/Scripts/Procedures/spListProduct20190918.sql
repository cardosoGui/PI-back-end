CREATE PROCEDURE spListProduct
AS
SELECT
Id
,Title
,Description
,Image
,Price
,QuantityOnHand
FROM Product
WHERE IsDeleted = 0
