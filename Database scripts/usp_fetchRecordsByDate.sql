USE [AnwiamServices]
GO

/****** Object:  StoredProcedure [dbo].[usp_fetchRecordsByDate]    Script Date: 12/12/2023 12:05:54 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[usp_fetchRecordsByDate](@st date,@end date)
as
begin
if(@st >@end)
return null
return select * from VFT where [date] between @st and @end

end
GO

