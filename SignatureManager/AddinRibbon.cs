using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Tools.Ribbon;

namespace SignatureManager
{
    public partial class AddinRibbon
    {
        private void AddinRibbon_Load(object sender, RibbonUIEventArgs e)
        {

        }

        private void but_settings_Click(object sender, RibbonControlEventArgs e)
        {
           

            SettingsForm form = new SettingsForm();

            form.ShowDialog();
        }
    }
}
