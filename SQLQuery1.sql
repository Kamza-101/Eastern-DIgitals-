-- 1. Users Table (Handles Login credentials)
CREATE TABLE Users (
    UserID INT PRIMARY KEY IDENTITY(1,1),
    Email NVARCHAR(100) UNIQUE NOT NULL,
    Password NVARCHAR(100) NOT NULL,
    UserRole NVARCHAR(20) NOT NULL, -- 'Student', 'Provider', or 'Admin'
    Status NVARCHAR(20) DEFAULT 'Active'
);

-- 2. ServiceSeekers Table (Student Profiles)
CREATE TABLE ServiceSeekers (
    StudentID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT FOREIGN KEY REFERENCES Users(UserID),
    FullName NVARCHAR(100),
    ContactNumber NVARCHAR(10),
    University NVARCHAR(100),
    City NVARCHAR(50)
);

-- 3. ServiceProviders Table (Provider Profiles)
CREATE TABLE ServiceProviders (
    ProviderID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT FOREIGN KEY REFERENCES Users(UserID),
    FirstName NVARCHAR(50),
    Surname NVARCHAR(50),
    ContactNumber NVARCHAR(10),
    IDNumber NVARCHAR(13),
    Location NVARCHAR(50),
    ServiceType NVARCHAR(50),
    Bio NVARCHAR(500),
    Skills NVARCHAR(200)
);

-- 4. Services Table (The Marketplace)
CREATE TABLE Services (
    ServiceID INT PRIMARY KEY IDENTITY(1,1),
    ProviderID INT FOREIGN KEY REFERENCES ServiceProviders(ProviderID),
    ServiceName NVARCHAR(100),
    Description NVARCHAR(250),
    Category NVARCHAR(50),
    Price DECIMAL(10,2),
    Icon NVARCHAR(10),
    Tag NVARCHAR(50)
);

-- 5. Bookings Table (Transactional Data)
CREATE TABLE Bookings (
    BookingID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT NOT NULL,
    ProviderID INT NOT NULL,
    ServiceID INT NOT NULL,
    ServiceName NVARCHAR(100),
    Price DECIMAL(10,2),
    PaymentMethod NVARCHAR(50),
    Status NVARCHAR(20) DEFAULT 'Pending',
    BookingDate DATETIME DEFAULT GETDATE(),
    CONSTRAINT FK_Bookings_Users FOREIGN KEY (UserID) REFERENCES Users(UserID),
    CONSTRAINT FK_Bookings_Providers FOREIGN KEY (ProviderID) REFERENCES ServiceProviders(ProviderID),
    CONSTRAINT FK_Bookings_Services FOREIGN KEY (ServiceID) REFERENCES Services(ServiceID)
);

-- 6. Cart Table (Checkout Prep)
CREATE TABLE Cart (
    CartID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT FOREIGN KEY REFERENCES Users(UserID),
    ServiceID INT FOREIGN KEY REFERENCES Services(ServiceID),
    AddedDate DATETIME DEFAULT GETDATE()
);

-- 7. AuditLogs Table (Admin/Logs)
CREATE TABLE AuditLogs (
    LogID INT PRIMARY KEY IDENTITY(1,1),
    UserName NVARCHAR(100),
    ActionDescription NVARCHAR(255),
    LogTime DATETIME DEFAULT GETDATE()
);