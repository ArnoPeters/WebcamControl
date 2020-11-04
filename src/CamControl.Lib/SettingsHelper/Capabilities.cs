using MediaFoundation;
using MFCaptureD3D.Sample1;

namespace MFCaptureD3D
{
	public class Capabilities : Base
	{
		public CameraControlCapability Pan { get; }
		public CameraControlCapability Tilt { get; }
		public CameraControlCapability Roll { get; }
		public CameraControlCapability Zoom { get; }
		public CameraControlCapability Exposure { get; }
		public CameraControlCapability Iris { get; }
		public CameraControlCapability Focus { get; }

		public VideoProcAmpCapability Brightness { get; set; }
		public VideoProcAmpCapability Contrast { get; set; }
		public VideoProcAmpCapability Hue { get; set; }
		public VideoProcAmpCapability Saturation { get; set; }
		public VideoProcAmpCapability Sharpness { get; set; }
		public VideoProcAmpCapability Gamma { get; set; }
		public VideoProcAmpCapability ColorEnable { get; set; }
		public VideoProcAmpCapability WhiteBalance { get; set; }
		public VideoProcAmpCapability BacklightCompensation { get; set; }
		public VideoProcAmpCapability Gain { get; set; }
		private Capabilities() { }

		public Capabilities(string name, IMFMediaSource imfSource) : base(name)
		{
			try
			{
				IAMCameraControl imc = (IAMCameraControl) imfSource;

				this.Pan = new CameraControlCapability(CameraControlProperty.Pan, imc);
				this.Tilt = new CameraControlCapability(CameraControlProperty.Tilt, imc);
				this.Roll = new CameraControlCapability(CameraControlProperty.Roll, imc);
				this.Zoom = new CameraControlCapability(CameraControlProperty.Zoom, imc);
				this.Exposure = new CameraControlCapability(CameraControlProperty.Exposure, imc);
				this.Iris = new CameraControlCapability(CameraControlProperty.Iris, imc);
				this.Focus = new CameraControlCapability(CameraControlProperty.Focus, imc);

				IAMVideoProcAmp Ivp = (IAMVideoProcAmp) imfSource;

				this.Brightness = new VideoProcAmpCapability(VideoProcAmpProperty.Brightness, Ivp);
				this.Contrast = new VideoProcAmpCapability(VideoProcAmpProperty.Contrast, Ivp);
				this.Hue = new VideoProcAmpCapability(VideoProcAmpProperty.Hue, Ivp);
				this.Saturation = new VideoProcAmpCapability(VideoProcAmpProperty.Saturation, Ivp);
				this.Sharpness = new VideoProcAmpCapability(VideoProcAmpProperty.Sharpness, Ivp);
				this.Gamma = new VideoProcAmpCapability(VideoProcAmpProperty.Gamma, Ivp);
				this.ColorEnable = new VideoProcAmpCapability(VideoProcAmpProperty.ColorEnable, Ivp);
				this.WhiteBalance = new VideoProcAmpCapability(VideoProcAmpProperty.WhiteBalance, Ivp);
				this.BacklightCompensation = new VideoProcAmpCapability(VideoProcAmpProperty.BacklightCompensation, Ivp);
				this.Gain = new VideoProcAmpCapability(VideoProcAmpProperty.Gain, Ivp);
			}
			catch { }
		}
	}
}