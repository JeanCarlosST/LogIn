using LogIn.BLL;
using LogIn.Entidades;
using RegistroPrestamos.BLL;
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

namespace LogIn.UI.Consultas
{
    /// <summary>
    /// Interaction logic for cUsuarios.xaml
    /// </summary>
    public partial class cUsuarios : Window
    {
        public cUsuarios()
        {
            InitializeComponent();
        }

        private void ConsultarBoton_Click(object sender, RoutedEventArgs e)
        {
            var listado = new List<Usuarios>();

            string criterio = CriterioTextbox.Text.Trim();
            if (criterio.Length > 0)
            {
                switch (FiltroComboBox.SelectedIndex)
                {
                    case 0:
                        listado = UsuariosBLL.GetList(u => u.UsuarioId == Utilities.ToInt(CriterioTextbox.Text));
                        break;

                    case 1:
                        listado = UsuariosBLL.GetList(u => u.Nombres.ToLower().Contains(criterio.ToLower()));
                        break;

                    case 2:
                        listado = UsuariosBLL.GetList(u => u.Apellidos.ToLower().Contains(criterio.ToLower()));
                        break;

                    case 3:
                        listado = UsuariosBLL.GetList(u => u.Telefono.ToLower().Contains(criterio.ToLower()));
                        break;

                    case 4:
                        listado = UsuariosBLL.GetList(u => u.Email.ToLower().Contains(criterio.ToLower()));
                        break;
                }
            }
            else
            {
                listado = UsuariosBLL.GetList(c => true);
            }

            UsuariosDataGrid.ItemsSource = null;
            UsuariosDataGrid.ItemsSource = listado;
        }
    }
}
