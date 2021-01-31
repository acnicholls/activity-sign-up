update Activity
set
	ActivityName = 'Surfing in Tofino, BC',
	ActivityDescription = 'Let''s hit the waves together and catch a sunset by the beachside fire, while the crickets chirp.',
	ActivityDateTime = DATEFROMPARTS(2022, 06,15)
where
	Activity.ActivityId=57;

update Activity
set
	ActivityName = 'Rock Climbing at Grand River Rocks',
	ActivityDescription = 'Need a workout?  Look no further!',
	ActivityDateTime = DATEFROMPARTS(2022, 08, 22)
where
	Activity.ActivityId=55;

update Activity
set
	ActivityName = 'Nature Hike at Huron Conservation',
	ActivityDescription = 'Birdwatch while getting some exercise and network those connections!',
	ActivityDateTime = DATEFROMPARTS(2022, 07, 16)
where
	Activity.ActivityId=56;

update Activity
set
	ActivityName = 'Kayaking in the Grand',
	ActivityDescription = 'Here''s your chance to get out of the house!  Enjoy a few hours of sunshine, fresh air and exercise!  Join us on the Grand for some kayaking fun!',
	ActivityDateTime = DATEFROMPARTS(2022, 05, 13)
where
	Activity.ActivityId=54;

update Activity
set
	ActivityName = 'Dancing in the Rain',
	ActivityDescription = 'Rain is optional.  Bring your instruments and we''ll make a jam.  (Peanut butter optional also!)',
	ActivityDateTime = DATEFROMPARTS(2022, 06, 12)
where
	Activity.ActivityId=58;

select * from Activity;