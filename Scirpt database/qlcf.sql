CREATE DATABASE QLCF
GO

USE QLCF
GO

-- FOOD
-- FOOD_CATE
-- TABLES
-- ACCOUNT
-- BILL
-- BILL_INFO

CREATE TABLE TABLES
(
	TABLES_ID INT IDENTITY PRIMARY KEY,
	TABLES_STATUS INT NOT NULL DEFAULT 0 --0: TRỐNG, 1: ĐÃ CÓ KHÁCH
)
GO

CREATE TABLE ACCOUNT
(
	ACC_ID INT IDENTITY PRIMARY KEY,
	USERNAME NVARCHAR(100) NOT NULL DEFAULT N'USER',
	PASS NVARCHAR(1000) NOT NULL DEFAULT N'1',
	ACC_TYPE INT NOT NULL DEFAULT 0 -- 0:admin ; 1:user
)
GO



CREATE TABLE FOOD_CATE
(
	FCATE_ID INT IDENTITY PRIMARY KEY,
	FCATE_NAME NVARCHAR(100) NOT NULL DEFAULT N'FOOD_CATE'
)
GO

CREATE TABLE FOOD 
(
	FOOD_ID INT IDENTITY PRIMARY KEY,
	FOOD_NAME NVARCHAR(100) NOT NULL DEFAULT N'FOOD',
	FCATE_ID INT NOT NULL,
	PRICE MONEY NOT NULL DEFAULT 0

	FOREIGN KEY (FCATE_ID)  REFERENCES FOOD_CATE(FCATE_ID)
)
GO

CREATE TABLE BILL
(
	BILL_ID INT IDENTITY PRIMARY KEY,
	TABLE_ID INT NOT NULL,
	BILL_STATUS INT NOT NULL DEFAULT 0, --0: CHƯA THANH TOÁN, 1: ĐÃ THANH TOÁN
	DATECHECKIN DATE NOT NULL DEFAULT GETDATE(),
	DATECHECKOUT DATE,

	FOREIGN KEY (TABLE_ID) REFERENCES TABLES(TABLES_ID)
)

GO

CREATE TABLE BILL_INFO
(
	ID INT IDENTITY PRIMARY KEY,
	BILL_ID INT NOT NULL,
	FOOD_ID INT NOT NULL,
	COUNT INT NOT NULL DEFAULT 0,
	
	FOREIGN KEY (BILL_ID) REFERENCES BILL (BILL_ID),
	FOREIGN KEY (FOOD_ID) REFERENCES FOOD (FOOD_ID)
)
GO

CREATE PROCEDURE USP_GETACCOUNTBYUSERNAME @USERNAME NVARCHAR(100)
AS
BEGIN
	SELECT * FROM ACCOUNT WHERE USERNAME = @USERNAME
END

EXEC USP_GETACCOUNTBYUSERNAME @USERNAME = N'TUNGLETE'
go

CREATE PROC USP_LOGIN @USERNAME NVARCHAR(100), @PASS NVARCHAR(1000)
AS
BEGIN
	SELECT * FROM ACCOUNT WHERE USERNAME = @USERNAME  AND PASS = @PASS
END
GO

EXEC USP_LOGIN @USERNAME = N'TUNGLETE', @PASS = N'1'
GO

DECLARE @I INT = 0

WHILE @I <= 10
BEGIN
	INSERT INTO dbo.TABLES
	(
	    TABLES_STATUS
	)
	VALUES
	(   0-- TABLES_STATUS - int
	    )
	SET @I = @I + 1
END




-- Thêm Food_Cate
INSERT INTO dbo.FOOD_CATE
(
    FCATE_NAME
)
VALUES
(N'Đồ ăn' -- FCATE_NAME - nvarchar(100)
    )
INSERT INTO dbo.FOOD_CATE
(
    FCATE_NAME
)
VALUES
(N'Nước uống' -- FCATE_NAME - nvarchar(100)
    )

-- Thêm Food
INSERT INTO dbo.FOOD
(
    FOOD_NAME,
    FCATE_ID,
    PRICE
)
VALUES
(   N'Cơm chiên dương châu', -- FOOD_NAME - nvarchar(100)
    1,   -- FCATE_ID - int
    10000 -- PRICE - money
    ),
(   N'Cơm gà chiên', -- FOOD_NAME - nvarchar(100)
    1,   -- FCATE_ID - int
    15000 -- PRICE - money
    ),
