/*
	To add

	Lists
	Dictionaries
	Tuples

	Lambda

	Break
	Continue
*/

using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class Parser {

    List<Tuple<Token,string>> toks;

    public Parser(string code) {
        toks = Lexer.Tokenize(code);
		Console.WriteLine(Lexer.ListAll(toks));
		Statement s = Parse_CodeBlock(0);
		Console.WriteLine(s);
    }

	private Token Lookahead() {
		if(toks.Count > 0) {
			return toks[0].Item1;
		} else {
			throw new InvalidInputException("There is no avalible token");
		}
	}

	private Tuple<Token, string> consume(Token tok) {
		if(toks[0].Item1 == tok) {
			Tuple<Token, string> t = toks[0];
			toks.RemoveAt(0);
			return t;
		} else {
			throw new InvalidInputException("got token: " + toks[0] + ". Expected token: " + tok);
		}
	}

	private Expression Parse_Expressison() {
		return Parse_Or();
	}

	private Expression Parse_Or() {
		Expression and_expr = Parse_And();
		if(Lookahead() == Token.Or) {
			consume(Token.Or);
			Expression or_expr = Parse_Or();
			return new OrExpression(and_expr, or_expr);
		}
		return and_expr;
	}

	private Expression Parse_And() {
		Expression equality_expr = Parse_Equality();
		if(Lookahead() == Token.And) {
			consume(Token.And);
			Expression and_expr = Parse_And();
			return new AndExpression(equality_expr, and_expr);
		}
		return equality_expr;
	}

	private Expression Parse_Equality() {
		Expression relational_expr = Parse_Relational();
		if(Lookahead() == Token.Equal) {
			consume(Token.Equal);
			Expression equality_expr = Parse_Equality();
			return new EqualExpression(relational_expr, equality_expr);
		} else if(Lookahead() == Token.NotEqual) {
			consume(Token.NotEqual);
			Expression equality_expr = Parse_Equality();
			return new NotEqualExpression(relational_expr, equality_expr);
		}
		return relational_expr;
	}

	private Expression Parse_Relational() {
		Expression additive_expr = Parse_Additive();
		if(Lookahead() == Token.Less) {
			consume(Token.Less);
			Expression relational_expr = Parse_Relational();
			return new LessExpression(additive_expr, relational_expr);
		} else if(Lookahead() == Token.Greater) {
			consume(Token.Greater);
			Expression relational_expr = Parse_Relational();
			return new GreaterExpression(additive_expr, relational_expr);
		} else if(Lookahead() == Token.LessEqual) {
			consume(Token.LessEqual);
			Expression relational_expr = Parse_Relational();
			return new LessEqualExpression(additive_expr, relational_expr);
		} else if(Lookahead() == Token.GreaterEqual) {
			consume(Token.GreaterEqual);
			Expression relational_expr = Parse_Relational();
			return new GreaterEqualExpression(additive_expr, relational_expr);
		}
		return additive_expr;
	}

	private Expression Parse_Additive() {
		Expression mult_expr = Parse_Multiplicative();
		if(Lookahead() == Token.Add) {
			consume(Token.Add);
			Expression additive_expr = Parse_Additive();
			return new AddExpression(mult_expr, additive_expr);
		} else if(Lookahead() == Token.Sub) {
			consume(Token.Sub);
			Expression additive_expr = Parse_Additive();
			return new SubExpression(mult_expr, additive_expr);
		}
		return mult_expr;
	}

	private Expression Parse_Multiplicative() {
		Expression pow_expr = Parse_Power();
		if(Lookahead() == Token.Mult) {
			consume(Token.Mult);
			Expression mult_expr = Parse_Multiplicative();
			return new MultExpression(pow_expr, mult_expr);
		} else if(Lookahead() == Token.Div) {
			consume(Token.Div);
			Expression mult_expr = Parse_Additive();
			return new DivExpression(pow_expr, mult_expr);
		}
		return pow_expr;
	}

	private Expression Parse_Power() {
		Expression unary_expr = Parse_Unary();
		if(Lookahead() == Token.Pow) {
			consume(Token.Pow);
			Expression pow_expr = Parse_Power();
			return new PowExpression(unary_expr, pow_expr);
		}
		return unary_expr;
	}

	private Expression Parse_Unary() {
		if(Lookahead() == Token.Not) {
			consume(Token.Not);
			Expression unary_expr = Parse_Unary();
			return new NotExpression(unary_expr);
		}
		return Parse_Primary();
	}

	public Expression Parse_Primary() {
		if(Lookahead() == Token.Int) {
			int val = Int32.Parse(consume(Token.Int).Item2);
			return new IntExpression(val);
		} else if(Lookahead() == Token.Float) {
			float val = float.Parse(consume(Token.Int).Item2);
			return new FloatExpression(val);
		} else if(Lookahead() == Token.True) {
			consume(Token.True);
			return new BoolExpression(true);
		} else if(Lookahead() == Token.False) {
			consume(Token.False);
			return new BoolExpression(false);
		} else if(Lookahead() == Token.String) {
			return new StringExpression(consume(Token.String).Item2);
		} else if(Lookahead() == Token.ID) {
			return Parse_DotExpression();
		} else if(Lookahead() == Token.LParen) {
			consume(Token.LParen);
			Expression expr = Parse_Expressison();
			consume(Token.RParen);
			return expr;
		} else {
			return null;
		}
	}

	public Expression Parse_DotExpression() {
		Expression expr = Parse_FunctionCall();
		return Parse_DotExpressionHelp(expr);
	}

	public Expression Parse_DotExpressionHelp(Expression expr) {
		if(Lookahead() == Token.Dot) {
			consume(Token.Dot);
			if(Lookahead() == Token.LParen) {
				consume(Token.LParen);
				Expression inner = Parse_DotExpression();
				consume(Token.RParen);
				return Parse_DotExpressionHelp(new DotExpression(expr, inner));
			} else  {
				Expression fc = Parse_FunctionCall();
				return Parse_DotExpressionHelp(new DotExpression(expr,fc));
			}
		} else {
			return expr;
		}
	}

	public Expression Parse_FunctionCall() {
		string id = consume(Token.ID).Item2;
		if(Lookahead() == Token.LParen) {
			consume(Token.LParen);
			List<Expression> args = new List<Expression>();
			if(Lookahead() != Token.RParen) {
				args.Add(Parse_Expressison());
				while(Lookahead() == Token.Comma) {
					consume(Token.Comma);
					args.Add(Parse_Expressison());
				}
			}
			consume(Token.RParen);
			return new FunctionCallExpression(id,args);
		} else {
			return new IDExpression(id);
		}
	}

	public Statement Parse_CodeBlock(int indent) {
		Console.WriteLine(Lookahead());
		if(Lookahead() == Token.EOF) {
			return new NoOpStatement();
		}
		//counts the indents
		for(int i = 0; i < indent; i++) {
			if(toks[i].Item1 != Token.Indent) {
				return new NoOpStatement();
			}
		}
		//consumes the indents
		for(int i = 0; i < indent; i++) {
			consume(Token.Indent);
		}
		Statement stmt = Parse_Statement(indent);
		Statement code_block = Parse_CodeBlock(indent);
		return new SeqStatement(stmt,code_block);
	}

	public Statement Parse_Statement(int indent) {
		if(Lookahead() == Token.EndLine) {
			consume(Token.EndLine);
			return new NoOpStatement();
		} else if (Lookahead() == Token.Def){
			consume(Token.Def);
			string id = consume(Token.ID).Item2;
			consume(Token.LParen);
			List<Expression> args = new List<Expression>();
			if(Lookahead() != Token.RParen) {
				args.Add(Parse_Expressison());
				while(Lookahead() == Token.Comma) {
					consume(Token.Comma);
					args.Add(Parse_Expressison());
				}
			}
			consume(Token.RParen);
			consume(Token.Colon);
			if(Lookahead() != Token.EOF) {
				consume(Token.EndLine);
			}
			Statement code = Parse_CodeBlock(indent+1);
			return new FunctionDeclarationStatement(id,args,code);
		} else if(Lookahead() == Token.Class){
			consume(Token.Class);
			string id = consume(Token.ID).Item2;
			consume(Token.Colon);
			consume(Token.EndLine);
			Statement block = Parse_CodeBlock(indent+1);
			return new ClassStatement(id,block);
		} else if (Lookahead() == Token.Print) {
			consume(Token.Print);
			consume(Token.LParen);
			Expression expr = Parse_Expressison();
			consume(Token.RParen);
			if(Lookahead() != Token.EOF) {
				consume(Token.EndLine);
			}
			return new PrintStatement(expr);
		} else if (Lookahead() == Token.Return) {
			consume(Token.Return);
			Expression ret = Parse_Expressison();
			if(Lookahead() != Token.EOF) {
				consume(Token.EndLine);
			}
			return new ReturnStatement(ret);
		} else if (Lookahead() == Token.If) {
			consume(Token.If);
			Expression cond = Parse_Expressison();
			consume(Token.Colon);
			consume(Token.EndLine);
			Statement if_block = Parse_CodeBlock(indent+1);
			Statement else_block = new NoOpStatement();
			if(Lookahead() == Token.Else) {
				consume(Token.Else);
				else_block = Parse_CodeBlock(indent+1);
			}
			return new IfStatement(cond, if_block, else_block);
		} else if (Lookahead() == Token.While) {
			consume(Token.While);
			Expression cond = Parse_Expressison();
			consume(Token.Colon);
			consume(Token.EndLine);
			Statement code = Parse_CodeBlock(indent+1);
			return new WhileStatement(cond, code);
		} else if (Lookahead() == Token.For) {
			consume(Token.For);
			string id = consume(Token.ID).Item2;
			consume(Token.In);
			Expression iter = Parse_Expressison();
			consume(Token.Colon);
			Statement code = Parse_CodeBlock(indent+1);
			return new ForStatement(id, iter, code);
		} else {
			Expression expr = Parse_Expressison();
			if(Lookahead() == Token.EndLine) {
				consume(Token.EndLine);
				return new CommandStatement(expr);
			}
			if(Lookahead() == Token.EOF) {
				return new CommandStatement(expr);
			}
			//typeof(Array).IsInstanceOfType(arr)
			if(Lookahead() == Token.Assign) {
				consume(Token.Assign);
				Expression val = Parse_Expressison();
				return new AssignStatement(expr, val);				
			}
		}
		//technically unreachable
		return null;
	}

}

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

    public static void Main(string[] args) {
        Parser parser = new Parser(File.ReadAllText(args[0]));
    }

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

