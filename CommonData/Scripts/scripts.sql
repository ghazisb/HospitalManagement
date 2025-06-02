GO

CREATE TABLE Patients (
    Id INT PRIMARY KEY IDENTITY,
    FirstName NVARCHAR(100),
    LastName NVARCHAR(100),
    DateOfBirth DATE,
    Gender NVARCHAR(10),
    Phone NVARCHAR(15),
    Address NVARCHAR(255),
	Age INT NULL
);

CREATE TABLE Doctors (
    Id INT PRIMARY KEY IDENTITY,
    FirstName NVARCHAR(100),
    LastName NVARCHAR(100),
    Specialty NVARCHAR(100),
    Phone NVARCHAR(15)
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


GO
INSERT INTO Appointments (PatientId, DoctorId, AppointmentDate, Reason) VALUES
(1, 1, '2025-06-01 10:00:00', 'Chest pain and shortness of breath'),
(2, 2, '2025-06-02 11:30:00', 'Skin rash and irritation'),
(3, 3, '2025-06-03 09:00:00', 'Routine child check-up');


GO
INSERT INTO MedicalRecords (PatientId, Diagnosis, Treatment, RecordDate) VALUES
(1, 'Angina', 'Prescribed beta-blockers and advised ECG', '2025-06-01 11:00:00'),
(2, 'Eczema', 'Topical steroid cream prescribed', '2025-06-02 12:00:00'),
(3, 'Healthy', 'Vaccination completed', '2025-06-03 10:00:00');


GO

ALTER TABLE dbo.Patients ADD Age INT NULL;