#pragma checksum "E:\Project\New folder\UMS\UMS\Areas\Admin\Views\Course\_CourseTable.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "e78521964ddb562edc6c1902cc91b7c7fa04c484"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Admin_Views_Course__CourseTable), @"mvc.1.0.view", @"/Areas/Admin/Views/Course/_CourseTable.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e78521964ddb562edc6c1902cc91b7c7fa04c484", @"/Areas/Admin/Views/Course/_CourseTable.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"09e50ace8fe3b4184cf126cd47ae178edfbfa883", @"/Areas/Admin/Views/_ViewImports.cshtml")]
    public class Areas_Admin_Views_Course__CourseTable : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<UMS.Models.ViewModels.CourseVM>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-info"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("data-toggle", new global::Microsoft.AspNetCore.Html.HtmlString("tooltip"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("data-placement", new global::Microsoft.AspNetCore.Html.HtmlString("top"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("title", new global::Microsoft.AspNetCore.Html.HtmlString("Prerequisite"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "ManagePrerequisite", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-primary"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("title", new global::Microsoft.AspNetCore.Html.HtmlString("Edit"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_7 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Upsert", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
            WriteLiteral(@"
<div class=""table-responsive"">
    <table class=""table table-hover table-bordered"">
        <thead class=""table-secondary "">
            <tr class=""table-head"">
                <th>#</th>
                <th>Name</th>
                <th>Initial</th>
                <th>Department</th>
                <th class=""text-center"">Prerequisites</th>
  
                <th>Actions</th>

            </tr>
        </thead>
        <tbody class=""table-body"">
");
#nullable restore
#line 18 "E:\Project\New folder\UMS\UMS\Areas\Admin\Views\Course\_CourseTable.cshtml"
             if (Model.CourseList.Count() > 0)
            {
                var i = 0;
                foreach (var course in Model.CourseList)
                {
                    i++;

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <tr>\r\n                        <td>");
#nullable restore
#line 25 "E:\Project\New folder\UMS\UMS\Areas\Admin\Views\Course\_CourseTable.cshtml"
                       Write(i);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                        \r\n                        <td>");
#nullable restore
#line 27 "E:\Project\New folder\UMS\UMS\Areas\Admin\Views\Course\_CourseTable.cshtml"
                       Write(course.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                        <td>");
#nullable restore
#line 28 "E:\Project\New folder\UMS\UMS\Areas\Admin\Views\Course\_CourseTable.cshtml"
                       Write(course.Initial);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                        <td>");
#nullable restore
#line 29 "E:\Project\New folder\UMS\UMS\Areas\Admin\Views\Course\_CourseTable.cshtml"
                       Write(course.Department.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n\r\n                        <td>\r\n");
#nullable restore
#line 32 "E:\Project\New folder\UMS\UMS\Areas\Admin\Views\Course\_CourseTable.cshtml"
                             foreach (var coursePre in course.CourseToCoursePrerequisites)
                            {
                                

#line default
#line hidden
#nullable disable
#nullable restore
#line 34 "E:\Project\New folder\UMS\UMS\Areas\Admin\Views\Course\_CourseTable.cshtml"
                            Write(coursePre.CoursePrerequisite.InitialName);

#line default
#line hidden
#nullable disable
#nullable restore
#line 34 "E:\Project\New folder\UMS\UMS\Areas\Admin\Views\Course\_CourseTable.cshtml"
                                                                           
                                if (course.CourseToCoursePrerequisites.Count() > 1)
                                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    <span>,</span>\r\n");
#nullable restore
#line 38 "E:\Project\New folder\UMS\UMS\Areas\Admin\Views\Course\_CourseTable.cshtml"
                                }
                            }

#line default
#line hidden
#nullable disable
            WriteLiteral("                        </td>\r\n                        <td>\r\n                            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "e78521964ddb562edc6c1902cc91b7c7fa04c4849220", async() => {
                WriteLiteral("<i class=\"fas fa-book-open\"></i>");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 42 "E:\Project\New folder\UMS\UMS\Areas\Admin\Views\Course\_CourseTable.cshtml"
                                                                                                                                                      WriteLiteral(course.Id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "e78521964ddb562edc6c1902cc91b7c7fa04c48411865", async() => {
                WriteLiteral("<i class=\"fas fa-edit\"></i>");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_7.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_7);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 43 "E:\Project\New folder\UMS\UMS\Areas\Admin\Views\Course\_CourseTable.cshtml"
                                                                                                                                     WriteLiteral(course.Id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                            <button class=\"btn btn-danger deleteBtn\" data-toggle=\"tooltip\" data-placement=\"top\" title=\"Delete\" data-id=\"");
#nullable restore
#line 44 "E:\Project\New folder\UMS\UMS\Areas\Admin\Views\Course\_CourseTable.cshtml"
                                                                                                                                   Write(course.Id);

#line default
#line hidden
#nullable disable
            WriteLiteral("\"><i class=\"fas fa-trash-alt\"></i></button>\r\n                        </td>\r\n                    </tr>\r\n");
#nullable restore
#line 47 "E:\Project\New folder\UMS\UMS\Areas\Admin\Views\Course\_CourseTable.cshtml"
                }
            }
            else
            {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                <tr>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td class=""text-danger"">No Course  Available</td>

                    <td></td>
                    <td></td>
                </tr>
");
#nullable restore
#line 60 "E:\Project\New folder\UMS\UMS\Areas\Admin\Views\Course\_CourseTable.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </tbody>\r\n    </table>\r\n</div>\r\n\r\n<div class=\"text-center col-12\">\r\n");
#nullable restore
#line 66 "E:\Project\New folder\UMS\UMS\Areas\Admin\Views\Course\_CourseTable.cshtml"
     if (Model.Pager != null && Model.Pager.TotalPages > 1)
    {
        

#line default
#line hidden
#nullable disable
#nullable restore
#line 68 "E:\Project\New folder\UMS\UMS\Areas\Admin\Views\Course\_CourseTable.cshtml"
         if (Model.Pager.CurrentPage > 1)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <button class=\"btn btn-outline-info page-buttons\" data-pageNo=\"1\">First</button>\r\n            <button class=\"btn btn-outline-info page-buttons\" data-pageNo=\"");
#nullable restore
#line 71 "E:\Project\New folder\UMS\UMS\Areas\Admin\Views\Course\_CourseTable.cshtml"
                                                                       Write(Model.Pager.CurrentPage+1);

#line default
#line hidden
#nullable disable
            WriteLiteral("\">Next</button>\r\n");
#nullable restore
#line 72 "E:\Project\New folder\UMS\UMS\Areas\Admin\Views\Course\_CourseTable.cshtml"
        }

#line default
#line hidden
#nullable disable
#nullable restore
#line 72 "E:\Project\New folder\UMS\UMS\Areas\Admin\Views\Course\_CourseTable.cshtml"
         
        for (int page = Model.Pager.StartPage; page <= Model.Pager.EndPage; page++)
        {
            string activeClass = Model.Pager.CurrentPage == page ? "active bg-info" : "";

#line default
#line hidden
#nullable disable
            WriteLiteral("            <button");
            BeginWriteAttribute("class", " class=\"", 3113, "\"", 3167, 4);
            WriteAttributeValue("", 3121, "btn", 3121, 3, true);
            WriteAttributeValue(" ", 3124, "btn-outline-info", 3125, 17, true);
            WriteAttributeValue(" ", 3141, "page-buttons", 3142, 13, true);
#nullable restore
#line 76 "E:\Project\New folder\UMS\UMS\Areas\Admin\Views\Course\_CourseTable.cshtml"
WriteAttributeValue(" ", 3154, activeClass, 3155, 12, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" data-pageNo=\"");
#nullable restore
#line 76 "E:\Project\New folder\UMS\UMS\Areas\Admin\Views\Course\_CourseTable.cshtml"
                                                                                    Write(page);

#line default
#line hidden
#nullable disable
            WriteLiteral("\">");
#nullable restore
#line 76 "E:\Project\New folder\UMS\UMS\Areas\Admin\Views\Course\_CourseTable.cshtml"
                                                                                             Write(page);

#line default
#line hidden
#nullable disable
            WriteLiteral("</button>\r\n");
#nullable restore
#line 77 "E:\Project\New folder\UMS\UMS\Areas\Admin\Views\Course\_CourseTable.cshtml"
        }
        if (Model.Pager.CurrentPage < Model.Pager.EndPage)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <button class=\"btn btn-outline-info page-buttons\" data-pageNo=\"");
#nullable restore
#line 80 "E:\Project\New folder\UMS\UMS\Areas\Admin\Views\Course\_CourseTable.cshtml"
                                                                       Write(Model.Pager.CurrentPage+1);

#line default
#line hidden
#nullable disable
            WriteLiteral("\">Next</button>\r\n            <button class=\"btn btn-outline-info page-buttons\" data-pageNo=\"");
#nullable restore
#line 81 "E:\Project\New folder\UMS\UMS\Areas\Admin\Views\Course\_CourseTable.cshtml"
                                                                       Write(Model.Pager.EndPage);

#line default
#line hidden
#nullable disable
            WriteLiteral("\">Last</button>\r\n");
#nullable restore
#line 82 "E:\Project\New folder\UMS\UMS\Areas\Admin\Views\Course\_CourseTable.cshtml"
        }
    }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</div>

<script>
    $(document).ready(function () {

        $("".deleteBtn"").click(function () {
            swal({
                title: ""Are you sure that you want to delete this data"",
                text: ""If you once delete this you can not restore it!"",
                icon: ""warning"",
                dangerMode: true,
                buttons: true
            }).then((willDelete) => {
                if (willDelete) {
                    $.ajax({
                        url: '");
#nullable restore
#line 99 "E:\Project\New folder\UMS\UMS\Areas\Admin\Views\Course\_CourseTable.cshtml"
                         Write(Url.Action("Delete", "Course"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"',
                        type: ""POST"",
                        data: {
                            id: $(this).attr(""data-id"")
                        }
                    }).done(function (response) {

                        swal(""Successfully Deleted"", {
                            icon: ""success""
                        });
                        $("".loader"").fadeOut(1000);
                        $("".wrapper"").fadeIn(1000);
                        $(""#table-container"").html(response);
                    }).fail(function (xhr) {
                        $("".loader"").fadeOut(1000);
                        $("".wrapper"").fadeIn(1000);
                        toastr.error(""Something went wrong"");
                    })
                }
            })
        });

        $("".page-buttons"").click(function () {
            $.ajax({
                url: '");
#nullable restore
#line 123 "E:\Project\New folder\UMS\UMS\Areas\Admin\Views\Course\_CourseTable.cshtml"
                 Write(Url.Action("CourseTable", "Course"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\',\r\n                data: {\r\n                    pageNo: $(this).attr(\"data-pageNo\"),\r\n                    searchValue: \'");
#nullable restore
#line 126 "E:\Project\New folder\UMS\UMS\Areas\Admin\Views\Course\_CourseTable.cshtml"
                             Write(Model.Search);

#line default
#line hidden
#nullable disable
            WriteLiteral("\',\r\n                    departmentId: \'");
#nullable restore
#line 127 "E:\Project\New folder\UMS\UMS\Areas\Admin\Views\Course\_CourseTable.cshtml"
                              Write(Model.DepartmentId);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"'
                }
            }).done(function (response) {
                $("".loader"").fadeOut(1000);
                $("".wrapper"").fadeIn(1000);
                $(""#table-container"").html(response);
            }).fail(function (xhr) {
                $("".loader"").fadeOut(1000);
                $("".wrapper"").fadeIn(1000);
                toastr.error(""Something Went Wrong"");
            })
        })

    });


</script>
");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<UMS.Models.ViewModels.CourseVM> Html { get; private set; }
    }
}
#pragma warning restore 1591