public interface Statement{}

public class NoOpStatement : Statement{}

public class SeqStatement : Statement{
	public SeqStatement(Statement left, Statement right) {
		this.left = left;
		this.right = right;
	}
	private Statement left;
	private Statement right;

	public override string ToString() {
		return "Seq( "+left+", "+right+" )";
	}
}

public class CommandStatement : Statement{
	public CommandStatement(Expression expr) {
		this.expr = expr;
	}
	private Expression expr;

	public override string ToString() {
		return "Command( "+expr+" )";
	}
}

public class ClassStatement : Statement{
	public ClassStatement(string id, Statement block) {
		this.id = id;
		this.block = block;
	}
	private string id;
	private Statement block;

	public override string ToString() {
		return "Class( "+id+", "+block+" )";
	}
}

public class AssignStatement : Statement{
	public AssignStatement(Expression var, Expression val) {
		this.var = var;
		this.val = val;
	}
	private Expression var;
	private Expression val;

	public override string ToString() {
		return "Assign( "+var+", "+val+" )";
	}
}

public class IfStatement : Statement{
	public IfStatement(Expression cond, Statement if_block, Statement else_block) {
		this.cond = cond;
		this.if_block = if_block;
		this.else_block = else_block;
	}
	private Expression cond;
	private Statement if_block;
	private Statement else_block;

