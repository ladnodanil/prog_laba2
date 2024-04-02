using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prog_laba2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string login = textBox1.Text;
            string password = textBox2.Text;

            User user = null;
            try
            {
                using (StreamReader sr = new StreamReader("USERS.txt"))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (line.StartsWith("#"))
                        {
                            string[] parts = line.Split(' ');
                            string username = parts[0].Substring(1);
                            string Password = parts[1];
                            user = new User(username, Password);
                            if (user.Username == login && user.Password == password)
                            {
                                
                                while ((line = sr.ReadLine()) != null)
                                {
                                    if (line.StartsWith("#"))
                                    {
                                        
                                        break;
                                    }
                                    string[] part = line.Split(' ');
                                    string menuItem = part[0];
                                    int accessStatus = int.Parse(part[1]);
                                    user.AccessRights.Add(menuItem, accessStatus);
                                }
                                
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка чтения файла пользователей: " + ex.Message);
                return; 
            }

            if (user != null && user.Username == login && user.Password == password)
            {
                
                Menu menu = new Menu(user.AccessRights);
                menu.Show();
                this.Hide();
                
            }
            else
            {
                
                MessageBox.Show("Неправильный логин или пароль.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
        }
    }
}

