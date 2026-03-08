/*
=============================================
 Sensitive Words Stored Procedures
 Project: SensitiveWordsService
 Database: SensitiveWordsDb
 Version: 1.1
 Author: Ndiphiwe Nombula
=============================================
*/

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


/*
=============================================
 Get All Sensitive Words
=============================================
*/
IF OBJECT_ID('spSensitiveWords_GetAll', 'P') IS NOT NULL
DROP PROCEDURE spSensitiveWords_GetAll
GO

CREATE PROCEDURE spSensitiveWords_GetAll
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        Id,
        Word,
        CreatedAt
    FROM SensitiveWords
END
GO


/*
=============================================
 Get Sensitive Word By Id
=============================================
*/
IF OBJECT_ID('spSensitiveWords_GetById', 'P') IS NOT NULL
DROP PROCEDURE spSensitiveWords_GetById
GO

CREATE PROCEDURE spSensitiveWords_GetById
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        Id,
        Word,
        CreatedAt
    FROM SensitiveWords
    WHERE Id = @Id;
END
GO


/*
=============================================
 Insert Sensitive Word
=============================================
*/
IF OBJECT_ID('spSensitiveWords_Insert', 'P') IS NOT NULL
DROP PROCEDURE spSensitiveWords_Insert
GO

CREATE PROCEDURE spSensitiveWords_Insert
    @Word NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    -- Normalize word
    SET @Word = UPPER(LTRIM(RTRIM(@Word)));

    -- Prevent duplicates
    IF EXISTS (
        SELECT 1
        FROM SensitiveWords
        WHERE Word = @Word
    )
    BEGIN
        ;THROW 50001, 'Sensitive word already exists.', 1;
    END

    INSERT INTO SensitiveWords (Word)
    VALUES (@Word);

    SELECT CAST(SCOPE_IDENTITY() AS INT) AS Id;
END
GO


/*
=============================================
 Update Sensitive Word
=============================================
*/
IF OBJECT_ID('spSensitiveWords_Update', 'P') IS NOT NULL
DROP PROCEDURE spSensitiveWords_Update
GO

CREATE PROCEDURE spSensitiveWords_Update
    @Id INT,
    @Word NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    SET @Word = UPPER(LTRIM(RTRIM(@Word)));

    -- Check if record exists
    IF NOT EXISTS (
        SELECT 1
        FROM SensitiveWords
        WHERE Id = @Id
    )
    BEGIN
        ;THROW 50002, 'Sensitive word not found.', 1;
    END

    -- Prevent duplicates
    IF EXISTS (
        SELECT 1
        FROM SensitiveWords
        WHERE Word = @Word
        AND Id <> @Id
    )
    BEGIN
        ;THROW 50003, 'Sensitive word already exists.', 1;
    END

    UPDATE SensitiveWords
    SET Word = @Word
    WHERE Id = @Id;
END
GO


/*
=============================================
 Delete Sensitive Word
=============================================
*/
IF OBJECT_ID('spSensitiveWords_Delete', 'P') IS NOT NULL
DROP PROCEDURE spSensitiveWords_Delete
GO

CREATE PROCEDURE spSensitiveWords_Delete
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;

    -- Check if record exists
    IF NOT EXISTS (
        SELECT 1
        FROM SensitiveWords
        WHERE Id = @Id
    )
    BEGIN
        ;THROW 50004, 'Sensitive word not found.', 1;
    END

    DELETE FROM SensitiveWords
    WHERE Id = @Id;
END
GO