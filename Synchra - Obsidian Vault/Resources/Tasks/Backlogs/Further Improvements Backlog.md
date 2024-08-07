-  Improve Logger Layouts
	- Use Colored Console Logger instead of Console Logger
	- Show only relative Paths or even only file names and directory names
- Improve business logic
	- If Files are and A and also in B but in another location, then actually MOVE the File and Dir
	- File Moving would improve the logs, 
		- as they are confusing atm cause of the "Delete x, y, z" and "Create x, y, z" in the same log cycle
		- as they are waaaay to big cause of the issue above
- Improve Code quality
	- Define a logger strategy which is more clear
		- How or even if to use the logger wrapper classes
		- How or even if to use the logger factories
	- Unify the Methods in State Modifier
		- Create a delegate, call the specific method, and try catch only once over the invoke
		- To keep the logs clear, define string variables for the current action (which might fail) 
		- Don't log only the exception messages but state more clearer to the user what happened and if the user was the problem or not
	- Define Test cases which are necessary for the future but momentarily missing
		- Exception throws of State Modifier
		- Out of Sync Recursively
		- CLA Validation
	- Improve the CLA Validation for paths
	- Improve the usability
		- If a given src, dest or log path as CLA doesn't exist, ASK THE USER if the system should create it or wants to re-enter the respective path
		- Provide a -h argument to give help to the user
		- Instead of Thread.Sleep in Main, wait for an input to interrupt the Task Scheduling and adjust parameters like the synchronization period or further options
- Far picture ideas
	- Allow the user to sync to multiple targets (is that really necessary? But a customer might want to have that)
	- Allow the user to sync from multiple sources (e.g. as a consolidation process)
	- Make sync over the internet possible (keep a local file system in sync with a cloud system / a ftp server / ...)
	- Incorporate different file systems
		- Is that even possible
		- What would it mean to sync between different systems (e.g. you cannot store a file greater than 4GB on a FAT32 System but only on NTFS)
		- In which cases are different file systems used?
	- Provide a graphical user interface to the customer
	- AND
	- AND
	- AND 
	- ...