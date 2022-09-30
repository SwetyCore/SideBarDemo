using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Default.Model.BiliHelper;

namespace Default.ViewModel
{ 
    internal class BiliHelper : ObservableObject
    {


        public BiliHelper(Guid g)
        {

        }



        private space_acc_info.Root _acc_Info;

        public space_acc_info.Root Acc_Info
        {
            get { return _acc_Info; }
            set { SetProperty(ref _acc_Info, value); }
        }

        private space_myinfo.Root _space_Myinfo;

        public space_myinfo.Root Space_Myinfo
        {
            get { return _space_Myinfo; }
            set { SetProperty(ref _space_Myinfo, value); }
        }

        private web_interface_card.Root _card;

        public web_interface_card.Root Card
        {
            get { return _card; }
            set { SetProperty(ref _card, value); }
        }

        private string _cookie;

        public string Cookie
        {
            get { return _cookie; }
            set { SetProperty(ref _cookie, value); }
        }


        private bool _loading;

        public bool Loading
        {
            get { return _loading; }
            set { SetProperty(ref _loading, value); }
        }




    }
   
}
