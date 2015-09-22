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
		_createdAy = DATEADD(DAY, DATEDIFF(DAY, __updatedAt, GETUTCDATE()), _createdAt),
		_updatedAt = DATEADD(DAY, DATEDIFF(DAY, __updatedAt, GETUTCDATE()), _updatedAt),
		order_date = DATEADD(DAY, DATEDIFF(DAY, __updatedAt, GETUTCDATE()), order_date),
		due_date = DATEADD(DAY, DATEDIFF(DAY, __updatedAt, GETUTCDATE()), due_date),
		closed_date = 
		(
			CASE
				WHEN 
					closed_date IS NOT NULL
				THEN
					DATEADD(DAY, DATEDIFF(DAY, __updatedAt, GETUTCDATE()), closed_date)
				ELSE
					NULL
			END
		)

	SELECT Count(*) AS UpdatedOrderCount FROM [XamarinCRMv2_CustomerDataService].[Order]

	RETURN 0
END

GO