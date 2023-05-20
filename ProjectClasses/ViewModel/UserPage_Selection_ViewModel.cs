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

namespace CoolStoreProject
{
    internal class UserPage_Selection_ViewModel : INotifyPropertyChanged
    {
        // Fields
        private User ?currentUser;
        private string warningText;
        //

        // Properties
        public ObservableCollection<User> ?Users { get; set; }
        public User ?CurrentUser {
            get => currentUser;
            set
            {
                currentUser = value;
                OnPropertyChanged("CurrentUser");
            }
        }
        public string ?WarningText
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
        public UserPage_Selection_ViewModel()
        {
            LoadUserData(Environment.CurrentDirectory + @"\Data\Users.xml");
        }
        //

        // Methods
        /// <summary>
        /// Load user data from Users.xml
        /// </summary>
        /// <param name="path"></param>
        /// <exception cref="Exception"></exception>
        public void LoadUserData(string path)
        {
            try
            {
                Users = new ObservableCollection<User>();
                XmlDocument users_XML = new();
                users_XML.Load(path);
                XmlElement ?users_Root = users_XML.DocumentElement;
                if (users_XML != null)
                {
                    foreach (XmlElement users_Node in users_Root)
                    {
                        double cash = 0;
                        double cardMoney = 0;
                        string bonusCard = "";


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

                        Users.Add(new(cash, cardMoney, bonusCard));
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
        public RelayCommand? StartCommand
        {
            get
            {
                return startCommand ??
                  (startCommand = new RelayCommand(obj =>
                  {
                      if (currentUser != null)
                      {
                          Controller.CurrentPage = Controller.UserPage_Actions;
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
