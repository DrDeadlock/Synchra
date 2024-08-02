- [ ] React smoothly on the [[Application Exit]] Event to avoid file corrupting during a creation / copying / deleting process. 

# Test Cases
- [ ] Synch process can be dispatched only once at a time
- [ ] On Application exit the sync completes its most recent started action (File Create / Copy / Remove) and THEN the sync will be interrupted.