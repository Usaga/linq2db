using LinqToDB.CodeGen.CodeModel;

namespace LinqToDB.CodeGen.Schema
{
	public interface ITypeMappingProvider
	{
		(IType clrType, DataType? dataType)? GetTypeMapping(DatabaseType databaseType);
	}
}
