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

	public override Value Eval(Environment env) {
		Value lv = left.Eval(env);
		Value rv = right.Eval(env);
		if(lv.valType == rv.valType) {
			switch(lv.valType) {
				case Value.ValType.String:
					return new Value(lv.str_val == rv.str_val);
				case Value.ValType.Float:
					return new Value(lv.float_val == rv.float_val);
				case Value.ValType.Bool:
					return new Value(lv.bool_val == rv.bool_val);
				case Value.ValType.Function:
					return new Value(lv.func_val == rv.func_val);
			}
			//should never run!!!
			return null;
		} else {
			return new Value(false);
		}
	}
}