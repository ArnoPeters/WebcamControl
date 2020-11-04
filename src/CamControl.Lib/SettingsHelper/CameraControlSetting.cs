using System.Diagnostics;
using MediaFoundation;

namespace MFCaptureD3D.Sample1
{
	[DebuggerDisplay("[{SettingMethod}] {Value}")]
	public class CameraControlSetting
	{
		public int Value { get; set; }

		public CameraControlFlags SettingMethod { get; set; }

		public static CameraControlSetting Create(CameraControlProperty property, IAMCameraControl imc)
		{
			return new CameraControlSetting().Get(property, imc);
		}

		internal CameraControlSetting Get(CameraControlProperty property, IAMCameraControl imc)
		{
			HResult rslt2 = imc.Get(property, out int current, out CameraControlFlags cflag);
			this.Value = current;
			this.SettingMethod = cflag;
			return this;
		}

		internal void Apply(CameraControlProperty property, IAMCameraControl imc)
		{
			imc.Set(property, this.Value, this.SettingMethod);
			//Ensure to get the actual value: if the device does not support setting values on this property, it will fail silently.
			this.Get(property, imc);
		}
	}
}