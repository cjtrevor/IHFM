CREATE PROCEDURE [dbo].[sp_ExportTBCRecord]
@ObjectID int,
@SiteID int,
@SiteName varchar(50),
@Month varchar(15),
@Year int,
@Cost decimal(10,2),
@TBCType varchar(5)
AS

insert into TimeBasedCareExport (ObjectID,SiteId,SiteName,[Month],[Year],Cost,TBCType)
VALUES
(@ObjectID,@SiteID,@SiteName,@Month,@Year,@Cost,@TBCType)

CREATE PROCEDURE [dbo].[sp_ExportWardStockRecord]
@ObjectID int,
@SiteId int,
@SiteName varchar(50),
@Direction varchar(5),
@Month varchar(15),
@Year int,
@CostPrice decimal(10,2),
@SellingPrice decimal (10,2)
AS

INSERT INTO [dbo].[WardStockExport]
           ([ObjectID]
           ,[SiteID]
		   ,SiteName
		   ,[Direction]
           ,[Month]
           ,[Year]
           ,[CostPrice]
           ,[SellingPrice])
     VALUES
           (@ObjectID,
		   @SiteId,
		   @SiteName,
		   @Direction,
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