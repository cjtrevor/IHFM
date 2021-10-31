CREATE TABLE [dbo].[WardStockExport](
	[Identifier] [int] IDENTITY(1,1) NOT NULL,
	[ObjectID] [int] NOT NULL,
	[SiteID] [int] NOT NULL,
	[SiteName] [varchar](50) NOT NULL,
	[Direction] [varchar](5) NOT NULL,
	[Month] [varchar](15) NOT NULL,
	[Year] [int] NOT NULL,
	[CostPrice] [decimal](10, 2) NULL,
	[SellingPrice] [decimal](10, 2) NULL
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[TimeBasedCareExport](
	[Identifier] [int] IDENTITY(1,1) NOT NULL,
	[ObjectID] [int] NOT NULL,
	[SiteId] [int] NOT NULL,
	[SiteName] [varchar](50) NOT NULL,
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