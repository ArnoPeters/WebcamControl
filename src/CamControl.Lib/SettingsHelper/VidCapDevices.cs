using System.Collections.Generic;
using System.Linq;
using MediaFoundation;
using MFCaptureD3D;

namespace CamControl.UnitTests
{
	public class VidCapDevices
	{
		//TODO: Maybe not return array but collection with indexer by name?
		public static MFDevice[] List()
		{
			return MFDevice.GetDevicesOfCat(CLSID.MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE_VIDCAP_GUID);
		}

		public static IEnumerable<Capabilities> GetCapabilities(params MFDevice[] devices)
		{
			foreach (MFDevice mfDevice in devices)
			{
				using (CameraDevice cameraDevice = new CameraDevice(mfDevice)) { yield return cameraDevice.Capabilities(); }
			}
		}

		public static IEnumerable<Settings> GetSettings(params MFDevice[] devices)
		{
			foreach (MFDevice mfDevice in devices)
			{
				using (CameraDevice cameraDevice = new CameraDevice(mfDevice))
				{
					Settings settings = cameraDevice.GetSettings();
					if (settings != null) { yield return settings; }
				}
			}
		}

		public static void ApplySettings(IReadOnlyCollection<Settings> settingsCollection)
		{
			MFDevice[] devices = List();

			foreach (MFDevice device in devices)
			{
				Settings settings = settingsCollection.FirstOrDefault(x => x.Name == device.Name);
				if (settings != null)
				{
					using (CameraDevice cameraDevice = new CameraDevice(device)) { cameraDevice.ApplySettings(settings); }
				}
			}
		}
	}
}