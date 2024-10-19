INSERT INTO Claims (LecturerID, HoursWorked, HourlyRate, Status, Comments)
VALUES ('1', 10, 100, 'Pending', 'Awaiting approval'),
       ('1', 20, 150, 'Approved', 'Claim approved'),
       ('1', 5, 80, 'Rejected', 'Incorrect hours claimed');

ALTER TABLE Claims
ADD Comments NVARCHAR(255);

SELECT * FROM Claims;