(   N'Cơm chiên cá mặn', -- FOOD_NAME - nvarchar(100)
    1,   -- FCATE_ID - int
    15000 -- PRICE - money
    ),
(   N'Cà phê sữa đá', -- FOOD_NAME - nvarchar(100)
    2,   -- FCATE_ID - int
    15000 -- PRICE - money
    ),
(   N'Bạc xỉu', -- FOOD_NAME - nvarchar(100)
    2,   -- FCATE_ID - int
    15000 -- PRICE - money
    )
GO

-- Thêm Bill

INSERT INTO dbo.BILL
(
	BILL_ID,
    TABLE_ID,
    BILL_STATUS,
    DATECHECKIN,
    DATECHECKOUT
)
VALUES
(   1,
	1,         -- TABLE_ID - int
    0,         -- BILL_STATUS - int
    GETDATE(), -- DATECHECKIN - date
    NULL  -- DATECHECKOUT - date
    ),
(   2,
	2,         -- TABLE_ID - int
    0,         -- BILL_STATUS - int
    GETDATE(), -- DATECHECKIN - date
    NULL  -- DATECHECKOUT - date
    ),

(   3, 
	3,         -- TABLE_ID - int
    0,         -- BILL_STATUS - int
    GETDATE(), -- DATECHECKIN - date
    NULL  -- DATECHECKOUT - date
    )

-- Thêm BillInfo
INSERT INTO dbo.BILL_INFO
(
    BILL_ID,
    FOOD_ID
)
VALUES
(   1, -- BILL_ID - int
    1  -- FOOD_ID - int
    ),
(   1, -- BILL_ID - int
    2  -- FOOD_ID - int
    ),
(   1, -- BILL_ID - int
    3  -- FOOD_ID - int
    ),
(   2, -- BILL_ID - int
    1  -- FOOD_ID - int
    ),
(   2, -- BILL_ID - int
    3  -- FOOD_ID - int
    ),
(   2, -- BILL_ID - int
    4  -- FOOD_ID - int
    )

	
	--DBCC CHECKIDENT ('bill_info', RESEED, 0)  
	--DBCC CHECKIDENT ('bill', RESEED, 0)  
	

INSERT INTO dbo.BILL_INFO
(
	BILL_ID,
	FOOD_ID
)
VALUES
(   3, -- BILL_ID - int
	2  -- FOOD_ID - int
	)

INSERT INTO dbo.ACCOUNT
(
	USERNAME,
	PASS,
	ACC_TYPE
)
VALUES
(   N'1', -- USERNAME - nvarchar(100)
	N'1', -- PASS - nvarchar(1000)
	0    -- ACC_TYPE - int
)

GO
ALTER PROC USP_InsertBill
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



ALTER PROC USP_InsertBillInfo
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

	SELECT * FROM dbo.BILL_INFO
    
	EXEC dbo.USP_InsertBillInfo @idBill = 3,  -- int
	                            @idFood = 1,  -- int
	                            @count = 10    -- int
	go
	
-- Thêm trigger

ALTER TRIGGER UTG_InsertBillInfo
ON dbo.BILL_INFO  FOR INSERT, UPDATE 
AS 
BEGIN
	DECLARE @idBill INT, @idTable INT

	SELECT @idBill = Inserted.BILL_ID 
	FROM Inserted
	
	SELECT @idTable = TABLE_ID 
	FROM dbo.BILL
	WHERE @idBill = BILL_ID

	UPDATE dbo.TABLES SET TABLES_STATUS = 1
	WHERE @idTable = TABLES_ID

END	
GO

ALTER TRIGGER UTG_UpdateBill ON dbo.BILL
FOR UPDATE
AS 
BEGIN
	DECLARE @countNew INT, @countOld INT, @idTableNew INT, @idTableOld INT

	SELECT @idTableNew = Inserted.TABLE_ID 
	FROM Inserted

	SELECT @idTableOld = Deleted.TABLE_ID
	FROM Deleted

	SELECT @countNew = COUNT(*)
	FROM dbo.BILL
	WHERE @idTableNew = TABLE_ID AND BILL_STATUS = 0

	SELECT @countOld = COUNT(*)
	FROM dbo.BILL
	WHERE @idTableOld = TABLE_ID AND BILL_STATUS = 0

	IF (@countNew = 0)
	UPDATE dbo.TABLES 
	SET TABLES_STATUS = 0
	WHERE TABLES_ID = @idTableNew

	ELSE
	UPDATE dbo.TABLES 
	SET TABLES_STATUS = 1
	WHERE TABLES_ID = @idTableNew

	IF (@countOld = 0)
	UPDATE dbo.TABLES
	SET TABLES_STATUS = 0
	WHERE TABLES_ID = @idTableOld

