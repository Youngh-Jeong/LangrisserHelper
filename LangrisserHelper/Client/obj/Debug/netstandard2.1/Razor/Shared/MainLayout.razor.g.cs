#pragma checksum "D:\Langrisser\LangrisserHelper\LangrisserHelper\Client\Shared\MainLayout.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "faf398920994566ba96ec7abdb74b6fa37aadad6"
// <auto-generated/>
#pragma warning disable 1591
namespace LangrisserHelper.Client.Shared
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "D:\Langrisser\LangrisserHelper\LangrisserHelper\Client\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Langrisser\LangrisserHelper\LangrisserHelper\Client\_Imports.razor"
using System.Net.Http.Json;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "D:\Langrisser\LangrisserHelper\LangrisserHelper\Client\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "D:\Langrisser\LangrisserHelper\LangrisserHelper\Client\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "D:\Langrisser\LangrisserHelper\LangrisserHelper\Client\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "D:\Langrisser\LangrisserHelper\LangrisserHelper\Client\_Imports.razor"
using Microsoft.AspNetCore.Components.WebAssembly.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "D:\Langrisser\LangrisserHelper\LangrisserHelper\Client\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "D:\Langrisser\LangrisserHelper\LangrisserHelper\Client\_Imports.razor"
using LangrisserHelper.Client;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "D:\Langrisser\LangrisserHelper\LangrisserHelper\Client\_Imports.razor"
using LangrisserHelper.Client.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "D:\Langrisser\LangrisserHelper\LangrisserHelper\Client\_Imports.razor"
using Blazored.Toast;

#line default
#line hidden
#nullable disable
#nullable restore
#line 11 "D:\Langrisser\LangrisserHelper\LangrisserHelper\Client\_Imports.razor"
using Blazored.Toast.Services;

#line default
#line hidden
#nullable disable
#nullable restore
#line 12 "D:\Langrisser\LangrisserHelper\LangrisserHelper\Client\_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 13 "D:\Langrisser\LangrisserHelper\LangrisserHelper\Client\_Imports.razor"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Langrisser\LangrisserHelper\LangrisserHelper\Client\Shared\MainLayout.razor"
using Blazored.Toast.Configuration;

#line default
#line hidden
#nullable disable
    public partial class MainLayout : LayoutComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.OpenComponent<Blazored.Toast.BlazoredToasts>(0);
            __builder.CloseComponent();
            __builder.AddMarkupContent(1, "\r\n");
            __builder.OpenElement(2, "div");
            __builder.AddAttribute(3, "class", "sidebar");
            __builder.OpenComponent<LangrisserHelper.Client.Shared.NavMenu>(4);
            __builder.CloseComponent();
            __builder.CloseElement();
            __builder.AddMarkupContent(5, "\r\n\r\n");
            __builder.OpenElement(6, "div");
            __builder.AddAttribute(7, "class", "main");
            __builder.OpenElement(8, "div");
            __builder.AddAttribute(9, "class", "top-row px-4");
            __builder.OpenComponent<LangrisserHelper.Client.Shared.TopMenu>(10);
            __builder.CloseComponent();
            __builder.CloseElement();
            __builder.AddMarkupContent(11, "\r\n\r\n\r\n    ");
            __builder.OpenElement(12, "div");
            __builder.AddAttribute(13, "class", "content px-4");
            __builder.AddContent(14, 
#nullable restore
#line 16 "D:\Langrisser\LangrisserHelper\LangrisserHelper\Client\Shared\MainLayout.razor"
         Body

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.CloseElement();
        }
        #pragma warning restore 1998
    }
}
#pragma warning restore 1591
