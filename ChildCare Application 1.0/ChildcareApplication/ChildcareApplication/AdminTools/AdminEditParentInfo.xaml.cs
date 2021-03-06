﻿using System;
using System.Data;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;


namespace AdminTools {
    /// <summary>
    /// Interaction logic for win_AdminEditParentInfo.xaml
    /// </summary>
    public partial class AdminEditParentInfo : Window {

        private LoadParentInfoDatabase db;
        public AdminEditParentInfo(string parentID) {

            InitializeComponent();
            AddStates();
            this.db = new LoadParentInfoDatabase();
            cnv_ParentIcon.Background = new SolidColorBrush(Colors.Aqua); //setting canvas color so we can see it
            btn_Delete.Background = new SolidColorBrush(Colors.Red);
            LoadParentInfo(parentID);
        }

        private void btn_Submit_Click(object sender, RoutedEventArgs e) {

            bool formNotComplete = true; 
            formNotComplete = CheckIfNull(); 

            //save all information to database
            if (formNotComplete == false)
            {
                string pID, firstName, lastName, address, address2, city, state, zip, email, phone, path;

                pID = txt_IDNumber.Text; 
                firstName = txt_FirstName.Text;
                lastName = txt_LastName.Text;

                phone = txt_PhoneNumber.Text; 
                email = txt_Email.Text; 

                address = txt_Address.Text;
                address2 = txt_Address2.Text; 
                city = txt_City.Text;
                state = cbo_State.Text; //dont know if this will work yet
                zip = txt_Zip.Text;
                path = txt_FilePath.Text; 

                this.db.UpdateParentInfo(pID, firstName, lastName, phone, email, address, address2, city, state, zip, path); 


               //ClearFields();
            }
        }//end btn_Submit_Click

        private void btn_Finish_Click(object sender, RoutedEventArgs e) {

            AdminMenu adminMenu = new AdminMenu();
            adminMenu.Show();
            this.Close(); 
        }//end btn_Finish_Click

        private void btn_AddChild_Click(object sender, RoutedEventArgs e) {

            string pID = txt_IDNumber.Text; 
            win_AdminEditChildInfo AdminEditChildInfo = new win_AdminEditChildInfo(pID);
            AdminEditChildInfo.Show();
            this.Close();

        }//end btn_AddChild_Click

        private void btn_Delete_Click(object sender, RoutedEventArgs e) {

            bool? delete;
             
            DeleteConformation DeleteConformation = new DeleteConformation();
            delete = DeleteConformation.ShowDialog();
            if ((bool)delete == true)
            {
                string pID = txt_IDNumber.Text;
                this.db.DeleteParentInfo(pID);
                ClearFields();
                DisableForm(); 
            }


        }//end btn_Delete_Click
        private void DisableForm() {
            btn_EditChild.IsEnabled = false;
            btn_Delete.IsEnabled = false;
            btn_SubmitInfo.IsEnabled = false;
            btn_ChangePicture.IsEnabled = false; 
        }

        private void ClearFields() {
            txt_Address.Clear();
            txt_Address2.Clear(); 
            txt_City.Clear();
            txt_FirstName.Clear();
            txt_LastName.Clear();
            txt_IDNumber.Clear();
            txt_PhoneNumber.Clear();
            txt_Zip.Clear();
            txt_Email.Clear();
            txt_IDNumber.Clear();
            txt_FilePath.Clear(); 
        }//end ClearFields

        private bool CheckIfNull() {

            if (string.IsNullOrWhiteSpace(this.txt_Address.Text))
            {
                MessageBox.Show("Please enter your address.");
                return true; 
            }

            else if (string.IsNullOrWhiteSpace(this.txt_City.Text))
            {
                MessageBox.Show("Please enter your city.");
                return true; 
            }

            else if (string.IsNullOrWhiteSpace(this.txt_Zip.Text))
            {
                MessageBox.Show("Please enter your zip.");
                return true;     
            }

            else if (string.IsNullOrWhiteSpace(this.txt_FirstName.Text))
            {
                MessageBox.Show("Please enter your first name.");
                return true;
            }

            else if (string.IsNullOrWhiteSpace(this.txt_LastName.Text))
            {
                MessageBox.Show("Please enter your last name.");
                return true;
            }

            else if (string.IsNullOrWhiteSpace(this.cbo_State.Text))
            {
                MessageBox.Show("Please enter your state.");
                return true;
            }

            else if (string.IsNullOrWhiteSpace(this.txt_Email.Text))
            {
                MessageBox.Show("Please enter your e-mail.");
                return true;
            }
            return false; 
        }//end checkIfNull

