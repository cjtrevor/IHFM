create proc sp_GetOutstandingMedsGivenReport
@SiteID int
as

declare @StartDate smalldatetime = GETDATE() - 7
declare @ShiftStart varchar(8) = CONCAT(FORMAT(datepart(day,@StartDate),'00'), FORMAT(datepart(month,@StartDate),'00'), RIGHT(datepart(year,@StartDate),2))

create table #Shifts(
ShiftNo varchar(8)
)

Insert into #Shifts
select CONCAT(FORMAT(datepart(day,@StartDate),'00'), FORMAT(datepart(month,@StartDate),'00'), RIGHT(datepart(year,@StartDate),2))
union 
select CONCAT(FORMAT(datepart(day,@StartDate + 1),'00'), FORMAT(datepart(month,@StartDate + 1),'00'), RIGHT(datepart(year,@StartDate + 1),2))
union 
select CONCAT(FORMAT(datepart(day,@StartDate + 2),'00'), FORMAT(datepart(month,@StartDate + 2),'00'), RIGHT(datepart(year,@StartDate + 2),2))
union 
select CONCAT(FORMAT(datepart(day,@StartDate + 3),'00'), FORMAT(datepart(month,@StartDate + 3),'00'), RIGHT(datepart(year,@StartDate + 3),2))
union 
select CONCAT(FORMAT(datepart(day,@StartDate + 4),'00'), FORMAT(datepart(month,@StartDate + 4),'00'), RIGHT(datepart(year,@StartDate + 4),2))
union 
select CONCAT(FORMAT(datepart(day,@StartDate + 5),'00'), FORMAT(datepart(month,@StartDate + 5),'00'), RIGHT(datepart(year,@StartDate + 5),2))
union 
select CONCAT(FORMAT(datepart(day,@StartDate + 6),'00'), FORMAT(datepart(month,@StartDate + 6),'00'), RIGHT(datepart(year,@StartDate + 6),2))
union 
select CONCAT(FORMAT(datepart(day,@StartDate + 7),'00'), FORMAT(datepart(month,@StartDate + 7),'00'), RIGHT(datepart(year,@StartDate + 7),2))

--Temp
select * into #AllMedsOnScript from
(
select sce.SiteID, sce.ResidentID, sce.Resident,mos.ObjectID as MedsOnScriptID, mos.MedsName, 6 as Timeslot
from MedsOnScriptExport  mos
	join ScriptControlExport sce on mos.ScriptControlID = sce.ObjectID
where EndDate >= GETDATE() and sce.SiteID = @SiteID and GiveEveryday = 1 and Give6AM = 1
union
select sce.SiteID, sce.ResidentID, sce.Resident,mos.ObjectID as MedsOnScriptID, mos.MedsName, 9 as Timeslot
from MedsOnScriptExport  mos
	join ScriptControlExport sce on mos.ScriptControlID = sce.ObjectID
where EndDate >= GETDATE() and sce.SiteID = @SiteID and GiveEveryday = 1 and Give9AM = 1
union
select sce.SiteID, sce.ResidentID, sce.Resident,mos.ObjectID as MedsOnScriptID, mos.MedsName, 12 as Timeslot
from MedsOnScriptExport  mos
	join ScriptControlExport sce on mos.ScriptControlID = sce.ObjectID
where EndDate >= GETDATE() and sce.SiteID = @SiteID and GiveEveryday = 1 and Give12PM = 1
union
select sce.SiteID, sce.ResidentID, sce.Resident,mos.ObjectID as MedsOnScriptID, mos.MedsName, 17 as Timeslot
from MedsOnScriptExport  mos
	join ScriptControlExport sce on mos.ScriptControlID = sce.ObjectID
where EndDate >= GETDATE() and sce.SiteID = @SiteID and GiveEveryday = 1 and Give5PM = 1
union
select sce.SiteID, sce.ResidentID, sce.Resident,mos.ObjectID as MedsOnScriptID, mos.MedsName, 20 as Timeslot
from MedsOnScriptExport  mos
	join ScriptControlExport sce on mos.ScriptControlID = sce.ObjectID
where EndDate >= GETDATE() and sce.SiteID = @SiteID and GiveEveryday = 1 and Give8PM = 1) as amos
cross join #Shifts

