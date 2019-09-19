CREATE PROCEDURE spCreateCustomer
 @Id		    UNIQUEIDENTIFIER
,@FirstName     VARCHAR(40)
,@LastName	    VARCHAR(40)
,@Document	    CHAR(11)
,@Email		    VARCHAR(160)
,@Phone		    VARCHAR(13)
,@RegisterDate  DATETIME
,@AlterationDate  DATETIME

AS

INSERT INTO [Customer] (Id
,FirstName
,LastName
,Document
,Email
,Phone
,RegisterDate
,AlterationDate) VALUES (@Id		
,@FirstName 
,@LastName	
,@Document	
,@Email		
,@Phone
,@RegisterDate
,@AlterationDate)