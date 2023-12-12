USE [AnwiamServices]
GO

/****** Object:  UserDefinedFunction [dbo].[udf_fetchRecordsByDate]    Script Date: 12/12/2023 12:06:47 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


create function [dbo].[udf_fetchRecordsByDate](@st date,@end date)
returns table
as
return
select * from VFT where [date] between @st and @end
GO