	public override string ToString() {
		return "If( "+cond+", "+if_block+", "+else_block+" )";
	}
}

public class ForStatement : Statement{
	public ForStatement(string id, Expression iter, Statement code) {
		this.id = id;
		this.iter = iter;
		this.code = code;
	}
	private string id;
	private Expression iter;
	private Statement code;

	public override string ToString() {
		return "For( "+id+", "+iter+", "+code+" )";
	}
}

public class WhileStatement : Statement{
	public WhileStatement(Expression cond, Statement code) {
		this.cond = cond;
		this.code = code;
	}
	private Expression cond;
	private Statement code;

	public override string ToString() {
		return "While( "+cond+", "+code+" )";
	}
}

public class PrintStatement : Statement{
	public PrintStatement(Expression expr) {
		this.expr = expr;
	}
	private Expression expr;

	public override string ToString() {
		return "Print( "+expr+" )";
	}
}

public class ReturnStatement : Statement{
	public ReturnStatement(Expression expr) {
		this.expr = expr;
	}
	private Expression expr;

	public override string ToString() {
		return "Return( "+expr+" )";
	}
}

public class FunctionDeclarationStatement : Statement{
	public FunctionDeclarationStatement(string id, List<Expression> args, Statement code) {
		this.id = id;
		this.args = args;
		this.code = code;
	}
	private string id;
	private List<Expression> args;
	private Statement code;

