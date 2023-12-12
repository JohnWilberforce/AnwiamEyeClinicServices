USE [AnwiamServices]
GO

/****** Object:  Table [dbo].[OCT]    Script Date: 12/12/2023 11:59:46 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[OCT](
	[scanId] [varchar](30) NOT NULL,
	[patientName] [varchar](100) NULL,
	[referredDrName] [varchar](100) NULL,
	[reFfacility] [varchar](100) NULL,
	[ONH] [varchar](3) NULL,
	[Macula] [varchar](3) NULL,
	[pachymetry] [varchar](3) NULL,
	[date] [date] NULL
) ON [PRIMARY]
GO

