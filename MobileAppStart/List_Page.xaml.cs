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
    public partial class List_Page : ContentPage
    {
        //public List<Telefon> telefons { get; set; }
        public ObservableCollection<Telefon> telefons { get; set; }
        public ObservableCollection<Ruhm<string, Telefon>> telefonideruhmades { get; set; }
        Label lbl_list;
        ListView list;
        Button lisa;
        Button kustuta;
        public List_Page()
        {
            /*telefons = new ObservableCollection<Telefon>
            {
                new Telefon {Nimetus="Samsung Galaxy S22 Ultra", Tootja="Samsung", Hind=1349, Pilt="samsungGaS22.png"},
                new Telefon {Nimetus="Xiaomi Mi 11 Lite 5G NE", Tootja="Xiaomi", Hind=399, Pilt="xiaomimi11.jpg"},
                new Telefon {Nimetus="HUAWEI P50 Pro", Tootja="Huawei", Hind=1199, Pilt="huaweiP50.jpg"},
                new Telefon {Nimetus="iPhone 13", Tootja="Apple", Hind=1179, Pilt="iphone13.png"},
            };*/

            var telefonid = new List<Telefon>
            {
                new Telefon {Nimetus="Samsung Galaxy S22 Ultra", Tootja="Samsung", Hind=1349, Pilt="samsungGaS22.png"},
                new Telefon {Nimetus="Xiaomi Mi 11 Lite 5G NE", Tootja="Xiaomi", Hind=399, Pilt="xiaomimi11.jpg"},
                new Telefon {Nimetus="HUAWEI P50 Pro", Tootja="Huawei", Hind=1199, Pilt="huaweiP50.jpg"},
                new Telefon {Nimetus="iPhone 13", Tootja="Apple", Hind=1179, Pilt="iphone13.png"},
            };
            var ruhmad = telefonid.GroupBy(p => p.Tootja).Select(g => new Ruhm<string, Telefon>(g.Key, g));
            telefonideruhmades = new ObservableCollection<Ruhm<string, Telefon>>(ruhmad);
            lbl_list = new Label
            {
                Text = "Telefomide loetelu",
                HorizontalOptions = LayoutOptions.Center,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label))
            };
            list = new ListView
            {
                SeparatorColor = Color.Orange,
                Header="Minu oma kolektion:",
                Footer = DateTime.Now.ToString("T"),

                HasUnevenRows = true,
                ItemsSource = telefonideruhmades,
                IsGroupingEnabled = true,
                //ItemsSource = telefons,
                /*GroupHeaderTemplate = new DataTemplate(() => 
                {
                    Label tootja = new Label();
                    tootja.SetBinding(Label.TextProperty, "Nimetus");
                    return new ViewCell
                    {
                        View = new StackLayout
                        {
                            Padding = new Thickness(0, 5),
                            Orientation = StackOrientation
                        }
                    }
                }),*/
                ItemTemplate = new DataTemplate(()=>
                {
                    /*Label nimetus = new Label { FontSize = 20 };
                    nimetus.SetBinding(Label.TextProperty, "Nimetus");
                    Label tootja = new Label();
                    tootja.SetBinding(Label.TextProperty, "Tootja");
                    Label hind = new Label();
                    hind.SetBinding(Label.TextProperty, "Hind");
                    return new ViewCell
                    {
                        View = new StackLayout
                        {
                            Padding = new Thickness(0, 5),
                            Orientation = StackOrientation.Vertical,
                            Children  = { nimetus, tootja, hind}
                        }
                    };*/
                    ImageCell imageCell = new ImageCell { TextColor = Color.Moccasin, DetailColor = Color.White };
                    imageCell.SetBinding(ImageCell.TextProperty, "Nimetus");
                    Binding companyBinding = new Binding { Path = "Tootja", StringFormat = "Tore telefon firmalt {0}" };
                    imageCell.SetBinding(ImageCell.DetailProperty, companyBinding);
                    imageCell.SetBinding(ImageCell.ImageSourceProperty, "Pilt");
                    return imageCell;
                })
            };
            lisa = new Button { 
                Text = "Lisa telefon",
                //HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
            };
            lisa.Clicked += Lisa_Clicked;
            kustuta = new Button { 
                Text = "Kustuta telefon",
                //HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
            };
            kustuta.Clicked += Kustuta_Clicked;
            //list.ItemSelected += List_ItemSelected;
            list.ItemTapped += List_ItemTapped;
            this.Content = new StackLayout { Children = { lbl_list, list, lisa, kustuta } };
            this.BackgroundColor = Color.DimGray;
        }

        private void Kustuta_Clicked(object sender, EventArgs e)
        {
            Telefon phone = list.SelectedItem as Telefon;
            if (phone != null)
            {
                telefons.Remove(phone);
                //list.SelectedItem = null;
            }
        }

        private void Lisa_Clicked(object sender, EventArgs e)
        {
            telefons.Add(new Telefon { Nimetus = "Telefon", Tootja = "Tootja", Hind = 1 });
        }

        private async void List_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Telefon selectedPhone = e.Item as Telefon;
            if (selectedPhone != null)
            {
                await DisplayAlert("Выбранная модель", $"{selectedPhone.Tootja} - {selectedPhone.Nimetus}", "OK");
            }
        }

        private void List_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                lbl_list.Text = e.SelectedItem.ToString();
            }
        }
    }
}