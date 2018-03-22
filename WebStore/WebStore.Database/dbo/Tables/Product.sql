CREATE TABLE [dbo].[Product] (
    [ProductID] INT          IDENTITY (1, 1) NOT NULL,
    [Name]      NCHAR (10)   NULL,
    [Price]     DECIMAL (18) NULL,
    [ProductImage] VARBINARY(MAX) NULL, 
    CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED ([ProductID] ASC)
);

