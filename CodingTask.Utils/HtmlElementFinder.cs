namespace CodingTask.Utils
{
    using System.Linq;
    using AngleSharp.Dom;
    public static class HtmlElementFinder
    {
        public static string FindChildElementBySelector(IDocument document, string cssSelector, int index)
        {
            IHtmlCollection<IElement> cells = document.QuerySelectorAll(cssSelector);

            return index < cells.Length ? cells[index].TextContent.Trim() : string.Empty;
        }

        public static string FindElementBySelector(IDocument element, string cssSelector)
        {
            IHtmlCollection<IElement> resultantElement = element?.QuerySelectorAll(cssSelector);

            return resultantElement != null && resultantElement.Length > 0
                       ? resultantElement.FirstOrDefault().Text().Trim()
                       : string.Empty;
        }

        public static int FindElementsCountBySelector(IDocument document, string cssSelector)
        {
            return document.QuerySelectorAll(cssSelector).Length;
        }
    }
}