#Chloride - C# libraries which are useful in general
Licensed under the LGPL (v3), which in the case of .NET managed languages is really easy to fulfill
when libraries compile as dynamically linkable by standard. To comply with the license, all you
need is to include the license somewhere, and leave the included DLLs as is.

For other licenses, contact me. Most libraries are the work of my own, or I have the rights to
sublicense them.

##Goals and Features
These are the goals and features of these libraries, if any of the libraries in this repos
has issues with these, you should consider it a bug and file a report.

* Compiles under **Mono without issues.**
* Written in **pure C#** without native, platform restricted libraries written in C.
* Split into multiple libraries, so **you don't have to include unneeded cruft.**
* Libraries should not require you to build your entire application on top of their classes,
  as doing so locks you into their ecosystem. It should be easy to change library without
  putting work in retrofitting your application into another ecosystem. **Changing two lines
  should be enough to change API if you don't like what you have.**
* Implementation should be **quick to implement** in your application, and **quick to perform it's
  task**, while still remaining readable.
* **Semantic Versioning**, with a new version on every release, so **you'll instantly know about
  breaking changes.**
* **Used by the primary developer**, me. Most **bugs will be found and fixed quickly**.

##The Name
Should be quite obvious. It's because <s>Chlorine is extremely poisonous and can kill at levels
as low as 1000 ppm</s> I'm the primary user of these libraries. If I make something which might
be useful to someone else, I shove it in a library here; either a new one or extend an already
existing one.