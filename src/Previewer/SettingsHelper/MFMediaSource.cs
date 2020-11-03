using System;
using MediaFoundation;
using MFCaptureD3D.Sample1;
namespace MFCaptureD3D.Sample1
{

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
}