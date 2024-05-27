<h1> Let's Talk</h1>
<h3> Developed by Joshua Petersen</h3>
<br>
<h3> Please install the application as I have made more fixes to it than what is shown in the videos</h3>

<br>
<h2>Contents</h2>
<ul>
<li> <a href="#solution-structure"> Solution Structure</a></li>
<li><a href="#description">Description and answers for some of my programming choices.</a></li>
<li><a href="./Documentation/Pages/TypeDiagrams.md">Type Diagrams</a></li>
<li><a href="#installation">Installation</a></li>
<li><a href="./Documentation/Pages/Credits.md">Credits</a></li>
</ul>



<h3 id="installation">Installation</h3>

-----

<p> To install the application, download the <a href="https://github.com/Jvp2001/LetsTalk/releases/tag/binaries">LetsTalk.zip</a>and extract it.
If the <b>installer</b> does not work, please go into the LetsTalk folder and run the <b>LetsTalk.exe</b>.</p>


<h3 id="description"> Description</h3>

----------------------------
<p> I used for loops instead of foreach loops  due to the fact that foreach loops create a new enumerator object, which is not needed in this case. I also used for loops because they are faster than foreach loops, in C# 7.3.</p>

<p> If I had more time, I would convert the application from using Singletons to Dependency Injection. This would make the application easier to test, and would make the code more modular. I would also add unit testing to the application.</p>

<p>I used selead classes on types that are not inherited from as this increases performanceas it helps the JIT to de-virtualise method calls.</p>

<p>I used extensions classes for the mixin interfaces to allow me to easily add functionality to my page classes. One example is the <b>IPdfExportablePage</b> interface.</p>
