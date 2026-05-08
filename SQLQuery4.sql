-- ==========================================
-- COMPETITOR 1: Tutoring (Alice)
-- ==========================================
INSERT INTO Users (Email, Password, UserRole, Status) 
VALUES ('zanele.tutor@easterndigital.co.za', 'Pass123', 'Provider', 'Active');

DECLARE @UID4 INT = SCOPE_IDENTITY();

INSERT INTO ServiceProviders (UserID, FirstName, Surname, ContactNumber, Location, ServiceType)
VALUES (@UID4, 'Zanele', 'Mthembu', '0781112222', 'Alice', 'Tutoring Services');

DECLARE @PID4 INT = SCOPE_IDENTITY();

INSERT INTO Services (ProviderID, ServiceName, Description, Category, Price, Icon, Tag)
VALUES 
(@PID4, 'C# & SQL Exam Crash Course', 'Intensive past-paper revision for Information Systems students.', 'Tutoring', 130.00, N'💻', 'High Demand'),
(@PID4, 'Statistics 101 Assignment Help', 'Guided help to ensure you get 80%+ on your stats assignments.', 'Tutoring', 110.00, N'📊', 'Top Rated');


-- ==========================================
-- COMPETITOR 2: Printing (Gqeberha)
-- ==========================================
INSERT INTO Users (Email, Password, UserRole, Status) 
VALUES ('david.print@easterndigital.co.za', 'Pass123', 'Provider', 'Active');

DECLARE @UID5 INT = SCOPE_IDENTITY();

INSERT INTO ServiceProviders (UserID, FirstName, Surname, ContactNumber, Location, ServiceType)
VALUES (@UID5, 'David', 'Naidoo', '0812223333', 'Gqeberha', 'Printing Services');

DECLARE @PID5 INT = SCOPE_IDENTITY();

INSERT INTO Services (ProviderID, ServiceName, Description, Category, Price, Icon, Tag)
VALUES 
(@PID5, 'Overnight Res Printing Delivery', 'Send me your PDFs at 2AM, get them printed by 8AM.', 'Printing', 4.00, N'🖨️', 'Fast'),
(@PID5, 'Bulk Study Guide Printing', 'Discounted black and white printing for 50+ pages.', 'Printing', 1.20, N'🖨️', 'Affordable');


-- ==========================================
-- COMPETITOR 3: Graphic Design (Mthatha)
-- ==========================================
INSERT INTO Users (Email, Password, UserRole, Status) 
VALUES ('amahle.designs@easterndigital.co.za', 'Pass123', 'Provider', 'Active');

DECLARE @UID6 INT = SCOPE_IDENTITY();

INSERT INTO ServiceProviders (UserID, FirstName, Surname, ContactNumber, Location, ServiceType)
VALUES (@UID6, 'Amahle', 'Dlamini', '0733334444', 'Mthatha', 'Graphic Design Services');

DECLARE @PID6 INT = SCOPE_IDENTITY();

INSERT INTO Services (ProviderID, ServiceName, Description, Category, Price, Icon, Tag)
VALUES 
(@PID6, 'Premium Startup Logos', 'High-quality vector logos for your student business.', 'Graphic Design', 350.00, N'🎨', 'Premium'),
(@PID6, 'Club & Society Banners', 'Digital banners and flyers for campus societies.', 'Graphic Design', 150.00, N'🖼️', 'Creative');


-- ==========================================
-- COMPETITOR 4: Device Repair (East London)
-- ==========================================
INSERT INTO Users (Email, Password, UserRole, Status) 
VALUES ('jason.repairs@easterndigital.co.za', 'Pass123', 'Provider', 'Active');

DECLARE @UID7 INT = SCOPE_IDENTITY();

INSERT INTO ServiceProviders (UserID, FirstName, Surname, ContactNumber, Location, ServiceType)
VALUES (@UID7, 'Jason', 'Smith', '0624445555', 'KuGompo/East London', 'Device Repair Services');

DECLARE @PID7 INT = SCOPE_IDENTITY();

INSERT INTO Services (ProviderID, ServiceName, Description, Category, Price, Icon, Tag)
VALUES 
(@PID7, 'iPhone Screen & Battery Repair', 'Screen and battery replacements done on campus.', 'Device Repair Services', 600.00, N'📱', 'Same Day'),
(@PID7, 'MacBook Deep Clean & Service', 'Dust removal and thermal paste replacement to fix overheating.', 'Device Repair Services', 300.00, N'💻', 'Maintenance');