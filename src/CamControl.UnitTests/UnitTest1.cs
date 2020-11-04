using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using MFCaptureD3D;
using MFCaptureD3D.Sample1;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CamControl.UnitTests
{
	[TestClass]
	public class UnitTest1
	{
		[TestMethod]
		public async Task TestMethod1()
		{
			MFDevice[] arDevices = VidCapDevices.List();

			arDevices = arDevices.Where(d => d.Name == "Logitech BRIO").ToArray();

			//List<Capabilities> caps = VidCapDevices.GetCapabilities(arDevices).ToList();

			//MFDevice[] arDevices2 = VidCapDevices.List();

			List<Settings> settingsSet = VidCapDevices.GetSettings(arDevices).ToList();
			
		
		}
	}

	
}