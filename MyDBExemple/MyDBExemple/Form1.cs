using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyDBExemple
{
    public partial class Form1 : Form
    {
        private SqlConnection sqlConnection;
        private SqlCommand sqlCommand;
        private SqlDataReader sqlDataReader;

        private String conectDB = @"Data Source=.;
                                    Initial Catalog=UsersDB;
                                    Integrated Security=True;
                                    Connect Timeout=30;
                                    Encrypt=False;
                                    TrustServerCertificate=False;
                                    ApplicationIntent=ReadWrite;
                                    MultiSubnetFailover=False";

        public Form1()
        {
            InitializeComponent();
            sqlConnection = new SqlConnection(conectDB);
            sqlConnection.Open();
        }

        private void NewUserButtonSignUp_Click(object sender, EventArgs e)
        {
            if (sqlConnection != null && sqlConnection.State == ConnectionState.Closed)
                sqlConnection.Open();

            sqlCommand = new SqlCommand("INSERT INTO [Users] Values (@Login_Name, @Password, @First_Name, @Last_Name, @Email, @Phone)", sqlConnection);
            sqlCommand.Parameters.AddWithValue("Login_Name", NewUserTextBoxLoginName.Text);
            sqlCommand.Parameters.AddWithValue("Password", NewUserTextBoxPassword.Text);
            sqlCommand.Parameters.AddWithValue("First_Name", NewUserTextBoxFirstName.Text);
            sqlCommand.Parameters.AddWithValue("Last_Name", NewUserTextBoxLastName.Text);
            sqlCommand.Parameters.AddWithValue("Email", NewUserTextBoxEmail.Text);
            sqlCommand.Parameters.AddWithValue("Phone", NewUserTextBoxPhone.Text);
            sqlCommand.ExecuteNonQuery();
            GetAllUsersData();
        }

        private void DeleteUserButton_Click(object sender, EventArgs e)
        {
            sqlCommand = new SqlCommand("DELETE FROM [Users] WHERE [User_Id] = @User_Id", sqlConnection);
            sqlCommand.Parameters.AddWithValue("User_Id", DeleteUserTextBoxID.Text);
            sqlCommand.ExecuteNonQuery();
            GetAllUsersData();
        }

        private void UpdateUserDataButtonGetUserData_Click(object sender, EventArgs e)
        {
            sqlCommand = new SqlCommand("SELECT * FROM [Users] WHERE [User_Id] = @User_Id", sqlConnection);
            sqlCommand.Parameters.AddWithValue("User_Id", UpdateUserDateTextBoxID.Text);
            sqlDataReader = sqlCommand.ExecuteReader();
            sqlDataReader.Read();
            UpdateUserDateTextBoxLoginName.Text = Convert.ToString(sqlDataReader["Login_Name"]);
            UpdateUserDateTextBoxPassword.Text = Convert.ToString(sqlDataReader["Password"]);
            UpdateUserDateTextBoxFirstName.Text = Convert.ToString(sqlDataReader["First_Name"]);
            UpdateUserDateTextBoxLastName.Text = Convert.ToString(sqlDataReader["Last_Name"]);
            UpdateUserDateTextBoxEmail.Text = Convert.ToString(sqlDataReader["Email"]);
            UpdateUserDateTextBoxPhone.Text = Convert.ToString(sqlDataReader["Phone"]);
            sqlDataReader.Close();
        }

        private void UpdateUserDataButton_Click(object sender, EventArgs e)
        {

        }

        private void SelectAllUserDataButton_Click(object sender, EventArgs e)
        {
            GetAllUsersData();
        }

        private void GetAllUsersData()
        {
            SelectAllUserDataListBox.Items.Clear();
            sqlCommand = new SqlCommand("SELECT * FROM [Users]", sqlConnection);

            sqlDataReader = sqlCommand.ExecuteReader();

            while (sqlDataReader.Read())
            {
                SelectAllUserDataListBox.Items.Add(Convert.ToString(sqlDataReader["User_Id"]) + "   " +
                                                   Convert.ToString(sqlDataReader["Login_Name"]) + "   " +
                                                   Convert.ToString(sqlDataReader["Password"]) + "   " +
                                                   Convert.ToString(sqlDataReader["First_Name"]) + "   " +
                                                   Convert.ToString(sqlDataReader["Last_Name"]) + "   " +
                                                   Convert.ToString(sqlDataReader["Email"]) + "   " +
                                                   Convert.ToString(sqlDataReader["Phone"]));
            }
            sqlDataReader.Close();
        }
    }
}
