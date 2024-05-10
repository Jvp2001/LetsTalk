// using System;
// using System.Collections.Generic;
// using System.Collections.Immutable;
// using System.Linq;
// using System.Text.RegularExpressions;
// using Microsoft.CodeAnalysis;
// using Microsoft.CodeAnalysis.CSharp.Syntax;
//
// namespace AutoRegisterSourceGen;
//
// [Generator]
// public class PageServiceRegisterGenerator : IIncrementalGenerator
// {
//     private const string ViewModelString = "ViewModel";
//     private const string PageString = "Page";
//     private readonly Regex _classNameRegex = new(Constants.ClassNameRegex);
//
//     public void Initialize(IncrementalGeneratorInitializationContext context)
//     {
// #if DEBUG
//         if (System.Diagnostics.Debugger.IsAttached)
//         {
//             System.Diagnostics.Debugger.Launch();
//         }
// #endif
//         var syntaxProvider = context.SyntaxProvider.CreateSyntaxProvider(
//                 (syntaxNode, _) => syntaxNode is ClassDeclarationSyntax,
//                 (ctx, _) => (ClassDeclarationSyntax)ctx.Node)
//             .Where(c => c is not null);
//
//         var compilation = context.CompilationProvider.Combine(syntaxProvider.Collect());
//
//         context.RegisterPostInitializationOutput(ctx => ctx.AddSource(
//             Constants.PageRegisterServiceAttribute,
//             BuildPageServiceRegisterAttribute()));
//
//         context.RegisterPostInitializationOutput((ctx) => ctx.AddSource(
//             Constants.DoNotRegisterAttribute,
//             BuildPageDoNotRegisterAttribute()));
//
//         context.RegisterSourceOutput(compilation, Execute);
//     }
//
//     private void Execute(SourceProductionContext context,
//         (Compilation Left, ImmutableArray<ClassDeclarationSyntax> Right) compilationTuple)
//     {
//         var (compilation, classes) = compilationTuple;
//         
//         var attributeAutoGenSymbol =
//             compilation.GetTypeByMetadataName(Constants.PageRegisterGeneratorAttributeFullName);
//
//         if (attributeAutoGenSymbol is null)
//         {
//             // Stop the generator if no such attribute has been found (shouldn't happen as it's defined in the same assembly)
//             return;
//         }
//
//
//         // create a namespace symbol from the namespace name
//
//
//         // Get all view models and pages that do not have the DoNotRegisterAttribute
//         var pages = classes
//             // .SkipWhile(c => c.AttributeLists.First(ad =>
//             //         ad.Attributes.First(a => a.Name.ToString().Contains(Constants.DoNotRegisterAttribute)) is not null)
//             //     is not null)
//             .Where(c =>
//                 c.Identifier.Text.EndsWith("Page"))
//             .Select(c => c.Identifier.Text)
//             .ToImmutableHashSet();
//         
//
//         var viewModels = classes
//             // .SkipWhile(c => c.AttributeLists.First(ad =>
//             //         ad.Attributes.First(a => a.Name.ToString().Contains(Constants.DoNotRegisterAttribute)) is not null)
//             //     is not null)
//             .Where(c => c.Identifier.Text.EndsWith("ViewModel"))
//             .Select(c => c.Identifier.Text)
//             .ToImmutableHashSet();
//
//
//         if (viewModels.Count != pages.Count)
//         {
//             context.ReportDiagnostic(Diagnostic.Create
//                 (
//                     new DiagnosticDescriptor
//                     (
//                         Constants.ARG001,
//                         Constants.Error,
//                         $"The number of ViewModels and Pages must be equal!\nPages: {pages}\nViewModels: {viewModels}",
//                         Constants.ErrorCategoryCompilation, DiagnosticSeverity.Error,
//                         true
//                     ),
//                     null
//                 )
//             );
//             context.ReportDiagnostic(Diagnostic.Create
//                 (
//                     new DiagnosticDescriptor
//                     (
//                         Constants.ARG002,
//                         Constants.Error,
//                         $"Pages: {pages.Count}   ViewModels: {viewModels.Count}",
//                         Constants.ErrorCategoryCompilation, DiagnosticSeverity.Error,
//                         true
//                     ),
//                     null
//                 )
//             );
//             return;
//         }
//
//
//         IEnumerable<string> classesToIgnore = classes.Select(cds => cds.AttributeLists.First(als =>
//             als.Attributes.First(a => a.Name.ToString().Contains(Constants.DoNotRegisterAttribute)) is not null)
//             .Attributes.First(a => a.Name.ToString().Contains(Constants.DoNotRegisterAttribute))
//             .ToString());
//
//
//
//
//         HashSet<(string ViewModelName, string PageName)> routeNameList = new();
//         var pageClassDeclarationSyntaxList = classes.Where(c => c.Identifier.Text.EndsWith("Page")).ToList();
//         var viewModelClassDeclarationSyntaxList = classes.Where(c => c.Identifier.Text.EndsWith("ViewModel")).ToList();
//
//         foreach (var viewModel in viewModels)
//         {
//             var viewModelSpan = viewModel.AsSpan();
//             
//             var viewModelName = viewModelSpan.Slice(0, viewModelSpan.Length - ViewModelString.Length);
//             
//            
//             foreach (var page in pages)
//             {
//                 var pageSpan = page.AsSpan();
//                 var pageName = pageSpan.Slice(0, pageSpan.Length - PageString.Length);
//
//               
//
//
//                 if (viewModelName.Equals(pageName, StringComparison.OrdinalIgnoreCase))
//                 {
//                     routeNameList.Add((viewModel, page));
//                 }
//             }
//         }
//
//
//         // group each Page with its corresponding ViewModel to tuple
//
//
//         var source = BuildSource(routeNameList);
//
//         context.AddSource(Constants.PagesGenFileName, source);
//     }
//
//     private static string BuildSource(
//         IEnumerable<(string ViewModelName, string PageName)> routeNameList)
//     {
//         var code = string.Join("\n", routeNameList.Select(r =>
//         {
//             return $@"Configure<{r.ViewModelName},{r.PageName}>();";
//         }));
//
//         var source = $$"""
//                        // <auto-generated/>
//                        using System.Collections.ObjectModel;
//                        using Microsoft.Extensions.DependencyInjection;
//                        using LetsTalk.Pages;
//                        using LetsTalk.ViewModels;
//                        using LetsTalk.Views;
//
//                        namespace LetsTalk.Services
//                        {
//                            public partial class PageService
//                            {
//                            
//                                public void AutoRegisterPages()
//                                {
//                                     {{code}}
//                                 }
//                            
//                            
//                            
//                            }
//                        }
//                        """;
//         return source;
//     }
//
//     // Helper method to get all classes in a namespace
//     private static IEnumerable<INamedTypeSymbol> GetAllClasses(INamespaceSymbol namespaceSymbol)
//     {
//         foreach (var member in namespaceSymbol.GetMembers())
//         {
//             if (member is INamespaceSymbol childNamespace)
//             {
//                 foreach (var childClass in GetAllClasses(childNamespace))
//                 {
//                     yield return childClass;
//                 }
//             }
//             else if (member is INamedTypeSymbol { TypeKind: TypeKind.Class } classSymbol)
//             {
//                 yield return classSymbol;
//             }
//         }
//     }
//
//
//     private static string BuildPageServiceRegisterAttribute()
//     {
//         return $$$""""
//                   using System;
//                   namespace {{{Constants.Namespace}}};
//
//                   [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
//                   public class {{{Constants.PageRegisterServiceAttribute}}}: Attribute
//                   {
//                    
//                   
//                       public {{{Constants.PageRegisterServiceAttribute}}}()
//                       {
//                          
//                       }
//
//                   }
//                  
//                   
//                   
//                   """";
//     }
//
//
//     private static string BuildPageDoNotRegisterAttribute()
//     {
//         return $$"""
//                  using System;
//                  namespace {{Constants.Namespace}};
//
//                  [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
//                  public class {{Constants.DoNotRegisterAttribute}}: Attribute
//                  {
//                      
//                      public {{Constants.DoNotRegisterAttribute}}()
//                      {
//                  
//                      }
//
//                  }
//                  """;
//     }
// }