--Meds For everyday
select amos.*, mge.Identifier, CASE WHEN ISNULL(mge.Identifier,'') = '' THEN 'Not Given'
						 WHEN mge.MedsTaken = 'R' THEN 'Resident Refused' END as Stat
	from #AllMedsOnScript amos
	 left join MedsGivenExport mge 
		on amos.SiteID = mge.SiteID
			and amos.ResidentID = mge.ResidentID
			and amos.MedsOnScriptID = mge.MedsOnScriptID
			and amos.Timeslot = mge.Timeslot
			and amos.ShiftNo = left(mge.Shift,6)
	where mge.Identifier is null or mge.MedsTaken = 'R'



drop table #AllMedsOnScript
drop table #Shifts




create proc sp_ExportScriptControl
@ObjectID int,
@SiteID int,
@SiteName varchar(50),
@ResidentID int,
@Resident varchar(70),
@Validity int,
@StartDate smalldatetime,
@EndDate smalldatetime,
@Provider varchar(50)
as

insert into ScriptControlExport (ObjectID, SiteID, SiteName, ResidentID, Resident, Validity, StartDate, EndDate, [Provider])
values
(@ObjectID, @SiteID, @SiteName, @ResidentID, @Resident, @Validity, @StartDate, @EndDate, @Provider)

create proc [dbo].[sp_ExportMedsOnScript]
@ScriptControlID int,
@ObjectID int,
@MedsName varchar(50),
@Dosage varchar(50),
@Quantity decimal(10,3),
@Give6AM bit,
@Give9AM bit,
@Give12PM bit,
@Give5PM bit,
@Give8PM bit,
@GiveEveryday bit,
@GiveMonday bit,
@GiveTuesday bit,
@GiveWednesday bit,
@GiveThursday bit,
@GiveFriday bit,
@GiveSaturday bit,
@GiveSunday bit,
@SpecificDayOfMonth int,
@Cycle4Hours bit,
@CycleStartTime varchar(20)

as

if not exists (select 1 from MedsOnScriptExport where ScriptControlID = @ScriptControlID and ObjectID = @ObjectID)
begin
	insert into MedsOnScriptExport (ScriptControlID, ObjectID, MedsName,Dosage,Quantity, Give6AM, Give9AM, Give12PM, Give5PM, Give8PM, GiveEveryday,
		GiveMonday, GiveTuesday,GiveWednesday,GiveThursday,GiveFriday,GiveSaturday,GiveSunday, SpecificDayOfMonth,Cycle4Hours,CycleStartTime)
	values
	(@ScriptControlID, @ObjectID, @MedsName,@Dosage,@Quantity, @Give6AM, @Give9AM, @Give12PM, @Give5PM, @Give8PM, @GiveEveryday, @GiveMonday, @GiveTuesday,
	@GiveWednesday, @GiveThursday, @GiveFriday, @GiveSaturday, @GiveSunday,@SpecificDayOfMonth,@Cycle4Hours,@CycleStartTime)
end
else
begin
	update MedsOnScriptExport
		set MedsName = @MedsName,
		Dosage = @Dosage,
		Quantity = @Quantity,
		Give6AM = @Give6AM,
		Give9AM = @Give9AM,
		Give12PM = @Give12PM,
		Give5PM = @Give5PM,
		Give8PM = @Give8PM,
		GiveEveryday = @GiveEveryday,
		GiveMonday = @GiveMonday,
		GiveTuesday = @GiveTuesday,
		GiveWednesday = @GiveWednesday,
		GiveThursday = @GiveThursday,
		GiveFriday = @GiveFriday,
		GiveSaturday = @GiveSaturday,
		GiveSunday = @GiveSunday,
		SpecificDayOfMonth = @SpecificDayOfMonth,
		Cycle4Hours = @Cycle4Hours,
		CycleStartTime = @CycleStartTime
	where ScriptControlID = @ScriptControlID and ObjectID = @ObjectID
end
GO

create proc sp_ExportMedsGiven
@MedsOnScriptID int,
@Shift varchar(15),
@SiteID int,
@SiteName varchar(50),
@ResidentID int,
@Resident varchar(50),
@ObjectID int,
@MedsTaken varchar(1),
@Timeslot int,
@MedsType varchar(3)
as

