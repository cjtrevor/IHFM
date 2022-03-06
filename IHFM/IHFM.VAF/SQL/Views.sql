CREATE View [dbo].[vw_TimeBasedCareSummary]
as
select SiteId,SiteName, [Month],[Year], 
	SUM(case when TBCType = 'ADL' then Cost else 0 end) as ADL,
	SUM(case when TBCType = 'CLNC' then Cost else 0 end) as CLNC,
	SUM(Cost) as Total
from TimeBasedCareExport
group by SiteId,SiteName,[Month],[Year]
GO

Create view [dbo].[vw_WardStockSummary]
AS
select SiteID,SiteName, [Month],[Year],
	SUM(case when Direction = 'IN' then CostPrice else 0 end) as TransIn,
	SUM(case when Direction = 'OUT' then SellingPrice else 0 end) as TransOut
from WardStockExport
group by SiteID,SiteName,[Month],[Year]
GO

Create view vw_VitalsRecordsSummary
as
select [Shift], SiteName, Resident, DateTaken, Temperature, SystolicBP, DiastolicBP, HeartRate, Weight,
		HGT,Saturation,HB, Monthly
from VitalsRecordExport
GO