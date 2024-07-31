# Internal Program Tasks
## General Behaviour
- [ ] One Way Synchronization
	- [ ] CLA: Define Source Folder
	- [ ] CLA: Define Destination Folder
	- [ ] CLA: Define Synchronization Interval in seconds
	- [ ] CLA: Log File Path
	- [ ] After Synchronization: The Destination Folder has the exact same structure as the source folder

## CLA Specifics
- [ ] Source Folder and Destination Folder must be specified
- [ ] Sync Interval is optional, a default value is provided otherwise
- [ ] Log File Path is optional, a default value is provided otherwise

- [ ] If Sync interfal and Log Fiile are left empty, the program asks, if the default values should be used. A confirmation -y starts execution.


> [!NOTE] Nice to have
> - [ ] -h can be called to get information about usable CLAs
>- [ ] -xml can be used to load a config set entered into the xml
>	- [ ] How to explain that to the user?
>	- [ ] Save config as xml during program execution or per CLA at the start?
>	- [ ] Nice to have

## Synchronization Specifics
- [ ] Changing 
	- [ ] A files...
		- [ ] Content
		- [ ] Location
	- [ ] A path
	- [ ] ...
in the source folder is carried to the destination folder.

- [ ] Changings in the destination folder made by the user are thrown away during a sync process if not the user made the exact same changes also to the source folder.

### Performance -  Dozes of Thousands
- [ ] Instead of iterating through each folder and file in the first place, better use a checksum technique to find out if sth. has changed

## Logging
- [ ] The operations
	- [ ] File Creation
	- [ ] File Copy
	- [ ] File Removal
are logged into 
- [ ] A separate File
- [ ] The console Output

### Log Output - You never know
We don't know, what the customer might wish in the future as a log output (might be a database or the LEDs of its fridge...)
- [ ] Log against an interface
- [ ] Provide optional ~~CLAs~~ or an XML to configure, which log outputs get used 
	- [ ] XML might be the better option due to login credentials for a database connection or sth.

# External Soft Tasks
- [x] Create Obsidian Vault
- [ ] Create C# Solution
- [ ] Create Git Structure 
	- [ ] Include Obsidian Vault
	- [ ] Include C# Solution
- [ ] Mark Git Structure as public