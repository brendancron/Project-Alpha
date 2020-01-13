using System;

public class DotExpression : Expression {
	public DotExpression(Expression left, Expression right) {
		this.left = left;
		this.right = right;
	}
	private Expression left;
	private Expression right;

	public override string ToString() {
		return "Dot( "+left+", "+right+" )";
	}

	public override Value Eval(Environment env) {
		Value lv = left.Eval(env);
		if(typeof(ObjectVal).IsInstanceOfType(lv)) {
			return right.Eval(((ObjectVal)lv).env);
		} else if(typeof(ClassVal).IsInstanceOfType(lv)) {
			return right.Eval(((ClassVal)lv).env);
		} else {
			throw new VariableDoesNotExistException("Dot Operations must be used on an object or class!");
		}
	}
}