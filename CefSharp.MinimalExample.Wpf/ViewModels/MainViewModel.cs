using CefSharp.MinimalExample.Wpf.Mvvm;
using CefSharp.Wpf;
using System.ComponentModel;
using System.Windows;

namespace CefSharp.MinimalExample.Wpf.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private IWpfWebBrowser webBrowser;
        public IWpfWebBrowser WebBrowser
        {
            get { return webBrowser; }
            set
			{
				PropertyChanged.ChangeAndNotify(ref webBrowser, value, () => WebBrowser);

				if (webBrowser != null)
					((ChromiumWebBrowser)webBrowser).LoadingStateChanged += webBrowser_LoadingStateChanged;
			}
        }

        private string title;
        public string Title
        {
            get { return title; }
            set { PropertyChanged.ChangeAndNotify(ref title, value, () => Title); }
        }

        public MainViewModel()
        {
            PropertyChanged += OnPropertyChanged;
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Title")
            {
                Application.Current.MainWindow.Title = "CefSharp.MinimalExample.Wpf - " + Title;
            }
        }

		void webBrowser_LoadingStateChanged(object sender, LoadingStateChangedEventArgs e)
		{
			if (e.IsLoading)
			{
				BrowserHeight = "Auto";
				BrowserVisibility = Visibility.Collapsed;
			}
			else
			{
				BrowserHeight = "500";
				BrowserVisibility = Visibility.Visible;
			}
		}

		private Visibility browserVisibility = Visibility.Collapsed;
		public Visibility BrowserVisibility
		{
			get { return browserVisibility; }
			set { PropertyChanged.ChangeAndNotify(ref browserVisibility, value, () => BrowserVisibility); }
		}

		private string browserHeight = "Auto";
		public string BrowserHeight
		{
			get { return browserHeight; }
			set { PropertyChanged.ChangeAndNotify(ref browserHeight, value, () => BrowserHeight); }
		}
    }
}
