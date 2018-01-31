using ForDevUniversel.MVVM.ViewsModels;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=234238

namespace ForDevUniversel.MVVM.Views
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class RubricPage : Page
    {
        public RubricPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            RubricViewModel rubricVM = new RubricViewModel();

            DataContext = rubricVM;

            SystemNavigationManager.GetForCurrentView().BackRequested += MainPage_BackRequested;

            ((RubricViewModel)DataContext).GetListRubric();
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            SystemNavigationManager.GetForCurrentView().BackRequested -= MainPage_BackRequested;
        }

        private void MainPage_BackRequested(object sender, BackRequestedEventArgs e)
        {
            // On interdit la sortie de l'application par ce bouton
            e.Handled = true;
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Exit();
        }

        private void BtnReturn_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RubricViewModel rubricVM = (RubricViewModel)((Button)sender).DataContext;

            Frame.Navigate(typeof(TopicPage), rubricVM);
        }

        private void Button_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            //Button btn = (Button) sender;
            //btn.Style = Style;
        }
    }
}
