using System.Linq;
using System.Collections.Generic;

namespace LinqToDB.CodeGen.CodeModel
{
	public class ScopedNamesCollector : NoopCodeModelVisitor
	{
		private readonly Dictionary<string, ISet<string>> _nameScopes = new ();
		private string? _currentScope;

		private readonly Dictionary<string, ISet<string>> _knownTypes = new ();

		private readonly ILanguageServices _langServices;

		public Dictionary<string, ISet<string>> ScopedNames => _nameScopes;
		public IReadOnlyDictionary<string, ISet<string>> KnownTypes => _knownTypes;

		public ScopedNamesCollector(ILanguageServices langServices)
		{
			_langServices = langServices;
			SetNewScope(_currentScope);
		}

		protected override void Visit(CodeMethod method)
		{
			AddToCurrent(method.Name.Name);

			base.Visit(method);
		}

		protected override void Visit(CodeProperty property)
		{
			AddToCurrent(property.Name.Name);

			base.Visit(property);
		}

		protected override void Visit(CodeElementNamespace @namespace)
		{
			var current = _currentScope;

			foreach (var name in  @namespace.Name)
			{
				AddToCurrent(name.Name);
				SetNewScope((_currentScope != null ? _currentScope + "." : null) + name.Name);
			}

			base.Visit(@namespace);

			_currentScope = current;
		}

		protected override void Visit(CodeClass @class)
		{
			var current = _currentScope;

			AddToCurrent(@class.Name.Name);
			AddType(@class.Type);

			SetNewScope((_currentScope != null ? _currentScope + "." : null) + @class.Name.Name);
			base.Visit(@class);

			_currentScope = current;
		}

		protected override void Visit(CodeField field)
		{
			AddToCurrent(field.Name.Name);

			base.Visit(field);
		}

		protected override void Visit(TypeReference type)
		{
			AddType(type.Type);
			base.Visit(type);
		}

		protected override void Visit(TypeToken type)
		{
			AddType(type.Type);
			base.Visit(type);
		}

		private void SetNewScope(string? scope)
		{
			if (!_nameScopes.ContainsKey(scope ?? string.Empty))
				_nameScopes.Add(scope ?? string.Empty, new HashSet<string>(_langServices.GetNameComparer()));

			_currentScope = scope;
		}

		private void AddToCurrent(string name)
		{
			_nameScopes[_currentScope ?? string.Empty].Add(name);
		}

		private void AddType(IType type)
		{
			if ((type.Kind == TypeKind.Regular
				|| type.Kind == TypeKind.Generic
				|| type.Kind == TypeKind.OpenGeneric)
				&& type.Parent == null)
			{
				var scope = type.Namespace != null ? string.Join(".", type.Namespace.Select(_ => _.Name)) : string.Empty;
				if (!_knownTypes.TryGetValue(type.Name!.Name, out var namespaces))
					_knownTypes.Add(type.Name!.Name, namespaces = new HashSet<string>(_langServices.GetNameComparer()));

				namespaces.Add(scope);
			}
		}
	}
}
