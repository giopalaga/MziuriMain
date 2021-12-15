﻿using System;
using System.Drawing;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using static System.String;

namespace MainProject
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
        }

        private void RegisterForm_Load(object sender, System.EventArgs e)
        {
            label3.Hide();
        }

        private bool IsValid(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        private bool MailExists(string mail)
        {
            var z = SQLprocedure.SelectUserByMail(mail).Rows.Count;
            return z > 0;
        }
        private static string ToMd5(string input)
        {
            using var md5 = System.Security.Cryptography.MD5.Create();
            var inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            var hashBytes = md5.ComputeHash(inputBytes);
            var sb = new StringBuilder();
            foreach (var b in hashBytes)
            {
                sb.Append(b.ToString("X2"));
            }
            return sb.ToString();
        }
        private void log_Click(object sender, System.EventArgs e)
        {
            Hide();
            var form1 = new Form1();
            form1.ShowDialog();
        }
        private void register_Click_1(object sender, EventArgs e)
        {
            label3.ForeColor = Color.Red;
            var name = textBox1.Text;
            var lastname = textBox2.Text;
            var phoneNumber = textBox3.Text.Length > 0 && textBox3.Text.Length < 10? textBox3.Text : "";
            var email = textBox4.Text;
            var password = textBox5.Text;
            var rPassword = textBox6.Text;
           
            if (name.Length > 0 && lastname.Length  > 0 && email.Length > 0 && password.Length > 0 && rPassword.Length > 0)
            {
                label3.Hide();
                if (IsValid(email))
                {
                    label3.Hide();
                    if (!MailExists(email))
                    {
                        label3.Hide();
                        if (password == rPassword)
                        {
                            label3.Hide();
                            //final
                            SQLprocedure.InsertUser(new User
                            {
                                Id = 0, password = ToMd5(password), Lastname = lastname, Name = name, Mail = email,
                                Role = "default", PhoneNumber = phoneNumber
                            });
                            var popUp = new PopUp {text = "registered", route = "login"};
                            
                            Hide();
                            popUp.ShowDialog();
                        }
                        else
                        {
                            label3.Show();
                            label3.Text = "password don't match";
                        }
                    }
                    else
                    {
                        label3.Show();
                        label3.Text = "mail already exists";
                    }
                }
                else
                {
                    label3.Show();
                    label3.Text = "invalid email";
                }
            }
            else
            {
                label3.Show();
                label3.Text = "only phone number field can be empty";
            }
        }
    }

}