if not exists (select 1 from MedsGivenExport where MedsOnScriptID = @MedsOnScriptID and ObjectID = @ObjectID)
begin
	insert into MedsGivenExport (MedsOnScriptID, Shift, SiteID, SiteName, ResidentID, Resident, ObjectID, MedsTaken, Timeslot, MedsType)
	values
	(@MedsOnScriptID,@Shift, @SiteID, @SiteName, @ResidentID, @Resident, @ObjectID, @MedsTaken, @Timeslot, @MedsType)
end


CREATE PROCEDURE [dbo].[sp_ExportTBCRecord]
@ObjectID int,
@SiteID int,
@SiteName varchar(50),
@ResidentID int,
@TransactionDate smalldatetime,
@Month varchar(15),
@Year int,
@Cost decimal(10,2),
@TBCType varchar(5)
AS

insert into TimeBasedCareExport (ObjectID,SiteId,SiteName,ResidentID, TransactionDate,[Month],[Year],Cost,TBCType)
VALUES
(@ObjectID,@SiteID,@SiteName,@ResidentID, @TransactionDate,@Month,@Year,@Cost,@TBCType)

CREATE PROCEDURE [dbo].[sp_ExportWardStockRecord]
@ObjectID int,
@SiteId int,
@SiteName varchar(50),
@ResidentID int,
@Direction varchar(5),
@TransactionDate smalldatetime,
@Month varchar(15),
@Year int,
@CostPrice decimal(10,2),
@SellingPrice decimal (10,2)
AS

INSERT INTO [dbo].[WardStockExport]
           ([ObjectID]
           ,[SiteID]
		   ,SiteName
           ,ResidentID
		   ,[Direction]
           ,TransactionDate,
           ,[Month]
           ,[Year]
           ,[CostPrice]
           ,[SellingPrice])
     VALUES
           (@ObjectID,
		   @SiteId,
		   @SiteName,
           @ResidentID,
		   @Direction,
           @TransactionDate,
		   @Month,
		   @Year,
		   @CostPrice,
		   @SellingPrice)

create procedure [dbo].[sp_ExportVitalsRecord]
@Shift varchar(15),
@ObjectID int,
@SiteID	int,
@SiteName varchar(50),
@Resident varchar(70),
@DateTaken smalldatetime,
@Temperature decimal(6,2),
@SystolicBP int,
@DiastolicBP int,
@HeartRate int,
@Weight decimal(6,2),
@HGT decimal (10,2),
@Saturation int,
@HB decimal (10,2),
@Monthly bit

as

INSERT INTO [dbo].[VitalsRecordExport]
           ([Shift]
           ,[ObjectID]
           ,[SiteID]
           ,[SiteName]
           ,[Resident]
           ,[DateTaken]
           ,[Temperature]
           ,[SystolicBP]
           ,[DiastolicBP]
           ,[HeartRate]
           ,[Weight]
           ,[HGT]
           ,[Saturation]
           ,[HB],
		   [Monthly])
     VALUES
	 (
	 @Shift
    ,@ObjectID
    ,@SiteID
    ,@SiteName
    ,@Resident
    ,@DateTaken
    ,@Temperature
    ,@SystolicBP
    ,@DiastolicBP
    ,@HeartRate
    ,@Weight
    ,@HGT
    ,@Saturation
    ,@HB,
	@Monthly
	)
GO

create procedure sp_SetSageExportLastRun
@SiteId int,
@LastRun smalldatetime

as
update ExportLastRun set SageExportLastRun = @LastRun where SiteId = @SiteId
GO

create procedure sp_GetSageExportLastRun
@SiteId int

as
select SageExportLastRun from ExportLastRun where SiteId = @SiteId

Create proc sp_GetTimeBasedCareRecordsForResidentPeriod
@ResidentID int,
@StartDate smalldatetime,
@EndDate smalldatetime,
@Type varchar(4)
as
select ISNULL(SUM(Cost), 0) as Cost 
	from TimeBasedCareExport 
where ResidentID = @ResidentID
	and TBCType = @Type
	and TransactionDate between @StartDate and @EndDate

Create proc sp_GetWardStockRecordsForResidentPeriod
@ResidentID int,
@StartDate smalldatetime,
@EndDate smalldatetime
as
select ISNULL(SUM(SellingPrice), 0) as Cost
	from WardStockExport 
where ResidentID = @ResidentID
	and TransactionDate between @StartDate and @EndDate

