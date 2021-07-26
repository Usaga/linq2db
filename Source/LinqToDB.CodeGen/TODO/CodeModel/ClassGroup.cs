using System;

namespace LinqToDB.CodeGen.CodeModel
{
	public class ClassGroup : MemberGroup<CodeClass>, ITopLevelCodeElement
	{
		private readonly CodeClass? _class;
		private readonly CodeElementNamespace? _namespace;

		public ClassGroup(ITopLevelCodeElement? owner)
		{
			if (owner is CodeClass @class)
				_class = @class;
			else if (owner is CodeElementNamespace ns)
				_namespace = ns;
			else if (owner != null)
				throw new InvalidOperationException();
		}

		public override CodeElementType ElementType => CodeElementType.ClassGroup;

		public ClassBuilder New(CodeIdentifier name)
		{
			var @class = _class != null ? new CodeClass(_class, name) : new CodeClass(_namespace?.Name, name);
			Members.Add(@class);
			return new ClassBuilder(@class);
		}
	}
}
