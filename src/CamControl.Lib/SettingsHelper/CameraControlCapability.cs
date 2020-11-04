using MediaFoundation;
using MFCaptureD3D.Sample1;

namespace MFCaptureD3D
{
	public class CameraControlCapability
	{
		public Range Range { get; }
		public CameraControlFlags SupportedFlags { get; }

		public CameraControlCapability(CameraControlProperty property, IAMCameraControl imc)
		{
			int rMin, rMax, rDelta, rDeflt;
			CameraControlFlags cflag;
			HResult rslt = imc.GetRange(property, out rMin, out rMax, out rDelta, out rDeflt, out cflag);
			this.Range = new Range(rMin, rMax, rDelta, rDeflt);
			this.SupportedFlags = cflag;
		}
	}
}