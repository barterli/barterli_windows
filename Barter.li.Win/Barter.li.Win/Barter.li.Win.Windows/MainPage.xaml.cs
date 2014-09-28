using Barter.Li.Win.BL.DataHandler;
using Barter.Li.Win.DL;
using Barter.Li.Win.Model.SearchResponseJsonTypes;
using Barter.Li.Win.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Barter.Li.Win
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        BookDataVM vm;        
        public MainPage()
        {
            this.InitializeComponent();          
            vm = new BookDataVM();
            this.DataContext = vm;            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {               
        }
    }
}
