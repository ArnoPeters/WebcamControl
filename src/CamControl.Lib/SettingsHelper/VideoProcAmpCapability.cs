using MediaFoundation;

namespace MFCaptureD3D.Sample1
{
	public class VideoProcAmpCapability
	{
		public Range Range { get; }

		public VideoProcAmpFlags SupportedFlags { get; }

		public VideoProcAmpCapability(VideoProcAmpProperty property, IAMVideoProcAmp imc)
		{
			int rMin, rMax, rDelta, rDeflt;
			VideoProcAmpFlags cflag;
			HResult rslt = imc.GetRange(property, out rMin, out rMax, out rDelta, out rDeflt, out cflag);
			this.Range = new Range(rMin, rMax, rDelta, rDeflt);
			this.SupportedFlags = cflag;
		}
	}
}