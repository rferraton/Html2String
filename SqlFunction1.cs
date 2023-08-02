using System;
using System.Data.Sql;
using System.Data.SqlTypes;
using System.Collections.Generic;
using Microsoft.SqlServer.Server;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;

public partial class UserDefinedFunctions
{
	[SqlFunction(IsDeterministic = true, IsPrecise = true, DataAccess = DataAccessKind.None)]
	public static SqlString CleanHTML(string s) => pCleanHTML(s);

	private static string pCleanHTML(string s)
	{
		
		if (string.IsNullOrEmpty(s))
			return null;


		var html = s;
		string newLine = Environment.NewLine;
		
		html = Regex.Replace(html, "<br />|<br/>|<br>|</ br>|</br>)", newLine, RegexOptions.IgnoreCase);
		html = Regex.Replace(html, "&nbsp;", newLine, RegexOptions.IgnoreCase);		
		html = Regex.Replace(html, "<.*?>", string.Empty);
		html = WebUtility.HtmlDecode(html);

		return html;
	}
}
