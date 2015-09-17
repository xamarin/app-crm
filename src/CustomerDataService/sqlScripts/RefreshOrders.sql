USE [XamarinCRMv2];
GO

SET ANSI_NULLS ON;
GO

SET QUOTED_IDENTIFIER ON;
GO

IF object_id('[XamarinCRMv2_CustomerDataService].[RefreshOrders]') IS NOT NULL
  DROP PROCEDURE [XamarinCRMv2_CustomerDataService].[RefreshOrders];
GO

CREATE PROCEDURE [XamarinCRMv2_CustomerDataService].[RefreshOrders]
AS
BEGIN
	
	SET NOCOUNT ON;

	UPDATE 
		[XamarinCRMv2_CustomerDataService].[Order]
	SET 
		order_date = DATEADD(DAY, DATEDIFF(DAY, __updatedAt, GETDATE()), order_date)

	UPDATE 
		[XamarinCRMv2_CustomerDataService].[Order]
	SET 
		due_date = DATEADD(DAY, DATEDIFF(DAY, __updatedAt, GETDATE()), due_date)

	UPDATE 
		[XamarinCRMv2_CustomerDataService].[Order]
	SET 
		closed_date = DATEADD(DAY, DATEDIFF(DAY, __updatedAt, GETDATE()), closed_date)
	WHERE 
		closed_date IS NOT NULL

	SELECT Count(*) AS UpdatedOrderCount FROM [XamarinCRMv2_CustomerDataService].[Order]

	RETURN 0
END

GO