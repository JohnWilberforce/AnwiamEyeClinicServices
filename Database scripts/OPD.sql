USE [AnwiamServices]
GO

/****** Object:  Table [dbo].[OPD]    Script Date: 12/12/2023 12:03:33 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[OPD](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PatientId] [varchar](30) NULL,
	[PatientName] [varchar](100) NULL,
	[Address] [varchar](100) NULL,
	[Contact] [varchar](15) NULL,
	[Services] [varchar](120) NULL,
	[Amount] [decimal](18, 0) NOT NULL,
	[Date] [date] NULL,
	[Status] [varchar](15) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

