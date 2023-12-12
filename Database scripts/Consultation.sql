USE [AnwiamServices]
GO

/****** Object:  Table [dbo].[Consultation]    Script Date: 12/12/2023 11:56:59 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Consultation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PatientId] [varchar](30) NULL,
	[PatientName] [varchar](100) NULL,
	[VisualAcuity] [varchar](20) NULL,
	[ChiefComplaint] [varchar](100) NULL,
	[PatientHistory] [varchar](500) NULL,
	[FamilyHistory] [varchar](500) NULL,
	[Diagnosis] [varchar](100) NULL,
	[Medications] [varchar](500) NULL,
	[SpectacleRx] [varchar](200) NULL,
	[Date] [date] NOT NULL,
	[Eyelids] [varchar](200) NULL,
	[Cornea] [varchar](200) NULL,
	[Pupils] [varchar](200) NULL,
	[Media] [varchar](200) NULL,
	[Lens] [varchar](200) NULL,
	[OpticNerve] [varchar](200) NULL,
	[Fundus] [varchar](200) NULL,
	[Note] [varchar](200) NULL,
	[Conjunctiva] [varchar](200) NULL
) ON [PRIMARY]
GO

