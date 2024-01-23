ALTER TABLE UserProfile ADD StatusId int DEFAULT 1;

CREATE TABLE UserStatus (
	Id int,
	Name varChar(255)
);

INSERT INTO UserStatus (Id, Name)
VALUES (1, 'Active');
INSERT INTO UserStatus (Id, Name)
VALUES (2, 'Deactivated');

UPDATE UserProfile
SET StatusId = 1;

SELECT * FROM UserStatus;
SELECT * FROM UserProfile;