	public override string ToString() {
		return "FunctionDecl( "+id+", "+args+", "+code+" )";
	}
}

//Expressions
public interface Expression{}

public class IDExpression : Expression{
	public IDExpression(string id) {
		this.id = id;
	}
	private string id;

	public override string ToString() {
		return "ID( "+id+" )";
	}
}

public class DotExpression : Expression{
	public DotExpression(Expression left, Expression right) {
		this.left = left;
		this.right = right;
	}
	private Expression left;
	private Expression right;

	public override string ToString() {
		return "Dot( "+left+", "+right+" )";
	}
}

public class IntExpression : Expression{
	public IntExpression(int val) {
		this.val = val;
	}
	private int val;

	public override string ToString() {
		return "Int( "+val+" )";
	}
}

public class FloatExpression : Expression{
	public FloatExpression(float val) {
		this.val = val;
	}
	private float val;

	public override string ToString() {
		return "Float( "+val+" )";
	}
}

public class BoolExpression : Expression {
	public BoolExpression(bool val) {
		this.val = val;
	}
	private bool val;

	public override string ToString() {
		return "Bool( "+val+" )";
	}
}

public class StringExpression : Expression {
	public StringExpression(string str) {
		this.str = str;
	}
	private string str;

	public override string ToString() {
		return "String( "+str+" )";
	}
}

public class OrExpression : Expression{
	public OrExpression(Expression left, Expression right) {
		this.left = left;
		this.right = right;
	}
	private Expression left;
	private Expression right;

	public override string ToString() {
		return "Or( "+left+", "+right+" )";
	}
}

public class AndExpression : Expression{
	public AndExpression(Expression left, Expression right) {
		this.left = left;
		this.right = right;
	}
	private Expression left;
	private Expression right;

	public override string ToString() {
		return "And( "+left+", "+right+" )";
	}
}

public class EqualExpression : Expression{
	public EqualExpression(Expression left, Expression right) {
		this.left = left;
		this.right = right;
	}
	private Expression left;
	private Expression right;

	public override string ToString() {
		return "Equal( "+left+", "+right+" )";
	}
}

public class NotEqualExpression : Expression{
	public NotEqualExpression(Expression left, Expression right) {
		this.left = left;
		this.right = right;
	}
	private Expression left;
	private Expression right;

