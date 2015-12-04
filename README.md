## CSharpTurtorial
> C# msdn samples studying.

----------

##Documents
- [MSDN document](https://msdn.microsoft.com/en-us/library/aa287558(v=vs.71).aspx)
- [Books- Professtional C# 4.0 and .NET 4]
- [Windows系统的线程调度](http://blog.csdn.net/xywlpo/article/details/6831840) 线程内核对象、上下文；线程优先级的动态调整；进程、线程的亲缘性；
- [操作系统知识回顾---进程线程模型 ](http://blog.chinaunix.net/uid-26430381-id-3746859.html) **进程状态切换**
- [操作系统之CPU调度](http://blog.csdn.net/xiazdong/article/details/6280345)


----------

###Examples
- [AsynchronousDelegates](#Thread) **Polling/Wait Handle/Asynchronous Callback(use lambda)** to check if sub-thread already finish and waiting for the result.
- [ThreadStartStop](#Thread)
- [BackgroundThread](#Thread) 
- [TheadPools](#Thread)

----------

### Threads, Tasks, and Synchronization
- [ThreadTurtorals-msdn](https://msdn.microsoft.com/en-us/library/aa645740(v=vs.71).aspx)

- A thread is a independent stream of instructions in a program. 

- A process contains resources, which include virtual memory and Window Handles, and contains at least one thread.

- The process keeping running as long as at least one foreground thread is running. A thread created with **Thread** class, by default, is a foreground thread.
- Thread Pools. All thread pool threads are background threads. You cannot set the priority or name of a pooled thread.