USE [AnwiamServices]
GO

/****** Object:  Table [dbo].[Pharmacy]    Script Date: 12/12/2023 12:04:06 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Pharmacy](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PatientId] [varchar](15) NULL,
	[PatientName] [varchar](100) NULL,
	[Diagnosis] [varchar](400) NULL,
	[Medications] [varchar](500) NULL,
	[Date] [date] NULL,
	[Status] [varchar](30) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

