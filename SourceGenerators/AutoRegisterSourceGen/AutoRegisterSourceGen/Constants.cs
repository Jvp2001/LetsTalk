namespace AutoRegisterSourceGen;

public static class Constants
{
    public const string Namespace = "AutoRegisterSourceGen";
    public const string AutoRegisterViewModelsAttribute = "AutoRegisterViewModelsAttribute";
    public const string AutoRegisterServicesAttribute = "AutoRegisterServicesAttribute";
    public const string ARG001 = "ARG001";
    public const string ARG002 = "ARG002";

    public const string AutoRegisterViewModelsFullName = $"{Namespace}.{AutoRegisterViewModelsAttribute}";
    public const string AutoRegisterServicesFullName = $"{Namespace}.{AutoRegisterServicesAttribute}";
    public const string ViewModelGenFileName = "AutoRegister.ViewModels.g.cs";
    public const string PagesGenFileName = "AutoRegister.Pages.g.cs";
    public const string Error = "Error";
    public const string Warning = "Warning";
    public const string ErrorCategoryCompilation = "Compilation";
    public const string ClassNameRegex = "^[a-zA-Z_][a-zA-Z0-9_]*$";
    public const string PageRegisterGeneratorAttribute = "AutoRegisterPagesAttribute";
    public const string PageRegisterGeneratorAttributeFullName = $"{Namespace}.{PageRegisterGeneratorAttribute}";

    public const string PageRegisterServiceAttribute = "AutoRegisterPageServiceAttribute";
    public const string PageRegisterServiceAttributeFullName = $"{Namespace}.{PageRegisterServiceAttribute}";
    public const string PageRegisterServiceGenFileName = "AutoRegister.PageService.g.cs";
    public const string DoNotRegisterAttribute = "DoNotRegisterAttribute";
    public const string DoNotRegister = "DoNotRegister";
    public const string DoNotRegisterAttributeFullName = $"{Namespace}.{DoNotRegisterAttribute}";
}