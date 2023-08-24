using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Omu.Awem.Helpers;
using Omu.AwesomeMvc;

namespace CASPAR.ApplicationCore.Helpers.Awesome
{
    public static class CrudHelpers
    {
        private static IUrlHelper GetUrlHelper<T>(IHtmlHelper<T> html)
        {
            return ((IUrlHelperFactory)html.ViewContext.HttpContext.RequestServices.GetService(typeof(IUrlHelperFactory))).GetUrlHelper(html.ViewContext);
        }

        /*beging*/
        public static IHtmlContent InitCrudPopupsForGrid<T>(this IHtmlHelper<T> html, string gridId, string crudController, int createPopupHeight = 430, int maxWidth = 0)
        {
            var url = GetUrlHelper(html);

            gridId = html.Awe().GetContextPrefix() + gridId;

            var result =
            html.Awe()
                .InitPopupForm()
                .Name("create" + gridId)
                .Group(gridId)
                .Height(createPopupHeight)
                .MaxWidth(maxWidth)
                .Url(url.Action("Create", crudController))
                .Title("Create item")
                .Modal()
                .Success("utils.itemCreated('" + gridId + "')")
                .ToString()

            + html.Awe()
                  .InitPopupForm()
                  .Name("edit" + gridId)
                  .Group(gridId)
                  .Height(createPopupHeight)
                  .MaxWidth(maxWidth)
                  .Url(url.Action("Edit", crudController))
                  .Title("Edit item")
                  .Modal()
                  .Success("utils.itemEdited('" + gridId + "')")

            + html.Awe()
                  .InitPopupForm()
                  .Name("delete" + gridId)
                  .Group(gridId)
                  .Url(url.Action("Delete", crudController))
                  .Success("utils.itemDeleted('" + gridId + "')")
                  .OnLoad("utils.delConfirmLoad('" + gridId + "')") // calls grid.api.select and animates the row
                  .Height(200)
                  .Modal();

            return new HtmlString(result);
        }
        /*endg*/

        public static IHtmlContent InitCrudForGridNest<T>(this IHtmlHelper<T> html, string gridId, string crudController)
        {
            var url = GetUrlHelper(html);
            gridId = html.Awe().GetContextPrefix() + gridId;

            var result =
                html.Awe()
                    .InitPopupForm()
                    .Name("create" + gridId)
                    .Group(gridId)
                    .Url(url.Action("Create", crudController))
                    .Mod(o => o.Inline().ShowHeader(false))
                    .Success("utils.itemCreated('" + gridId + "')")
                    .ToString()
                + html.Awe()
                      .InitPopupForm()
                      .Name("edit" + gridId)
                      .Group(gridId)
                      .Url(url.Action("Edit", crudController))
                      .Mod(o => o.Inline().ShowHeader(false))
                      .Success("utils.itemEdited('" + gridId + "')")
                + html.Awe()
                      .InitPopupForm()
                      .Name("delete" + gridId)
                      .Group(gridId)
                      .Url(url.Action("Delete", crudController))
                      .Mod(o => o.Inline().ShowHeader(false))
                      .Success("utils.itemDeleted('" + gridId + "')");

            return new HtmlString(result);
        }

        /*beginal*/
        public static IHtmlContent InitCrudPopupsForAjaxList<T>(
           this IHtmlHelper<T> html, string ajaxListId, string controller, string popupName)
        {
            var url = GetUrlHelper(html);

            var result =
                html.Awe()
                    .InitPopupForm()
                    .Name("create" + popupName)
                    .Url(url.Action("Create", controller))
                    .Height(430)
                    .Success("utils.itemCreatedAlTbl('" + ajaxListId + "')")
                    .Group(ajaxListId)
                    .Title("create item")
                    .ToString()

                + html.Awe()
                      .InitPopupForm()
                      .Name("edit" + popupName)
                      .Url(url.Action("Edit", controller))
                      .Height(430)
                      .Success("utils.itemEditedAl('" + ajaxListId + "')")
                      .Group(ajaxListId)
                      .Title("edit item")

                + html.Awe()
                      .InitPopupForm()
                      .Name("delete" + popupName)
                      .Url(url.Action("Delete", controller))
                      .Success("utils.itemDeletedAl('" + ajaxListId + "')")
                      .Group(ajaxListId)
                      .OkText("Yes")
                      .CancelText("No")
                      .Height(200)
                      .Title("delete item");

            return new HtmlString(result);
        }
        /*endal*/

        public static IHtmlContent InitDeletePopupForGrid<T>(this IHtmlHelper<T> html, string gridId, string crudController, string action = "Delete")
        {
            var url = GetUrlHelper(html);
            gridId = html.Awe().GetContextPrefix() + gridId;

            var result =
                html.Awe()
                  .InitPopupForm()
                  .Name("delete" + gridId)
                  .Group(gridId)
                  .Url(url.Action(action, crudController))
                  .Success("utils.itemDeleted('" + gridId + "')")
                  .OnLoad("utils.delConfirmLoad('" + gridId + "')") // calls grid.api.select and animates the row
                  .Height(200)
                  .Modal()
                  .ToString();

            return new HtmlString(result);
        }
    }
}