using System;
using System.Diagnostics;
using MediaFoundation;
using MFCaptureD3D.Sample1;
namespace MFCaptureD3D.Sample1
{

	public static class X
	{

		public static void Y(this IMFMediaSource imfSource)
		{

			//IMFMediaSource imfSource = ppv as IMFMediaSource;
			if (imfSource != null)
			{
				try
				{
					// control the 'camera'
					var imc = imfSource as IAMCameraControl;
					int rMin, rMax, rDelta, rDeflt;
					CameraControlFlags cflag;
					var rslt = imc.GetRange(MFCaptureD3D.Sample1.CameraControlProperty.Exposure, out rMin, out rMax, out rDelta, out rDeflt, out cflag);
					// mdDevice.CameraController = imc;

					// control the 'video'
					var imu = imfSource as IAMVideoProcAmp;
					VideoProcAmpFlags vflag;
					rslt = imu.GetRange(VideoProcAmpProperty.WhiteBalance, out rMin, out rMax, out rDelta, out rDeflt, out vflag);
					// mdDevice.VideoController = imu;
				}
				catch (Exception ex)
				{
					//Utility.Logger.Error("Failed to create: " + ex.Message);
				}
			}
		}
	}
	public class MFMediaSource
	{

		public MFMediaSource(IMFMediaSource imfSource)
		{


			Settings = new DeviceSettings(imfSource);
      ImfSource = imfSource;
    }
		public DeviceSettings Settings { get; }
    public IMFMediaSource ImfSource { get; }

    public void Apply()
    {
			Settings.Apply(ImfSource);
    }
	}




	public class DeviceSettings
	{
		public DeviceSettings(IMFMediaSource imfSource)
		{
			Pan = new camContrProp(CameraControlProperty.Pan, imfSource);
			Tilt = new camContrProp(CameraControlProperty.Tilt, imfSource);
			Roll = new camContrProp(CameraControlProperty.Roll, imfSource);
			Zoom = new camContrProp(CameraControlProperty.Zoom, imfSource);
			Exposure = new camContrProp(CameraControlProperty.Exposure, imfSource);
			Iris = new camContrProp(CameraControlProperty.Iris, imfSource);
			Focus = new camContrProp(CameraControlProperty.Focus, imfSource);


			Brightness = new VideoAmpProperty(VideoProcAmpProperty.Brightness, imfSource);
			Contrast = new VideoAmpProperty(VideoProcAmpProperty.Contrast, imfSource);
			Hue = new VideoAmpProperty(VideoProcAmpProperty.Hue, imfSource);
			Saturation = new VideoAmpProperty(VideoProcAmpProperty.Saturation, imfSource);
			Sharpness = new VideoAmpProperty(VideoProcAmpProperty.Sharpness, imfSource);
			Gamma = new VideoAmpProperty(VideoProcAmpProperty.Gamma, imfSource);
			ColorEnable = new VideoAmpProperty(VideoProcAmpProperty.ColorEnable, imfSource);
			WhiteBalance = new VideoAmpProperty(VideoProcAmpProperty.WhiteBalance, imfSource);
			BacklightCompensation = new VideoAmpProperty(VideoProcAmpProperty.BacklightCompensation, imfSource);
			Gain = new VideoAmpProperty(VideoProcAmpProperty.Gain, imfSource);


		}
		public void Apply(IMFMediaSource imfSource)
    {
			Pan.Apply(imfSource);
			Tilt.Apply(imfSource);
			Roll.Apply(imfSource);
			Zoom.Apply(imfSource);
			Exposure.Apply(imfSource);
			Iris.Apply(imfSource);
			Focus.Apply(imfSource);
			Brightness.Apply(imfSource);
			Contrast.Apply(imfSource);
			Hue.Apply(imfSource);
			Saturation.Apply(imfSource);
			Sharpness.Apply(imfSource);
			Gamma.Apply(imfSource);
			ColorEnable.Apply(imfSource);
			WhiteBalance.Apply(imfSource);
			BacklightCompensation.Apply(imfSource);
			Gain.Apply(imfSource);

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

	public class Property
	{
		public Range Range { get; set; }
		public int CurrentValue { get; set; }
	}

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
			SupportedFlags = cflag;
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
			imc.Set(Name, this.CurrentValue, this.CurrentFlag);
		}
  }

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
			SupportedFlags = cflag;
			HResult rslt2 = imc.Get(property, out current, out cflag);
			this.CurrentValue = current;
			
			this.CurrentFlag = cflag;
			this.Name = property;
		}


		public void Apply(IMFMediaSource imfSource)
		{
			IAMVideoProcAmp imc = (IAMVideoProcAmp)imfSource;
			imc.Set(Name, this.CurrentValue, this.CurrentFlag);
		}


		public VideoProcAmpFlags SupportedFlags { get; set; }
		public VideoProcAmpFlags CurrentFlag { get; set; }
	}

}