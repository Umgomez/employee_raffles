SELECT TOP 1
	a.ID
	, a.[Sequence] 
	, a.Amount 
	, a.IsSelected 
FROM [dbo].[Awards] a 
WHERE a.IsSelected = 0 ORDER BY a.ID 