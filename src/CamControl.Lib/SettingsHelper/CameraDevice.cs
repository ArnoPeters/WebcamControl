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

//using System.Text.Json;
//using System.Text.Json.Serialization;

namespace MFCaptureD3D
{
	public class CameraDevice : COMBase, IDisposable
	{
		private readonly MFDevice _PDevice;

		public CameraDevice(MFDevice pDevice)
		{
			_PDevice = pDevice;

			HResult hr = MFExtern.MFStartup(0x20070, MFStartup.Lite);
			MFError.ThrowExceptionForHR(hr);
		}

		private void SetDevice(Action<IMFMediaSource> action)
		{
			this.SetDevice<object>(d =>
			{
				action(d);
				return null;
			});
		}

		private T SetDevice<T>(Func<IMFMediaSource, T> action)
		{
			T result = default;
			IMFActivate pActivate = _PDevice.Activator;
			IMFMediaSource pSource = null;
			IMFAttributes pAttributes = null;

			lock (this)
			{
				try
				{
					// Create the media source for the device.
					HResult hr = pActivate.ActivateObject(typeof(IMFMediaSource).GUID, out object o);
					if (Succeeded(hr))
					{
						pSource = (IMFMediaSource) o;
						result = action(pSource);
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

			return result;
		}

		private void ReleaseUnmanagedResources()
		{
			// Shutdown the Media Foundation platform
			//HResult hr = MFExtern.MFShutdown();
			//MFError.ThrowExceptionForHR(hr);
		}

		~CameraDevice()
		{
			this.ReleaseUnmanagedResources();
		}

		public Capabilities Capabilities()
		{
			return this.SetDevice(d => { return new Capabilities(_PDevice.Name, d); });
		}

		public Settings GetSettings()
		{
			return this.SetDevice(d => { return Settings.Create(_PDevice.Name, d); });
		}

		public void ApplySettings(Settings settings)
		{
			this.SetDevice(d => { settings.Apply(d); });
		}

		public void Dispose()
		{
			_PDevice?.Dispose();
			this.ReleaseUnmanagedResources();
			GC.SuppressFinalize(this);
		}
	}
}
/*MFMediaSource s = new MFMediaSource(pSource);
						s.Settings.WhiteBalance.SettingMethod = VideoProcAmpFlags.Manual;
						int a = s.Settings.WhiteBalance.Value;
						s.Settings.WhiteBalance.Value = 3000;
						//s.Settings.Exposure.SettingMethod = CameraControlFlags.Manual;
						//s.Settings.Exposure.Value = -7;
						s.Apply();

						s.Settings.WhiteBalance.Value = a;
						s.Settings.WhiteBalance.SettingMethod = VideoProcAmpFlags.Auto;
						s.Apply();

						//SETTINGS GO HERE - after opening the camera. 
*/