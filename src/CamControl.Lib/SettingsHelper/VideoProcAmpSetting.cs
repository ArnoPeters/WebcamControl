using System.Diagnostics;
using MediaFoundation;

namespace MFCaptureD3D.Sample1
{
	[DebuggerDisplay("[{SettingMethod}] {Value}")]
	public class VideoProcAmpSetting
	{
		public int Value { get; set; }

		public VideoProcAmpFlags SettingMethod { get; set; }

		public static VideoProcAmpSetting Create(VideoProcAmpProperty property, IAMVideoProcAmp imc)
		{
			return new VideoProcAmpSetting().Get(property, imc);
		}

		internal VideoProcAmpSetting Get(VideoProcAmpProperty property, IAMVideoProcAmp imc)
		{
			HResult rslt2 = imc.Get(property, out int current, out VideoProcAmpFlags cflag);
			this.Value = current;
			this.SettingMethod = cflag;
			return this;
		}

		internal void Apply(VideoProcAmpProperty property, IAMVideoProcAmp imc)
		{
			imc.Set(property, this.Value, this.SettingMethod);
			//Ensure to get the actual value: if the device does not support setting values on this property, it will fail silently.
			this.Get(property, imc);
		}
	}
}