        private void LoadParentInfo(string parentID) {

            txt_IDNumber.Text = parentID;
            DataSet DS = new DataSet();
            DS = this.db.GetParentInfo(parentID);
            int count = DS.Tables[0].Rows.Count;
            if(count > 0)
            {
                string deletionDate = ""; 
                deletionDate = DS.Tables[0].Rows[0][12].ToString();

                if (String.IsNullOrEmpty(deletionDate))
                {
                    txt_FirstName.Text = DS.Tables[0].Rows[0][2].ToString();
                    txt_LastName.Text = DS.Tables[0].Rows[0][3].ToString();

                    txt_PhoneNumber.Text = DS.Tables[0].Rows[0][4].ToString();
                    txt_Email.Text = DS.Tables[0].Rows[0][5].ToString();

                    txt_Address.Text = DS.Tables[0].Rows[0][6].ToString();
                    txt_Address2.Text = DS.Tables[0].Rows[0][7].ToString();
                    txt_City.Text = DS.Tables[0].Rows[0][8].ToString();
                    txt_Zip.Text = DS.Tables[0].Rows[0][10].ToString();
                    cbo_State.Text = DS.Tables[0].Rows[0][9].ToString();

                    txt_FilePath.Text = DS.Tables[0].Rows[0][11].ToString();
                    string imageLink = DS.Tables[0].Rows[0][11].ToString();
                    ImageBrush ib = new ImageBrush();
                    ib.ImageSource = new BitmapImage(new Uri(imageLink, UriKind.Relative));
                    cnv_ParentIcon.Background = ib;
                }
                else
                {            
                    ClearFields();
                    DisableForm();
                    MessageBox.Show("This Parent has already been deleted.");
                }
            }

        }//end LoadParentInfo
        private void AddStates() {

            cbo_State.SelectedIndex = 46;
            
            cbo_State.Items.Add("AL");
            cbo_State.Items.Add("AK");
            cbo_State.Items.Add("AZ");
            cbo_State.Items.Add("AR");
            cbo_State.Items.Add("CA");
            cbo_State.Items.Add("CO");
            cbo_State.Items.Add("CT");
            cbo_State.Items.Add("DE");
            cbo_State.Items.Add("FL");
            cbo_State.Items.Add("GA");

            cbo_State.Items.Add("HI");
            cbo_State.Items.Add("ID");
            cbo_State.Items.Add("IL");
            cbo_State.Items.Add("IN");
            cbo_State.Items.Add("IA");
            cbo_State.Items.Add("KS");
            cbo_State.Items.Add("KY");
            cbo_State.Items.Add("LA");
            cbo_State.Items.Add("ME");
            cbo_State.Items.Add("MD");

            cbo_State.Items.Add("MA");
            cbo_State.Items.Add("MI");
            cbo_State.Items.Add("MN");
            cbo_State.Items.Add("MS");
            cbo_State.Items.Add("MO");
            cbo_State.Items.Add("MT");
            cbo_State.Items.Add("NE");
            cbo_State.Items.Add("NV");
            cbo_State.Items.Add("NH");
            cbo_State.Items.Add("NJ");

            cbo_State.Items.Add("NM");
            cbo_State.Items.Add("NY");
            cbo_State.Items.Add("NC");
            cbo_State.Items.Add("ND");
            cbo_State.Items.Add("OH");
            cbo_State.Items.Add("OK");
            cbo_State.Items.Add("OR");
            cbo_State.Items.Add("PA");
            cbo_State.Items.Add("RI");
            cbo_State.Items.Add("SC");

            cbo_State.Items.Add("SD");
            cbo_State.Items.Add("TN");
            cbo_State.Items.Add("TX");
            cbo_State.Items.Add("UT");
            cbo_State.Items.Add("VT");
            cbo_State.Items.Add("VA");
            cbo_State.Items.Add("WA");
            cbo_State.Items.Add("WV");
            cbo_State.Items.Add("WI");
            cbo_State.Items.Add("WY");

        }

        private void btn_ChangePicture_Click(object sender, RoutedEventArgs e)
        {
           
                //string dir = @"..\..\..\..\Photos"; 
                string dir = @"C:\";
                Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

                dlg.FileName = "default"; // Default file name
                dlg.DefaultExt = ".jpg"; // Default file extension
                dlg.Filter = "Pictures (.jpg)|*.jpg"; // Filter files by extension 
                dlg.InitialDirectory = dir;
                Nullable<bool> result = dlg.ShowDialog();

                // Process open file dialog box results 
                if (result == true)
                {
                    // Open document 
                    string path = "../../Pictures/"; 
                    string filename = dlg.FileName;
                    string[] words = filename.Split('\\');

                    path += words[words.Length - 1]; 
                    

                    try
                    {
                        string imageLink = path;
                        ImageBrush ib = new ImageBrush();
                        ib.ImageSource = new BitmapImage(new Uri(imageLink, UriKind.Relative));
                        cnv_ParentIcon.Background = ib;
                        txt_FilePath.Text = path;
                    }
                    catch(Exception)
                    {
                        MessageBox.Show("Could not change picture to" + path);

                    }

                }


        }//end AddStates


    }//end class
}//end nameSpace
