CREATE TABLE [dbo].[OrderDetail] (
    [OrderDetailID]             INT          IDENTITY (1, 1) NOT NULL,
    [ProductID]                 INT          NULL,
    [Price]                     DECIMAL (18) NULL,
    [Quantity]                  DECIMAL (18) NULL,
    [TotalPriceWithoutDiscount] DECIMAL (18) NULL,
    [TotalPrice]                DECIMAL (18) NULL,
    [OrderID] INT NULL, 
    CONSTRAINT [PK_OrderDetail] PRIMARY KEY CLUSTERED ([OrderDetailID] ASC),
    FOREIGN KEY ([ProductID]) REFERENCES [dbo].[Product] ([ProductID])
);

