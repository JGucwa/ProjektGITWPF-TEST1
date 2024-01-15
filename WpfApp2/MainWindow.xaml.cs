using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ItemsControl Lista;
        public MainWindow()
        {
            InitializeComponent();

            SortujZadania();
        }
        private void SortujZadania()
        {
            List<Zadanie> tmp = new List<Zadanie>();
            for (int i = 0;i < new Database().WszystkieZadania().Count;i++)
            {
                if (new Database().WszystkieZadania()[i].Data == Data.SelectedDate)
                {
                    tmp.Add(new Database().WszystkieZadania()[i]);
                }
            }

            tmp.Reverse();
            Lista.ItemsSource = tmp;
        }
        public void DodajZadanie(object s, RoutedEventArgs e)
        {
            if(!string.IsNullOrWhiteSpace(Nazwa) && !string.IsNullOrWhiteSpace(Opis))
            {
                new Database().DodajZadanie(new Zadanie() { Data = Data.SelectedDate, Nazwa = Nazwa.Text, Opis = Opis.Text, Priorytet = Priorytet.SelectedIndex });

                SortujZadania();

                Nazwa.Text = Opis.Text = String.Empty;

                MessageBox.Show("Pomyślnie dodano zadanie");
            }
            else
            {
                MessageBox.Show("Podane dane są nieprawidłowe");
            }
        }
        Zadanie zadanieZaznaczone = new Zadanie();
        public void WyswietlOpis(object s, RoutedEventArgs e)
        {
            StronaGlowna.Visibility = Visibility.Collapsed;
            opis.Visibility = Visibility.Visible;

            MenuItem menu = s as MenuItem;
            zadanieZaznaczone = menu.CommandParameter as Zadanie;

            Opis.DataContext = zadanieZaznaczone;
        }
        public void ZmienPriorytet(object s, RoutedEventArgs e)
        {
            //zadanieZaznaczone.Priorytet = NowyPriorytet.SelectedIndex;
            new Database().EdytujZadanie(zadanieZaznaczone);

            MessageBox.Show("Poprawnie zmieniono priorytet");
        }
        public void WyswietlStroneGlowna(object s, RoutedEventArgs e)
        {
            StronaGlowna.Visibility = Visibility.Visible;
            opis.Visibility = Visibility.Collapsed;

            SortujZadania();
        }

    }
}