USE [AnwiamServices]
GO

/****** Object:  UserDefinedFunction [dbo].[ufn_FetchRefractionsBtnDates]    Script Date: 12/12/2023 12:07:37 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [dbo].[ufn_FetchRefractionsBtnDates]
(
	@date1 Date,
	@date2 Date
)
RETURNS TABLE
as
return
select R.Name,R.Telephone,COALESCE(R.SpectacleRx,'')AS SpectacleRx, COALESCE(P.FrameType,'') AS FrameType,COALESCE(P.FramePrice,'') AS FramePrice,
COALESCE(P.LensType,'') AS LensType,COALESCE(P.LensPrice,'') AS LensPrice,P.TotalPrice,
COALESCE(P.AmountPaid,'') AS AmountPaid,P.date ,COALESCE(P.Quantity,'') AS Quantity from RefractionPx R inner join Purchase P on R.Id=P.PatientId where 
date >= @date1 and  date<=@date2;
GO

