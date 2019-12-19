using System;
using System.IO;
using System.Collections.Generic;

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