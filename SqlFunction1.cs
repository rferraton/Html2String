using System;
using System.Data.Sql;
using System.Data.SqlTypes;
using System.Collections.Generic;
using Microsoft.SqlServer.Server;
using System.Text;
using System.Text.RegularExpressions;
public partial class UserDefinedFunctions
{
	[Microsoft.SqlServer.Server.SqlFunction]
	public static SqlString CleanHTML(SqlString s)
	{
		if (s.IsNull) return String.Empty;
		string s1 = s.ToString().Trim();
		if (s1.Length == 0) return String.Empty;
		StringBuilder tmpS = new StringBuilder(s1.Length);
		//striping out the "control characters"
		if (!Char.IsControl(s1[0])) tmpS.Append(s1[0]);
		for (int i = 1; i <= s1.Length - 1; i++)
		{
			if (Char.IsControl(s1[i]))
			{
				if (s1[i - 1] != ' ') tmpS.Append(' ');
			}
			else
			{
				tmpS.Append(s1[i]);
			}
		}
		string result = tmpS.ToString();
		//finding the HTML tags and replacing them with an empty string
		string pattern = @"<[^>]*?>|<[^>]*>";
		Regex rgx = new Regex(pattern);
		return rgx.Replace(result, String.Empty);
	}
}
