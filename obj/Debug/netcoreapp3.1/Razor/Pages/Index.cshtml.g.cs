#pragma checksum "C:\Users\Yash Khanduja\Documents\Mohawk College\Winter 2022-Term 5\Advanced .NET\COMP-10068_Assignment_4-master (1)\comp-10068_assignment_4\Assignment4\Pages\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "a51d30f2a6ed3d17954ac571ca6fbdf9ec7c62d2"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(Assignment4.Pages.Pages_Index), @"mvc.1.0.razor-page", @"/Pages/Index.cshtml")]
namespace Assignment4.Pages
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\Yash Khanduja\Documents\Mohawk College\Winter 2022-Term 5\Advanced .NET\COMP-10068_Assignment_4-master (1)\comp-10068_assignment_4\Assignment4\Pages\_ViewImports.cshtml"
using Assignment4;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a51d30f2a6ed3d17954ac571ca6fbdf9ec7c62d2", @"/Pages/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"50e7c85c73b47d2c7ced30d4a9fc759058fef4d0", @"/Pages/_ViewImports.cshtml")]
    public class Pages_Index : global::Microsoft.AspNetCore.Mvc.RazorPages.Page
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 3 "C:\Users\Yash Khanduja\Documents\Mohawk College\Winter 2022-Term 5\Advanced .NET\COMP-10068_Assignment_4-master (1)\comp-10068_assignment_4\Assignment4\Pages\Index.cshtml"
  
    ViewData["Title"] = "Chat Room";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"<script src=""https://cdnjs.cloudflare.com/ajax/libs/aspnet-signalr/1.1.4/signalr.min.js"" integrity=""sha512-hqwtOj6waHZZoLRoJoLn0tq34JS97tR1QmhM69uvyrt3LVBiR1o0xeOVAskjUFL1L1fahQH2W7IdcMaFbqCEaw=="" crossorigin=""anonymous""></script>

<div class=""text-center"">
    <div id=""login"">
        <h1 class=""display-4"">Log In</h1>
        <p id=""loginError"" class=""error""></p>
        <input id=""username"" placeholder=""Username"" />
        <button id=""connect"">Connect</button>
    </div>
</div>");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IndexModel> Html { get; private set; }
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<IndexModel> ViewData => (global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<IndexModel>)PageContext?.ViewData;
        public IndexModel Model => ViewData.Model;
    }
}
#pragma warning restore 1591
