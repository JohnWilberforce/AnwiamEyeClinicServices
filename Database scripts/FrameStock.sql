USE [AnwiamServices]
GO

/****** Object:  Table [dbo].[FrameStock]    Script Date: 12/12/2023 11:59:21 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[FrameStock](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FrameTypeByColor] [varchar](10) NULL,
	[Price] [decimal](18, 0) NULL,
	[Quantity] [int] NULL,
	[date] [datetime] NULL
) ON [PRIMARY]
GO

