GO

CREATE TABLE Patients (
    Id INT PRIMARY KEY IDENTITY,
    FirstName NVARCHAR(100),
    LastName NVARCHAR(100),
    DateOfBirth DATE,
    Gender NVARCHAR(10),
    Phone NVARCHAR(18),
    Address NVARCHAR(255),
	Age INT NULL
);

CREATE TABLE Doctors (
    Id INT PRIMARY KEY IDENTITY,
    FirstName NVARCHAR(100),
    LastName NVARCHAR(100),
    Specialty NVARCHAR(100),
    Phone NVARCHAR(18)
);

CREATE TABLE Appointments (
    Id INT PRIMARY KEY IDENTITY,
    PatientId INT FOREIGN KEY REFERENCES Patients(Id),
    DoctorId INT FOREIGN KEY REFERENCES Doctors(Id),
    AppointmentDate DATETIME,
    Reason NVARCHAR(255)
);

CREATE TABLE MedicalRecords (
    Id INT PRIMARY KEY IDENTITY,
    PatientId INT FOREIGN KEY REFERENCES Patients(Id),
    DoctorId INT FOREIGN KEY REFERENCES Doctors(Id),
    Diagnosis NVARCHAR(255),
    Treatment NVARCHAR(255),
    RecordDate DATETIME
);

GO
INSERT INTO Patients (FirstName, LastName, DateOfBirth, Gender, Phone, Address) VALUES
('Ali', 'Khan', '1985-06-15', 'Male', '03001234567', '123 Main Street, Lahore'),
('Sara', 'Ahmed', '1990-09-21', 'Female', '03111234567', '456 Garden Town, Karachi'),
('Usman', 'Raza', '1978-03-05', 'Male', '03211234567', '789 DHA Phase 5, Islamabad');


GO
INSERT INTO Doctors (FirstName, LastName, Specialty, Phone) VALUES
('Dr. Ayesha', 'Siddiqui', 'Cardiologist', '03451234567'),
('Dr. Imran', 'Malik', 'Dermatologist', '03551234567'),
('Dr. Nadia', 'Hassan', 'Pediatrician', '03661234567');


--GO
--TRUNCATE TABLE Appointments
--GO
INSERT INTO Appointments (PatientId, DoctorId, AppointmentDate, Reason) VALUES
(1, 1, '2025-06-04 10:00:00', 'Chest pain and shortness of breath'),
(2, 2, '2025-06-18 11:30:00', 'Skin rash and irritation'),
(4, 4, '2025-06-13 09:00:00', 'Routine child check-up'),
(1, 3, '2025-06-11 09:00:00', 'Routine child check-up'),
(4, 2, '2025-06-08 09:00:00', 'Routine child check-up')



GO
INSERT INTO MedicalRecords (PatientId, DoctorId, Diagnosis, Treatment, RecordDate) VALUES
(1, 2, 'Angina', 'Prescribed beta-blockers and advised ECG', '2025-06-01 11:00:00'),
(2, 1, 'Eczema', 'Topical steroid cream prescribed', '2025-06-02 12:00:00'),
(3, 4, 'Healthy', 'Topical steroid cream prescribed', '2025-06-03 10:00:00');

GO


ALTER TABLE [Doctors]
ALTER COLUMN [Phone] VARCHAR(18) NOT NULL;

ALTER TABLE [Patients]
ALTER COLUMN [Phone] VARCHAR(18) NOT NULL;
