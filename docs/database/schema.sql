-- =============================================
-- AI Exam Cheating Risk Detector
-- Database Schema
-- =============================================

-- 1. Users Table
CREATE TABLE Users (
    Id          INT PRIMARY KEY IDENTITY(1,1),
    FullName    NVARCHAR(100)  NOT NULL,
    Email       NVARCHAR(150)  NOT NULL UNIQUE,
    Password    NVARCHAR(255)  NOT NULL,
    Role        NVARCHAR(20)   NOT NULL CHECK (Role IN ('Student', 'Admin')),
    CreatedAt   DATETIME       NOT NULL DEFAULT GETDATE()
);

-- 2. Exams Table
CREATE TABLE Exams (
    Id          INT PRIMARY KEY IDENTITY(1,1),
    StudentId   INT            NOT NULL FOREIGN KEY REFERENCES Users(Id),
    StartTime   DATETIME       NOT NULL DEFAULT GETDATE(),
    EndTime     DATETIME       NULL,
    Status      NVARCHAR(20)   NOT NULL DEFAULT 'Active' CHECK (Status IN ('Active', 'Completed'))
);

-- 3. ActivityLogs Table
CREATE TABLE ActivityLogs (
    Id          INT PRIMARY KEY IDENTITY(1,1),
    ExamId      INT            NOT NULL FOREIGN KEY REFERENCES Exams(Id),
    EventType   NVARCHAR(50)   NOT NULL,
    EventDetail NVARCHAR(255)  NULL,
    LoggedAt    DATETIME       NOT NULL DEFAULT GETDATE()
);

-- 4. RiskScores Table
CREATE TABLE RiskScores (
    Id          INT PRIMARY KEY IDENTITY(1,1),
    ExamId      INT            NOT NULL UNIQUE FOREIGN KEY REFERENCES Exams(Id),
    Score       DECIMAL(5,2)   NOT NULL DEFAULT 0,
    UpdatedAt   DATETIME       NOT NULL DEFAULT GETDATE()
);

-- 5. Alerts Table
CREATE TABLE Alerts (
    Id              INT PRIMARY KEY IDENTITY(1,1),
    ExamId          INT            NOT NULL FOREIGN KEY REFERENCES Exams(Id),
    Message         NVARCHAR(255)  NOT NULL,
    Severity        NVARCHAR(20)   NOT NULL CHECK (Severity IN ('Standard', 'High')),
    IsAcknowledged  BIT            NOT NULL DEFAULT 0,
    CreatedAt       DATETIME       NOT NULL DEFAULT GETDATE()
);

-- =============================================
-- Seed Data — Sample Users
-- =============================================

INSERT INTO Users (FullName, Email, Password, Role) VALUES
('John Student', 'student@exam.com', 'hashed_password_here', 'Student'),
('Admin User',   'admin@exam.com',   'hashed_password_here', 'Admin');

