﻿CREATE View [dbo].[vw_TimeBasedCareSummary]
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

create view vw_Resident_MedsOnScript
as
select sce.SiteName, sce.Resident, mos.MedsName, mos.Give6AM, mos.Give9AM, mos.Give12PM, mos.Give5PM, mos.Give8PM
from MedsOnScriptExport mos
	join ScriptControlExport sce 
		on mos.ScriptControlID = sce.ObjectID and EndDate > GETDATE()
GO

create view vw_TranspharmStockSold
as
select SiteName, Month,Year,LastMonth,LastYear, SUM(SellingPrice) as SellingPrice from
(
select SiteName, Month,Year, SellingPrice, DATENAME(month,DATEADD(month,-1,GETDATE())) as lastMonth, YEAR(DATEADD(month,-1,GETDATE())) as LastYear
from WardStockExport 
where Direction = 'OUT' and ISNULL(SiteName,'') <> ''
) a
Group by SiteName, Month,Year,LastMonth,LastYear

create view vw_TimeBasedCareBilled
as
select SiteName,Month,Year, SUM(case when TBCType = 'CLNC' then Cost else 0 end) as ClinicCost,
	SUM(case when TBCType = 'ADL' then Cost else 0 end) as AdlCost, SUM(Cost) as Total
from
(
select SiteName, Month,Year, TBCType, SUM(Cost) as Cost 
	from TimeBasedCareExport
where isnull(SiteName,'') <> ''
group by SiteName,Month,Year,TBCType
) a
group by SiteName,Month,Year
GO

create view vw_IncidentSummary
as
select SiteName, Shift, Month, Year, Count(*) as qty 
	from IncidentExport
where SiteID <> '999'
group by SiteName, Shift, Month, Year