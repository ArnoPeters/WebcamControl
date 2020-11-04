using System.Diagnostics;

namespace MFCaptureD3D.Sample1
{
	[DebuggerDisplay("({Min},{Max})")]
	public class Range
	{
		public int Min { get; }
		public int Max { get; }
		public int Delta { get; }
		public int Default { get; }

		public Range(int rMin, int rMax, int rDelta, int rDeflt)
		{
			this.Min = rMin;
			this.Max = rMax;
			this.Delta = rDelta;
			this.Default = rDeflt;
		}
	}
}