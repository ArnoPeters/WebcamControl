using System.Diagnostics;
using MediaFoundation;

namespace MFCaptureD3D.Sample1
{
	[DebuggerDisplay("[{CurrentFlag}] {CurrentValue}")]
	public class VideoAmpProperty : Property
	{
		public VideoProcAmpProperty Name { get; }

		public VideoAmpProperty(VideoProcAmpProperty property, IMFMediaSource imfSource)
		{
			IAMVideoProcAmp imc = (IAMVideoProcAmp)imfSource;

			int rMin, rMax, rDelta, rDeflt, current;
			VideoProcAmpFlags cflag;
			HResult rslt = imc.GetRange(property, out rMin, out rMax, out rDelta, out rDeflt, out cflag);
			this.Range = new Range(rMin, rMax, rDelta, rDeflt);
			this.SupportedFlags = cflag;
			HResult rslt2 = imc.Get(property, out current, out cflag);
			this.CurrentValue = current;
			
			this.CurrentFlag = cflag;
			this.Name = property;
		}


		public void Apply(IMFMediaSource imfSource)
		{
			IAMVideoProcAmp imc = (IAMVideoProcAmp)imfSource;
			imc.Set(this.Name, this.CurrentValue, this.CurrentFlag);
		}


		public VideoProcAmpFlags SupportedFlags { get; set; }
		public VideoProcAmpFlags CurrentFlag { get; set; }
	}
}