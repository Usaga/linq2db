using System.Collections.Generic;
using System.Globalization;
using LinqToDB.CodeGen.CodeGeneration;

namespace LinqToDB.CodeGen.CodeModel
{
	public interface ILanguageServices
	{
		string? GetAlias(CodeIdentifier[]? @namespace, string name);
		string NormalizeIdentifier(string name);
		ISet<string> GetUniqueNameCollection();
		IEqualityComparer<string> GetNameComparer();
		string MakeUnique(ISet<string> knownNames, string name);
		bool IsValidIdentifierFirstChar(string character, UnicodeCategory category);
		bool IsValidIdentifierNonFirstChar(string character, UnicodeCategory category);
		CodeModelVisitor GetNamesNormalizer();
		CodeGenerationVisitor GetCodeGenerator(CodeGenerationSettings settings, ISet<CodeIdentifier[]> namespaces, ITypeParser typeParser, IReadOnlyDictionary<string, ISet<string>> scopedNames, IReadOnlyDictionary<string, ISet<string>> knownTypes);

		string FileExtension { get; }
		string? MissingXmlCommentWarnCode { get; }
		bool SupportsNRT { get; }
	}
}
