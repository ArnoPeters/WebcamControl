using System;
using MediaFoundation;
using MFCaptureD3D;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CamControl.UnitTests
{
	[TestClass]
	public class UnitTest1
	{
		[TestMethod]
		public void TestMethod1()
		{
			MFDevice[] arDevices = MFDevice.GetDevicesOfCat(CLSID.MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE_VIDCAP_GUID);
		}
	}
}
