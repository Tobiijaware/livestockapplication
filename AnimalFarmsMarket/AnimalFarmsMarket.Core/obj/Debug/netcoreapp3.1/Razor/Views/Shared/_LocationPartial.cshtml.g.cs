#pragma checksum "C:\Users\hp\Documents\GitHub\livestock-platform\AnimalFarmsMarket\AnimalFarmsMarket.Core\Views\Shared\_LocationPartial.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "8b2dd8500ebd8498daeb242eb1b98b0efc8c5097"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared__LocationPartial), @"mvc.1.0.view", @"/Views/Shared/_LocationPartial.cshtml")]
namespace AspNetCore
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
#line 1 "C:\Users\hp\Documents\GitHub\livestock-platform\AnimalFarmsMarket\AnimalFarmsMarket.Core\Views\_ViewImports.cshtml"
using AnimalFarmsMarket.Core;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\hp\Documents\GitHub\livestock-platform\AnimalFarmsMarket\AnimalFarmsMarket.Core\Views\_ViewImports.cshtml"
using AnimalFarmsMarket.Data.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\hp\Documents\GitHub\livestock-platform\AnimalFarmsMarket\AnimalFarmsMarket.Core\Views\_ViewImports.cshtml"
using AnimalFarmsMarket.Data.DTOs;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\hp\Documents\GitHub\livestock-platform\AnimalFarmsMarket\AnimalFarmsMarket.Core\Views\_ViewImports.cshtml"
using AnimalFarmsMarket.Data.ViewModels;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\hp\Documents\GitHub\livestock-platform\AnimalFarmsMarket\AnimalFarmsMarket.Core\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Users\hp\Documents\GitHub\livestock-platform\AnimalFarmsMarket\AnimalFarmsMarket.Core\Views\_ViewImports.cshtml"
using AnimalFarmsMarket.Data.Enum;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "C:\Users\hp\Documents\GitHub\livestock-platform\AnimalFarmsMarket\AnimalFarmsMarket.Core\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "C:\Users\hp\Documents\GitHub\livestock-platform\AnimalFarmsMarket\AnimalFarmsMarket.Core\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Http;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8b2dd8500ebd8498daeb242eb1b98b0efc8c5097", @"/Views/Shared/_LocationPartial.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d607b1b8f0b44e92836bdc9ea7e18d6c28d6c4cd", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared__LocationPartial : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<LocationMarketVm>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("style", new global::Microsoft.AspNetCore.Html.HtmlString("text-decoration:none"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "fetchLivestockLocation-market", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Market", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-route-market", "Fish Market", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-route-market", "Sheep Market", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-route-market", "Pig Market", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\hp\Documents\GitHub\livestock-platform\AnimalFarmsMarket\AnimalFarmsMarket.Core\Views\Shared\_LocationPartial.cshtml"
  
    Object shouldRender = null;
    if (Model == null)
    {
        shouldRender = "";
    }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"<nav class=""navbar navbar-expand-sm navbar-toggleable-sm m-0 p-0"">
    <div class=""p-0 m-0 w-100"">
        <div class=""toggle-btn-close"">
            <button class=""navbar-toggler toggleColor d-flex align-items-center"" type=""button"" data-toggle=""collapse"" data-target="".navbar-collapse"" aria-expanded=""false"" aria-controls=""navbarSupportedContent""
                    aria-label=""Toggle navigation"">
                <span class=""fa fa-compass fa-2x  toggleColor""></span>
                <span>Market Menu</span>
            </button>
            <i id=""close-button"" class=""fa fa-close"" style=""position:absolute; font-size:30px; top: 10px; right:20px; cursor:pointer; color:red; z-index: 2000;""></i>
        </div>

        <button class=""navbar-toggler toggleColor"" type=""button"" data-toggle=""collapse"" data-target="".navbar-collapse"" aria-expanded=""false"" aria-controls=""navbarSupportedContent""
                aria-label=""Toggle navigation"">
            <i class=""fa fa-compass""></i>
            Market Menu");
            WriteLiteral("\n        </button>\r\n\r\n        <div class=\"navbar-collapse collapse d-sm-inline-flex\">\r\n            <div class=\"row w-100 mt-3\">\r\n                <div class=\"col-lg-12\">\r\n                    <div class=\"accordion box-shadow\" id=\"accordionFlushExample\">\r\n\r\n");
