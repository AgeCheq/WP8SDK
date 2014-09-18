using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using SampleAgeCheqApp.Resources;

namespace SampleAgeCheqApp
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        /* Get the device ID */
        string strDeviceID = "";
        private string getDeviceID()
        {
            //get the publisher host id for the unique device identifier - be sure to add ID_CAP_IDENTITY_DEVICE to the App Manifest to use this
            if (strDeviceID == "")
            {
                byte[] byteDeviceID = (byte[])Microsoft.Phone.Info.DeviceExtendedProperties.GetValue("DeviceUniqueId");
                strDeviceID = Convert.ToBase64String(byteDeviceID);
                return strDeviceID;
            }
            else
            {
                return strDeviceID;
            }
        }


        /* Global Variables */
        string strDeveloperKey = "ENTER_YOUR_AGECHEQ_DEVELOPER_KEY HERE";
        string strAppID = "ENTER_YOUR_AGECHEQ_APP_ID HERE";

        /* Callback Functions */
        public void isRegistered_Callback(params object[] returnArguments)
        {
            //have to use the dispatcher to change the UI from a different thread than the original    
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                MessageBox.Show("rtn:" + returnArguments[0].ToString() + " rtnMsg:" + returnArguments[1].ToString() +
                    " agecheq_deviceregistered:" + returnArguments[2].ToString() + " agegate_deviceregistered:" + returnArguments[3].ToString());
            });

        }
        public void register_Callback(params object[] returnArguments)
        {
            //have to use the dispatcher to change the UI from a different thread than the original    
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                MessageBox.Show("rtn:" + returnArguments[0].ToString() + " rtnMsg:" + returnArguments[1].ToString() );
            });

        }
        public void agegate_Callback(params object[] returnArguments)
        {
            //have to use the dispatcher to change the UI from a different thread than the original    
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                MessageBox.Show("rtn:" + returnArguments[0].ToString() + " rtnMsg:" + returnArguments[1].ToString());
            });

        }

        public void associateData_Callback(params object[] returnArguments)
        {
            //have to use the dispatcher to change the UI from a different thread than the original    
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                MessageBox.Show("rtn:" + returnArguments[0].ToString() + " rtnMsg:" + returnArguments[1].ToString());
            });

        }

        public void check_Callback(params object[] returnArguments)
        {
            //have to use the dispatcher to change the UI from a different thread than the original    
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                //parse out the associated data
                string strAssociatedData = "";

                //get the last argument and hack out the associated data
                List<AgeCheqLib.AgeCheq.objAssociateddata> objAssData = new List<AgeCheqLib.AgeCheq.objAssociateddata>();
                if (returnArguments[15].ToString() != "AgeCheqLib.AgeCheq+objAssociateddataEmpty")
                {
                    objAssData = (List<AgeCheqLib.AgeCheq.objAssociateddata>)returnArguments[15];
                }



                

                for (int x=0; x<objAssData.Count; x++) {
                    strAssociatedData = strAssociatedData + objAssData[x].key + " : " + objAssData[x].value + "  ";
                }

                MessageBox.Show("rtn:" + returnArguments[0].ToString() + " rtnMsg:" + returnArguments[1].ToString() +
                    " checkType: " +  returnArguments[2].ToString() + " agecheq_deviceregistered: " + returnArguments[3].ToString() +
                    " agecheq_appauthorized: " +  returnArguments[4].ToString() + " agecheq_appblocked: " + returnArguments[5].ToString() +
                    " agecheq_parentverified: " +  returnArguments[6].ToString() + " agecheq_under13: " + returnArguments[7].ToString() +
                    " agecheq_under18: " +  returnArguments[8].ToString() + " agecheq_underdevage: " + returnArguments[9].ToString() +
                    " agecheq_trials: " + returnArguments[10].ToString() +  " agegate_deviceregistered: " +  returnArguments[11].ToString() + 
                    " agegate_under13: " + returnArguments[12].ToString() + " agegate_under18: " +  returnArguments[13].ToString() + 
                    " agegate_underdevage: " + returnArguments[14].ToString() + " associatedata: " + strAssociatedData
                    );
            });

        }

        
        

        /* AgeCheq SDK Calls */
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            AgeCheqLib.AgeCheq.isRegistered(isRegistered_Callback, strDeveloperKey, getDeviceID());
        }


        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            AgeCheqLib.AgeCheq.register(register_Callback, strDeveloperKey, getDeviceID(), txtParentUsername.Text, "New Windows Phone");
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            AgeCheqLib.AgeCheq.agegate(agegate_Callback,strDeveloperKey,getDeviceID(),txtYear.Text,txtMonth.Text,txtDay.Text);
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            AgeCheqLib.AgeCheq.associateData(associateData_Callback, strDeveloperKey, getDeviceID(), strAppID, txtKey.Text, txtValue.Text);
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            AgeCheqLib.AgeCheq.check(check_Callback, strDeveloperKey, getDeviceID(), strAppID);
        }

     


    }
}
