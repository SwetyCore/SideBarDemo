using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using PluginSdk.Message;
using System.Collections.ObjectModel;
using System.IO;

namespace Default.ViewModel
{
    partial class FolderView : ObservableRecipient, IRecipient<OnExitMsg>
    {
        public Model.FolderView.Config cfg;

        View.FolderView view;
        public FolderView(View.FolderView view)
        {
            this.view = view;
            IsActive = true;
            cfg = Model.FolderView.Config.Load(view.GetPluginConfigFilePath());

            LoadData();
        }


        [ObservableProperty]
        private ObservableCollection<Model.FolderView.FileItem> fItems;


        public string APP_FOLDER = @"C:\Users\SwetyCore\Desktop";
        private void LoadData()
        {
            FItems = new ObservableCollection<Model.FolderView.FileItem>();

            if (!Directory.Exists(APP_FOLDER))
            {
                Directory.CreateDirectory(APP_FOLDER);
            }
            var files = Directory.GetFiles(APP_FOLDER);

            foreach (var item in files)
            {
                if (item.ToLower().EndsWith("lnk"))
                {
                    FItems.Add(Common.IconHelper.ReadShortcut(Path.GetFullPath(item)));

                }
            }
        }
        public void Receive(OnExitMsg message)
        {
            cfg.Save(view.GetPluginConfigFilePath());

        }
    }
}
