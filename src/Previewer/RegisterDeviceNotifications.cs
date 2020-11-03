using System;
using System.Runtime.InteropServices;
using System.Security;

namespace MFCaptureD3D
{
	public class RegisterDeviceNotifications : IDisposable
	{
		#region Definitions

		[StructLayout(LayoutKind.Sequential)]
		private class DEV_BROADCAST_HDR
		{
			public int dbch_size;
			public int dbch_devicetype;
			public int dbch_reserved;
		}

		[StructLayout(LayoutKind.Sequential)]
		private class DEV_BROADCAST_DEVICEINTERFACE
		{
			public int dbcc_size;
			public int dbcc_devicetype;
			public int dbcc_reserved;
			public Guid dbcc_classguid;
			public char dbcc_name;
		}

		[DllImport("User32.dll",
			 CharSet = CharSet.Unicode,
			 ExactSpelling = true,
			 EntryPoint = "RegisterDeviceNotificationW",
			 SetLastError = true),
		 SuppressUnmanagedCodeSecurity]
		private static extern IntPtr RegisterDeviceNotification(
			IntPtr hDlg,
			[MarshalAs(UnmanagedType.LPStruct)] DEV_BROADCAST_DEVICEINTERFACE di,
			int dwFlags
		);

		[DllImport("User32.dll", ExactSpelling = true, SetLastError = true), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool UnregisterDeviceNotification(
			IntPtr hDlg
		);

		private const int DEVICE_NOTIFY_WINDOW_HANDLE = 0x00000000;
		private const int DBT_DEVTYP_DEVICEINTERFACE = 0x00000005;
		private const int DBT_DEVICEARRIVAL = 0x8000;
		private const int DBT_DEVICEREMOVECOMPLETE = 0x8004;

		#endregion

		// Handle of the notification.  Used by unregister
		IntPtr m_hdevnotify = IntPtr.Zero;

		// Category of events
		Guid m_Category;

		public RegisterDeviceNotifications(IntPtr hWnd, Guid gCat)
		{
			m_Category = gCat;

			DEV_BROADCAST_DEVICEINTERFACE di = new DEV_BROADCAST_DEVICEINTERFACE();

			// Register to be notified of events of category gCat
			di.dbcc_size = Marshal.SizeOf(di);
			di.dbcc_devicetype = DBT_DEVTYP_DEVICEINTERFACE;
			di.dbcc_classguid = gCat;

			m_hdevnotify = RegisterDeviceNotification(
				hWnd,
				di,
				DEVICE_NOTIFY_WINDOW_HANDLE
			);

			// If it failed, throw an exception
			if (m_hdevnotify == IntPtr.Zero)
			{
				int i = unchecked((int)0x80070000);
				i += Marshal.GetLastWin32Error();
				throw new COMException("Failed to RegisterDeviceNotifications", i);
			}
		}

		public void Dispose()
		{
			if (m_hdevnotify != IntPtr.Zero)
			{
				UnregisterDeviceNotification(m_hdevnotify);
				m_hdevnotify = IntPtr.Zero;
			}
		}

		// Static routine to parse out the device type from the IntPtr received in WndProc
		public bool CheckEventDetails(IntPtr pReason, IntPtr pHdr)
		{
			int iValue = pReason.ToInt32();

			// Check the event type
			if (iValue != DBT_DEVICEREMOVECOMPLETE && iValue != DBT_DEVICEARRIVAL)
				return false;

			// Do we have device details yet?
			if (pHdr == IntPtr.Zero)
				return false;

			// Parse the first chunk
			DEV_BROADCAST_HDR pBH = new DEV_BROADCAST_HDR();
			Marshal.PtrToStructure(pHdr, pBH);

			// Check the device type
			if (pBH.dbch_devicetype != DBT_DEVTYP_DEVICEINTERFACE)
				return false;

			// Only parse this if the right device type
			DEV_BROADCAST_DEVICEINTERFACE pDI = new DEV_BROADCAST_DEVICEINTERFACE();
			Marshal.PtrToStructure(pHdr, pDI);

			return (pDI.dbcc_classguid == m_Category);
		}

		// Static routine to parse out the Symbolic name from the IntPtr received in WndProc
		public static string ParseDeviceSymbolicName(IntPtr pHdr)
		{
			IntPtr ip = Marshal.OffsetOf(typeof(DEV_BROADCAST_DEVICEINTERFACE), "dbcc_name");
			return Marshal.PtrToStringUni(pHdr + (ip.ToInt32()));
		}
	}
}