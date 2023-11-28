--DECLARE @VarIdentificationNumber varchar(100) = :IdentificationNumber--'40222349967'
SELECT e.* FROM dbo.Employees e WHERE e.Cedula = :IdentificationNumber