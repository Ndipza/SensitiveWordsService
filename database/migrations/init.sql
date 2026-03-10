/*
=============================================
 Database Initialization Script
 Project: SensitiveWordsService
 Database: SensitiveWordsDb
 Author: Ndiphiwe Nombula
=============================================
*/

IF DB_ID('SensitiveWordsDb') IS NULL
BEGIN
    CREATE DATABASE SensitiveWordsDb;
END
GO

USE SensitiveWordsDb
GO


/*
=============================================
 SensitiveWords Table
=============================================
*/
IF OBJECT_ID('SensitiveWords', 'U') IS NOT NULL
DROP TABLE SensitiveWords
GO

CREATE TABLE SensitiveWords
(
    Id INT IDENTITY(1,1) PRIMARY KEY,

    Word NVARCHAR(100) NOT NULL,

    CreatedAt DATETIME2 NOT NULL 
        CONSTRAINT DF_SensitiveWords_CreatedAt 
        DEFAULT GETUTCDATE()
)
GO


/*
=============================================
 Prevent Duplicate Words
=============================================
*/
CREATE UNIQUE INDEX UX_SensitiveWords_Word
ON SensitiveWords (Word)
GO