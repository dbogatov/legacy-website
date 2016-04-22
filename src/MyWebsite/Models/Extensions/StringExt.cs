using System;

public static class StringExtensions
{
	public static string Placeholder(this string str, string placeholder)
	{
        return String.IsNullOrWhiteSpace(str) ? placeholder : str;
    }
}