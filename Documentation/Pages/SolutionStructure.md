<a href="../../README.md"> Back</a>


<h1> Solution Structure</h1>

The solution is structured in a way that is easy to navigate. The project is a UWP application, so it has the following projects:
<ul> 
    <li> <b>LetsTalk</b> The main project that contains the UWP application.
        <ul>
            <li> <b> Activation</b> Contains the Activation logic for the application.</li>
            <li> <b>Assets</b> Contains the images and other assets used in the application.</li>
            <li> <b>Behaviours</b> Contains behviours for XAML which allow for easy to add functionality to XAML components.</li>
            <li> <b>Contracts</b> Contains the contracts that the application uses.</li>
            <li> <b> Convertors (yes, this is a valid spelling) </b> Contains the convertors that the application uses. Convertors allows for XAML binding properties to bind to prpoerties that are of different types.</li>
            <li> <b>Databases</b> Contains the database logic for the application.</li>
            <li> <b>Services</b> Contains the services that the application uses.</li>
            <li> <b>Strings</b> The strings are the localised files so that my application can be used in different languages.</li>
            <li> <b> Styles</b> Contains the styles that the application uses.</li>
            <li> <b>Views</b> Contains the pages and other controls that were used in this application.</li>
            <li> <b>ViewModels</b> Contains the view models that the application uses.</li>
            <li> <b>App.xaml</b> The main application file.</li>
            <li> <b>App.xaml.cs</b> The code behind for the main application file.</li>    
            <li> <b>Package.appxmanifest</b> The manifest file for the application.</li>
        </ul>
    </li>
    <li> <b>LetsTalk.Core</b> A .NetStandard project Contains the models, services and code that can be used between projects.</li>
</ul>

<a href="../../README.md"> Back</a>

