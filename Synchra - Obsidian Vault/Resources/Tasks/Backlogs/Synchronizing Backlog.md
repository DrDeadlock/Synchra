## Synchronization Specifics

- [x] Synchra is able to detect if Src File and Dest File are different
- [x] Synchra is able to detect the difference between two files without having to iterate through each line of the file

- [x] Synchra only makes changes to Dest
- [x] Synchra is able to Create Files which are in Src and are not in Dest
- [x] Synchra is able to Copy Update Files which are as well in Src and Dest but differ regarding their content (Compare by Checksum)
- [x] Synchra is able to Delete Files which are in Dest and are not in Src

# Exception Discussion
When can Exceptions be thrown and what does it mean in our context? Also how we wanna handle them?

## File

## Directory
### Get All Files
[Find more Information here](https://learn.microsoft.com/de-de/dotnet/api/system.io.directory.getfiles?view=net-8.0)
#### I/O Exception: 
Network Error or a Filename was entered.
--> Raise that exception. It is an error on our side. Our collections passed into methods using GetAllFiles() HAVE to be paths which are not files.
- [x] Create Testcase

#### UnauthorizedException:
Permission denied for the File.
--> Log a Warning to the LogFile and the console, that some files are protected and therefore not be able to read.
- [ ] Implement
- [ ] Create Test Case

>[!Note] Carefull!
>Since we thought about a Continuous file structure creation in setup you have to incorporate the fact, that some files now can be protected when using this test case. 

#### Argument Null Exception
The given Path is null.
We avoid that with the help of the CLA Validator

#### Path Too Long Exception
The path exceeds the maximum length defined by the system 
We might want to incorporate an initial method in CLA validation. Obtain the deepest hierarchy to initially avoid that.
BUT the user can create deeper paths afterwards. So we have to make a test for that and Error it to the user.
- [ ] Create Test Case
- [ ] Create a Checkup in the respective class method
- [x] Handle the Exception

#### Directory Not Found Exception
The given path could not be found.
Since our software gets the paths by the System.IO Library (besides the src and dest directory) we should assume that there is an implementation error. 
Source and Destination Paths get checked initially and then they will become recreated during Synchronizing. So we have different cases here, where this Exception could raise

- Exception raises on CLA Validation
	- [ ] Error a log to the user and abort further executions
- Exception raises during Sync Process
	- This means either that Src or Dest Folder were moved after synchra validated the paths initially
		- [ ] Recreate Source and Dest Folder and Log a Warning to the user, that this happened. If he wants to give new Paths for Src and Dest he should restart Synchra.
	- Or and error by getting SubDirectories occured. Then its highly likable an implementation error that should never occur
		- [ ] Raise the exception 


# Test Cases

# File Collection
- [x] All files from a bottom directory can be obtained
- [x] All files in subdirectories of current dir can be obtained
## Out Of Sync Checker
- [x] Checksums of src and dest file are equal
- [x] Checksums of src and dest fiile are different
- [x] Checksums of src and dest dir are equal
- [x] Checksums of src and dest dir are different

## Find out about problems
- [ ] Manipulation of dest is blocked by OS 
- [ ] File deletion is blocked by OS
- [ ] Folder is opened in Finder / Explorer

## Perform Sync Actions
- [x] The system clears excess files in Dest
- [x] The System clear excess Directories in Dest
- [x] The System copies excess Files in Src
- [x] The System copies excess Directories in Src
- [x] The System can delete a Directory with SubDirectories and Files in Dest
- [x] The System can copy a Directory with SubDirectories and Files in Src

## To be tested on running application

- [x] User Creates a new File under src
- [x] User Deletes a File
- [x] User Changes a File
- [x] User Creates a new top hierarchy directory and moves everything under it
- [x] After synch src and dest checksums are equal

# To be agreed with Customer

## Clarified
- [x] Changings in the destination folder made by the user are thrown away during a sync process if not the user made the exact same changes also to the source folder.
	- Customer says YES, we want exactly that. 

# Edge Cases
- [ ] What happens if the structure contains of 200.000 Folders and or files?
	- Look under Discussion at [[Get Checksum]]
	- Please don't fall into analysis paralysis. Maybe the best way to check performance is to create a folder with 70.000 and even 700.000 Files (we can delete them after the test, but then we should not perform those tests on SSDs) and see how long the test with the checksum will take. 
	  Then we can make a realistic assumption how well or bad the solution really is. 
	  Better do than think, we're running out of time... 
- [x] what happens if the user just moves all file into a folder to make the whole hierarchy deeper by one?
- [ ] What if the permission to read a file gets lost during runtime (or is even not available from the start?)
- [x] Read the documentation for possible thrown exceptions to get further inspiration for edge cases