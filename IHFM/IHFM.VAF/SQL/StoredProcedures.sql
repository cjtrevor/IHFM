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