CREATE TABLE [dbo].[Order] (
    [OrderID]         INT          IDENTITY (1, 1) NOT NULL,
    [CustomerID]      INT          NULL,
    [OrderDate]       DATETIME         NULL,
    [DiscountPercent] DECIMAL (18) NULL,
    CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED ([OrderID] ASC)
);

