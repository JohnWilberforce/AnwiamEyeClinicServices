USE [AnwiamServices]
GO

/****** Object:  Table [dbo].[VFT]    Script Date: 12/12/2023 12:05:16 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[VFT](
	[scanId] [varchar](30) NOT NULL,
	[patientName] [varchar](100) NULL,
	[referredDrName] [varchar](100) NULL,
	[reFfacility] [varchar](100) NULL,
	[date] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[scanId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

