using LinqToDB.CodeGen.CodeModel;

namespace LinqToDB.CodeGen.Model
{
	public class ParameterModel
	{
		public ParameterModel(string name, IType type, Direction direction)
		{
			Name = name;
			Type = type;
			Direction = direction;
		}
		public string Name { get; set; }
		public IType Type { get; set; }
		
		public string? Description { get; set; }

		public Direction Direction { get; set; }
	}
}
