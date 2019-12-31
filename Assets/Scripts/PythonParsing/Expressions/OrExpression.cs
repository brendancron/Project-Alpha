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

	public override Value Eval() {
		Value lv = left.Eval();
		Value rv = right.Eval();
		if(lv.valType == rv.valType) {
			switch(lv.valType) {
				case Value.ValType.String:
					return new Value(lv.str_val);
				case Value.ValType.Float:
					return new Value(lv.float_val);
				case Value.ValType.Bool:
					if(lv.bool_val) {
						return new Value(true);
					}
					switch(rv.valType) {
						case Value.ValType.Bool:
							return new Value(rv.bool_val);
						case Value.ValType.Float:
							return new Value(rv.float_val);
						case Value.ValType.String:
							return new Value(rv.str_val);
					}
					break;
			}
			return null;
		} else {
			return new Value(false);
		}
	}
}