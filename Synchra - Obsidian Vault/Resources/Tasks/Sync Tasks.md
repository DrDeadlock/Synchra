## Synchronization Specifics

- [ ] Synchra is able to detect if Src and Dest are different
- [ ] Synchra is able to make diff detections without iterating through each file
	- [ ] Creating a checksum of the folder structure?
	- [ ] Look Up intelligent ways to do that - [[Get Checksum]]
- [ ] Synchra is able to detect the difference between two files without having to iterate through each line of the file
	- [ ] It is enough to compare the last updated Time Stamp

- [ ] Synchra only makes changes to Dest
- [ ] Synchra is able to Create Files which are in Src and are not in Dest
- [ ] Synchra is able to Copy Update Files which are as well in Src and Dest but differ regarding their content (Compare by Updated TimeStamp)
- [ ] Synchra is able to Delete Files which are in Dest and are not in Src

# Test Cases
- [ ] Checksums of src and dest are equal
- [ ] Checksums of src and dest are different
- [ ] Manipulation of dest is blocked by OS
- [ ] After synch src and dest checksums are equal
- [ ] ???After synch src and dest are equal (test by iterative file check)

# To be agreed with Customer

- [ ] Changings in the destination folder made by the user are thrown away during a sync process if not the user made the exact same changes also to the source folder.

# Edge Cases
- [ ] What happens if the structure contains of 200.000 Folders and or files?
- [ ] what happens if the user just moves all file into a folder to make the whole hierarchy deeper by one?
- [ ] What if the permission to read a file gets lost during runtime (or is even not available from the start?)

- [ ] Read the documentation for possible thrown exceptions to get further inspiration for edge cases