CREATE DATABASE UsersDB;
GO

USE UsersDB;

Create Table Users
(
[User_Id] int identity(1,1),
[Login_Name] varchar(30) not null,
[Password] varchar(30) not null ,
[First_Name] nvarchar(30) not null,
[Last_Name] nvarchar(30) not null ,
[Email] nvarchar(30) not null ,
[Phone] nvarchar(30) not null,

Constraint PK_ Primary Key([User_Id]),
Constraint CK_Login_Name_No_Text Check (Login_Name != ''),
Constraint UQ_Login_Name Unique(Login_Name),
Constraint CK_Password_No_Text Check ([Password] != ''),
Constraint CK_First_Name_No_Text Check (First_Name != ''),
Constraint CK_Last_Name_No_Text Check (Last_Name != ''),
Constraint CK_Email_No_Text Check (Email != ''),
Constraint UQ_Email Unique(Email),
Constraint CK_Phone_No_Text Check (Phone != ''),
Constraint UQ_Phone Unique(Phone)
);
Go