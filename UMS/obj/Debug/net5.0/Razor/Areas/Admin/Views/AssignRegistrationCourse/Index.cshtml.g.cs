#pragma checksum "E:\Project\New folder\UMS\UMS\Areas\Admin\Views\AssignRegistrationCourse\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "4fcaea366aeff0fa2883ae82faa77f0461110e7b"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Admin_Views_AssignRegistrationCourse_Index), @"mvc.1.0.view", @"/Areas/Admin/Views/AssignRegistrationCourse/Index.cshtml")]
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
#line 1 "E:\Project\New folder\UMS\UMS\Areas\Admin\Views\_ViewImports.cshtml"
using UMS;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "E:\Project\New folder\UMS\UMS\Areas\Admin\Views\_ViewImports.cshtml"
using UMS.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"4fcaea366aeff0fa2883ae82faa77f0461110e7b", @"/Areas/Admin/Views/AssignRegistrationCourse/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"09e50ace8fe3b4184cf126cd47ae178edfbfa883", @"/Areas/Admin/Views/_ViewImports.cshtml")]
    public class Areas_Admin_Views_AssignRegistrationCourse_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<UMS.Models.ViewModels.AssinRegistrationCourseVM>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("form-control"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("semesterId"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("departmentId"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-success form-control"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Upsert", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.SelectTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "E:\Project\New folder\UMS\UMS\Areas\Admin\Views\AssignRegistrationCourse\Index.cshtml"
  
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div class=\"container\" id=\"main-container\">\r\n    <span id=\"messageBox\" class=\"d-none\">");
#nullable restore
#line 7 "E:\Project\New folder\UMS\UMS\Areas\Admin\Views\AssignRegistrationCourse\Index.cshtml"
                                    Write(TempData["message"]);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</span>
    <h2 class=""text-primary text-center border-bottom"">Assigned Registration Courses</h2>
    <div class=""row pt-4"">
        <div class=""col-6"">
            <div class=""input-group"">
                <input class=""form-control"" placeholder=""Search..."" id=""search"" />
                <div class=""input-group-prepend"">
                    <div class=""input-group-text""><i class=""fas fa-search""></i></div>
                </div>
            </div>
        </div>
        <div class=""col-6"">
            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("select", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "4fcaea366aeff0fa2883ae82faa77f0461110e7b6599", async() => {
                WriteLiteral("\r\n                ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "4fcaea366aeff0fa2883ae82faa77f0461110e7b6875", async() => {
                    WriteLiteral("----Select Semester----");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                BeginWriteTagHelperAttribute();
                __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
                __tagHelperExecutionContext.AddHtmlAttribute("selected", Html.Raw(__tagHelperStringValueBuffer), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.Minimized);
                BeginWriteTagHelperAttribute();
                __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
                __tagHelperExecutionContext.AddHtmlAttribute("disabled", Html.Raw(__tagHelperStringValueBuffer), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.Minimized);
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_0.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n            ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.SelectTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
#nullable restore
#line 19 "E:\Project\New folder\UMS\UMS\Areas\Admin\Views\AssignRegistrationCourse\Index.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper.Items = Model.SemesterList;

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-items", __Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper.Items, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n        </div>\r\n        <div class=\"col-6\">\r\n            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("select", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "4fcaea366aeff0fa2883ae82faa77f0461110e7b10170", async() => {
                WriteLiteral("\r\n                ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "4fcaea366aeff0fa2883ae82faa77f0461110e7b10447", async() => {
                    WriteLiteral("----Select Department----");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                BeginWriteTagHelperAttribute();
                __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
                __tagHelperExecutionContext.AddHtmlAttribute("selected", Html.Raw(__tagHelperStringValueBuffer), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.Minimized);
                BeginWriteTagHelperAttribute();
                __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
                __tagHelperExecutionContext.AddHtmlAttribute("disabled", Html.Raw(__tagHelperStringValueBuffer), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.Minimized);
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_0.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n            ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.SelectTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
#nullable restore
#line 24 "E:\Project\New folder\UMS\UMS\Areas\Admin\Views\AssignRegistrationCourse\Index.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper.Items = Model.DepartmentList;

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-items", __Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper.Items, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
        </div>
       
        <div class=""col-6"">
            <input class=""btn btn-info form-control"" type=""submit"" value=""Search"" id=""searchBtn"" />
        </div>
        <div class=""col-6"">
            <input class=""btn btn-primary form-control"" type=""submit"" value=""Reset"" id=""resetBtn"" />
        </div>
        <div class=""col-6"">
            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "4fcaea366aeff0fa2883ae82faa77f0461110e7b14064", async() => {
                WriteLiteral("<i class=\"fas fa-plus\"></i> Create New");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_5.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n        </div>\r\n        <div id=\"table-container\" class=\"col-lg-12 col-md-12 col-sm-12 mb-5\">\r\n\r\n        </div>\r\n    </div>\r\n</div>\r\n\r\n");
            DefineSection("Scripts", async() => {
                WriteLiteral("\r\n\r\n    <script>\r\n\r\n\r\n\r\n        $(document).ready(function () {\r\n            getData();\r\n        });\r\n        function getData() {\r\n            $.ajax({\r\n                url: \'");
#nullable restore
#line 55 "E:\Project\New folder\UMS\UMS\Areas\Admin\Views\AssignRegistrationCourse\Index.cshtml"
                 Write(Url.Action("AssignCourseTable", "AssignRegistrationCourse"));

#line default
#line hidden
#nullable disable
                WriteLiteral(@"',
                type: ""GET""
            }).done(function (response) {
                $("".loader"").fadeOut(1000);
                $("".wrapper"").fadeIn(1000);
                $(""#table-container"").html(response);
            }).fail(function (xhr) {
                $("".loader"").fadeOut(1000);
                $("".wrapper"").fadeIn(1000);
                toastr.error(""Something went wrong"");

            })
        }
        $(""#searchBtn"").click(function () {
            var searchValue = $(""#search"").val();
            var departmentId = $(""#departmentId"").val();
            var semesterId = $(""#semesterId"").val();
            var year = $(""#yearId"").val();
            $(""#department"").html(departmentId);
            if (searchValue != """" || departmentId != """" || semesterId != """" || year!="""") {
                $.ajax({
                    url: '");
#nullable restore
#line 76 "E:\Project\New folder\UMS\UMS\Areas\Admin\Views\AssignRegistrationCourse\Index.cshtml"
                     Write(Url.Action("AssignCourseTable", "AssignRegistrationCourse"));

#line default
#line hidden
#nullable disable
                WriteLiteral(@"',
                    type: ""GET"",
                    data: {
                        searchValue: searchValue,
                        departmentId: departmentId,
                        semesterId: semesterId,
                        year: year
                    }
                }).done(function (response) {
                    $("".loader"").fadeOut(1000);
                    $("".wrapper"").fadeIn(1000);
                    $(""#table-container"").html(response);
                }).fail(function (xhr) {
                    $("".loader"").fadeOut(1000);
                    $("".wrapper"").fadeIn(1000);
                    toastr.error(""Something went error"");
                })
            }
            else {
                $("".loader"").fadeOut(1000);
                $("".wrapper"").fadeIn(1000);
                toastr.error(""Nothing to Search"");
            }

        });

        var messageText = $(""#messageBox"").text();
        if (messageText != """") {
            toastr.success(");
                WriteLiteral(@"messageText);
            $(""#messageBox"").text('');
        }

        $(""#resetBtn"").click(function () {
            var searchValue = $(""#search"").val();
            var departmentId = $(""#departmentId"").val();
            var semesterId = $(""#semesterId"").val();
            var year = $(""#yearId"").val();
              if (searchValue != """" || departmentId != """" || semesterId != """" || year != """") {
                    var searchValue = $(""#search"").val('');
                    var departmentId = $(""#departmentId"").val('');
                    var semesterId = $(""#semesterId"").val('');
                    var year = $(""#yearId"").val('');
                    $.ajax({
                        url: '");
#nullable restore
#line 119 "E:\Project\New folder\UMS\UMS\Areas\Admin\Views\AssignRegistrationCourse\Index.cshtml"
                         Write(Url.Action("AssignCourseTable", "AssignRegistrationCourse"));

#line default
#line hidden
#nullable disable
                WriteLiteral(@"',
                        type: ""GET"",

                    }).done(function (response) {
                        $("".loader"").fadeOut(1000);
                        $("".wrapper"").fadeIn(1000);
                        $(""#table-container"").html(response);
                    }).fail(function (xhr) {
                        $("".loader"").fadeOut(1000);
                        $("".wrapper"").fadeIn(1000);
                        toastr.error(""Something went error"");
                    })
              }
            else {
                $("".loader"").fadeOut(1000);
                $("".wrapper"").fadeIn(1000);
                toastr.error(""You can not reset at this time"");
            }
        });
    </script>
");
            }
            );
            WriteLiteral("\r\n\r\n\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<UMS.Models.ViewModels.AssinRegistrationCourseVM> Html { get; private set; }
    }
}
#pragma warning restore 1591
