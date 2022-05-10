using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileAppStart
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Europarigid : ContentPage
    {
        public static ObservableCollection<Euuropa> eurupos { get; set; }
        Label lbl_list;
        ListView listeu;
        Button lisaeu;
        Button kustutaeu;
        Addeu addeu = new Addeu();
        public Europarigid()
        {
            
            eurupos = new ObservableCollection<Euuropa>
            {
                new Euuropa {Nimetus="Estonia", Pealinn="Tallinn", Elanikkond="1330068" , Pilt="estonia.png"},
                new Euuropa {Nimetus="Liechtenstein", Pealinn="Vaduz", Elanikkond="38896", Pilt="lihstein.png"},
                new Euuropa {Nimetus="Serbia", Pealinn="Belgrad", Elanikkond="7022268", Pilt="serbia.png"},
            };
            lbl_list = new Label
            {
                Text = "Euroopa riigid",
                HorizontalOptions = LayoutOptions.Center,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label))
            };
            listeu = new ListView
            {
                /*SeparatorColor = Color.Orange,
                Header = "Minu oma kolektion:",
                Footer = DateTime.Now.ToString("T"),*/

                HasUnevenRows = true,
                ItemsSource = eurupos,
                ItemTemplate = new DataTemplate(() => {

                    ImageCell imageCell = new ImageCell { TextColor = Color.Moccasin, DetailColor = Color.White };
                    imageCell.SetBinding(ImageCell.TextProperty, "Nimetus");
                    Binding companyBinding = new Binding { Path = "Pealinn", StringFormat = "Pealinn on: {0}" };
                    imageCell.SetBinding(ImageCell.DetailProperty, companyBinding);
                    imageCell.SetBinding(ImageCell.ImageSourceProperty, "Pilt");
                    return imageCell;
                })
            };
            lisaeu = new Button
            {
                Text = "Lisa riik",
                VerticalOptions = LayoutOptions.Center,
            };
            lisaeu.Clicked += Lisaeu_Clicked;
            kustutaeu = new Button
            {
                Text = "Kustuta riik",
                VerticalOptions = LayoutOptions.Center,
            };
            kustutaeu.Clicked += Kustutaeu_Clicked;
            listeu.ItemTapped += Listeu_ItemTapped;
            
            

            //string nimi = addeu.nimieu.Text;
            //eurupos.Add(new Euuropa { Nimetus = nimi, Pealinn = "test", Elanikkond = 1 });
      
            this.Content = new StackLayout { Children = { lbl_list, listeu, lisaeu, kustutaeu} };
            this.BackgroundColor = Color.DimGray;
        }

        private void Kustutaeu_Clicked(object sender, EventArgs e)
        {
            Euuropa euriik = listeu.SelectedItem as Euuropa;
            if (euriik != null)
            {
                eurupos.Remove(euriik);
                //list.SelectedItem = null;
            }
        }

        private async void Lisaeu_Clicked(object sender, EventArgs e)
        {
            /*string nimetuseu = await DisplayPromptAsync("Lisage riik", "Nimetus riik: ", initialValue: "", keyboard: Keyboard.Chat);
            string pealinneu = await DisplayPromptAsync("Lisage pealinn", $"{nimetuseu}" + " pealinna nimi: ", initialValue: "", keyboard: Keyboard.Chat);
            string elanikoodeu = await DisplayPromptAsync("Lisage elanikkond", "Kui palju inimesi elab " + $"{nimetuseu}" + ": ", initialValue: "1", keyboard: Keyboard.Numeric);
            int elanikuperedelen = Convert.ToInt32(elanikoodeu);
            eurupos.Add(new Euuropa { Nimetus = nimetuseu, Pealinn = pealinneu, Elanikkond = elanikuperedelen });*/
            
            await Navigation.PushAsync(new Addeu());
            Add();
        }

        private async void Listeu_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Euuropa selectedeu = e.Item as Euuropa;
            if (selectedeu != null)
            {
                await DisplayAlert("Euroopa riigid: ", $"{selectedeu.Nimetus}: {selectedeu.Pealinn} " + "\nElanikkond: " + $"{selectedeu.Elanikkond}", "OK");
            }
        }

        public async void Add()
        {
            /*lbl_list.Text = "Moskva";
            eurupos.Add(new Euuropa { Nimetus = "teset", Pealinn = "test", Elanikkond = 1 });*/
        }
    }
}