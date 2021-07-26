namespace LinqToDB.CodeGen.CodeModel
{
	public class TypeInitializer : CodeElementMethodBase, IMemberElement
	{
		public TypeInitializer(CodeClass type)
		{
			Type = type;
		}

		public CodeClass Type { get; }

		public override CodeElementType ElementType => CodeElementType.TypeConstructor;
	}
}
