-- 1. Change the column to support Unicode (emojis)
ALTER TABLE Services ALTER COLUMN Icon NVARCHAR(50);

-- 2. Update the dummy data. The "N" before the quotes is the magic trick that saves emojis!
UPDATE Services SET Icon = N'💻' WHERE Category = 'Tutoring Services' OR Category = 'Tutoring';
UPDATE Services SET Icon = N'🖨️' WHERE Category = 'Printing Services' OR Category = 'Printing';
UPDATE Services SET Icon = N'🎨' WHERE Category = 'Graphic Design Services' OR Category = 'Graphic Design';
UPDATE Services SET Icon = N'📱' WHERE Category = 'Device Repair Services' OR Category = 'Device Repair';

-- Update any stragglers based on their tags or names just in case
UPDATE Services SET Icon = N'📊' WHERE ServiceName LIKE '%Stats%';
UPDATE Services SET Icon = N'🧹' WHERE ServiceName LIKE '%Cleaning%';