using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace LinqToDB.CodeGen.CodeModel
{
	public class CodeFile : CodeElementList<ITopLevelCodeElement>, ICodeElement
	{
		private string _name = null!;

		public CodeFile(string fileName)
		{
			FileName = fileName;
		}

		public string FileName
		{
			get => _name;
			set => _name = NormalizeName(value);
		}

		public List<CodeElementComment> Header { get; } = new ();
		public List<CodeElementImport> Imports { get; } = new ();

		public CodeElementType ElementType => CodeElementType.File;

		private string NormalizeName(string name)
		{
			if (name.Length == 0)
				return "_";

			var sb = new StringBuilder();
			foreach (var (chr, cat) in name.EnumerateCharacters())
			{
				if (chr == ".")
				{
					sb.Append(chr);
					continue;
				}

				switch (cat)
				{
					// lazy validation for now
					case UnicodeCategory.UppercaseLetter:
					case UnicodeCategory.TitlecaseLetter:
					case UnicodeCategory.LowercaseLetter:
					case UnicodeCategory.DecimalDigitNumber:
						sb.Append(chr);
						break;
					default:
						sb.Append('_');
						break;
				}
			}

			return sb.ToString();
		}
	}
}
