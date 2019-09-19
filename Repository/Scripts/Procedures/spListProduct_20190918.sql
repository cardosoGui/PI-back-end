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
