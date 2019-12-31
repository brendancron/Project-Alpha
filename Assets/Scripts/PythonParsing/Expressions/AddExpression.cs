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

	public override Value Eval() {
		Value lv = left.Eval();
		Value rv = right.Eval();
		if(lv.valType == rv.valType) {
			switch(lv.valType) {
				case Value.ValType.String:
					return new Value(lv.str_val + rv.str_val);
				case Value.ValType.Float:
					return new Value(lv.float_val + rv.float_val);
				case Value.ValType.Bool:
					throw new TypeMismatchException("bools cannot be added!");
			}
			return null;
		} else {
			throw new TypeMismatchException("types must match");
		}
	}
}