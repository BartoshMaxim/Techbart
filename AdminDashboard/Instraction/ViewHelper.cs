using Techbart.DB.Interfaces;
using Techbart.DB.Repositories;
using System.Web.Mvc;

namespace AdminDashboard.Instraction
{
	public static class ViewHelper
    {
        public static MvcHtmlString ImageHelper(this HtmlHelper helper, IImage image, UrlHelper urlHelper)
        {
            var tagBuilder = new TagBuilder("img");

            tagBuilder.Attributes["src"] = urlHelper.Content(image.ImagePath);

            tagBuilder.Attributes["alt"] = image.ImageName;

            tagBuilder.Attributes["title"] = image.ImageName;

            return new MvcHtmlString(tagBuilder.ToString());
        }

        public static MvcHtmlString ImageHelper(this HtmlHelper helper, IImage image, UrlHelper urlHelper, string className)
        {
            var tagBuilder = new TagBuilder("img");

            tagBuilder.Attributes["src"] = urlHelper.Content(image.ImagePath);

            tagBuilder.Attributes["alt"] = image.ImageName;

            tagBuilder.Attributes["title"] = image.ImageName;

            tagBuilder.Attributes["class"] = className;

            return new MvcHtmlString(tagBuilder.ToString());
        }

        public static MvcHtmlString ImageHelper(this HtmlHelper helper, int imageId, UrlHelper urlHelper)
        {
            var image = TechbartRepository.GetImageRepository().GetImage(imageId);

            var tagBuilder = new TagBuilder("img");

            tagBuilder.Attributes["src"] = urlHelper.Content(image.ImagePath);

            tagBuilder.Attributes["alt"] = image.ImageName;

            tagBuilder.Attributes["title"] = image.ImageName;

            return new MvcHtmlString(tagBuilder.ToString());
        }


        public static MvcHtmlString ImageHelper(this HtmlHelper helper, int imageId, UrlHelper urlHelper, string className)
        {
            var image = TechbartRepository.GetImageRepository().GetImage(imageId);

            var tagBuilder = new TagBuilder("img");

            tagBuilder.Attributes["src"] = urlHelper.Content(image.ImagePath);

            tagBuilder.Attributes["alt"] = image.ImageName;

            tagBuilder.Attributes["title"] = image.ImageName;

            tagBuilder.Attributes["class"] = className;

            return new MvcHtmlString(tagBuilder.ToString());
        }


    }
}