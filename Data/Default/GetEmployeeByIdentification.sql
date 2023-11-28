SELECT e.* FROM dbo.Employees e WHERE e.Asistencia = 1 and e.Cedula = :IdentificationNumber

--SELECT a.ID
--	, a.[Sequence]
--	, a.Amount
--	, a.IsSelected
--FROM [dbo].[Awards] a 
--OUTER apply (SELECT MAX(Sequence) Total FROM [dbo].[Awards] b WHERE b.amount = a.Amount) Total
--OUTER apply (SELECT MIN(sequence) Actual FROM [SCHAD].[dbo].[Awards] b WHERE b.amount = a.amount and isselected = 0) Actual