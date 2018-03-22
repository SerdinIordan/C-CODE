CREATE TABLE [dbo].[Customer] (
    [CustomerID]      INT          IDENTITY (1, 1) NOT NULL,
    [Name]            NCHAR (10)   NULL,
    [DiscountPercent] DECIMAL (18) NULL,
    [CustomerImage] VARBINARY(MAX) NULL, 
    CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED ([CustomerID] ASC)
);

