WITH LatestOrderPayments AS (
    SELECT
        OrderId,
        JSON_VALUE(Data, '$.TotalAmount') AS TotalAmount,
        JSON_VALUE(Data, '$.Currency') AS Currency,
        ROW_NUMBER() OVER (PARTITION BY OrderId ORDER BY CreatedDate DESC) AS RowNum
    FROM
        [dbo].[OrderTransactions]
)
SELECT
    OrderId,
    TotalAmount,
    Currency
FROM
    LatestOrderPayments
WHERE
    RowNum = 1;
