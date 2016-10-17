SELECT   s.ProductId
	   , COUNT(*) as FirstPurchaseTimes
FROM dbo.Sales s
		INNER JOIN (SELECT CustomerId, MIN(DateCreated) as FirstDate
					FROM dbo.Sales
					GROUP BY CustomerId) s1
			ON     s.CustomerId = s1.CustomerId
			   and s.DateCreated = s1.FirstDate
GROUP BY s.ProductId