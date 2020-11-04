using System;
using System.Runtime.InteropServices;
using MediaFoundation;

namespace MFCaptureD3D
{
	public class MFDevice : IDisposable
	{
		private string m_FriendlyName;
		private string m_SymbolicName;

		public IMFActivate Activator { get; private set; }

		public string Name
		{
			get
			{
				if (m_FriendlyName == null)
				{
					HResult hr = 0;
					int iSize = 0;

					hr = this.Activator.GetAllocatedString(MFAttributesClsid.MF_DEVSOURCE_ATTRIBUTE_FRIENDLY_NAME, out m_FriendlyName, out iSize);
				}

				return m_FriendlyName;
			}
		}

		/// <summary>
		///   Returns a unique identifier for a device
		/// </summary>
		public string SymbolicName
		{
			get
			{
				if (m_SymbolicName == null)
				{
					int iSize;
					HResult hr = this.Activator.GetAllocatedString(MFAttributesClsid.MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE_VIDCAP_SYMBOLIC_LINK, out m_SymbolicName, out iSize);
				}

				return m_SymbolicName;
			}
		}

		public MFDevice(IMFActivate Mon)
		{
			this.Activator = Mon;
			m_FriendlyName = null;
			m_SymbolicName = null;
		}

		~MFDevice()
		{
			this.Dispose();
		}

		/// <summary>
		///   Returns an array of DsDevices of type devcat.
		/// </summary>
		/// <param name="cat">Any one of FilterCategory</param>
		public static MFDevice[] GetDevicesOfCat(Guid FilterCategory)
		{
			// Use arrayList to build the retun list since it is easily resizable
			MFDevice[] devret = null;
			IMFActivate[] ppDevices;

			//////////

			HResult hr = 0;
			IMFAttributes pAttributes = null;

			// Initialize an attribute store. We will use this to
			// specify the enumeration parameters.

			hr = MFExtern.MFCreateAttributes(out pAttributes, 1);

			// Ask for source type = video capture devices
			if (hr >= 0) { hr = pAttributes.SetGUID(MFAttributesClsid.MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE, FilterCategory); }

			// Enumerate devices.
			int cDevices;
			if (hr >= 0)
			{
				hr = MFExtern.MFEnumDeviceSources(pAttributes, out ppDevices, out cDevices);

				if (hr >= 0)
				{
					devret = new MFDevice[cDevices];

					for (int x = 0; x < cDevices; x++) { devret[x] = new MFDevice(ppDevices[x]); }
				}
			}

			if (pAttributes != null) { Marshal.ReleaseComObject(pAttributes); }

			return devret;
		}

		public override string ToString()
		{
			return this.Name;
		}

		public void Dispose()
		{
			if (this.Activator != null)
			{
				Marshal.ReleaseComObject(this.Activator);
				this.Activator = null;
			}
			m_FriendlyName = null;
			m_SymbolicName = null;

			GC.SuppressFinalize(this);
		}
	}
}