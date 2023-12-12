USE [AnwiamServices]
GO

/****** Object:  Table [dbo].[RefractionPx]    Script Date: 12/12/2023 12:04:55 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[RefractionPx](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[Telephone] [varchar](15) NULL,
	[SpectacleRx] [varchar](100) NULL
) ON [PRIMARY]
GO

