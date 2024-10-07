using System.Text.RegularExpressions;

namespace Web
{
	public static class CommonHelper
	{
		public static string ExtractTextFromHtml(string? html)
		{
			if (html == null)
			{
				return "";
			}

			string plainText = Regex.Replace(html, "<[^>]+?>", " ");
			plainText = System.Net.WebUtility.HtmlDecode(plainText).Trim();

			return plainText;
		}
	}
}
