using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Xml;

namespace CoolStoreProject.UserVVM
{
    internal class UserPage_Selection_ViewModel : INotifyPropertyChanged
    {
        // Fields
        private string? warningText;
        //

        // Properties
        /// <summary>
        /// Represents collection displaying in user list box
        /// </summary>
        internal ObservableCollection<User> ?Users { get; set; }

        /// <summary>
        /// Represents current user selected in user list box
        /// </summary>
        internal User ?CurrentUser {
            get => UserController.CurrentUser;
            set
            {
                UserController.CurrentUser = value;
                OnPropertyChanged("CurrentUser");
            }
        }

        /// <summary>
        /// Represents displaying warning text
        /// </summary>
        internal string ?WarningText
        {
            get => warningText;
            set
            {
                warningText = value;
                OnPropertyChanged("WarningText");
            }
        }
        //

        // Constructors
        internal UserPage_Selection_ViewModel()
        {
            LoadUserData(Environment.CurrentDirectory + @"\Data\Users.xml", Environment.CurrentDirectory + @"\Data\BonusCards.xml");
        }
        //

        // Methods
        /// <summary>
        /// Load user data from Users.xml
        /// </summary>
        internal void LoadUserData(string usersDataPath, string bonusDataPath)
        {
            try
            {
                Users = new ObservableCollection<User>();
                XmlDocument users_XML = new();
                users_XML.Load(usersDataPath);
                XmlElement ?users_Root = users_XML.DocumentElement;
                if (users_XML != null)
                {
                    foreach (XmlElement users_Node in users_Root)
                    {
                        double cash = 0;
                        double cardMoney = 0;
                        string bonusCard = "";
                        int bonuses = -1;


                        foreach (XmlNode user_Node in users_Node.ChildNodes)
                        {
                            if (user_Node.Name == "Cash")
                            {
                                string tempString = "";
                                for (int i = 0; i < user_Node.InnerText.Length; i++)
                                {
                                    if (user_Node.InnerText[i] == '.')
                                    {
                                        tempString += ",";
                                    }
                                    else
                                    {
                                        tempString += user_Node.InnerText[i];
                                    }
                                }
                                cash = Convert.ToDouble(tempString);
                            }
                            if (user_Node.Name == "CardMoney")
                            {
                                string tempString = "";
                                for (int i = 0; i < user_Node.InnerText.Length; i++)
                                {
                                    if (user_Node.InnerText[i] == '.')
                                    {
                                        tempString += ",";
                                    }
                                    else
                                    {
                                        tempString += user_Node.InnerText[i];
                                    }
                                }
                                cardMoney = Convert.ToDouble(tempString);
                            }

                            if (user_Node.Name == "BonusCard")
                            {
                                bonusCard = user_Node.InnerText;
                            }
                        }

                        XmlDocument bonusCards_XML = new();
                        bonusCards_XML.Load(bonusDataPath);
                        XmlElement? bonusCards_Root = bonusCards_XML.DocumentElement;

                        if (bonusCards_XML != null)
                        {
                            foreach (XmlElement bonusCards_Node in bonusCards_Root)
                            {
                                foreach (XmlNode bonusCard_Node in bonusCards_Node.ChildNodes)
                                {
                                    if (bonusCard_Node.Name == "Number")
                                    {
                                        if (bonusCard_Node.InnerText != bonusCard)
                                        {
                                            break;
                                        }
                                    }
                                    if (bonusCard_Node.Name == "Bonuses")
                                    {
                                        bonuses = Convert.ToInt32(bonusCard_Node.InnerText);
                                    }
                                }
                                if (bonuses != -1)
                                {
                                    break;
                                }
                            }

                            Users.Add(new(cash, cardMoney, bonusCard, bonuses));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }
        //

        // Commands
        private RelayCommand ?startCommand;

        /// <summary>
        /// Start working with kiosk
        /// </summary>
        internal RelayCommand? StartCommand
        {
            get
            {
                return startCommand ??
                  (startCommand = new RelayCommand(obj =>
                  {
                      if (CurrentUser != null)
                      {
                          UserController.CurrentPage = UserController.UserPage_Actions;
                          UserController.LaunchKiosk();
                          WarningText = "";
                      }
                      else
                      {
                          WarningText = "*You haven't selected a user!";
                      }
                  }));
            }
        }
        //

        // INotifyPropertyChanged realization
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        //
    }
}
