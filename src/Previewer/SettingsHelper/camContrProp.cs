using System.Diagnostics;
using MediaFoundation;

namespace MFCaptureD3D.Sample1
{
	[DebuggerDisplay("[{CurrentFlag}] {CurrentValue}")]
	public class camContrProp : Property
	{
		public  CameraControlProperty Name {get ;}

		public camContrProp(CameraControlProperty property, IMFMediaSource imfSource)
		{
			IAMCameraControl imc = (IAMCameraControl)imfSource;

			int rMin, rMax, rDelta, rDeflt, current;
			CameraControlFlags cflag;
			HResult rslt = imc.GetRange(property, out rMin, out rMax, out rDelta, out rDeflt, out cflag);
			this.Range = new Range(rMin, rMax, rDelta, rDeflt);
			this.SupportedFlags = cflag;
			HResult rslt2 = imc.Get(property, out current, out cflag);
			this.CurrentValue = current;
			this.CurrentFlag = cflag;
			this.Name = property;
		}
		public CameraControlFlags SupportedFlags { get; set; }

		public CameraControlFlags CurrentFlag { get; set; }

		public  void Apply(IMFMediaSource imfSource)
		{
			IAMCameraControl imc = (IAMCameraControl)imfSource;
			imc.Set(this.Name, this.CurrentValue, this.CurrentFlag);
		}
	}
}