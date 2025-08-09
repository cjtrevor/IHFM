
CREATE SCHEMA adl;

GO

CREATE TABLE adl.DeletedTBCItems(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    ObjectId INT,
    TimeSlot VARCHAR(20),
    DeletedDate DATETIME
);

CREATE TABLE adl.Map_TBCItem_MFObject(
	TbcItemId INT,
	MFObjectId INT
);

GO

CREATE PROCEDURE [dbo].[Insert_DeletedTBCItems]
@ObjectID INT,
@TimeSlot VARCHAR(20),
@DeletedDate DATETIME
AS

INSERT INTO adl.DeletedTBCItems (ObjectId, TimeSlot, DeletedDate)
VALUES
(@ObjectID, @TimeSlot, @DeletedDate)

GO