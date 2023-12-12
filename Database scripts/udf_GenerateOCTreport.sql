USE [AnwiamServices]
GO

/****** Object:  UserDefinedFunction [dbo].[udf_GenerateOCTreport]    Script Date: 12/12/2023 12:07:04 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE function [dbo].[udf_GenerateOCTreport](@st date,@end date)
returns table
as
return
select referredDrName,count(ONH) as ONHcount, COUNT(Macula) as MaculaCount, 
count(pachymetry) as PachCount, cast(((count(ONH)*25)+(count(Macula)*10) + (count(pachymetry)*10)) as decimal) as Amount
from OCT 
WHERE date >= @st
  AND date <= @end
group by referredDrName
GO

