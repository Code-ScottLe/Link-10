# What?
Not to be confused with [Datalink Link-10](https://en.wikipedia.org/wiki/Tactical_data_link). Link-10 is the framework built on top of the Universal Windows platform that aim to help with cross-app communication between UWP apps and even Win32 application using App Services Connection and various other tools that was provided like Remote Session from Project Rome. *Although I won't confirm or deny that with me being an avid flight simulator has anything to do with the naming of this project.*

# Why?
Project Rome provides an easy API to enables cross-application communication, but how to fit them in the standard MVVM UWP application well is another story (can be a bit of manual plumbing). Link-10 is *aimed* to provide helper classes that can help speed up the development process / adoption of such API, while staying dependecy-injectable and respect single-responsibility principle of doing one thing at a time. I said aimed, because whether it succeed doing so (for you) is another story. 

Oh and btw, it enables UWP application to talk to a Win32 counterpart, which can access all of the "missing" API that one would ever want from Win32. We'll get to that, eventually. 

# How?
Eventually i will write in the read me/wiki, but soon there will be a sample application. 

# When?
I started this project on a whimp after writing 2 side projects that both ended up using some part of Project Rome (App Services, Remote Session... etc) and facing the same problem of how to write it properly within a well-defined MVVM application. So the result that you are seeing here is direct impact of how they are being used in development of 2 MVVM UWP application that used dependency-injection pattern. I need a place to back up and share the code between the 2 projects. 
