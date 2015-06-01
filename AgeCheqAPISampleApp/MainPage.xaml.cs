using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using AgeCheqAPISampleApp.Resources;

namespace AgeCheqAPISampleApp
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        /* Global Variables */
        string strDeveloperKey = "06c3a8ba-8d2e-429c-9ce6-4f86a70815d6";
        string strAppID = "cb8741ae-e7d1-4e92-99d4-851fb4566603";

        string strAgeCheqPIN = "";


        /* Callback Functions */
        public void check_Callback(params object[] returnArguments)
        {


            //have to use the dispatcher to change the UI from a different thread than the original    
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                MessageBox.Show("rtn:" + returnArguments[0].ToString() + " rtnMsg:" + returnArguments[1].ToString() +
                    " agecheq_deviceregistered:" + returnArguments[2].ToString() + " agegate_deviceregistered:" + returnArguments[3].ToString());
            });

        }


        public void associate_Callback(params object[] returnArguments)
        {


            //have to use the dispatcher to change the UI from a different thread than the original    
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                MessageBox.Show("rtn:" + returnArguments[0].ToString() + " rtnMsg:" + returnArguments[1].ToString());
            });

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //do check
            AgeCheqLib.AgeCheq.check(check_Callback, strDeveloperKey, strAppID, txtAgeCheqPIN.Text, true);

            strAgeCheqPIN = txtAgeCheqPIN.Text;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (strAgeCheqPIN != "")
            {

                //do associate
                AgeCheqLib.AgeCheq.associate(associate_Callback, strDeveloperKey, strAppID, strAgeCheqPIN, txtAssociateData.Text, true);

            }
            else
            {
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    MessageBox.Show("Make sure to do a check first to store a valid AgeCheq PIN");
                });
            }
        }


    }
}