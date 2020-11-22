using LogIn.Entidades;
using LogIn.UI.Registro;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using LogIn.BLL;
using RegistroPrestamos.BLL;

namespace LogIn.UI
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void IniciarSesionButton_Click(object sender, RoutedEventArgs e)
        {
            string claveHash = Utilities.getHashSha256(ClavePasswordbox.Password);

            Usuarios user = UsuariosBLL.Buscar(UsuarioTextbox.Text, claveHash);

            if(user == null)
            {
                IncorrectoLabel.Visibility = Visibility.Visible;
            }
            else
            {
                new MainWindow(user).Show();
                this.Close();
            }
        }

        private void RegistrarButton_Click(object sender, RoutedEventArgs e)
        {
            new rUsuarios().Show();
            this.Close();
        }
    }
}
