#pragma checksum "C:\Users\luiz.veronezzi\Documents\basicos\Loja\Loja\Loja\Views\Parceiro\_ListaParceiros.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "033b8ef03ad44ff8864f220c8c0c10e02b36dcad"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Parceiro__ListaParceiros), @"mvc.1.0.view", @"/Views/Parceiro/_ListaParceiros.cshtml")]
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
#line 1 "C:\Users\luiz.veronezzi\Documents\basicos\Loja\Loja\Loja\Views\_ViewImports.cshtml"
using Loja;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\luiz.veronezzi\Documents\basicos\Loja\Loja\Loja\Views\_ViewImports.cshtml"
using Loja.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"033b8ef03ad44ff8864f220c8c0c10e02b36dcad", @"/Views/Parceiro/_ListaParceiros.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"28fc758d369a71fad9b35490d7ee3307e3aea246", @"/Views/_ViewImports.cshtml")]
    public class Views_Parceiro__ListaParceiros : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<Data.Models.ParceiroModel>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n<div class=\"col-12 shape p-3\">\r\n    <table id=\"tableParceiro\" class=\"table table-hover table-sm\">\r\n        <thead class=\"table-light\">\r\n            <tr>\r\n                <th>");
#nullable restore
#line 7 "C:\Users\luiz.veronezzi\Documents\basicos\Loja\Loja\Loja\Views\Parceiro\_ListaParceiros.cshtml"
               Write(Html.DisplayNameFor(m => Model.FirstOrDefault().Nome));

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n                <th>");
#nullable restore
#line 8 "C:\Users\luiz.veronezzi\Documents\basicos\Loja\Loja\Loja\Views\Parceiro\_ListaParceiros.cshtml"
               Write(Html.DisplayNameFor(m => Model.FirstOrDefault().Celular));

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n                <th>");
#nullable restore
#line 9 "C:\Users\luiz.veronezzi\Documents\basicos\Loja\Loja\Loja\Views\Parceiro\_ListaParceiros.cshtml"
               Write(Html.DisplayNameFor(m => Model.FirstOrDefault().descrTipoParc));

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n                <th>");
#nullable restore
#line 10 "C:\Users\luiz.veronezzi\Documents\basicos\Loja\Loja\Loja\Views\Parceiro\_ListaParceiros.cshtml"
               Write(Html.DisplayNameFor(m => Model.FirstOrDefault().descrCargo));

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n                <th>Ação</th>\r\n\r\n            </tr>\r\n        </thead>\r\n\r\n        <tbody>\r\n");
#nullable restore
#line 17 "C:\Users\luiz.veronezzi\Documents\basicos\Loja\Loja\Loja\Views\Parceiro\_ListaParceiros.cshtml"
             foreach (var item in Model)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <tr>\r\n                <td>");
#nullable restore
#line 20 "C:\Users\luiz.veronezzi\Documents\basicos\Loja\Loja\Loja\Views\Parceiro\_ListaParceiros.cshtml"
               Write(item.Nome);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 21 "C:\Users\luiz.veronezzi\Documents\basicos\Loja\Loja\Loja\Views\Parceiro\_ListaParceiros.cshtml"
               Write(item.Celular);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 22 "C:\Users\luiz.veronezzi\Documents\basicos\Loja\Loja\Loja\Views\Parceiro\_ListaParceiros.cshtml"
               Write(item.descrTipoParc);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 23 "C:\Users\luiz.veronezzi\Documents\basicos\Loja\Loja\Loja\Views\Parceiro\_ListaParceiros.cshtml"
               Write(item.descrCargo);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>\r\n                    <buttom");
            BeginWriteAttribute("onclick", " onclick=\"", 917, "\"", 949, 3);
            WriteAttributeValue("", 927, "Manutencao(", 927, 11, true);
#nullable restore
#line 25 "C:\Users\luiz.veronezzi\Documents\basicos\Loja\Loja\Loja\Views\Parceiro\_ListaParceiros.cshtml"
WriteAttributeValue("", 938, item.Id, 938, 8, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 946, ",1)", 946, 3, true);
            EndWriteAttribute();
            WriteLiteral(" data-toggle=\"tooltip\" title=\"Editar\"><i class=\"fas fa-pencil-alt fa-lg\" style=\"color:gray;\"></i></buttom>\r\n                    <buttom");
            BeginWriteAttribute("onclick", " onclick=\"", 1085, "\"", 1117, 3);
            WriteAttributeValue("", 1095, "Manutencao(", 1095, 11, true);
#nullable restore
#line 26 "C:\Users\luiz.veronezzi\Documents\basicos\Loja\Loja\Loja\Views\Parceiro\_ListaParceiros.cshtml"
WriteAttributeValue("", 1106, item.Id, 1106, 8, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 1114, ",0)", 1114, 3, true);
            EndWriteAttribute();
            WriteLiteral(" data-toggle=\"tooltip\" title=\"Excluir\"><i class=\"far fa-trash-alt fa-lg\" style=\"color:gray;\"></i></buttom>\r\n                </td>\r\n            </tr>\r\n");
#nullable restore
#line 29 "C:\Users\luiz.veronezzi\Documents\basicos\Loja\Loja\Loja\Views\Parceiro\_ListaParceiros.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </tbody>\r\n    </table>\r\n</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<Data.Models.ParceiroModel>> Html { get; private set; }
    }
}
#pragma warning restore 1591