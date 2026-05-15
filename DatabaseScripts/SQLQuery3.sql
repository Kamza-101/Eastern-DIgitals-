-- 1. Create a dummy User account for the Provider
INSERT INTO Users (Email, Password, UserRole, Status) 
VALUES ('sipho.provider@easterndigital.co.za', 'Pass123', 'Provider', 'Active');

-- Grab the ID of the user we just created
DECLARE @NewUserID INT = SCOPE_IDENTITY();

-- 2. Create the Provider Profile linked to that User
INSERT INTO ServiceProviders (UserID, FirstName, Surname, ContactNumber, Location, ServiceType)
VALUES (@NewUserID, 'Sipho', 'Jobe', '0712345678', 'KuGompo/East London', 'Multiple Services');

-- Grab the ID of the provider we just created
DECLARE @NewProvID INT = SCOPE_IDENTITY();

-- 3. Create 3 test Services linked to Sipho
INSERT INTO Services (ProviderID, ServiceName, Description, Category, Price, Icon, Tag)
VALUES 
(@NewProvID, 'C# Programming Tutor', '1-on-1 ASP.NET and SQL database tutoring.', 'Tutoring', 150.00, '💻', 'Top Rated'),
(@NewProvID, 'Campus Printing Delivery', 'A4 Color printing delivered to your res.', 'Printing', 3.50, '🖨️', 'Fast'),
(@NewProvID, 'Student Business Logos', 'Custom graphic design for your side hustle.', 'Graphic Design', 250.00, '🎨', 'Creative');

-- ==========================================
-- PROVIDER 1: Device Repair (Gqeberha)
-- ==========================================
INSERT INTO Users (Email, Password, UserRole, Status) 
VALUES ('thabo.repairs@easterndigital.co.za', 'Pass123', 'Provider', 'Active');

DECLARE @UID1 INT = SCOPE_IDENTITY();

INSERT INTO ServiceProviders (UserID, FirstName, Surname, ContactNumber, Location, ServiceType)
VALUES (@UID1, 'Thabo', 'Mothubi', '0823456789', 'Gqeberha', 'Device Repair Services');

DECLARE @PID1 INT = SCOPE_IDENTITY();

INSERT INTO Services (ProviderID, ServiceName, Description, Category, Price, Icon, Tag)
VALUES 
(@PID1, 'Phone Screen Repair', 'Fast screen replacements for most smartphones.', 'Device Repair Services', 550.00, '📱', 'Same Day'),
(@PID1, 'Laptop Cleaning & Thermal Paste', 'Prevent overheating and speed up your laptop.', 'Device Repair Services', 250.00, '💻', 'Maintenance');

-- ==========================================
-- PROVIDER 2: Graphic Design & Printing (Alice)
-- ==========================================
INSERT INTO Users (Email, Password, UserRole, Status) 
VALUES ('dineo.designs@easterndigital.co.za', 'Pass123', 'Provider', 'Active');

DECLARE @UID2 INT = SCOPE_IDENTITY();

INSERT INTO ServiceProviders (UserID, FirstName, Surname, ContactNumber, Location, ServiceType)
VALUES (@UID2, 'Dineo', 'Phuthi', '0734567890', 'Alice', 'Graphic Design Services');

DECLARE @PID2 INT = SCOPE_IDENTITY();

INSERT INTO Services (ProviderID, ServiceName, Description, Category, Price, Icon, Tag)
VALUES 
(@PID2, 'Custom Event Flyers', 'Eye-catching posters for your campus events.', 'Graphic Design', 180.00, '🖼️', 'Creative'),
(@PID2, 'Bulk Assignment Printing', 'Black and white or full-color assignment printing.', 'Printing', 1.50, '🖨️', 'Affordable');

-- ==========================================
-- PROVIDER 3: Tutoring (Mthatha)
-- ==========================================
INSERT INTO Users (Email, Password, UserRole, Status) 
VALUES ('lerato.tutor@easterndigital.co.za', 'Pass123', 'Provider', 'Active');

DECLARE @UID3 INT = SCOPE_IDENTITY();

INSERT INTO ServiceProviders (UserID, FirstName, Surname, ContactNumber, Location, ServiceType)
VALUES (@UID3, 'Lerato', 'Ngomane', '0612345678', 'Mthatha', 'Tutoring Services');

DECLARE @PID3 INT = SCOPE_IDENTITY();

INSERT INTO Services (ProviderID, ServiceName, Description, Category, Price, Icon, Tag)
VALUES 
(@PID3, 'Statistics 101 Tutoring', 'Ace your first-year stats and probability modules.', 'Tutoring', 120.00, '📊', 'Exam Prep'),
(@PID3, 'Commercial Law Tutoring', 'Detailed help with contract and commercial law.', 'Tutoring', 140.00, '⚖️', 'In-Depth');