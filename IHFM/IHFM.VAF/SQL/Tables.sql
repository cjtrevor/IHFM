CREATE TABLE [dbo].[WardStockExport](
	[Identifier] [int] IDENTITY(1,1) NOT NULL,
	[ObjectID] [int] NOT NULL,
	[SiteID] [int] NOT NULL,
	[SiteName] [varchar](50) NOT NULL,
	[ResidentID] [int], [NOT NULL],
	[Direction] [varchar](5) NOT NULL,
	[TransactionDate] [smalldatetime] [NULL],
	[Month] [varchar](15) NOT NULL,
	[Year] [int] NOT NULL,
	[CostPrice] [decimal](10, 2) NULL,
	[SellingPrice] [decimal](10, 2) NULL
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[TimeBasedCareExport](
	[Identifier] [int] IDENTITY(1,1) NOT NULL,
	[ObjectID] [int] NOT NULL,
	[ResidentID] [int], [NOT NULL],
	[SiteId] [int] NOT NULL,
	[SiteName] [varchar](50) NOT NULL,
	[TransactionDate] [smalldatetime] [NULL],
	[Month] [varchar](15) NOT NULL,
	[Year] [int] NOT NULL,
	[Cost] [decimal](10, 2) NOT NULL,
	[TBCType] [varchar](5) NOT NULL,
 CONSTRAINT [PK_TimeBasedCareExport] PRIMARY KEY CLUSTERED 
(
	[Identifier] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[VitalsRecordExport](
	[Identifier] [int] IDENTITY(1,1) NOT NULL,
	[Shift] [varchar](15) NOT NULL,
	[ObjectID] [int] NOT NULL,
	[SiteID] [int] NOT NULL,
	[SiteName] [varchar](50) NOT NULL,
	[Resident] [varchar](70) NOT NULL,
	[DateTaken] [smalldatetime] NOT NULL,
	[Temperature] [decimal](6, 2) NULL,
	[SystolicBP] [int] NULL,
	[DiastolicBP] [int] NULL,
	[HeartRate] [int] NULL,
	[Weight] [decimal](6, 2) NULL,
	[HGT] [decimal](10, 2) NULL,
	[Saturation] [int] NULL,
	[HB] [decimal](10, 2) NULL,
	[Monthly] [bit] NULL,
 CONSTRAINT [PK_VitalsRecordExport] PRIMARY KEY CLUSTERED 
(
	[Identifier] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[ScriptControlExport](
	[Identifier] [int] IDENTITY(1,1) NOT NULL,
	[ObjectID] [int] NOT NULL,
	[SiteID] [int] NOT NULL,
	[SiteName] [varchar](50) NOT NULL,
	[ResidentID] [int] NOT NULL,
	[Resident] [varchar](70) NOT NULL,
	[Validity] [int] NOT NULL,
	[StartDate] [smalldatetime] NOT NULL,
	[EndDate] [smalldatetime] NOT NULL,
	[Provider] [varchar](50) NOT NULL,
 CONSTRAINT [PK_ScriptControlExport] PRIMARY KEY CLUSTERED 
(
	[Identifier] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[MedsOnScriptExport](
	[Identifier] [int] IDENTITY(1,1) NOT NULL,
	[ScriptControlID] [int] NOT NULL,
	[ObjectID] [int] NOT NULL,
	[MedsName] [varchar](50) NOT NULL,
	[Dosage] [varchar](50) NULL,
	[Quantity] [decimal](10, 3) NULL,
	[Give6AM] [bit] NOT NULL,
	[Give9AM] [bit] NOT NULL,
	[Give12PM] [bit] NOT NULL,
	[Give5PM] [bit] NOT NULL,
	[Give8PM] [bit] NOT NULL,
	[GiveEveryday] [bit] NULL,
	[GiveMonday] [bit] NOT NULL,
	[GiveTuesday] [bit] NOT NULL,
	[GiveWednesday] [bit] NOT NULL,
	[GiveThursday] [bit] NOT NULL,
	[GiveFriday] [bit] NOT NULL,
	[GiveSaturday] [bit] NOT NULL,
	[GiveSunday] [bit] NOT NULL,
	[SpecificDayOfMonth] [int] NULL,
	[Cycle4Hours] [bit] NULL,
	[CycleStartTime] [varchar](20) NULL
 CONSTRAINT [PK_MedsOnScriptExport] PRIMARY KEY CLUSTERED 
(
	[Identifier] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[MedsGivenExport](
	[Identifier] [int] IDENTITY(1,1) NOT NULL,
	[MedsOnScriptID] [int] NOT NULL,
	[Shift] [varchar](15) NOT NULL,
	[SiteID] [int] NOT NULL,
	[SiteName] [varchar](50) NOT NULL,
	[ResidentID] [int] NOT NULL,
	[Resident] [varchar](50) NOT NULL,
	[ObjectID] [int] NOT NULL,
	[MedsTaken] [varchar](1) NOT NULL,
	[Timeslot] [int] NOT NULL,
	[MedsType] [varchar](3) NOT NULL,
 CONSTRAINT [PK_MedsGivenExport] PRIMARY KEY CLUSTERED 
(
	[Identifier] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[ExportLastRun](
	[SiteId] [int] [NOT NULL],
	[SageExportLastRun] [smalldatetime] NOT NULL
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[QMRSiteDetailExport](
	[Identifier] [int] IDENTITY(1,1) NOT NULL,
	[SiteID] [int] NOT NULL,
	[SiteName] [varchar](50) NOT NULL,
	[SiteCareManager] [varchar](50) NOT NULL,
	[QuarterNumber] [int] NOT NULL,
	[YearNumber] [int] NOT NULL,
	[HealthStatusIndependant] [int] NOT NULL,
	[HealthStatusAssisted] [int] NOT NULL,
	[HealthStatusDependant] [int] NOT NULL,
	[MentallyFrail] [int] NOT NULL,
	[PhysicallyFrail] [int] NOT NULL,
	[PartiallyFrail] [int] NOT NULL,
	[TotallyFrail] [int] NOT NULL,
	[Diabetics] [int] NOT NULL,
	[InfectiousDisease] [int] NOT NULL,
	[PressureSores] [int] NOT NULL,
	[WheelchairCases] [int] NOT NULL,
 CONSTRAINT [PK_QMRSiteDetailExport] PRIMARY KEY CLUSTERED 
(
	[Identifier] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[QMRAdmissionsExport](
	[Identifier] [int] IDENTITY(1,1) NOT NULL,
	[ObjID] [int] NOT NULL,
	[SiteID] [int] NOT NULL,
	[SiteName] [varchar](50) NOT NULL,
	[QuarterNumber] [int] NOT NULL,
	[YearNumber] [int] NOT NULL,
	[ResidentName] [varchar](50) NOT NULL,
	[Age] [int] NOT NULL,
	[Sex] [varchar](15) NOT NULL,
	[DateAdmitted] [varchar](20) NOT NULL,
	[MedicalConditions] [varchar](max) NOT NULL,
 CONSTRAINT [PK_QMRAdmissionsExport] PRIMARY KEY CLUSTERED 
(
	[Identifier] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[QMRDeathsExport](
	[Identifier] [int] IDENTITY(1,1) NOT NULL,
	[ObjID] [int] NOT NULL,
	[SiteID] [int] NOT NULL,
	[SiteName] [varchar](50) NOT NULL,
	[QuarterNumber] [int] NOT NULL,
	[YearNumber] [int] NOT NULL,
	[ResidentName] [varchar](50) NOT NULL,
	[Age] [int] NOT NULL,
	[Sex] [varchar](15) NOT NULL,
	[DateOfDeath] [varchar](20) NOT NULL,
	[MedicalConditions] [varchar](max) NOT NULL,
 CONSTRAINT [PK_QMRDeathsExport] PRIMARY KEY CLUSTERED 
(
	[Identifier] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[QMRIncidentsExport](
	[Identifier] [int] IDENTITY(1,1) NOT NULL,
	[ObjID] [int] NOT NULL,
	[SiteID] [int] NOT NULL,
	[SiteName] [varchar](50) NOT NULL,
	[QuarterNumber] [int] NOT NULL,
	[YearNumber] [int] NOT NULL,
	[ResidentName] [varchar](50) NOT NULL,
	[DateOfIncident] [varchar](20) NOT NULL,
	[TimeOfIncident] [varchar](20) NOT NULL,
	[Benzo] [char](1) NOT NULL,
	[Cause] [varchar](max) NOT NULL,
	[Injury] [varchar](max) NOT NULL,
	[Treatment] [varchar](max) NOT NULL
 CONSTRAINT [PK_QMRIncidentsExport] PRIMARY KEY CLUSTERED 
(
	[Identifier] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[IncidentExport](
	[Identifier] [int] IDENTITY(1,1) NOT NULL,
	[IncidentID] [int] NOT NULL,
	[Incident] [varchar](50) NOT NULL,
	[Shift] [varchar](15) NOT NULL,
	[SiteID] [int] NOT NULL,
	[SiteName] [varchar](50) NOT NULL,
	[ResidentID] [int] NOT NULL,
	[Resident] [varchar](50) NOT NULL,
	[IncidentDate] [smalldatetime] NOT NULL,
	[Month] [varchar](15) NOT NULL,
	[Year] [int] NOT NULL,
 CONSTRAINT [PK_IncidentExport] PRIMARY KEY CLUSTERED 
(
	[Identifier] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
