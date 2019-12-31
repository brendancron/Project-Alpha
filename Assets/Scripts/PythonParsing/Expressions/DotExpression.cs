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
		switch(lv.valType) {
			case Value.ValType.ObjectType:
				return right.Eval(lv.obj_val.env);
			case Value.ValType.ClassType:
				return right.Eval(lv.class_val.env);
			default:
				throw new VariableDoesNotExistException("Dot Operations must be used on an object or class!");
		}
	}
}