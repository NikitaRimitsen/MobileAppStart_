using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Messaging;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileAppStart
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Table_Page : ContentPage
    {
        TableView tabelview;
        SwitchCell sc;
        ImageCell ic;
        TableSection fotosection;
        Button call_btn;
        Button sms_btn;
        Button mail_btn;
        EntryCell tel;
        EntryCell email;
        EntryCell textvpisat;
        EntryCell nimi;
        public Table_Page()
        {
            tel = new EntryCell
            {
                Label = "Telefon",
                Placeholder = "Sisesta tel. number",
                Keyboard = Keyboard.Telephone
            };
            email = new EntryCell
            {
                Label = "Email",
                Placeholder = "Sisesta email",
                Keyboard = Keyboard.Email
            };
            textvpisat = new EntryCell
            {
                Label = "Sõnum:",
                Placeholder = "Kirjuta sõnum",
                Keyboard = Keyboard.Default
            };
            nimi = new EntryCell
            {
                Label = "Nimi:",
                Placeholder = "Sisesta oma sõbra nimi",
                Keyboard = Keyboard.Default
            };
            sc = new SwitchCell { Text = "Näita veel" };
            sc.OnChanged += Sc_OnChanged;
            call_btn = new Button
            {
                Text = "Helista",
                /*HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,*/
                BackgroundColor = Color.Tomato,
                TextColor = Color.Black
            };
            call_btn.Clicked += Call_btn_Clicked;
            sms_btn = new Button
            {
                Text = "SAADA SMS",
                /*HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,*/
                BackgroundColor = Color.Tomato,
                TextColor = Color.Black
            };
            sms_btn.Clicked += Sms_btn_Clicked;
            mail_btn = new Button
            {
                Text = "SAADA EMAIL",
                /*HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,*/
                BackgroundColor = Color.Tomato,
                TextColor = Color.Black
            };
            mail_btn.Clicked += Mail_btn_Clicked;
            ic = new ImageCell
            {
                ImageSource = ImageSource.FromFile("cat.jpg"),
                Text = "Nimi",
                Detail = "Tel:"
            };
            fotosection = new TableSection();
            tabelview = new TableView
            {
                Intent = TableIntent.Form, //могут быть ещё Menu, Data, Settings
                Root = new TableRoot("Andmete sisestamine")
                {
                    new TableSection("Põhiandmed:")
                    {
                        nimi
                    },
                    new TableSection("Kontaktandmed:")
                    {
                        tel,
                        email,
                        textvpisat,
                        sc
                    },
                    fotosection
                }
            };
            StackLayout vertical = new StackLayout
            {
                Children = {tabelview, call_btn, sms_btn, mail_btn },
            };

            Content = vertical;
        }

        private void Call_btn_Clicked(object sender, EventArgs e)
        {
            var call = CrossMessaging.Current.PhoneDialer;
            if (call.CanMakePhoneCall)
            {
                call.MakePhoneCall(tel.Text);
            }
        }
        private void Sms_btn_Clicked(object sender, EventArgs e)
        {
            var sms = CrossMessaging.Current.SmsMessenger;
            if (sms.CanSendSms)
            {
                sms.SendSms(tel.Text, "Tere, " + nimi.Text +"!\n"  + textvpisat.Text);
            }
        }
        private void Mail_btn_Clicked(object sender, EventArgs e)
        {
            var mail = CrossMessaging.Current.EmailMessenger;
            if (mail.CanSendEmail)
            {
                mail.SendEmail(tel.Text, "Tervitus!", textvpisat.Text);
            }
        }

        private void Sc_OnChanged(object sender, ToggledEventArgs e)
        {
            if (e.Value)
            {
                fotosection.Title = "Foto:";
                fotosection.Add(ic);
                sc.Text = "Peida";
                ic.Text = nimi.Text;
                ic.Detail = tel.Text;
            }
            else
            {
                fotosection.Title = "";
                fotosection.Remove(ic);
                sc.Text = "Näita veel";
                ic.Text = nimi.Text;
                ic.Detail = tel.Text;
            }
        }
    }
}