using MediaFoundation;

namespace MFCaptureD3D.Sample1
{
	public class DeviceSettings
	{
		public DeviceSettings(IMFMediaSource imfSource)
		{
			this.Pan = new camContrProp(CameraControlProperty.Pan, imfSource);
			this.Tilt = new camContrProp(CameraControlProperty.Tilt, imfSource);
			this.Roll = new camContrProp(CameraControlProperty.Roll, imfSource);
			this.Zoom = new camContrProp(CameraControlProperty.Zoom, imfSource);
			this.Exposure = new camContrProp(CameraControlProperty.Exposure, imfSource);
			this.Iris = new camContrProp(CameraControlProperty.Iris, imfSource);
			this.Focus = new camContrProp(CameraControlProperty.Focus, imfSource);


			this.Brightness = new VideoAmpProperty(VideoProcAmpProperty.Brightness, imfSource);
			this.Contrast = new VideoAmpProperty(VideoProcAmpProperty.Contrast, imfSource);
			this.Hue = new VideoAmpProperty(VideoProcAmpProperty.Hue, imfSource);
			this.Saturation = new VideoAmpProperty(VideoProcAmpProperty.Saturation, imfSource);
			this.Sharpness = new VideoAmpProperty(VideoProcAmpProperty.Sharpness, imfSource);
			this.Gamma = new VideoAmpProperty(VideoProcAmpProperty.Gamma, imfSource);
			this.ColorEnable = new VideoAmpProperty(VideoProcAmpProperty.ColorEnable, imfSource);
			this.WhiteBalance = new VideoAmpProperty(VideoProcAmpProperty.WhiteBalance, imfSource);
			this.BacklightCompensation = new VideoAmpProperty(VideoProcAmpProperty.BacklightCompensation, imfSource);
			this.Gain = new VideoAmpProperty(VideoProcAmpProperty.Gain, imfSource);


		}
		public void Apply(IMFMediaSource imfSource)
		{
			this.Pan.Apply(imfSource);
			this.Tilt.Apply(imfSource);
			this.Roll.Apply(imfSource);
			this.Zoom.Apply(imfSource);
			this.Exposure.Apply(imfSource);
			this.Iris.Apply(imfSource);
			this.Focus.Apply(imfSource);
			this.Brightness.Apply(imfSource);
			this.Contrast.Apply(imfSource);
			this.Hue.Apply(imfSource);
			this.Saturation.Apply(imfSource);
			this.Sharpness.Apply(imfSource);
			this.Gamma.Apply(imfSource);
			this.ColorEnable.Apply(imfSource);
			this.WhiteBalance.Apply(imfSource);
			this.BacklightCompensation.Apply(imfSource);
			this.Gain.Apply(imfSource);

		}


		public camContrProp Pan { get; set; }
		public camContrProp Tilt { get; set; }
		public camContrProp Roll { get; set; }
		public camContrProp Zoom { get; set; }
		public camContrProp Exposure { get; set; }
		public camContrProp Iris { get; set; }
		public camContrProp Focus { get; set; }


		public VideoAmpProperty Brightness { get; set; }
		public VideoAmpProperty Contrast { get; set; }
		public VideoAmpProperty Hue { get; set; }
		public VideoAmpProperty Saturation { get; set; }
		public VideoAmpProperty Sharpness { get; set; }
		public VideoAmpProperty Gamma { get; set; }
		public VideoAmpProperty ColorEnable { get; set; }
		public VideoAmpProperty WhiteBalance { get; set; }
		public VideoAmpProperty BacklightCompensation { get; set; }
		public VideoAmpProperty Gain { get; set; }
	}
}