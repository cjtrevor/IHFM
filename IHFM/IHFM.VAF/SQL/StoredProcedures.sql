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