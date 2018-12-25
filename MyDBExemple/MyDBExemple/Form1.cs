using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace MyDBExemple
{
    public partial class Form1 : Form
    {
        private List<TextBox> listNewUserTextBoxs = new List<TextBox>();

        private SqlConnection sqlConnection;
        private SqlCommand sqlCommand;
        private SqlDataReader sqlDataReader;

        private string conectDB = @"Data Source=.;
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

            listNewUserTextBoxs.Add(NewUserTextBoxLoginName);
            listNewUserTextBoxs.Add(NewUserTextBoxPassword);
            listNewUserTextBoxs.Add(NewUserTextBoxFirstName);
            listNewUserTextBoxs.Add(NewUserTextBoxLastName);
            listNewUserTextBoxs.Add(NewUserTextBoxEmail);
            listNewUserTextBoxs.Add(NewUserTextBoxPhone);

            sqlConnection = new SqlConnection(conectDB);
            sqlConnection.Open();
        }

        /// <summary>
        /// Создает нового пользователя
        /// </summary>
        private void NewUserButtonSignUp_Click(object sender, EventArgs e)
        {
            if (sqlConnection != null && sqlConnection.State == ConnectionState.Closed)
                sqlConnection.Open();

            sqlCommand = new SqlCommand($"INSERT INTO [Users] " +
                                        $"Values ('{NewUserTextBoxLoginName.Text}', " +
                                                $"'{NewUserTextBoxPassword.Text}', " +
                                                $"'{NewUserTextBoxFirstName.Text}', " +
                                                $"'{NewUserTextBoxLastName.Text}', " +
                                                $"'{NewUserTextBoxEmail.Text}', " +
                                                $"'{NewUserTextBoxPhone.Text}')", sqlConnection);

            sqlCommand.ExecuteNonQuery();

            foreach (var item in listNewUserTextBoxs)
                item.Clear();

            GetAllUsersData();
        }

        /// <summary>
        /// Удоляет пользователя по User_Id
        /// </summary>
        private void DeleteUserButton_Click(object sender, EventArgs e)
        {
            sqlCommand = new SqlCommand($"DELETE FROM [Users] " +
                                        $"WHERE [User_Id] = {DeleteUserTextBoxID.Text}", sqlConnection);
            sqlCommand.ExecuteNonQuery();
            GetAllUsersData();
        }

        /// <summary>
        /// Возвращает данные о пользователя по User_Id  
        /// </summary>
        private void UpdateUserDataButtonGetUserData_Click(object sender, EventArgs e)
        {
            sqlCommand = new SqlCommand($"SELECT * FROM [Users] WHERE [User_Id] = {UpdateUserDateTextBoxID.Text}", sqlConnection);
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

        /// <summary>
        /// Обнавляет данные Пользователя по User_Id
        /// </summary>
        private void UpdateUserDataButton_Click(object sender, EventArgs e)
        {
            sqlCommand = new SqlCommand($"UPDATE [Users] " +
                                        $"SET [Login_Name] = '{UpdateUserDateTextBoxLoginName.Text}' , " +
                                        $"[Password] = '{UpdateUserDateTextBoxPassword.Text}' , " +
                                        $"[First_Name] = '{UpdateUserDateTextBoxFirstName.Text}' , " +
                                        $"[Last_Name] = '{UpdateUserDateTextBoxLastName.Text}' , " +
                                        $"[Email] = '{UpdateUserDateTextBoxEmail.Text}' , " +
                                        $"[Phone] = '{UpdateUserDateTextBoxPhone.Text}' " +
                                        $"WHERE [User_Id] = {UpdateUserDateTextBoxID.Text}", sqlConnection);
            sqlCommand.ExecuteNonQuery();
            GetAllUsersData();
        }

        /// <summary>
        /// Возвращает все данные всех пользователей
        /// </summary>
        private void SelectAllUserDataButton_Click(object sender, EventArgs e)
        {
            GetAllUsersData();
        }

        /// <summary>
        /// Возвращает все данные о всех пользователей и добавляет в ListBox
        /// </summary>
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
