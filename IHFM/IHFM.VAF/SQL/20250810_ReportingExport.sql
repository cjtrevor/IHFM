
ALTER TABLE adl.DeletedTBCItems ADD ResidentId INT

GO

ALTER PROCEDURE [dbo].[Insert_DeletedTBCItems]
@ObjectId INT,
@ResidentId INT,
@TimeSlot VARCHAR(20),
@DeletedDate DATETIME
AS

INSERT INTO adl.DeletedTBCItems (ObjectId, ResidentId, TimeSlot, DeletedDate)
VALUES
(@ObjectId, @ResidentId, @TimeSlot, @DeletedDate)

GO