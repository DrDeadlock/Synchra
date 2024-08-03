## Synchronization Specifics

- [x] Synchra is able to detect if Src File and Dest File are different
- [x] Synchra is able to detect the difference between two files without having to iterate through each line of the file

- [ ] Synchra only makes changes to Dest
- [ ] Synchra is able to Create Files which are in Src and are not in Dest
- [ ] Synchra is able to Copy Update Files which are as well in Src and Dest but differ regarding their content (Compare by Checksum)
- [ ] Synchra is able to Delete Files which are in Dest and are not in Src

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
- [ ] User Creates a new File under src
- [ ] User Deletes a File
- [ ] User Changes a File
- [ ] User Creates a new top hierarchy directory and moves everything under it
- [ ] After synch src and dest checksums are equal
- [ ] ???After synch src and dest are equal (test by iterative file check)

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
- [ ] what happens if the user just moves all file into a folder to make the whole hierarchy deeper by one?
- [ ] What if the permission to read a file gets lost during runtime (or is even not available from the start?)

- [ ] Read the documentation for possible thrown exceptions to get further inspiration for edge cases