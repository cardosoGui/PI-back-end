CREATE PROCEDURE spEditProduct
 @Id UNIQUEIDENTIFIER
,@Title VARCHAR(MAX)
,@Description TEXT
,@Image VARCHAR(MAX)
,@Price MONEY
,@QuantityOnHand INT
,@RegisterDate DATETIME
,@AlterationDate DATETIME

AS
UPDATE Product
SET 
 Title = @Title
,Description = @Description
,Image = @Image
,Price = @Price
,QuantityOnHand = @QuantityOnHand
,AlterationDate = @AlterationDate
WHERE Id = @Id









