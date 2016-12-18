using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SignatureManager
{
    public partial class SettingsForm : Form
    {

        public SettingsForm()
        {

            InitializeComponent();
        }

        private void but_save_Click(object sender, EventArgs e)
        {
            foreach (string item in cklist_accounts.SelectedItems)
            {
                Registry.CurrentUser.OpenSubKey("SignatureManager", RegistryKeyPermissionCheck.ReadWriteSubTree).SetValue(item, "1");
            }
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {

            if (Registry.CurrentUser.OpenSubKey("SignatureManager") == null)
            {
                Registry.CurrentUser.CreateSubKey("SignatureManager");
            }
            else
            {

                foreach (string account in ThisAddIn.AccountNames)
                {
                    bool check = false;

                    foreach (string value in Registry.CurrentUser.OpenSubKey("SignatureManager").GetValueNames())
                    {


                        if (value == account)
                        {
                            cklist_accounts.Items.Add(account, true);
                            check = true;
                        }

                    }

                    if (!check)
                    {
                        cklist_accounts.Items.Add(account, false);
                    }

                }


             
            }


        }
    }
}
