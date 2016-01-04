-- Use this sproc if you'd like to generate random orders for your service intance's DB.
-- You'll need to update the schema name to match your own.
-- Must be executed in SQLCMD mode

:setvar SchemaName "[XamarinCRMService-dev]"

SET ANSI_NULLS ON;
GO

SET QUOTED_IDENTIFIER ON;
GO

IF object_id('$(SchemaName).[GenerateRandomOrders]') IS NOT NULL
  DROP PROCEDURE $(SchemaName).[GenerateRandomOrders];
GO

CREATE PROCEDURE $(SchemaName).[GenerateRandomOrders] @numberOfOrdersToGenerate INT = 200
AS
BEGIN

	SET NOCOUNT ON;

	TRUNCATE TABLE $(SchemaName).[Orders];

	DECLARE @TempOrders TABLE
	(
	   id NVARCHAR(255),
	   createdAt DATETIMEOFFSET(3),
	   updatedAt DATETIMEOFFSET(3),
	   isOpen BIT,
	   accountId NVARCHAR(MAX),
	   price FLOAT,
	   item NVARCHAR(MAX),
	   order_date DATETIMEOFFSET(3),
	   due_date DATETIMEOFFSET(3),
	   closed_date DATETIMEOFFSET(3),
	   deleted BIT
	);

	DECLARE @inceptionDate DATETIMEOFFSET(3) = CONVERT(datetimeoffset, '2015-12-06 00:00:00 -00:00');

	DECLARE @Iteration INT;
	DECLARE @OrderStatusIteration INT;
	SET @Iteration = 1;
	SET @OrderStatusIteration = 1;
	WHILE @Iteration <= @numberOfOrdersToGenerate
	BEGIN
		
		DECLARE @isOpen AS INT;
		DECLARE @accountId AS NVARCHAR(MAX);
		DECLARE @price AS FLOAT;
		DECLARE @item AS NVARCHAR(MAX);
		DECLARE @order_date DATETIMEOFFSET(3);
		DECLARE @due_date DATETIMEOFFSET(3);
		DECLARE @closed_date DATETIMEOFFSET(3) = NULL;

		-- select random isOpen bit
		-- SELECT @isOpen = CAST(CAST(CRYPT_GEN_RANDOM(1) AS int) % 2 AS BIT);

		-- make 25% of orders open, 75% closed
		IF (@OrderStatusIteration < 4)
			BEGIN
				SELECT @isOpen = 0;
			END
		ELSE
			BEGIN
				SELECT @isOpen = 1;
			END

		-- Select random account
		SELECT TOP 1 @accountId = id FROM $(SchemaName).[Accounts] WHERE IsLead = 0 ORDER BY NEWID();

		-- Select random product
		SELECT TOP 1 @item = Name, @price = Price FROM $(SchemaName).[Products] ORDER BY NEWID();

		-- Select random date in the past within 6 weeks
		SELECT @order_date = (SELECT DATEADD(DAY, -(ABS(CHECKSUM(NEWID()) % 41) + 1), @inceptionDate));

		-- get due date 1 week after order date
		SELECT @due_date = (SELECT DATEADD(DAY, 7, @order_date));
		IF (@due_date > @inceptionDate)
			BEGIN
				SELECT @due_date = @inceptionDate;
			END

		-- if the order is not open, get a closed date that is random within 4 days in either direction of the due date
		IF (@isOpen = 0)
			BEGIN
				DECLARE @random_bool AS BIT;
				SELECT @random_bool = CAST(CAST(CRYPT_GEN_RANDOM(1) AS int) % 2 AS BIT);
				DECLARE @random_closed_day_range AS INT;
				SELECT @random_closed_day_range = ABS(CHECKSUM(NEWID()) % 3) + 1;
				IF @random_bool = 0
					BEGIN
						SELECT @closed_date = (SELECT DATEADD(day, -@random_closed_day_range, @due_date));
					END
				ELSE 
					BEGIN
						SELECT @closed_date = (SELECT DATEADD(day, @random_closed_day_range, @due_date));
					END

				IF (@closed_date > @inceptionDate)
				BEGIN
					SELECT @closed_date = @inceptionDate;
				END
			END

		-- insert order into temp table
		INSERT @TempOrders 
		VALUES 
		(
			NEWID(),
			@inceptionDate,
			@inceptionDate,
			@isOpen,
			@accountId,
			@price,
			@item,
			@order_date,
			@due_date,
			@closed_date,
			0
		)
		SET @Iteration = @Iteration + 1;
		SET @OrderStatusIteration = @OrderStatusIteration + 1;

		IF (@OrderStatusIteration > 4)
			BEGIN
				SET @OrderStatusIteration = 1;
			END
	END

	-- insert all temp table records into destination table
	INSERT INTO $(SchemaName).[Orders] 
	(
		Id,
		CreatedAt,
		UpdatedAt,
		IsOpen,
		AccountId,
		Price,
		Item,
		OrderDate,
		DueDate,
		ClosedDate,
		Deleted
	)
	SELECT 
		id,
		createdAt,
		updatedAt,
		isOpen,
		accountId,
		price,
		item,
		order_date,
		due_date,
		closed_date,
		deleted 
	FROM @TempOrders
END

GO