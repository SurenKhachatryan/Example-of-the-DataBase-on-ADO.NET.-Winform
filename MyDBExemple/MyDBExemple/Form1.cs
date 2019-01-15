using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace MyDBExemple
{
    public partial class Form1 : Form
    {
        #region DB Prop
        private SqlConnection connect;
        private SqlCommand command;
        private SqlDataReader read;

        private string connectionString = @"Data Source=.;
                                            Initial Catalog=UsersDB;
                                            Integrated Security=True;
                                            Connect Timeout=30;
                                            Encrypt=False;
                                            TrustServerCertificate=False;
                                            ApplicationIntent=ReadWrite;
                                            MultiSubnetFailover=False";
        #endregion

        #region TextBoxArrays
        private TextBox[] updateUserDataTextBoxArr;
        private TextBox[] newUserTextBoxsArr;
        #endregion

        public Form1()
        {
            InitializeComponent();

            newUserTextBoxsArr = new TextBox[]
            {
                NewUserTextBoxFirstName,
                NewUserTextBoxLastName,
                NewUserTextBoxLoginName,
                NewUserTextBoxPassword,
                NewUserTextBoxEmail,
                NewUserTextBoxPhone
            };

            updateUserDataTextBoxArr = new TextBox[]
            {
                UpdateUserDataTextBoxFirstName,
                UpdateUserDataTextBoxLastName,
                UpdateUserDataTextBoxLoginName,
                UpdateUserDataTextBoxPassword,
                UpdateUserDataTextBoxEmail,
                UpdateUserDataTextBoxPhone
            };
        }

        /// <summary>
        /// Создает нового пользователя
        /// </summary>
        private void New_User_Button_Sign_Up_Click(object sender, EventArgs e)
        {
            using (connect = new SqlConnection(connectionString))
            {
                connect.Open();

                command = new SqlCommand("INSERT INTO [Users] Values (@LoginName , @Password ," +
                                                                    " @FirstName , @LastName ," +
                                                                    " @Email , @Phone)", connect);

                command.Parameters.Add(new SqlParameter("@LoginName", NewUserTextBoxLoginName.Text));
                command.Parameters.Add(new SqlParameter("@Password", NewUserTextBoxPassword.Text));
                command.Parameters.Add(new SqlParameter("@FirstName", NewUserTextBoxFirstName.Text));
                command.Parameters.Add(new SqlParameter("@LastName", NewUserTextBoxLastName.Text));
                command.Parameters.Add(new SqlParameter("@Email", NewUserTextBoxEmail.Text));
                command.Parameters.Add(new SqlParameter("@Phone", NewUserTextBoxPhone.Text));

                command.ExecuteNonQuery();
            }

            ClearTextBoxs.Clear(newUserTextBoxsArr);
            Get_All_Users_Data();
        }

        /// <summary>
        /// Удоляет пользователя по User_Id
        /// </summary>
        private void Delete_User_Button_Click(object sender, EventArgs e)
        {
            using (connect = new SqlConnection(connectionString))
            {
                connect.Open();

                command = new SqlCommand($"DELETE FROM [Users] WHERE [User_Id] = @User_ID", connect);

                command.Parameters.Add(new SqlParameter("@User_ID", DeleteUserTextBoxID.Text));

                command.ExecuteNonQuery();
            }

            DeleteUserTextBoxID.Text = string.Empty;
            Get_All_Users_Data();
        }

        /// <summary>
        /// Возвращает данные о пользователя по User_Id  
        /// </summary>
        private void Update_User_Data_Button_Get_User_Data_Click(object sender, EventArgs e)
        {
            using (connect = new SqlConnection(connectionString))
            {
                connect.Open();

                command = new SqlCommand($"SELECT * FROM [Users] WHERE [User_Id] = @User_ID", connect);

                command.Parameters.Add(new SqlParameter("@User_ID", UpdateUserDataTextBoxID.Text));

                read = command.ExecuteReader();

                read.Read();

                UpdateUserDataTextBoxLoginName.Text = read["Login_Name"].ToString();
                UpdateUserDataTextBoxPassword.Text = read["Password"].ToString();
                UpdateUserDataTextBoxFirstName.Text = read["First_Name"].ToString();
                UpdateUserDataTextBoxLastName.Text = read["Last_Name"].ToString();
                UpdateUserDataTextBoxEmail.Text = read["Email"].ToString();
                UpdateUserDataTextBoxPhone.Text = read["Phone"].ToString();
            }
        }

        /// <summary>
        /// Обнавляет данные Пользователя по User_Id
        /// </summary>
        private void Update_User_Data_Button_Click(object sender, EventArgs e)
        {
            using (connect = new SqlConnection(connectionString))
            {
                connect.Open();

                command = new SqlCommand($"UPDATE [Users] SET [Login_Name] = @LoginName , " +
                                                             "[Password] = @Password , " +
                                                             "[First_Name] = @FirstName , " +
                                                             "[Last_Name] = @LastName , " +
                                                             "[Email] = @Email , " +
                                                             "[Phone] = @Phone " +
                                                             "WHERE [User_Id] = @User_ID", connect);

                command.Parameters.Add(new SqlParameter("@LoginName", UpdateUserDataTextBoxLoginName.Text));
                command.Parameters.Add(new SqlParameter("@Password", UpdateUserDataTextBoxPassword.Text));
                command.Parameters.Add(new SqlParameter("@FirstName", UpdateUserDataTextBoxFirstName.Text));
                command.Parameters.Add(new SqlParameter("@LastName", UpdateUserDataTextBoxLastName.Text));
                command.Parameters.Add(new SqlParameter("@Email", UpdateUserDataTextBoxEmail.Text));
                command.Parameters.Add(new SqlParameter("@Phone", UpdateUserDataTextBoxPhone.Text));
                command.Parameters.Add(new SqlParameter("@User_ID", UpdateUserDataTextBoxID.Text));

                command.ExecuteNonQuery();
            }

            ClearTextBoxs.Clear(updateUserDataTextBoxArr);
            Get_All_Users_Data();
        }

        /// <summary>
        /// Возвращает все данные всех пользователей
        /// </summary>
        private void SelectAllUserDataButton_Click(object sender, EventArgs e)
        {
            Get_All_Users_Data();
        }

        /// <summary>
        /// Возвращает все данные о всех пользователей и добавляет в ListBox
        /// </summary>
        private void Get_All_Users_Data()
        {
            using (connect = new SqlConnection(connectionString))
            {
                connect.Open();

                SelectAllUserDataGridView.Rows.Clear();

                command = new SqlCommand("SELECT * FROM [Users]", connect);

                read = command.ExecuteReader();

                while (read.Read())
                {
                    SelectAllUserDataGridView.Rows.Add(new string[] { read["User_Id"].ToString(),
                                                                      read["First_Name"].ToString(),
                                                                      read["Last_Name"].ToString(),
                                                                      read["Login_Name"].ToString(),
                                                                      read["Password"].ToString(),
                                                                      read["Email"].ToString(),
                                                                      read["Phone"].ToString()});
                }
            }
        }
    }
}
