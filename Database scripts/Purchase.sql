USE [AnwiamServices]
GO

/****** Object:  Table [dbo].[Purchase]    Script Date: 12/12/2023 12:04:33 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Purchase](
	[PurchaseId] [int] IDENTITY(1,1) NOT NULL,
	[PatientId] [int] NULL,
	[PatientName] [varchar](50) NOT NULL,
	[FrameType] [varchar](10) NULL,
	[FramePrice] [decimal](18, 0) NULL,
	[LensType] [varchar](50) NULL,
	[LensPrice] [decimal](18, 0) NULL,
	[TotalPrice] [decimal](18, 0) NULL,
	[AmountPaid] [decimal](18, 0) NULL,
	[date] [datetime] NULL,
	[Quantity] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[PurchaseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

