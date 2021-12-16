﻿using System;
using System.Drawing;
using System.Windows.Forms;

namespace MainProject
{
    public partial class Form1 : Form
    {
      
        public Form1()
        {
            InitializeComponent();
            CssDark();
        }
        private void CssDark()
        {
            BackColor = ColorTranslator.FromHtml("#1B1C22");
            Username.ForeColor = Color.White;
            password.ForeColor = Color.White;
            textBox1.BackColor = ColorTranslator.FromHtml("#25262C");
            textBox2.BackColor = ColorTranslator.FromHtml("#25262C");
            Log.BackColor = ColorTranslator.FromHtml("#3077E3");
            Log.FlatStyle = FlatStyle.Flat;
            Log.FlatAppearance.BorderColor = ColorTranslator.FromHtml("#25262C");
            register.ForeColor = Color.White;
            Log.Font = new Font("Arial", 10,FontStyle.Bold);
            label1.Font = new Font("Arial", 12, FontStyle.Bold);
        }

        private void CssLight()
        {
            BackColor = ColorTranslator.FromHtml("#FAFBFD");
            textBox1.BackColor = ColorTranslator.FromHtml("#FAFAFA");
            textBox2.BackColor = ColorTranslator.FromHtml("#25262C");
        }
        private void Form1_Load(object sender, EventArgs e)
        {
        }
        private void register_Click(object sender, EventArgs e)
        {
            Hide();
            var registerForm = new RegisterForm();
            registerForm.ShowDialog();
        }

        private void Log_Click(object sender, EventArgs e)
        {
            var mail = textBox1.Text;
            var password = textBox2.Text;
            
            if (password.Length > 0 && mail.Length > 0)
            {
                if (SQLprocedure.LogIn(mail, password))
                {
                    textBox1.Text = "logged in";
                }
            }
        }
       
    }
}