- [ ] What is the difference between Task and Thread?

[Thread Start](https://learn.microsoft.com/de-de/dotnet/api/system.threading.thread.start?view=net-8.0)
[TaskStart](https://learn.microsoft.com/de-de/dotnet/api/system.threading.tasks.task.start?view=net-8.0)

> [!NOTE] Answer
>[Lies here](https://stackoverflow.com/questions/4130194/what-is-the-difference-between-task-and-thread)
>Tasks wrap Threads. 
>For simple CPU heavy operations, use normal Tasks.
>For I/O or REST dependant operatioins, use longrunning Tasks.
>The community points out, that tasks are the better ones to use because
> - Threads are expensive to create
> - Tasks are taken from the Thread Pool internally handled by .NET





Task Run vs Task.Factory.StartNew
[Stack Overflow Discussion](https://stackoverflow.com/questions/38423472/what-is-the-difference-between-task-run-and-task-factory-startnew)