END
GO

ALTER TABLE dbo.BILL
	ADD discount INT

go
ALTER PROC USP_SwitchTable
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
CREATE PROC USP_GetBillListByDate
@DateIn DATETIME ,@DateOut DATETIME
AS
BEGIN
SELECT  t.name AS 'Tên bàn', b.totalPrice AS 'Tổng giá', b.DATECHECKIN AS 'Ngày vào', b.DATECHECKOUT AS 'Ngày ra', b.discount AS 'Giảm giá'
FROM dbo.BILL b, dbo.TABLES t
WHERE b.TABLE_ID = t.TABLES_ID AND b.DATECHECKIN >= @DateIn AND b.DATECHECKOUT <= @DateOut 
END

--EXEC dbo.USP_GetBillListByDate @DateIn = '2021-Feb-03', -- datetime
                              -- @DateOut = '2021-Feb-03' -- datetime

GO 
CREATE PROC USP_GetBillListByDateÀndPage
@DateIn DATETIME ,@DateOut DATETIME, @page INT 
AS
BEGIN
SELECT  t.name AS 'Tên bàn', b.totalPrice AS 'Tổng giá', b.DATECHECKIN AS 'Ngày vào', b.DATECHECKOUT AS 'Ngày ra', b.discount AS 'Giảm giá'
FROM dbo.BILL b, dbo.TABLES t
WHERE b.TABLE_ID = t.TABLES_ID AND b.DATECHECKIN >= @DateIn AND b.DATECHECKOUT <= @DateOut 
END


GO
CREATE PROC USP_UpdateAccount
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

go
ALTER TRIGGER UTG_DeleteBillInfo ON dbo.BILL_INFO FOR DELETE
AS
BEGIN
	DECLARE @idBill INT, @idTable INT

	SELECT @idBill = Deleted.BILL_ID 
	FROM Deleted

	SELECT @idTable = TABLE_ID
	FROM dbo.BILL
	WHERE @idBill = BILL_ID

	DECLARE @count INT =  0
	SELECT @count =  COUNT(*)
	FROM dbo.BILL b, dbo.BILL_INFO bi
	WHERE @idTable = b.TABLE_ID AND b.BILL_STATUS = 0
	
	

	IF (@count = 0)
	BEGIN
		UPDATE dbo.TABLES 
		SET TABLES_STATUS = 0
		WHERE TABLES_ID = @idTable
	END

END 
GO 

CREATE PROC USP_DeleteTableByID 
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

CREATE FUNCTION [dbo].[fuConvertToUnsign1] ( @strInput NVARCHAR(4000) ) RETURNS NVARCHAR(4000) AS BEGIN IF @strInput IS NULL RETURN @strInput IF @strInput = '' RETURN @strInput DECLARE @RT NVARCHAR(4000) DECLARE @SIGN_CHARS NCHAR(136) DECLARE @UNSIGN_CHARS NCHAR (136) SET @SIGN_CHARS = N'ăâđêôơưàảãạáằẳẵặắầẩẫậấèẻẽẹéềểễệế ìỉĩịíòỏõọóồổỗộốờởỡợớùủũụúừửữựứỳỷỹỵý ĂÂĐÊÔƠƯÀẢÃẠÁẰẲẴẶẮẦẨẪẬẤÈẺẼẸÉỀỂỄỆẾÌỈĨỊÍ ÒỎÕỌÓỒỔỖỘỐỜỞỠỢỚÙỦŨỤÚỪỬỮỰỨỲỶỸỴÝ' +NCHAR(272)+ NCHAR(208) SET @UNSIGN_CHARS = N'aadeoouaaaaaaaaaaaaaaaeeeeeeeeee iiiiiooooooooooooooouuuuuuuuuuyyyyy AADEOOUAAAAAAAAAAAAAAAEEEEEEEEEEIIIII OOOOOOOOOOOOOOOUUUUUUUUUUYYYYYDD' DECLARE @COUNTER int DECLARE @COUNTER1 int SET @COUNTER = 1 WHILE (@COUNTER <=LEN(@strInput)) BEGIN SET @COUNTER1 = 1 WHILE (@COUNTER1 <=LEN(@SIGN_CHARS)+1) BEGIN IF UNICODE(SUBSTRING(@SIGN_CHARS, @COUNTER1,1)) = UNICODE(SUBSTRING(@strInput,@COUNTER ,1) ) BEGIN IF @COUNTER=1 SET @strInput = SUBSTRING(@UNSIGN_CHARS, @COUNTER1,1) + SUBSTRING(@strInput, @COUNTER+1,LEN(@strInput)-1) ELSE SET @strInput = SUBSTRING(@strInput, 1, @COUNTER-1) +SUBSTRING(@UNSIGN_CHARS, @COUNTER1,1) + SUBSTRING(@strInput, @COUNTER+1,LEN(@strInput)- @COUNTER) BREAK END SET @COUNTER1 = @COUNTER1 +1 END SET @COUNTER = @COUNTER +1 END SET @strInput = replace(@strInput,' ','-') RETURN @strInput END
GO 

