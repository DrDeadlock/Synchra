-  Log against an interface
	-  log4net is exactly what you desire.
-  Each Logger must be usable in parallel
	-  log4net does this by default. Assign two appenders to the same logger and its done.
- The Loggers must be triggerable regarding their state of activeness
	- This would be part of further developent, since you incorporate another logging mechanism. And then you can create further loggers according to a configuration (might use a factory then...)

- [ ] Add log4net package
- [ ] Setup a logger with two appenders (FileAppender and ConsoleAppender)
- [ ] Setup an appender Layout
	- [ ] Specify an appender Layout for File Creation / Move / Copy / Remove
	- [ ] Specify an appender Layout for Error Messages
- [ ] Setup a simple factory, taking a simple config and  returning  the logger
- [ ] Setup a Console Only Logger which is responsible for Init Messages during CLA input

> [!NOTE] Nice to have 
> We don't know, what the customer might wish in the future as a log output 
>(might be a database or the LEDs of its fridge...)
> - [ ] Provide optional XML to configure, which log outputs get used 
	>	- [ ] XML might a nice option due to login credentials for a database connection or sth.