#nullable restore
#line 32 "C:\Users\hp\Documents\GitHub\livestock-platform\AnimalFarmsMarket\AnimalFarmsMarket.Core\Views\Shared\_LocationPartial.cshtml"
                         if (shouldRender == "")
                        {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                    <div class=""myaccordion "">
                        <div class=""heading"" id=""header"">
                            <h2 class=""mb-0"" id=""flush-headingOne"">
                                <button class=""d-flex btn accordion-btn accordion-active text-left collapsed w-100 "" type=""button"" data-toggle=""collapse"" data-target=""#flush-collapseOne"" aria-expanded=""true"" aria-controls=""flush-collapseOne"">
                                    Lagos <i class=""fa fa-angle-down ml-auto p-1""></i>
                                </button>
                            </h2>
                        </div>

                        <div id=""flush-collapseOne"" class=""collapse"" aria-labelledby=""flush-headingOne"" data-parent=""#accordionFlushExample"">


                            <div>
                                <ul class=""list-group"">
                                    <li class=""list-group-item border-0 text-secondary"" style=""cursor:pointer"">
                                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "8b2dd8500ebd8498daeb242eb1b98b0efc8c50979688", async() => {
                WriteLiteral("\r\n                                            Fish Market  <span class=\"badge bg-warning float-right bg-info\">5</span>\r\n\r\n                                        ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-market", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["market"] = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"

                                    </li>
                                </ul>

                            </div>

                        </div>
                    </div>
                                    <div class=""myaccordion "">
                                        <div class=""heading"" id=""header"">
                                            <h2 class=""mb-0"" id=""flush-headingTwo"">
                                                <button class=""d-flex btn accordion-btn accordion-active text-left collapsed w-100 "" type=""button"" data-toggle=""collapse"" data-target=""#flush-collapseTwo"" aria-expanded=""true"" aria-controls=""flush-collapseOne"">
                                                    Kano <i class=""fa fa-angle-down ml-auto p-1""></i>
                                                </button>
                                            </h2>
                                        </div>

                                        <div id=""flush-collapseTwo"" class=""collapse"" aria-l");
            WriteLiteral(@"abelledby=""flush-headingTwo"" data-parent=""#accordionFlushExample"">


                                            <div>
                                                <ul class=""list-group"">
                                                    <li class=""list-group-item border-0 text-secondary"" style=""cursor:pointer"">
                                                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "8b2dd8500ebd8498daeb242eb1b98b0efc8c509713286", async() => {
                WriteLiteral("\r\n                                                            Sheep Market  <span class=\"badge bg-warning float-right bg-info\">5</span>\r\n\r\n                                                        ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-market", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["market"] = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"

                                                    </li>
                                                </ul>

                                            </div>

                                        </div>
                                    </div>
                                                    <div class=""myaccordion "">
                                                        <div class=""heading"" id=""header"">
                                                            <h2 class=""mb-0"" id=""flush-headingTri"">
                                                                <button class=""d-flex btn accordion-btn accordion-active text-left collapsed w-100 "" type=""button"" data-toggle=""collapse"" data-target=""#flush-collapseTri"" aria-expanded=""true"" aria-controls=""flush-collapseOne"">
                                                                    Enugu  <i class=""fa fa-angle-down ml-auto p-1""></i>
                                                                </button>
              ");
            WriteLiteral(@"                                              </h2>
                                                        </div>

                                                        <div id=""flush-collapseTri"" class=""collapse"" aria-labelledby=""flush-headingTri"" data-parent=""#accordionFlushExample"">


                                                            <div>
                                                                <ul class=""list-group"">
                                                                    <li class=""list-group-item border-0 text-secondary"" style=""cursor:pointer"">
                                                                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "8b2dd8500ebd8498daeb242eb1b98b0efc8c509717208", async() => {
                WriteLiteral("\r\n                                                                            Pig Market  <span class=\"badge bg-warning float-right bg-info\">2</span>\r\n\r\n                                                                        ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-market", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["market"] = (string)__tagHelperAttribute_5.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"

                                                                    </li>
                                                                </ul>

                                                            </div>

                                                        </div>
                                                    </div> ");
#nullable restore
#line 114 "C:\Users\hp\Documents\GitHub\livestock-platform\AnimalFarmsMarket\AnimalFarmsMarket.Core\Views\Shared\_LocationPartial.cshtml"
                                                           }
                                                else
                                                {


                                    

#line default
#line hidden
#nullable disable
#nullable restore
#line 119 "C:\Users\hp\Documents\GitHub\livestock-platform\AnimalFarmsMarket\AnimalFarmsMarket.Core\Views\Shared\_LocationPartial.cshtml"
                                     foreach (var item in @Model)
                                    {


#line default
#line hidden
#nullable disable
            WriteLiteral(@"                    <div class=""myaccordion "">
                        <div class=""heading"" id=""header"">
                            <h2 class=""mb-0"" id=""flush-headingOne"">
                                <button class=""d-flex btn accordion-btn accordion-active text-left collapsed w-100 "" type=""button"" data-toggle=""collapse"" data-target=""#flush-collapseOne_");
#nullable restore
#line 125 "C:\Users\hp\Documents\GitHub\livestock-platform\AnimalFarmsMarket\AnimalFarmsMarket.Core\Views\Shared\_LocationPartial.cshtml"
                                                                                                                                                                                     Write(item.location);

#line default
#line hidden
#nullable disable
            WriteLiteral("\" aria-expanded=\"true\" aria-controls=\"flush-collapseOne\">\r\n                                    ");
#nullable restore
#line 126 "C:\Users\hp\Documents\GitHub\livestock-platform\AnimalFarmsMarket\AnimalFarmsMarket.Core\Views\Shared\_LocationPartial.cshtml"
                               Write(item.location);

#line default
#line hidden
#nullable disable
            WriteLiteral("<i class=\"fa fa-angle-down ml-auto p-1\"></i>\r\n                                </button>\r\n                            </h2>\r\n                        </div>\r\n\r\n                        <div");
            BeginWriteAttribute("id", " id=\"", 7834, "\"", 7871, 2);
            WriteAttributeValue("", 7839, "flush-collapseOne_", 7839, 18, true);
#nullable restore
#line 131 "C:\Users\hp\Documents\GitHub\livestock-platform\AnimalFarmsMarket\AnimalFarmsMarket.Core\Views\Shared\_LocationPartial.cshtml"
WriteAttributeValue("", 7857, item.location, 7857, 14, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@" class=""collapse"" aria-labelledby=""flush-headingOne"" data-parent=""#accordionFlushExample"">


                            <div>
                                <ul class=""list-group"">
                                    <li class=""list-group-item border-0 text-secondary"" style=""cursor:pointer"">
                                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "8b2dd8500ebd8498daeb242eb1b98b0efc8c509722764", async() => {
                WriteLiteral("\r\n                                            ");
#nullable restore
#line 138 "C:\Users\hp\Documents\GitHub\livestock-platform\AnimalFarmsMarket\AnimalFarmsMarket.Core\Views\Shared\_LocationPartial.cshtml"
                                       Write(item.marketName);

#line default
#line hidden
#nullable disable
                WriteLiteral(" <span class=\"badge bg-warning float-right bg-info\">21</span>\r\n\r\n                                        ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-market", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 137 "C:\Users\hp\Documents\GitHub\livestock-platform\AnimalFarmsMarket\AnimalFarmsMarket.Core\Views\Shared\_LocationPartial.cshtml"
                                                                                                                                                 WriteLiteral(item.marketName);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["market"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-market", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["market"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginWriteTagHelperAttribute();
#nullable restore
#line 137 "C:\Users\hp\Documents\GitHub\livestock-platform\AnimalFarmsMarket\AnimalFarmsMarket.Core\Views\Shared\_LocationPartial.cshtml"
                                                                                                                                                                                       WriteLiteral(item.location);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["location"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-location", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["location"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n\r\n                                    </li>\r\n                                </ul>\r\n\r\n                            </div>\r\n\r\n                        </div>\r\n                    </div>");
#nullable restore
#line 148 "C:\Users\hp\Documents\GitHub\livestock-platform\AnimalFarmsMarket\AnimalFarmsMarket.Core\Views\Shared\_LocationPartial.cshtml"
                          }

#line default
#line hidden
#nullable disable
#nullable restore
#line 148 "C:\Users\hp\Documents\GitHub\livestock-platform\AnimalFarmsMarket\AnimalFarmsMarket.Core\Views\Shared\_LocationPartial.cshtml"
                           }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </div>\r\n\r\n\r\n                </div>\r\n\r\n            </div>\r\n\r\n        </div>\r\n\r\n\r\n    </div>\r\n</nav>");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public Microsoft.AspNetCore.Http.IHttpContextAccessor HttpContextAccessor { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public IAuthorizationService _authorization { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public UserManager<AppUser> _userManager { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<LocationMarketVm>> Html { get; private set; }
    }
}
#pragma warning restore 1591