create proc sp_ExportQMRAdmission
@ObjID int,
@SiteID int,
@SiteName varchar(50),
@QuarterNumber int,
@YearNumber int,
@ResidentName varchar(50),
@Age int,
@Sex varchar(20),
@DateAdmitted varchar(20),
@MedicalConditions varchar(max)
as

INSERT INTO [dbo].[QMRAdmissionsExport]
           ([ObjID]
           ,[SiteID]
           ,[SiteName]
           ,[QuarterNumber]
           ,[YearNumber]
           ,[ResidentName]
           ,[Age]
           ,[Sex]
           ,[DateAdmitted]
           ,[MedicalConditions])
     VALUES
           (@ObjID
           ,@SiteID
           ,@SiteName
           ,@QuarterNumber
           ,@YearNumber
           ,@ResidentName
           ,@Age
           ,@Sex
           ,@DateAdmitted
           ,@MedicalConditions)

create proc sp_ExportQMRDeath
@ObjID int,
@SiteID int,
@SiteName varchar(50),
@QuarterNumber int,
@YearNumber int,
@ResidentName varchar(50),
@Age int,
@Sex varchar(20),
@DateOfDeath varchar(20),
@MedicalConditions varchar(max)
as

INSERT INTO [dbo].[QMRDeathsExport]
           ([ObjID]
           ,[SiteID]
           ,[SiteName]
           ,[QuarterNumber]
           ,[YearNumber]
           ,[ResidentName]
           ,[Age]
           ,[Sex]
           ,[DateOfDeath]
           ,[MedicalConditions])
     VALUES
           (@ObjID
           ,@SiteID
           ,@SiteName
           ,@QuarterNumber
           ,@YearNumber
           ,@ResidentName
           ,@Age
           ,@Sex
           ,@DateOfDeath
           ,@MedicalConditions)

create proc sp_ExportQMRIncident
@ObjID int,
@SiteID int,
@SiteName varchar(50),
@QuarterNumber int,
@YearNumber int,
@ResidentName varchar(50),
@DateOfIncident varchar(20),
@TimeOfIncident varchar(20),
@Benzo char(1),
@Cause varchar(max),
@Injury varchar(20),
@Treatment varchar(20)
as

INSERT INTO [dbo].[QMRIncidentsExport]
           ([ObjID]
           ,[SiteID]
           ,[SiteName]
           ,[QuarterNumber]
           ,[YearNumber]
           ,[ResidentName]
           ,[DateOfIncident]
           ,[TimeOfIncident]
           ,[Benzo]
           ,[Cause]
           ,[Injury]
           ,[Treatment])
     VALUES
           (@ObjID
           ,@SiteID
           ,@SiteName
           ,@QuarterNumber
           ,@YearNumber
           ,@ResidentName
           ,@DateOfIncident
           ,@TimeOfIncident
           ,@Benzo
           ,@Cause
           ,@Injury
           ,@Treatment)

create proc sp_ExportQMRSiteDetail
@SiteID int,
@SiteName varchar(50),
@SiteCareManager varchar(50),
@QuarterNumber int,
@YearNumber int,
@HealthStatusIndependant int,
@HealthStatusAssisted int,
@HealthStatusDependant int,
@MentallyFrail int,
@PhysicallyFrail int,
@PartiallyFrail int,
@TotallyFrail int,
@Diabetics int,
@InfectiousDisease int,
@PressureSores int,
@WheelchairCases int
as

INSERT INTO [dbo].[QMRSiteDetailExport]
           ([SiteID]
           ,[SiteName]
           ,[SiteCareManager]
           ,[QuarterNumber]
           ,[YearNumber]
           ,[HealthStatusIndependant]
           ,[HealthStatusAssisted]
           ,[HealthStatusDependant]
           ,[MentallyFrail]
           ,[PhysicallyFrail]
           ,[PartiallyFrail]
           ,[TotallyFrail]
           ,[Diabetics]
           ,[InfectiousDisease]
           ,[PressureSores]
           ,[WheelchairCases])
     VALUES
           (@SiteID
           ,@SiteName
           ,@SiteCareManager
           ,@QuarterNumber
           ,@YearNumber
           ,@HealthStatusIndependant
           ,@HealthStatusAssisted
           ,@HealthStatusDependant
           ,@MentallyFrail
           ,@PhysicallyFrail
           ,@PartiallyFrail
           ,@TotallyFrail
           ,@Diabetics
           ,@InfectiousDisease
           ,@PressureSores
           ,@WheelchairCases)
