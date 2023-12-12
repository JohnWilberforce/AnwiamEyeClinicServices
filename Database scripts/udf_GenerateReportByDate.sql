USE [AnwiamServices]
GO

/****** Object:  UserDefinedFunction [dbo].[udf_GenerateReportByDate]    Script Date: 12/12/2023 12:07:21 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE function [dbo].[udf_GenerateReportByDate](@st date,@end date)
returns table
as
return

SELECT referredDrName, COUNT(*) AS TotalVFTReferrals,cast((COUNT(*)*15) as decimal) AS Amount
FROM VFT
WHERE date >= @st
  AND date <= @end
GROUP BY referredDrName;
GO

