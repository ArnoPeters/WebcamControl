using System.Diagnostics;

namespace MFCaptureD3D
{
	[DebuggerDisplay("{" + nameof(Name) + "}")]
	public class Base
	{
		public string Name { get; set; }
		protected Base() { }

		protected Base(string name)
		{
			this.Name = name;
		}
	}
}