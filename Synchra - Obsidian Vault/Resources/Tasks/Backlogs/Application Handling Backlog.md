- [ ] React smoothly on the [[Application Exit]] Event to avoid file corrupting during a creation / copying / deleting process. 

# Currently Doing
- [ ] Sync process can be dispatched only once at a time
	- The SyncPerform.Execute() is called and started as a Task
	- The main Thread goes to sleep for the in CLA given time period
	- On Wake Up, main checks if SyncPerform Task is done
	- SyncPerform Task never runs more than once in parallel. If after main waking up the sync perform is still running, it goes to sleep again and waits for the next wake up.
# Test Cases
- [ ] On Application exit the sync completes its most recent started action (File Create / Copy / Remove) and THEN the sync will be interrupted.