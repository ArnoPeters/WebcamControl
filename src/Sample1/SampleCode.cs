using System;
using MediaFoundation;
using MFCaptureD3D.Sample1;

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
				var rslt = imc.GetRange(CameraControlProperty.Exposure, out rMin, out rMax, out rDelta, out rDeflt, out cflag);
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