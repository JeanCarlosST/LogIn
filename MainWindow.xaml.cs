using LogIn.Entidades;
using LogIn.UI;
using LogIn.UI.Consultas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LogIn
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(Usuarios user)
        {
            InitializeComponent();
            NombreLabel.Content = user.Nombres;
        }
        private void VolverLogin(object sender, CancelEventArgs e)
        {
            new Login().Show();
        }

        private void ConsultarBoton_Click(object sender, RoutedEventArgs e)
        {
            new cUsuarios().ShowDialog();
        }

        private void CerrarSesionBoton_Click(object sender, RoutedEventArgs e)
        {
            new Login().Show();
            this.Close();
        }
    }
}
