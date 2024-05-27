<a href="../README.md"> Back</a>

<h1 id="credits">Credits</h1>


<ul>
    <li> <h4>Microsoft</h4>
        <ul>
            <li><a href="https://docs.microsoft.com/en-us/dotnet/csharp/">C# Documentation</a></li>
            <li><a href="https://docs.microsoft.com/en-us/dotnet/standard/net-standard">.NET Standard Documentation</a></li> 
            <li><a href="https://visualstudio.microsoft.com/">Visual Studio</a></li>             
            <li><a href="https://docs.microsoft.com/en-us/windows/uwp/">UWP Documentation</a></li>
            <li><a href="https://marketplace.visualstudio.com/items?itemName=TemplateStudio.TemplateStudioForUWP">Template Studio Visual Studio Plugin for UWP</a> This helps accelerate UWP application development</li>
            <ul class="no-points"> <h4>Images of how it Accelerated Development</h4>
            <li> <img src="Documentation/Images/TemplateStudio/TemplateStudioNewFeatureWizard.png">Feature Wizard</li><br>
            <li> <img src="Documentation/Images/TemplateStudio/TemplateStudioPageWizard.png" alt="Page Wizard">Page Wizard</li>
            </ul><br>
            <li>UWP and WinUI 3 Samples</li>
                <ul>
                    <li><a href="https://github.com/microsoft/Windows-universal-samples/tree/main/Samples/SpeechRecognitionAndSynthesis/cs">Speaech Recognition and Synthesis</a> Used for the text to speech portion of my application.</li>
                    <li><a href="https://github.com/microsoft/WindowsAppSDK-Samples/tree/main/Samples/PhotoEditor/cs-winui"> Photo Editor</a> Used to help me display the cards in grid format.</li> 
                </ul>
            <li> <a href="https://code.visualstudio.com/">Visual Studio Code</a> Used for editing markdown files.
                <ul>
                    <li> <a href="https://marketplace.visualstudio.com/items?itemName=mushan.vscode-paste-image">Past Image</a> Let's you past images into the explorer from your clipboard.
                    </li>
                </ul>
            </li>
        </ul>
    </li>
    <li> <h4> JetBrains</h4></li>
        <ul>
            <li> <a href="https://www.jetbrains.com/rider/">Rider</a> Helped with development.</li>
            <li> <a href="https://www.jetbrains.com/resharper/">ReSharper</a> Helped with development and generating the <a href="./Documentation/TypeDiagrams/TypeDiagrams.md"> type digrams</a>.</li>
        </ul>
    </li>
    <li> <h4>NuGet</h4></li>
        <ul>
            <li><a href="https://www.nuget.org/packages/Syncfusion.Pdf.UWP/25.2.5?_src=template">Syncfusion PDF Generator for UWP</a> Used to export a card board to a PDF.<a href="https://help.syncfusion.com/file-formats/pdf/create-pdf-file-in-uwp"> Starter code to help generate a PDF.</a></li>
            <li><a href="https://www.nuget.org/packages/AsyncAwaitBestPractices.MVVM/7.0.0?_src=template"> Allows for asynchronous commands with the MVVM pattern</a></li><li><a href="https://www.nuget.org/packages/AsyncAwaitBestPractices/7.0.0?_src=template"> Extensions for the System.Threading.Tasks</a></li>
            <li><a href="https://www.nuget.org/packages/Microsoft.Toolkit.Mvvm/7.0.2?_src=template">
            Microsoft Toolkit MVVM</a> Used for the MVVM pattern. If I could use C# 8, I would use the <b>CommunityToolkit.MVVM</b> instead, as the source code generation removes the <b>INotifyPropertyChanged</b>, the code to create observable properties and the code to create <b>ICommand</b>s boilerplate. Luckily ReSharper can generate the code for all the properties in a class, which dervies from <b>INotifyPropertyChanged</b> for you.</li>
        </ul>

</ul>


<a href="../README.md"> Back</a>
