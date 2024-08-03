
[Link To Post](https://stackoverflow.com/questions/10520048/calculate-md5-checksum-for-a-file)
![[Findout Checksum.png]]

Framework: System.Security.Cryptography.MD5
Dispose the MD5 by using it in a using block.
Dispose the StreamReader the same way. 


Here is a [Post](https://stackoverflow.com/questions/3625658/how-do-you-create-the-hash-of-a-folder-in-c) how to create a checksum for all files in a Directory:
![[Findout Checksum of Directory.png]]

# Discussion

| Checksum over all Files                                                                                              | Timestamp of a Directory                                                                                                                      |
| -------------------------------------------------------------------------------------------------------------------- | --------------------------------------------------------------------------------------------------------------------------------------------- |
| - Assuming a folder containing 70.000 Files,<br>it might take a while to get the checksum.                           | - Just checking the last modified timestamp of <br>folder goes really fast                                                                    |
| - When Checksum over 70.000 Files differ,<br>you still don't know which (maybe) ONE File <br>has been changed        | - However, the timestamp can be inaccurate. <br>We could try to set the last modified ts of <br>dest to the ts of src after sync              |
| - We might want to use a divide and conquer<br>or octree solution to make an efficient <br>iteration over the folder | - BUT this will only work if the folder isn't <br>opened in the explorer! Otherwise Synchra<br>raise an exception and we have to start over.  |
