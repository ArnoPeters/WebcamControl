using SharpDX.Mathematics.Interop;

namespace MFCaptureD3D
{
	public static class RawRectangleExtensions
	{

		public static int Height(this RawRectangle rect)
		{
			return rect.Bottom - rect.Top;
		}

		public static int Width(this RawRectangle rect)
		{
			return rect.Right - rect.Left;
		}
	}
}