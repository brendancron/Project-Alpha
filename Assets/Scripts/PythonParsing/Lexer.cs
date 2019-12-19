using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class Lexer {

    public static Dictionary<string, Token> toks_from_regex = new Dictionary<string, Token>() {
    	{@"^\n",Token.EndLine},
    	{@"^\(",Token.LParen},
		{@"^\)",Token.RParen},
		{@"^{",Token.LBrace},
		{@"^}",Token.RBrace},
		{@"^\[",Token.LBracket},
		{@"^\]",Token.RBracket},
		{@"^,",Token.Comma},
		{@"^<",Token.Less},
		{@"^<=",Token.LessEqual},
		{@"^>",Token.Greater},
		{@"^>=",Token.GreaterEqual},
		{@"^==",Token.Equal},
		{@"^!=",Token.NotEqual},
		{@"^=",Token.Assign},
		{@"^\+",Token.Add},
		{@"^\.",Token.Dot},
		{@"^-",Token.Sub},
		{@"^/",Token.Div},
		{@"^\*\*",Token.Pow},
		{@"^\*",Token.Mult},
		{@"^not",Token.Not},
		{@"^or",Token.Or},
		{@"^and",Token.And},
		{@"^for",Token.For},
		{@"^in",Token.In},
		{@"^if",Token.If},
		{@"^else",Token.Else},
		{@"^while",Token.While},
		{@"^False",Token.False},
		{@"^True",Token.True},
		{@"^:",Token.Colon},
		{@"^(?:    |\t)",Token.Indent},
		{@"^print",Token.Print},
		{@"^def",Token.Def},
		{@"^class",Token.Class},
		{@"^return",Token.Return},
		{@"^yield",Token.Yield},
		{@"^([0-9]+(?:\.[0-9]+))",Token.Float}, 
		{@"^([0-9]+)",Token.Int},
		{@"^""((?:[^""])*)""",Token.String},
		{@"^'((?:[^'])*)'",Token.String},
		{@"^([a-zA-Z]+)",Token.ID}
    };

    public static string ListAll(List<Tuple<Token,string>> toks) {
        return "["+String.Join(", ", toks)+"]";
    }

    public static string ListString(List<Tuple<Token,string>> toks) {
        List<Token> tokens = new List<Token>();
		toks.ForEach((x) => {tokens.Add(x.Item1);});
        return "["+String.Join(", ", tokens)+"]";
    }

    public static string ListValues(List<Tuple<Token,string>> toks) {
        List<string> tokens = new List<string>();
		toks.ForEach((x) => {tokens.Add(x.Item2);});
        return "["+String.Join(", ", tokens)+"]";
    }

    public static List<Tuple<Token,string>> Tokenize(string code) {
    	List<Tuple<Token,string>> tokens = new List<Tuple<Token,string>>();
		while(code.Length > 0) {
	    	int length = code.Length;
	    	foreach(string pattern in toks_from_regex.Keys) {
	        	Match m = Regex.Match(code,pattern);
				if(m.Success) {
		    		tokens.Add(new Tuple<Token,string>(toks_from_regex[pattern],(string)(m.Groups[1].Value)));
		    		code = code.Substring(m.Value.Length);
		    		break;
				}
	    	}
	    	if(code.Length == length) {
	        	code = code.Substring(1);
	    	}
		}
		tokens.Add(new Tuple<Token,string>(Token.EOF,""));
		return tokens;
    }

    
}