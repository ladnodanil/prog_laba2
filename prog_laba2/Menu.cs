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
    public partial class Menu : Form
    {
        List<MenuItem> menuItems;
        public Menu(Dictionary<string, int> AccessRights,string filename = "menu.txt")
        {
            InitializeComponent();
            menuItems = new List<MenuItem>();

            ReadMenu(filename);

            ShowMenuV1();

            MenuForUser(AccessRights);
           
        }

        private void ShowMenuV1() // через цикл
        {
            ToolStripMenuItem parentItem = new ToolStripMenuItem();
            ToolStripMenuItem tmp0 = new ToolStripMenuItem();



            for (int i = 0; i < menuItems.Count; i++)
            {
                ToolStripMenuItem newItem = new ToolStripMenuItem(menuItems[i].Name);

                switch (menuItems[i].Status)
                {
                    case 0:
                        break;
                    case 1:
                        newItem.Enabled = false;
                        break;
                    case 2:
                        newItem.Visible = false;
                        break;
                }
                if (menuItems[i].Level == 0)
                {
                    
                    menuStrip1.Items.Add(newItem);
                    parentItem = newItem;
                    tmp0 = newItem;
                }
                else
                {
                    parentItem.DropDownItems.Add(newItem);
                    if (i < menuItems.Count - 1 && menuItems[i + 1].Level != menuItems[i].Level )
                    {
                        parentItem = newItem;
                    }
                    if(i < menuItems.Count - 1 && menuItems[i].Level > menuItems[i + 1].Level)
                    {
                        parentItem = tmp0;
                    }
                }
                
            }
        }
        private void ShowMenuV2() // через словарь
        {
            Dictionary<int, ToolStripMenuItem> menuItemsLevel = new Dictionary<int, ToolStripMenuItem>();


            for (int i = 0; i < menuItems.Count; i++)
            {
                ToolStripMenuItem newItem = new ToolStripMenuItem(menuItems[i].Name);
                switch (menuItems[i].Status)
                {
                    case 0:
                        break;
                    case 1:
                        newItem.Enabled = false;
                        break;
                    case 2:
                        newItem.Visible = false;
                        break;
                }
                //добавляем текущий уровень в словарь
                menuItemsLevel[menuItems[i].Level] = newItem;

                if (menuItems[i].Level == 0)
                {
                    //добавляем меню
                    menuStrip1.Items.Add(newItem);

                }
                else
                {
                    // добавляем подменю
                    ToolStripMenuItem parentItem = menuItemsLevel[menuItems[i].Level - 1];
                    parentItem.DropDownItems.Add(newItem);
                }
            }
        }
        private void MenuForUser(Dictionary<string, int> AccessRights)
        {
            foreach (ToolStripMenuItem menuItem in menuStrip1.Items)
            {
                
                foreach (string name in AccessRights.Keys)
                {
                    if (name == menuItem.Text)
                    {
                        switch (AccessRights[name])
                        {
                            case 0:
                                break;
                            case 1:
                                menuItem.Enabled = false;
                                break;
                            case 2:
                                menuItem.Visible = false;
                                break;
                        }
                    }
                }

                // Рекурсивно применяем логику для вложенных элементов меню
                HideNestedMenuItems(menuItem, AccessRights);
            }
        }

        private void HideNestedMenuItems(ToolStripMenuItem menuItem, Dictionary<string, int> AccessRights)
        {
            foreach (var item in menuItem.DropDownItems)
            {
                if (item is ToolStripMenuItem)
                {
                    ToolStripMenuItem subMenuItem = (ToolStripMenuItem)item;
                    
                    foreach (string name in AccessRights.Keys)
                    {
                        if (name == subMenuItem.Text)
                        {
                            switch (AccessRights[name])
                            {
                                case 0:
                                    break;
                                case 1:
                                    subMenuItem.Enabled = false;
                                    break;
                                case 2:
                                    subMenuItem.Visible = false;
                                    break;
                            }
                        }
                    }
                    
                    HideNestedMenuItems(subMenuItem, AccessRights);
                }
            }
        }


        private void ReadMenu(string filename)
        {


            using (StreamReader streamReader = new StreamReader(filename))
            {

                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    string[] parts = line.Split(' ');
                    int level = int.Parse(parts[0]);
                    string name = parts[1];
                    int status = int.Parse(parts[2]);
                    string method;
                    if (parts.Length == 4)
                    {
                        method = parts[3];
                    }
                    else
                    {
                        method = "NULL";
                    }

                    menuItems.Add(new MenuItem(level, name, status, method));
                }
            }
        }
    }
    public class MenuItem
    {
        public int Level { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }
        public string Method { get; set; }

        public MenuItem(int level, string name, int status, string method)
        {
            Level = level;
            Name = name;
            Status = status;
            Method = method;
        }
    }
}
