CREATE PROCEDURE spGetCustomerOrdersCount 
	@Document CHAR(11)
AS 
	SELECT Customer.Id
	      ,CONCAT(Customer.FirstName,'',Customer.LastName) AS [Name]
		  ,Customer.Document
		  ,COUNT([Order].Id) AS [Orders]
	FROM [Customer] Customer 
	INNER JOIN [Order] [Order] ON Customer.Id = [Order].CustomerId 
	GROUP BY 
	 Customer.Id
	,Customer.FirstName
	,Customer.Document
	,Customer.LastName


