/****************************************************************************
While the underlying library is covered by LGPL or BSD, this sample is released
as public domain.  It is distributed in the hope that it will be useful, but
WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY
or FITNESS FOR A PARTICULAR PURPOSE.
 *
Written by:
Gerardo Hernandez
BrightApp.com

Modified by snarfle
*****************************************************************************/

using System;
using MediaFoundation;
using MediaFoundation.Misc;
using MFCaptureD3D.Sample1;

//using System.Text.Json;
//using System.Text.Json.Serialization;

namespace MFCaptureD3D
{
	public class CPreview2 : COMBase, IDisposable
	{
		private readonly MFDevice _PDevice;

		public CPreview2(MFDevice pDevice)
		{
			_PDevice = pDevice;

			HResult hr = MFExtern.MFStartup(0x20070, MFStartup.Lite);
			MFError.ThrowExceptionForHR(hr);
		}

		public HResult SetDevice()
		{
			HResult hr = HResult.S_OK;

			IMFActivate pActivate = _PDevice.Activator;
			IMFMediaSource pSource = null;
			IMFAttributes pAttributes = null;

			lock (this)
			{
				try
				{
					// Create the media source for the device.
					hr = pActivate.ActivateObject(typeof(IMFMediaSource).GUID, out object o);

					if (Succeeded(hr))
					{
						pSource = (IMFMediaSource) o;

						MFMediaSource s = new MFMediaSource(pSource);
						s.Settings.WhiteBalance.CurrentFlag = VideoProcAmpFlags.Manual;
						int a = s.Settings.WhiteBalance.CurrentValue;
						s.Settings.WhiteBalance.CurrentValue = 3000;
						//s.Settings.Exposure.CurrentFlag = CameraControlFlags.Manual;
						//s.Settings.Exposure.CurrentValue = -7;
						s.Apply();

						s.Settings.WhiteBalance.CurrentValue = a;
						s.Settings.WhiteBalance.CurrentFlag = VideoProcAmpFlags.Auto;
						s.Apply();

						//SETTINGS GO HERE - after opening the camera. 
					}

					if (Failed(hr))
					{
						if (pSource != null) { pSource.Shutdown(); }
					}
				}
				finally
				{
					SafeRelease(pSource);
					SafeRelease(pAttributes);
				}
			}

			return hr;
		}

		private void ReleaseUnmanagedResources()
		{
			// Shutdown the Media Foundation platform
			HResult hr = MFExtern.MFShutdown();
			MFError.ThrowExceptionForHR(hr);
		}

		~CPreview2()
		{
			this.ReleaseUnmanagedResources();
		}

		public void Dispose()
		{
			_PDevice?.Dispose();
			this.ReleaseUnmanagedResources();
			GC.SuppressFinalize(this);
		}
	}
}