using System.Diagnostics;

namespace MFCaptureD3D.Sample1
{
	[DebuggerDisplay("({Min},{Max})")]
	public class Range
	{


		public Range(int rMin, int rMax, int rDelta, int rDeflt)
		{
			this.Min = rMin;
			this.Max = rMax;
			this.Delta = rDelta;
			this.Default = rDeflt;
		}

		public int Min { get; set; }
		public int Max { get; set; }
		public int Delta { get; set; }
		public int Default { get; set; }
	}
}