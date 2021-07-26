using System;

namespace LinqToDB.CodeGen.CodeModel
{
	public interface ITypeParser
	{
		IType Parse(Type type);
		IType Parse<T>();
		IType Parse(string typeName, bool valueType);
	}
}
