using System;

namespace LinqToDB.CodeGen.CodeModel
{
	public class CodeConstant : ICodeExpression
	{
		private readonly object? _value;
		private readonly Func<object?>? _delayedValue;
		public CodeConstant(IType type, object? value, bool targetTyped)
		{
			Type = new(type);
			_value = value;
			TargetTyped = targetTyped;
		}

		public CodeConstant(IType type, Func<object?> delayedValue, bool targetTyped)
		{
			Type = new(type);
			_delayedValue = delayedValue;
			TargetTyped = targetTyped;
		}

		public TypeToken Type { get; }
		public object? Value => _delayedValue != null ? _delayedValue() : _value;
		public bool TargetTyped { get; }

		public CodeElementType ElementType => CodeElementType.Constant;
	}

}
