using MediaFoundation;
using MFCaptureD3D.Sample1;

namespace MFCaptureD3D
{
	public class Settings : Base
	{
		public CameraControlSetting Pan { get; set; }
		public CameraControlSetting Tilt { get; set; }
		public CameraControlSetting Roll { get; set; }
		public CameraControlSetting Zoom { get; set; }
		public CameraControlSetting Exposure { get; set; }
		public CameraControlSetting Iris { get; set; }
		public CameraControlSetting Focus { get; set; }

		public VideoProcAmpSetting Brightness { get; set; }
		public VideoProcAmpSetting Contrast { get; set; }
		public VideoProcAmpSetting Hue { get; set; }
		public VideoProcAmpSetting Saturation { get; set; }
		public VideoProcAmpSetting Sharpness { get; set; }
		public VideoProcAmpSetting Gamma { get; set; }
		public VideoProcAmpSetting ColorEnable { get; set; }
		public VideoProcAmpSetting WhiteBalance { get; set; }
		public VideoProcAmpSetting BacklightCompensation { get; set; }
		public VideoProcAmpSetting Gain { get; set; }
		public Settings() { }

		private Settings(string name) : base(name) { }

		public static Settings Create(string name, IMFMediaSource imfSource)
		{
			try
			{
				IAMCameraControl imc = (IAMCameraControl) imfSource;
				IAMVideoProcAmp Ivp = (IAMVideoProcAmp) imfSource;
				return new Settings(name)
				       {
					       Pan = CameraControlSetting.Create(CameraControlProperty.Pan, imc),
					       Tilt = CameraControlSetting.Create(CameraControlProperty.Tilt, imc),
					       Roll = CameraControlSetting.Create(CameraControlProperty.Roll, imc),
					       Zoom = CameraControlSetting.Create(CameraControlProperty.Zoom, imc),
					       Exposure = CameraControlSetting.Create(CameraControlProperty.Exposure, imc),
					       Iris = CameraControlSetting.Create(CameraControlProperty.Iris, imc),
					       Focus = CameraControlSetting.Create(CameraControlProperty.Focus, imc),
					       Brightness = VideoProcAmpSetting.Create(VideoProcAmpProperty.Brightness, Ivp),
					       Contrast = VideoProcAmpSetting.Create(VideoProcAmpProperty.Contrast, Ivp),
					       Hue = VideoProcAmpSetting.Create(VideoProcAmpProperty.Hue, Ivp),
					       Saturation = VideoProcAmpSetting.Create(VideoProcAmpProperty.Saturation, Ivp),
					       Sharpness = VideoProcAmpSetting.Create(VideoProcAmpProperty.Sharpness, Ivp),
					       Gamma = VideoProcAmpSetting.Create(VideoProcAmpProperty.Gamma, Ivp),
					       ColorEnable = VideoProcAmpSetting.Create(VideoProcAmpProperty.ColorEnable, Ivp),
					       WhiteBalance = VideoProcAmpSetting.Create(VideoProcAmpProperty.WhiteBalance, Ivp),
					       BacklightCompensation = VideoProcAmpSetting.Create(VideoProcAmpProperty.BacklightCompensation, Ivp),
					       Gain = VideoProcAmpSetting.Create(VideoProcAmpProperty.Gain, Ivp)
				       };
			}
			catch { return null; }
		}

		internal void Apply(IMFMediaSource imfSource)
		{
			try
			{
				IAMCameraControl imc = (IAMCameraControl) imfSource;
				IAMVideoProcAmp Ivp = (IAMVideoProcAmp) imfSource;
				this.Pan.Apply(CameraControlProperty.Pan, imc);
				this.Tilt.Apply(CameraControlProperty.Tilt, imc);
				this.Roll.Apply(CameraControlProperty.Roll, imc);
				this.Zoom.Apply(CameraControlProperty.Zoom, imc);
				this.Exposure.Apply(CameraControlProperty.Exposure, imc);
				this.Iris.Apply(CameraControlProperty.Iris, imc);
				this.Focus.Apply(CameraControlProperty.Focus, imc);

				this.Brightness.Apply(VideoProcAmpProperty.Brightness, Ivp);
				this.Contrast.Apply(VideoProcAmpProperty.Contrast, Ivp);
				this.Hue.Apply(VideoProcAmpProperty.Hue, Ivp);
				this.Saturation.Apply(VideoProcAmpProperty.Saturation, Ivp);
				this.Sharpness.Apply(VideoProcAmpProperty.Sharpness, Ivp);
				this.Gamma.Apply(VideoProcAmpProperty.Gamma, Ivp);
				this.ColorEnable.Apply(VideoProcAmpProperty.ColorEnable, Ivp);
				this.WhiteBalance.Apply(VideoProcAmpProperty.WhiteBalance, Ivp);
				this.BacklightCompensation.Apply(VideoProcAmpProperty.BacklightCompensation, Ivp);
				this.Gain.Apply(VideoProcAmpProperty.Gain, Ivp);
			}
			catch { }
		}
	}
}