	public override string ToString() {
		return "NotEqual( "+left+", "+right+" )";
	}
}

public class GreaterExpression : Expression{
	public GreaterExpression(Expression left, Expression right) {
		this.left = left;
		this.right = right;
	}
	private Expression left;
	private Expression right;

	public override string ToString() {
		return "Greater( "+left+", "+right+" )";
	}
}

public class LessExpression : Expression{
	public LessExpression(Expression left, Expression right) {
		this.left = left;
		this.right = right;
	}
	private Expression left;
	private Expression right;

	public override string ToString() {
		return "Less( "+left+", "+right+" )";
	}
}

public class GreaterEqualExpression : Expression{
	public GreaterEqualExpression(Expression left, Expression right) {
		this.left = left;
		this.right = right;
	}
	private Expression left;
	private Expression right;

	public override string ToString() {
		return "GreaterEqual( "+left+", "+right+" )";
	}
}

public class LessEqualExpression : Expression{
	public LessEqualExpression(Expression left, Expression right) {
		this.left = left;
		this.right = right;
	}
	private Expression left;
	private Expression right;

	public override string ToString() {
		return "LessEqual( "+left+", "+right+" )";
	}
}

public class AddExpression : Expression{
	public AddExpression(Expression left, Expression right) {
		this.left = left;
		this.right = right;
	}
	private Expression left;
	private Expression right;

	public override string ToString() {
		return "Add( "+left+", "+right+" )";
	}
}

public class SubExpression : Expression{
	public SubExpression(Expression left, Expression right) {
		this.left = left;
		this.right = right;
	}
	private Expression left;
	private Expression right;

	public override string ToString() {
		return "Sub( "+left+", "+right+" )";
	}
}

public class MultExpression : Expression{
	public MultExpression(Expression left, Expression right) {
		this.left = left;
		this.right = right;
	}
	private Expression left;
	private Expression right;

	public override string ToString() {
		return "Mult( "+left+", "+right+" )";
	}
}

public class DivExpression : Expression{
	public DivExpression(Expression left, Expression right) {
		this.left = left;
		this.right = right;
	}
	private Expression left;
	private Expression right;

	public override string ToString() {
		return "Div( "+left+", "+right+" )";
	}
}

public class PowExpression : Expression{
	public PowExpression(Expression left, Expression right) {
		this.left = left;
		this.right = right;
	}
	private Expression left;
	private Expression right;

	public override string ToString() {
		return "Pow( "+left+", "+right+" )";
	}
}

public class NotExpression : Expression{
	public NotExpression(Expression expr) {
		this.expr = expr;
	}
	private Expression expr;

	public override string ToString() {
		return "Not( "+expr+" )";
	}
}

public class FunctionCallExpression : Expression{
	public FunctionCallExpression(string id, List<Expression> arguments) {
		this.id = id;
		this.args = arguments;
	}
	private String id;
	private List<Expression> args;

	public override string ToString() {
		return "FunctionCall( "+id+", "+args+" )";
	}
}

public enum Token {
	//Organization
	LParen,
	RParen,
	LBrace,
	RBrace,
	LBracket,
	RBracket,
	Comma,
	Class,
	Def,
	Colon,
	//Returns
	Return,
	Yield,
	//Loop
	For,
	In,
	While,
	//conditionals
	If,
	Else,
	//Comparison
	Less,
	LessEqual,
	Greater,
	GreaterEqual,
	Equal,
	NotEqual,
	Not,
	Or,
	And,
	Add,
	//Operators
	Sub,
	Mult,
	Div,
	Pow,
	Dot,
	//Utility
	Indent,
	EndLine,
   	EOF,
	//Premade Functions
	Print,
	//Types
	Int,
	Float,
	String,
	False,
	True,
	//Variables
	ID,
	//Assignment
	Assign
}

public class InvalidInputException: Exception {
   public InvalidInputException(string message): base(message) {
   }
}