# JsonPosts

To run the application, execute:

PS D:\JsonPosts\JsonPosts> dotnet run

--- 

1. In C# there are several ways to make code run in multiple threads. To make things easier, the await keyword was introduced; what does this do?

The await keyword works in combination with the async keyword, to tell the program that the execution of the current method 
needs to be paused until the completion of the Task originated from the object that is being awaited. Async is not multithreading however.


2. If you make http requests to a remote API directly from a UI component, the UI will freeze for a while, how can you use await to avoid this and how 
does this work?

Calling the external API through an asynchronous call allows the UI to not freeze, since the main thread is not blocked until the request is finished.
The program keeps executing other tasks while the request is not completed. When it completes, the code resumes with the result of the task after the await call.

3. Imagine that you have to process a large unsorted CSV file with two columns: ProductId (int) and AvailableIn (ISO2 String, e.g. "US", "NL"). The goal is 
to group the file sorted by ProductId together with a list where the product is available. Example: 1, "DE" 2, "NL" 1, "US" 3, "US" Becomes: 1 -> ["DE", "US"] 2 
-> ["NL"] 3 -> ["US"]
	1. How would you do this using LINQ syntax (write a short example)?

	var csv = @"ProductId,AvailableIn
1,DE
2,NL
1,US
3,US";

	csv
		.Split(Environment.NewLine)
		.Skip(1)
		.Select(entry => entry.Split(','))
		.GroupBy(entry => entry[0], entry => entry[1])
		.ToDictionary(entry => entry.Key, entry => entry.ToArray())

	2. The program crashes with an OutOfMemoryError after processing approx. 80%. What would you do to succeed?

	In this case, to avoid out of memory, do not store the entire file in memory. Instead, process the items as a stream, reading
	line by line until completion. After the line is read, it cannot be read again.

	To further improve, create checkpoints with the current progress. In case of any failure, the program can resume the latest checkpoint
	instead of reprocessing everything.


4. In C# there is an interface IDisposable.
	1. Give an example of where and why to implement this interface.

	IDisposable interface makes the class to implement the Dispose method, which can be called to free unmanaged resources. One example is 
	the StreamReader class. You can read a file using the StreamReader. The Garbage Collector does not know about the file that StreamReader opens,
	therefore you must call Dispose after finishing using the file, otherwise the file will stay open and your program will keep using unnecessary memory.

	If you are creating a class that is going to use unmanaged resources, or that it has dependencies on classes that implements the IDisposable interface,
	you might want to make your class implement it too, so that the caller can decide when he is done using your class, and your class can then call the underlying Dispose of dependencies.


	2. We can use disposable objects in a using block. What is the purpose of doing this?

	The using statement allows the program to automatically dispose the object after it leaves the execution context.


5. When a user logs in on our API, a JWT token is issued and our Outlook plugin uses this token for every request for authentication. Here's an example of 
such a token: 
eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwi 
bmFtZSI6IkplcmVteSIsImFkbWluIjpmYWxzZX0.BgcLOiwBvyuisQk9yWW0q0ZSc MyIHNmDctw12-meCHU
Why and when is it (or isn't it) safe to use this?

The authentication token must be used when accessing resources that require authentication and/or authorization. The token must be passed only
through a connection that is encrypted (https) and to well known resources. You must not pass the authentication token of your application in a 
request to an unknown site, as they will have access to a valid token that might be used to get unauthorized access to your application.