SELECT TOP (2*3) t.name AS 'Tên bàn', b.totalPrice AS 'Tổng giá', b.DATECHECKIN AS 'Ngày vào', b.DATECHECKOUT AS 'Ngày ra', b.discount AS 'Giảm giá'
FROM dbo.BILL b, dbo.TABLES t
WHERE b.TABLE_ID = t.TABLES_ID AND b.DATECHECKIN >= '2021-02-03' AND b.DATECHECKOUT <= '2021-02-21'
EXCEPT
SELECT TOP (2*2) t.name AS 'Tên bàn', b.totalPrice AS 'Tổng giá', b.DATECHECKIN AS 'Ngày vào', b.DATECHECKOUT AS 'Ngày ra', b.discount AS 'Giảm giá'
FROM dbo.BILL b, dbo.TABLES t
WHERE b.TABLE_ID = t.TABLES_ID AND b.DATECHECKIN >= '2021-02-03' AND b.DATECHECKOUT <= '2021-02-21'

SELECT  t.name AS 'Tên bàn', b.totalPrice AS 'Tổng giá', b.DATECHECKIN AS 'Ngày vào', b.DATECHECKOUT AS 'Ngày ra', b.discount AS 'Giảm giá'
FROM dbo.BILL b, dbo.TABLES t
WHERE b.TABLE_ID = t.TABLES_ID AND b.DATECHECKIN >= '2021-02-03' AND b.DATECHECKOUT <= '2021-02-21'

GO 
ALTER PROC USP_GetBillListByDateAndPage
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

EXECUTE dbo.USP_GetBillListByDateAndPage @DateIn = '2021-02-01',  -- datetime
                                         @DateOut = '2021-02-21', -- datetime
                                         @page = 5                      -- int

SELECT TOP (15) b.BILL_ID, t.name AS 'Tên bàn', b.totalPrice AS 'Tổng giá', b.DATECHECKIN AS 'Ngày vào', b.DATECHECKOUT AS 'Ngày ra', b.discount AS 'Giảm giá'
FROM dbo.BILL b JOIN dbo.TABLES t ON b.TABLE_ID = t.TABLES_ID
WHERE b.DATECHECKIN >= '2021-02-01' AND b.DATECHECKOUT <= '2021-02-21'
EXCEPT
SELECT TOP (10) b.BILL_ID, t.name AS 'Tên bàn', b.totalPrice AS 'Tổng giá', b.DATECHECKIN AS 'Ngày vào', b.DATECHECKOUT AS 'Ngày ra', b.discount AS 'Giảm giá'
FROM dbo.BILL b JOIN dbo.TABLES t ON b.TABLE_ID = t.TABLES_ID
WHERE b.DATECHECKIN >= '2021-02-01' AND b.DATECHECKOUT <= '2021-02-21'

SELECT  t.name AS 'Tên bàn', b.totalPrice AS 'Tổng giá', b.DATECHECKIN AS 'Ngày vào', b.DATECHECKOUT AS 'Ngày ra', b.discount AS 'Giảm giá'
FROM dbo.BILL b, dbo.TABLES t
WHERE b.TABLE_ID = t.TABLES_ID AND  b.DATECHECKIN >= '2021-02-01' AND b.DATECHECKOUT <= '2021-02-21'

SELECT * FROM dbo.BILL

SELECT TOP 15 * FROM dbo.BILL WHERE  DATECHECKIN >= '2021-02-01' AND DATECHECKOUT <= '2021-02-21'
EXCEPT
SELECT TOP 14 * FROM dbo.BILL  WHERE  DATECHECKIN >= '2021-02-01' AND DATECHECKOUT <= '2021-02-21'