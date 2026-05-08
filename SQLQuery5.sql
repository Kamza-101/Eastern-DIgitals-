-- 1. Delete the old table
DROP TABLE Bookings;

-- 2. Create the new, updated table
CREATE TABLE Bookings (
    BookingID INT PRIMARY KEY IDENTITY(1,1),
    OrderReference NVARCHAR(20) NOT NULL,  
    UserID INT NOT NULL,                   
    ServiceID INT NOT NULL,                
    BookingDate DATETIME DEFAULT GETDATE(),
    PaymentMethod NVARCHAR(50),            
    Notes NVARCHAR(MAX),                   
    TotalCost DECIMAL(18,2) NOT NULL,      
    Status NVARCHAR(50) DEFAULT 'Pending Confirmation'
);