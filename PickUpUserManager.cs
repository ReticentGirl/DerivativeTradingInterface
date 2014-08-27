using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Data;
using System.Collections;
using System.Collections.ObjectModel;

namespace qiquanui
{
    public class PickUpUserData : INotifyPropertyChanged
    {
        private bool isChoose;
        private string userID;


        public bool IsChoose
        {
            get { return isChoose; }
            set
            {

                isChoose = value;
                OnPropertyChanged("IsChoose");
            }
        }

        public string UserID
        {
            get { return userID; }
            set
            {

                userID = value;
                OnPropertyChanged("userID");
            }
        }

        public PickUpUserData()
        {

        }

        public PickUpUserData(bool _isChoose, string _userID)
        {
            isChoose = _isChoose;
            userID = _userID;
        }



        #region INotifyPropertyChanged 成员

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
        }




        #endregion

    }

    public class PickUpUserManager
    {
        public static ObservableCollection<PickUpUserData> PickUpUserOC = new ObservableCollection<PickUpUserData>();

        public void OnAddPickUpUserDara(bool _isChoose,string _userID)
        {
            PickUpUserData add_puud = new PickUpUserData(_isChoose, _userID);

            PickUpUserOC.Add(add_puud);
        }

        public static bool isChooseUser(string _userID)
        {
            for (int i = 0; i < PickUpUserOC.Count; i++)
            {
                if (PickUpUserOC[i].UserID.Equals(_userID) && PickUpUserOC[i].IsChoose==true)
                    return true;
            }

            return false;
        }

    }
}
