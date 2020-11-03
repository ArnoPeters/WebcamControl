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
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Security;

using MediaFoundation;
using MediaFoundation.Misc;

namespace MFCaptureD3D
{
    partial class CaptureDevice : Form, IDisposable
    {
        const int WM_DEVICECHANGE = 0x0219;
        // Category for capture devices
        private readonly Guid KSCATEGORY_CAPTURE = new Guid("65E8773D-8F56-11D0-A3B9-00A0C9223196");

        private RegisterDeviceNotifications m_rdn;

        public CaptureDevice()
        {
            InitializeComponent();

            m_rdn = new RegisterDeviceNotifications(this.Handle, KSCATEGORY_CAPTURE);
            LoadDevicesList();
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_DEVICECHANGE:
                    OnDeviceChange(m.WParam, m.LParam);
                    break;

                default:
                    base.WndProc(ref m);
                    break;
            }
        }

        public MFDevice SelectedCaptureDevice
        {
            get { return lstbDevices.SelectedItem as MFDevice; }
        }

        private void LoadDevicesList()
        {
            // Populate the list with the friendly names of the devices.
            MFDevice[] arDevices = MFDevice.GetDevicesOfCat(CLSID.MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE_VIDCAP_GUID);

            lstbDevices.BeginUpdate();
            lstbDevices.Items.Clear();

            foreach (MFDevice m in arDevices)
            {
                lstbDevices.Items.Add(m);
            }
            lstbDevices.EndUpdate();

            bttOK.Enabled = false;
        }

        private void OnDeviceChange(IntPtr reason, IntPtr pHdr)
        {
            // Check for the right category of event
            if (m_rdn.CheckEventDetails(reason, pHdr))
            {
                ClearOld();
                LoadDevicesList();
            }
        }

        private void bttCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void bttOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void lstbDevices_DoubleClick(object sender, EventArgs e)
        {
            if (lstbDevices.SelectedIndex >= 0)
            {
                bttOK_Click(null, null);
            }
        }

        private void ClearOld()
        {
            foreach (MFDevice m in lstbDevices.Items)
            {
                m.Dispose();
            }
        }

        public new void Dispose()
        {
            ClearOld();
            m_rdn.Dispose();
            base.Dispose();
            GC.SuppressFinalize(this);
        }

        private void lstbDevices_SelectedIndexChanged(object sender, EventArgs e)
        {
            bttOK.Enabled = lstbDevices.SelectedIndex >= 0;
        }
    }

   

}