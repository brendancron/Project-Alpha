using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

/*
	To add

	Lists
	Dictionaries
	Tuples

	Lambda

	Break
	Continue
*/

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
			return Parse_Chain();
		} else if(Lookahead() == Token.LParen) {
			consume(Token.LParen);
			Expression expr = Parse_Expressison();
			consume(Token.RParen);
			return expr;
		} else {
			return null;
		}
	}

	public Expression Parse_Chain() {
		Tuple<Token, string> t = consume(Token.ID);
		Expression expr = new IDExpression(t.Item2);
		return Parse_ChainHelp(expr);
	}

	public Expression Parse_ChainHelp(Expression expr) {
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
			return Parse_ChainHelp(new FunctionCallExpression(expr,args));
		} else if(Lookahead() == Token.Dot) {
			consume(Token.Dot);
			if(Lookahead() == Token.LParen) {
				consume(Token.LParen);
				Expression right_expr = Parse_Expressison();
				consume(Token.RParen);
				return Parse_ChainHelp(new DotExpression(expr, right_expr));
			} else {
				Tuple<Token, string> t = consume(Token.ID);
				Expression right_expr = new IDExpression(t.Item2);
				return Parse_ChainHelp(new DotExpression(expr, right_expr));
			}	
		} else {
			return expr;
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