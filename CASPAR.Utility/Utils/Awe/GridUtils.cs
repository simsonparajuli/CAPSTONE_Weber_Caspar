using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Omu.AwesomeMvc;
using Omu.Awem.Helpers;
using System.Collections.Generic;

namespace CASPAR.Utility.Utils.Awe
{
    public static class GridUtils
    {
        private const string editContent = "Edit";
        private const string deleteContent = "Delete";

        private static string GetPopupName<T>(this IHtmlHelper<T> html, string action, string gridId)
        {
            return action + html.Awe().GetContextPrefix() + gridId;
        }

        public static string InlineCancelBtn<T>(this IHtmlHelper<T> html, string text = "Cancel")
        {
            return html.Awe()
                .Button()
                .Text(text)
                .CssClass("o-glcanb awe-nonselect o-gl o-glbtn").ToString();
        }

        public static string InlineDeleteFormatForGrid<T>(this IHtmlHelper<T> html, string gridId, string key = "Id", bool nofocus = false, string text = "Cancel")
        {
            var popupName = html.GetPopupName("delete", gridId);

            return DeleteFormat(popupName, key, btnClass: "o-glh", nofocus: nofocus) +
                   html.InlineCancelBtn(text);
        }

        public static IHtmlContent InlineCreateButtonForGrid<T>(this IHtmlHelper<T> html, string gridId, object parameters = null, string text = "Create")
        {
            gridId = html.Awe().GetContextPrefix() + gridId;
            var parms = Autil.JsonEncode(parameters);

            return html.Awe().Button()
                .Text(text)
                .OnClick(string.Format("$('#{0}').data('api').inlineCreate({1})", gridId, parms));
        }

        public static IHtmlContent CreateButtonForGrid<T>(this IHtmlHelper<T> html, string gridId, object parameters = null, string text = "Create")
        {
            return html.Awe().Button()
                .Text(text)
                .OnClick(html.Awe().OpenPopup(html.GetPopupName("create", gridId)).Params(parameters));
        }

        public static string EditFormatForGrid<T>(
            this IHtmlHelper<T> html, 
            string gridId, 
            string key = "Id", 
            bool setId = false, 
            bool nofocus = false, 
            int? height = null,
            string btnContent = null)
        {
            var popupName = html.GetPopupName("edit", gridId);

            var click = html.Awe().OpenPopup(popupName).Params(new { id = $".({key})" });
            if (height.HasValue)
            {
                click.Height(height.Value);
            }

            var button = html.Awe().Button()
                .CssClass("awe-nonselect editbtn")
                .HtmlAttributes(new { aria_label = "edit" })
                .Text(btnContent ?? editContent)
                .OnClick(click);

            var attrdict = new Dictionary<string, object>();

            if (setId)
            {
                attrdict.Add("id", $"gbtn{popupName}.{key}");
            }

            if (nofocus)
            {
                attrdict.Add("tabindex", "-1");
            }

            button.HtmlAttributes(attrdict);

            return button.ToString();
        }

        public static string DeleteFormatForGrid<T>(this IHtmlHelper<T> html, string gridId, string key = "Id", bool nofocus = false)
        {
            gridId = html.Awe().GetContextPrefix() + gridId;

            return DeleteFormatForGrid(gridId, key, nofocus);
        }

        public static string EditFormat(string popupName, string key = "Id", bool setId = false, bool nofocus = false)
        {
            var idattr = "";
            if (setId)
            {
                idattr = $"id = 'gbtn{popupName}.{key}'";
            }

            var tabindex = nofocus ? "tabindex = \"-1\"" : string.Empty;

            return string.Format("<button aria-label=\"edit\" type=\"button\" class=\"awe-btn awe-nonselect editbtn\" {3} {2}" +
                                 " onclick=\"awe.open('{0}', {{ params:{{ id: '.({1})' }} }}, event)\">{4}</button>",
                popupName, key, idattr, tabindex, editContent);
        }

        public static string DeleteFormat(string popupName, string key = "Id", string deleteContent = null, string btnClass = null, bool nofocus = false)
        {
            if (deleteContent == null)
            {
                deleteContent = "Delete";
            }

            var tabindex = nofocus ? "tabindex = \"-1\"" : string.Empty;

            return string.Format("<button aria-label=\"delete\" type=\"button\" class=\"awe-btn awe-nonselect delbtn {3}\"" +
                                 " {4} onclick=\"awe.open('{0}', {{ params:{{ id: '.({1})' }} }}, event)\">{2}</button>",
                popupName, key, deleteContent, btnClass, tabindex);
        }

        public static string InlineEditFormat(bool nofocus = false)
        {
            var tabindex = nofocus ? "tabindex = \"-1\"" : string.Empty;
            return string.Format("<button type=\"button\" class=\"awe-btn o-gledtb awe-nonselect o-glh o-glbtn\" {0} ><span class=\"btn-cont\">Edit</span></button>" +
                                 "<button type=\"button\" class=\"awe-btn o-glsvb awe-nonselect o-gl o-glbtn\"><span class=\"btn-cont\">Save</span></button>", tabindex);
        }

        public static string EditFormatForGrid(string gridId, string key = "Id", bool setId = false, bool nofocus = false)
        {
            return EditFormat("edit" + gridId, key, setId, nofocus);
        }

        public static string DeleteFormatForGrid(string gridId, string key = "Id", bool nofocus = false)
        {
            return DeleteFormat("delete" + gridId, key, null, null, nofocus);
        }

        public static string EditGridNestFormat()
        {
            return "<button type='button' class='awe-btn editnst'>" + editContent + "</button>";
        }

        public static string DeleteGridNestFormat()
        {
            return "<button type='button' class='awe-btn delnst'>" + deleteContent + "</button>";
        }

        public static string AddChildFormat()
        {
            return "<button type='button' class='awe-btn awe-nonselect' onclick=\"awe.open('createNode', { params:{ parentId: '.(Id)' } })\">add child</button>";
        }

        public static string MoveBtn()
        {
            return "<button type=\"button\" class=\"awe-movebtn awe-btn\" tabindex=\"-1\"><i class=\"awe-icon\"></i></button>";
        }

        public static Column EditColumnForGrid<T>(this IHtmlHelper<T> html, string gridId, string btnContent = null)
        {
            return new Column { ClientFormat = html.EditFormatForGrid(gridId, btnContent: btnContent), Width = EditBtnWidth }.Mod(o => o.Nohide());
        }

        public static Column DeleteColumnForGrid<T>(this IHtmlHelper<T> html, string gridId)
        {
            return new Column { ClientFormat = html.DeleteFormatForGrid(gridId), Width = DeleteBtnWidth }.Mod(o => o.Nohide());
        }

        public static Column InlEditColumn<T>(this IHtmlHelper<T> html)
        {
            return new Column { ClientFormat = InlineEditFormat(), Width = EditBtnWidth }.Mod(o => o.Nohide());
        }

        public static Column InlDeleteColumn<T>(this IHtmlHelper<T> html, string gridId)
        {
            return new Column { ClientFormat = html.InlineDeleteFormatForGrid(gridId), Width = DeleteBtnWidth }.Mod(o => o.Nohide());
        }

        public const int EditBtnWidth = 90;
        public const int DeleteBtnWidth = 100;
    }
}