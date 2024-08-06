-  Log against an interface
	-  log4net is exactly what you desire.
-  Each Logger must be usable in parallel
	-  log4net does this by default. Assign two appenders to the same logger and its done.
- The Loggers must be triggerable regarding their state of activeness
	- This would be part of further developent, since you incorporate another logging mechanism. And then you can create further loggers according to a configuration (might use a factory then...)

# Listed
## Low Priority
- [ ] Rework Logging Layouts

# Currently Doing

# Done
## Logger Setup
- [x] Add log4net package
- [x] Setup a logger with two appenders (FileAppender and ConsoleAppender)
- [x] Setup an appender Layout
	- [x] Specify an appender Layout for File Creation / Move / Copy / Remove
	- [x] Specify an appender Layout for Error Messages
- [x] Setup a simple factory, taking a simple config and  returning  the logger
	- [x] factory setup
	- [x] config pass
- [x] Setup a wrapper solution to encapsulate the logs
- [x] Setup a Console Only Logger which is responsible for Init Messages during CLA input

## Log File and Directory Results
- [x] Log each Sync Perform for
	- [x] File Creation
	- [x] File Modification 
		- [x] Delete 
		- [x] Copy
	- [x] File Deletion
- [x] Log each Sync Perform Error 
	- [x] Path Too Long
	- [x] Access Denied
	- [x] Path not found
	- [x] ...

# Is Factory a good pattern for getting loggers?
Think about the Logger Factory.
Problem: Two classes need lets say a ConsoleLogger. Okay. 
Now consider that they need different ConsoleLoggers (e.g. filtering different LogLevels).
So. Now we have to configure them in XML anyway. Wouldn't those be related to two different factories? But isn't it redundant to create a bunch of factory classes for each different logger configuration?

So should one factory now choose between different loggers of the same general style?
(I - the factory - take a config A and return ConsoleLoggerA and take config B and return ConsoleLoggerB)
or is a name enough? (But why then even use a factory instead of a simple wrapper class which takes the class name and returns the respective logger?)
or just use the wrapper classes and let them get the loggers directly?
- [ ] Verify or Falsify that factory is the fitting pattern for getting Loggers
- [ ] Come up with a nice solution for that problem

It has a lower prio for now because we want to make progress with the core of Synchra.

> [!NOTE] Nice to have 
> We don't know, what the customer might wish in the future as a log output 
>(might be a database or the LEDs of its fridge...)
> - [ ] Provide optional XML to configure, which log outputs get used 
	>	- [ ] XML might a nice option due to login credentials for a database connection or sth.