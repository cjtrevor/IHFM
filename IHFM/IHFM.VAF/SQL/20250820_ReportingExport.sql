GO

EXEC sp_rename 'adl.DeletedTBCItems.ObjectId', 'TBCItemObjectId', 'COLUMN';

GO

ALTER TABLE adl.DeletedTBCItems ADD DailyCareObjectId INT

GO

ALTER TABLE adl.DeletedTBCItems ADD DailyCareCreatedDate DATETIME

GO

ALTER PROCEDURE [dbo].[Insert_DeletedTBCItems]
@TBCItemObjectId INT,
@DailyCareObjectId INT,
@DailyCareCreatedDate DATETIME,
@ResidentId INT,
@TimeSlot VARCHAR(20),
@DeletedDate DATETIME
AS

INSERT INTO adl.DeletedTBCItems (TBCItemObjectId, DailyCareObjectId, DailyCareCreatedDate, ResidentId, TimeSlot, DeletedDate)
VALUES
(@TBCItemObjectId, @DailyCareObjectId, @DailyCareCreatedDate, @ResidentId	, @TimeSlot, @DeletedDate)

GO
