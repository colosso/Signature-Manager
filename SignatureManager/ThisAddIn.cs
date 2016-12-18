using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Outlook = Microsoft.Office.Interop.Outlook;
using Office = Microsoft.Office.Core;
using Microsoft.Win32;

namespace SignatureManager
{
    public partial class ThisAddIn
    {
        public static List<string> AccountNames = new List<string>();

        public Outlook.Application OutlookApplication;
        public Outlook.Inspectors OutlookInspectors;
        public Outlook.Inspector OutlookInspector;
        public Outlook.MailItem OutlookMailItem;

        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            OutlookInspectors = OutlookApplication.Inspectors;
            OutlookInspectors.NewInspector += new Microsoft.Office.Interop.Outlook.InspectorsEvents_NewInspectorEventHandler(OutlookInspectors_NewInspector);
            OutlookApplication.ItemSend += new Microsoft.Office.Interop.Outlook.ApplicationEvents_11_ItemSendEventHandler(OutlookApplication_ItemSend);
        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
            // Note: Outlook no longer raises this event. If you have code that 
            //    must run when Outlook shuts down, see http://go.microsoft.com/fwlink/?LinkId=506785
        }


        void OutlookApplication_ItemSend(object Item, ref bool Cancel)
        {
            // loop registry 
            foreach (string value in Registry.CurrentUser.OpenSubKey("SignatureManager").GetValueNames())
            {
                // check if current sender is set in registry to send signature
                if (OutlookMailItem.SendUsingAccount.DisplayName == value)
                {
                    // sign mail
                    OutlookInspector.CommandBars.ExecuteMso("DigitallySignMessage");
                }
            }

        }

        void OutlookInspectors_NewInspector(Microsoft.Office.Interop.Outlook.Inspector Inspector)
        {
            // update global inspector obj, isnpector wil be used to send signing command
            OutlookInspector = (Outlook.Inspector)Inspector;

            if (Inspector.CurrentItem is Outlook.MailItem)
            {
                // update global mailitem obj, will be used to verify sender / check if signature should be added
                OutlookMailItem = (Outlook.MailItem)Inspector.CurrentItem;
            }

        }

        #region VSTO generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);

            // create gloabal application obj
            OutlookApplication = this.Application;

            // create list of all available accounts registered in outlook
            foreach (Outlook.Account item in this.Application.Session.Accounts)
            {
                ThisAddIn.AccountNames.Add(item.DisplayName);
            }
            
        }
        
        #endregion
    }
}
