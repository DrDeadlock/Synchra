# CLA Inputs
- [ ] Source Folder and Destination Folder must be specified
- [ ] Sync Interval 
	- [ ] As an integer number
	- [ ] In seconds
- [ ] Log File Path 


# To be agreed with customer
- [ ] Interval:
	- [ ] Sync Interval is optional, a default value is provided if no interval was entered by the user.
- [ ] Log File Path:
	- [ ] Suggestion to customer: also optional, a default value is provided if no path was enteredd by the user

- [ ] If Sync interval and Log File are left empty, the program asks, if the default values should be used. A confirmation -y starts execution.


> [!NOTE] Nice to have 
> - [ ] -h can be called to get information about usable CLAs
>- [ ] -xml can be used to load a config set entered into the xml
>	- [ ] How to explain that to the user?
>	- [ ] Save config as xml during program execution or per CLA at the start?
>	- [ ] Nice to have

# Edge Cases
- [ ] What happens if the user enters an interval of 5 seconds or 5 milliseconds?
	- [ ] We have to make sure, that the program runs through one time completely before we start the next run through
	- [ ] Let us realize that with a dispatcher mechanism -- [[Task Scheduling]]