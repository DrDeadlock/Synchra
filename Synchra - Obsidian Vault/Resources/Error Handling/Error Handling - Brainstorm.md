# How can User Behaviours break the program?

## The user is quick
During the process of checking for changes or applying changes the user changes, moves or deletes a file.
- [ ] Should Synchra recognize that?
	- [ ] How can we realize that?
- [ ] How to deal with those changes during the process of synching?

# The user manipulates the destination folder
Whether due to confusion or stupidness, doesn't matter, users could manipulate the destination folder by accident. 
- [ ] What happens to those changes made in the destination folder?
	- *Those changes will be overridden in the next synch period.*

> [!NOTE] Nice to have
> 	- Maybe we are nice to the user and play a little soft alert sound, that helps him to realize, that he is currently working on the dest folder, which he shouldn't
> 	- How would Synchra realize, that destination folder changes?

# Invalid CLAs
## Regarding Paths
The user inputs an invalid Source, Destination or log file path.
- Path is blocked or protected
- Path does not exist
	- should we offer to the user to create those directories?
- ...

# Regarding Interval
The user inputs
- a negative number
- a floating point number
- not even a number 
- a very small number

