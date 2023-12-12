USE [AnwiamServices]
GO

/****** Object:  UserDefinedFunction [dbo].[ufn_totals]    Script Date: 12/12/2023 12:07:56 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE function [dbo].[ufn_totals](@st date, @end date)
returns table 
as
return
select COUNT(V.scanId) as VftCount, COUNT(O.ONH) as OnhCount, COUNT(O.Macula) as MaculaCount, COUNT(O.pachymetry)as PachymetryCount,
cast((COUNT(V.scanId)*150) as decimal) AS VftTotalAmount, cast((COUNT(O.ONH)*250)+ (COUNT(O.Macula)*100)+(COUNT(O.pachymetry)*100) as decimal) 
AS TotalOfOnhMacPachyAmount
 from VFT V full outer join OCT O on V.scanId=O.scanId
 WHERE ((V.date >= @st AND V.date <= @end) 
 OR 
 (O.date >= @st AND O.date <= @end))
GO

