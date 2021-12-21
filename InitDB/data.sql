USE [master]
GO
/****** Object:  Database [QLCF]    Script Date: 21-Dec-21 8:17:49 PM ******/
CREATE DATABASE [QLCF]
GO

USE [QLCF]
GO
/****** Object:  UserDefinedFunction [dbo].[fuConvertToUnsign1]    Script Date: 21-Dec-21 8:17:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[fuConvertToUnsign1] ( @strInput NVARCHAR(4000) ) RETURNS NVARCHAR(4000) AS BEGIN IF @strInput IS NULL RETURN @strInput IF @strInput = '' RETURN @strInput DECLARE @RT NVARCHAR(4000) DECLARE @SIGN_CHARS NCHAR(136) DECLARE @UNSIGN_CHARS NCHAR (136) SET @SIGN_CHARS = N'ăâđêôơưàảãạáằẳẵặắầẩẫậấèẻẽẹéềểễệế ìỉĩịíòỏõọóồổỗộốờởỡợớùủũụúừửữựứỳỷỹỵý ĂÂĐÊÔƠƯÀẢÃẠÁẰẲẴẶẮẦẨẪẬẤÈẺẼẸÉỀỂỄỆẾÌỈĨỊÍ ÒỎÕỌÓỒỔỖỘỐỜỞỠỢỚÙỦŨỤÚỪỬỮỰỨỲỶỸỴÝ' +NCHAR(272)+ NCHAR(208) SET @UNSIGN_CHARS = N'aadeoouaaaaaaaaaaaaaaaeeeeeeeeee iiiiiooooooooooooooouuuuuuuuuuyyyyy AADEOOUAAAAAAAAAAAAAAAEEEEEEEEEEIIIII OOOOOOOOOOOOOOOUUUUUUUUUUYYYYYDD' DECLARE @COUNTER int DECLARE @COUNTER1 int SET @COUNTER = 1 WHILE (@COUNTER <=LEN(@strInput)) BEGIN SET @COUNTER1 = 1 WHILE (@COUNTER1 <=LEN(@SIGN_CHARS)+1) BEGIN IF UNICODE(SUBSTRING(@SIGN_CHARS, @COUNTER1,1)) = UNICODE(SUBSTRING(@strInput,@COUNTER ,1) ) BEGIN IF @COUNTER=1 SET @strInput = SUBSTRING(@UNSIGN_CHARS, @COUNTER1,1) + SUBSTRING(@strInput, @COUNTER+1,LEN(@strInput)-1) ELSE SET @strInput = SUBSTRING(@strInput, 1, @COUNTER-1) +SUBSTRING(@UNSIGN_CHARS, @COUNTER1,1) + SUBSTRING(@strInput, @COUNTER+1,LEN(@strInput)- @COUNTER) BREAK END SET @COUNTER1 = @COUNTER1 +1 END SET @COUNTER = @COUNTER +1 END SET @strInput = replace(@strInput,' ','-') RETURN @strInput END
GO
/****** Object:  Table [dbo].[ACCOUNT]    Script Date: 21-Dec-21 8:17:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ACCOUNT](
	[ACC_ID] [int] IDENTITY(1,1) NOT NULL,
	[USERNAME] [nvarchar](100) NOT NULL,
	[PASS] [nvarchar](1000) NOT NULL,
	[ACC_TYPE] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ACC_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BILL]    Script Date: 21-Dec-21 8:17:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BILL](
	[BILL_ID] [int] IDENTITY(1,1) NOT NULL,
	[TABLE_ID] [int] NOT NULL,
	[BILL_STATUS] [int] NOT NULL,
	[DATECHECKIN] [date] NOT NULL,
	[DATECHECKOUT] [date] NULL,
	[discount] [int] NULL,
	[totalPrice] [float] NULL,
PRIMARY KEY CLUSTERED 
(
	[BILL_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BILL_INFO]    Script Date: 21-Dec-21 8:17:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BILL_INFO](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[BILL_ID] [int] NOT NULL,
	[FOOD_ID] [int] NOT NULL,
	[COUNT] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FOOD]    Script Date: 21-Dec-21 8:17:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FOOD](
	[FOOD_ID] [int] IDENTITY(1,1) NOT NULL,
	[FOOD_NAME] [nvarchar](100) NOT NULL,
	[FCATE_ID] [int] NOT NULL,
	[PRICE] [money] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[FOOD_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FOOD_CATE]    Script Date: 21-Dec-21 8:17:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FOOD_CATE](
	[FCATE_ID] [int] IDENTITY(1,1) NOT NULL,
	[FCATE_NAME] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[FCATE_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TABLES]    Script Date: 21-Dec-21 8:17:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TABLES](
	[TABLES_ID] [int] IDENTITY(1,1) NOT NULL,
	[TABLES_STATUS] [int] NOT NULL,
	[name] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[TABLES_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[ACCOUNT] ON 
GO
INSERT [dbo].[ACCOUNT] ([ACC_ID], [USERNAME], [PASS], [ACC_TYPE]) VALUES (1, N'admin', N'33354741122871651676713774147412831195', 0)
GO
SET IDENTITY_INSERT [dbo].[ACCOUNT] OFF
GO
ALTER TABLE [dbo].[ACCOUNT] ADD  DEFAULT (N'USER') FOR [USERNAME]
GO
ALTER TABLE [dbo].[ACCOUNT] ADD  DEFAULT (N'1') FOR [PASS]
GO
ALTER TABLE [dbo].[ACCOUNT] ADD  DEFAULT ((0)) FOR [ACC_TYPE]
GO
ALTER TABLE [dbo].[BILL] ADD  DEFAULT ((0)) FOR [BILL_STATUS]
GO
ALTER TABLE [dbo].[BILL] ADD  DEFAULT (getdate()) FOR [DATECHECKIN]
GO
ALTER TABLE [dbo].[BILL_INFO] ADD  DEFAULT ((0)) FOR [COUNT]
GO
ALTER TABLE [dbo].[FOOD] ADD  DEFAULT (N'FOOD') FOR [FOOD_NAME]
GO
ALTER TABLE [dbo].[FOOD] ADD  DEFAULT ((0)) FOR [PRICE]
GO
ALTER TABLE [dbo].[FOOD_CATE] ADD  DEFAULT (N'FOOD_CATE') FOR [FCATE_NAME]
GO
ALTER TABLE [dbo].[TABLES] ADD  DEFAULT ((0)) FOR [TABLES_STATUS]
GO
ALTER TABLE [dbo].[BILL]  WITH CHECK ADD FOREIGN KEY([TABLE_ID])
REFERENCES [dbo].[TABLES] ([TABLES_ID])
GO
ALTER TABLE [dbo].[BILL_INFO]  WITH CHECK ADD FOREIGN KEY([BILL_ID])
REFERENCES [dbo].[BILL] ([BILL_ID])
GO
ALTER TABLE [dbo].[BILL_INFO]  WITH CHECK ADD FOREIGN KEY([FOOD_ID])
REFERENCES [dbo].[FOOD] ([FOOD_ID])
GO
ALTER TABLE [dbo].[FOOD]  WITH CHECK ADD FOREIGN KEY([FCATE_ID])
REFERENCES [dbo].[FOOD_CATE] ([FCATE_ID])
GO
/****** Object:  StoredProcedure [dbo].[USP_DeleteTableByID]    Script Date: 21-Dec-21 8:17:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[USP_DeleteTableByID] 
@idTable INT 
AS
BEGIN
	DECLARE @idBill INT

	SELECT @idBill = BILL_ID 
	FROM dbo.BILL

	IF (@idBill IS NOT NULL)
	BEGIN
		DELETE FROM dbo.BILL_INFO
		WHERE @idBill = BILL_ID

		DELETE FROM dbo.BILL
		WHERE @idTable = TABLE_ID
    END 

	DELETE FROM dbo.TABLES
	WHERE @idTable = TABLES_ID
END
GO
/****** Object:  StoredProcedure [dbo].[USP_GETACCOUNTBYUSERNAME]    Script Date: 21-Dec-21 8:17:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_GETACCOUNTBYUSERNAME] @USERNAME NVARCHAR(100)
AS
BEGIN
	SELECT * FROM ACCOUNT WHERE USERNAME = @USERNAME
END

EXEC USP_GETACCOUNTBYUSERNAME @USERNAME = N'TUNGLETE'
GO
/****** Object:  StoredProcedure [dbo].[USP_GetBillListByDate]    Script Date: 21-Dec-21 8:17:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[USP_GetBillListByDate]
@DateIn DATETIME ,@DateOut DATETIME
AS
BEGIN
SELECT  t.name AS 'Tên bàn', b.totalPrice AS 'Tổng giá', b.DATECHECKIN AS 'Ngày vào', b.DATECHECKOUT AS 'Ngày ra', b.discount AS 'Giảm giá'
FROM dbo.BILL b, dbo.TABLES t
WHERE b.TABLE_ID = t.TABLES_ID AND b.DATECHECKIN >= @DateIn AND b.DATECHECKOUT <= @DateOut 
END
GO
/****** Object:  StoredProcedure [dbo].[USP_GetBillListByDateAndPage]    Script Date: 21-Dec-21 8:17:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[USP_GetBillListByDateAndPage]
@DateIn DATETIME ,@DateOut DATETIME, @page INT 
AS
BEGIN
	DECLARE @defaultRow int = 5

	IF (@page = 1)
	BEGIN
		PRINT 'here'
		SELECT TOP 5 t.name AS 'Tên bàn', b.totalPrice AS 'Tổng giá', b.DATECHECKIN AS 'Ngày vào', b.DATECHECKOUT AS 'Ngày ra', b.discount AS 'Giảm giá'
		FROM dbo.BILL b, dbo.TABLES t
		WHERE b.TABLE_ID = t.TABLES_ID AND b.DATECHECKIN >= @DateIn AND b.DATECHECKOUT <= @DateOut
	END

	ELSE
	BEGIN
		PRINT 'there'
		SELECT TOP (@defaultRow * @page) b.BILL_ID AS 'ID', t.name AS 'Tên bàn', b.totalPrice AS 'Tổng giá', b.DATECHECKIN AS 'Ngày vào', b.DATECHECKOUT AS 'Ngày ra', b.discount AS 'Giảm giá'
		FROM dbo.BILL b, dbo.TABLES t
		WHERE b.TABLE_ID = t.TABLES_ID AND  b.DATECHECKIN >= @DateIn AND b.DATECHECKOUT <= @DateOut
		EXCEPT
		SELECT TOP (@defaultRow * (@page-1)) b.BILL_ID AS 'ID', t.name AS 'Tên bàn', b.totalPrice AS 'Tổng giá', b.DATECHECKIN AS 'Ngày vào', b.DATECHECKOUT AS 'Ngày ra', b.discount AS 'Giảm giá'
		FROM dbo.BILL b, dbo.TABLES t
		WHERE b.TABLE_ID = t.TABLES_ID AND  b.DATECHECKIN >= @DateIn AND b.DATECHECKOUT <= @DateOut
	END

END 
GO
/****** Object:  StoredProcedure [dbo].[USP_GetTableList]    Script Date: 21-Dec-21 8:17:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[USP_GetTableList]
AS SELECT * FROM dbo.TABLES
GO
/****** Object:  StoredProcedure [dbo].[USP_InsertBill]    Script Date: 21-Dec-21 8:17:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[USP_InsertBill]
@id_table int
AS
BEGIN
	INSERT dbo.BILL
	(
		TABLE_ID,
		BILL_STATUS,
		DATECHECKIN,
		DATECHECKOUT,
		discount
	)
	VALUES
	(   @id_table,         -- TABLE_ID - int
		0,         -- BILL_STATUS - int
		GETDATE(), -- DATECHECKIN - date
		NULL,-- DATECHECKOUT - date
		0
		)
END
GO
/****** Object:  StoredProcedure [dbo].[USP_InsertBillInfo]    Script Date: 21-Dec-21 8:17:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROC [dbo].[USP_InsertBillInfo]
@idBill int, @idFood int, @count INT
AS
BEGIN
	DECLARE @billInfoExist INT, @fcount INT = 0
		
	SELECT @billInfoExist = ID, @fcount = COUNT
	FROM dbo.BILL_INFO
	WHERE @idBill = BILL_ID AND @idFood = FOOD_ID

	DECLARE @tempt int = @fcount + @count

	IF @billInfoExist > 0
	BEGIN
			
		IF (@tempt > 0)
			UPDATE dbo.BILL_INFO SET COUNT = COUNT + @count WHERE @idBill = BILL_ID AND @idFood = FOOD_ID
		ELSE
			DELETE FROM dbo.BILL_INFO WHERE FOOD_ID = @idFood AND @idBill = BILL_ID 
	END
	ELSE
	BEGIN
		IF (@tempt > 0)
		INSERT	dbo.BILL_INFO
		(
		    BILL_ID,
		    FOOD_ID,
		    COUNT
		)
		VALUES
		(   @idBill, -- BILL_ID - int
		    @idFood, -- FOOD_ID - int
		    @count  -- COUNT - int
		    )
	END
END
GO
/****** Object:  StoredProcedure [dbo].[USP_LOGIN]    Script Date: 21-Dec-21 8:17:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[USP_LOGIN] @USERNAME NVARCHAR(100), @PASS NVARCHAR(1000)
AS
BEGIN
	SELECT * FROM ACCOUNT WHERE USERNAME = @USERNAME  AND PASS = @PASS
END
GO
/****** Object:  StoredProcedure [dbo].[USP_SwitchTable]    Script Date: 21-Dec-21 8:17:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[USP_SwitchTable]
@idTableOld int, @idTableNew int 
AS
BEGIN
	DECLARE @idBillOld INT, @idBillNew INT

	SELECT @idBillOld = BILL_ID
	FROM dbo.BILL
	WHERE @idTableOld = TABLE_ID AND BILL_STATUS = 0

	IF (@idBillOld IS NOT NULL)
	BEGIN

		SELECT @idBillNew = BILL_ID
		FROM dbo.BILL
		WHERE @idTableNew = TABLE_ID AND BILL_STATUS = 0

		IF (@idBillNew IS NULL)
		BEGIN
			INSERT dbo.BILL
			(
				TABLE_ID,
				BILL_STATUS,
				DATECHECKIN,
				DATECHECKOUT,
				discount
			)
			VALUES
			(   @idTableNew,         -- TABLE_ID - int
				0,         -- BILL_STATUS - int
				GETDATE(), -- DATECHECKIN - date
				NULL, -- DATECHECKOUT - date
				0          -- discount - int
			)

			SELECT @idBillNew = MAX(BILL_ID)
			FROM dbo.BILL
		END

		SELECT * INTO BillInfoOldTable
		FROM dbo.BILL_INFO
		WHERE @idBillOld = BILL_ID

		UPDATE dbo.BillInfoOldTable
		SET BILL_ID = @idBillNew

		DECLARE @i INT  = 0, @countRow INT = 0

		SELECT @countRow = COUNT(*)
		FROM dbo.BillInfoOldTable

		WHILE (@i < @countRow)
		BEGIN
			DECLARE @idBill1 INT, @idFood1 INT, @count1 INT
			SELECT @idBill1 = BILL_ID, @idFood1 = FOOD_ID, @count1 = COUNT 
			FROM dbo.BillInfoOldTable

			EXEC dbo.USP_InsertBillInfo @idBill = @idBill1, -- int
										@idFood = @idFood1, -- int
										@count = @count1   -- int
		

			DELETE  FROM dbo.BillInfoOldTable
			WHERE @idBill1 = BILL_ID

			DELETE FROM dbo.BILL_INFO
			WHERE @idBillOld = BILL_ID

			SET @i = @i + 1
		END

		DROP TABLE dbo.BillInfoOldTable

		UPDATE dbo.BILL
		SET BILL_STATUS = 1
		WHERE BILL_ID = @idBillOld
	END
END


ALTER TABLE dbo.BILL
ADD totalPrice FLOAT

ALTER TABLE dbo.TABLES
ADD name NVARCHAR(100)

GO
/****** Object:  StoredProcedure [dbo].[USP_UpdateAccount]    Script Date: 21-Dec-21 8:17:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[USP_UpdateAccount]
@username NVARCHAR(100), @passWord NVARCHAR(100), @newPass NVARCHAR(100)
AS
BEGIN
	DECLARE @isRight INT = 0
	
	SELECT @isRight = COUNT(*)
	FROM dbo.ACCOUNT
	WHERE @username = USERNAME AND @passWord = PASS

	IF (@isRight = 1)
	BEGIN
	    IF (@passWord <> '' AND @newPass <> '')
		BEGIN
		    UPDATE dbo.ACCOUNT
			SET PASS = @newPass
			WHERE @username = USERNAME
		END
	END
END
GO
USE [master]
GO
ALTER DATABASE [QLCF] SET  READ_WRITE 
GO
