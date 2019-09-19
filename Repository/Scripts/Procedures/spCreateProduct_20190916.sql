CREATE PROCEDURE spCreateProduct
 @Id				UNIQUEIDENTIFIER
,@Title				VARCHAR(40)
,@Description		VARCHAR(40)
,@Image				VARCHAR(11)
,@Price				DECIMAL(18,2)
,@QuantityOnHand	INT
,@RegisterDate		DATETIME
,@AlterationDate	DATETIME

AS

INSERT INTO [Product] (Id
,Title
,[Description]
,[Image]
,Price
,QuantityOnHand
,RegisterDate
,AlterationDate) VALUES (@Id		
,@Title 
,@Description	
,@Image	
,@Price		
,@QuantityOnHand
,@RegisterDate
,@AlterationDate)