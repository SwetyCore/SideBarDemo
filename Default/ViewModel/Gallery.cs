using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HandyControl.Controls;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Default.ViewModel
{
    [ObservableObject]
    partial class Gallery
    {
        Default.View.Gallery view;
        public Gallery(Default.View.Gallery view)
        {
            this.view = view;
        }

        public List<string> Files = new List<string>();

        [ObservableProperty]
        ImageSource img;



        private string[] exts = new string[]
        {
            ".jpg",
            ".png",
            ".bmp",
            ".jpeg",
            
        };
        [RelayCommand]
        private void SetTargetFolder()
        {
            var dl = new OpenFileDialog();
            var ret =dl.ShowDialog();
            if (ret == true)
            {
                var folder = Path.GetDirectoryName(dl.FileName);
                InitFolder(folder);

            }

        }

        public void InitFolder(string folder)
        {
            Files = new List<string>();
            var files = Directory.GetFiles(folder);
            foreach (var item in files)
            {
                var ext = Path.GetExtension(item).ToLower();
                if (exts.Contains(ext))
                {
                    Files.Add(item);
                }
            }

            LoadImg(Files.FirstOrDefault());
        }

        public void LoadImg(string f)
        {
            if (f==null)
            {
                return;
            }
            using (BinaryReader reader = new BinaryReader(File.Open(f, FileMode.Open)))
            {
                try
                {
                    FileInfo fi = new FileInfo(f);
                    byte[] bytes = reader.ReadBytes((int)fi.Length);
                    reader.Close();

                    BitmapImage bitmapImage = new BitmapImage();
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;

                    bitmapImage.BeginInit();
                    bitmapImage.StreamSource = new MemoryStream(bytes);
                    bitmapImage.EndInit();
                    bitmapImage.DecodePixelWidth=800;
                    bitmapImage.DecodePixelHeight=800;
                    Img = bitmapImage;
                    
                }
                catch (Exception ex) 
                {
                    Growl.Error(ex.Message);
                }
            }

        }
    }
}
