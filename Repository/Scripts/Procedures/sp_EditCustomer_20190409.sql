CREATE PROCEDURE spEditCustomer

 @Id UNIQUEIDENTIFIER
,@Document VARCHAR(11)
,@FirstName VARCHAR(50)
,@LastName VARCHAR(50)
,@Email VARCHAR(150)
,@AlterationDate DATETIME

AS
UPDATE Customer
SET FirstName = @FirstName
	,LastName = @LastName
	,Email = @Email
	,Document = @Document
	,AlterationDate = @AlterationDate
WHERE Id = @Id

