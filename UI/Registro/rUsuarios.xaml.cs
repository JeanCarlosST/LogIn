using LogIn.BLL;
using LogIn.Entidades;
using RegistroPrestamos.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LogIn.UI.Registro
{
    /// <summary>
    /// Interaction logic for rUsuarios.xaml
    /// </summary>
    public partial class rUsuarios : Window
    {
        Usuarios usuario;
        public rUsuarios()
        {
            InitializeComponent();
            Limpiar();
        }

        private void VolverLogin(object sender, CancelEventArgs e)
        {
            new Login().Show();
        }

        private void BuscarBoton_Click(object sender, RoutedEventArgs e)
        {
            var usuario = UsuariosBLL.Buscar(Utilities.ToInt(UsuarioIdTextbox.Text));
            Limpiar();

            if (usuario != null)
                this.usuario = usuario;
            else
            {
                this.usuario = new Usuarios();
                MessageBox.Show("No se encontró ningún registro", "Sin coincidencias",
                                MessageBoxButton.OK, MessageBoxImage.Information);
            }

            this.DataContext = this.usuario;
        }

        private void Limpiar()
        {
            this.usuario = new Usuarios();
            this.DataContext = this.usuario;
            ClaveTextbox.Clear();
            ConfirmarClaveTextbox.Clear();
            LongitudTextbox.Visibility = Visibility.Hidden;
            CoincidenciaLabel.Visibility = Visibility.Hidden;
        }

        private void NuevoBoton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private void GuardarBoton_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidarUsuario() || !ValidarClave())
                return;

            usuario.Clave = Utilities.getHashSha256(ClaveTextbox.Password);

            var found = UsuariosBLL.Guardar(usuario);

            if (found)
            {
                MessageBox.Show("Usuario guardado guardado", "Guardado exitoso",
                                MessageBoxButton.OK, MessageBoxImage.Information);
                Limpiar();

            }
            else
                MessageBox.Show("Error", "Hubo un error al guardar",
                                MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void EliminarBoton_Click(object sender, RoutedEventArgs e)
        {
            if (UsuariosBLL.Eliminar(Utilities.ToInt(UsuarioIdTextbox.Text)))
            {
                Limpiar();
                MessageBox.Show("Registro borrado", "Borrado exitoso",
                                MessageBoxButton.OK, MessageBoxImage.Information);

            }
            else
                MessageBox.Show("Error", "Hubo un error al borrar",
                                MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private bool ValidarUsuario()
        {
            if(NombresTextbox.Text.Length == 0)
            {
                MessageBox.Show("Ingrese un nombre", "Registro de usuarios",
                                MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            if (ApellidosTextbox.Text.Length == 0)
            {
                MessageBox.Show("Ingrese un apellido", "Registro de usuarios",
                                MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            if (TelefonoTextbox.Text.Length == 0)
            {
                MessageBox.Show("Ingrese un teléfono", "Registro de usuarios",
                                MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            if (TelefonoTextbox.Text.Length != 0 && TelefonoTextbox.Text.Length < 10)
            {
                MessageBox.Show("Ingrese un teléfono válido \n(longitud tiene que ser mayor a 10)", "Registro de usuarios",
                                MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            if (TelefonoTextbox.Text.Any(char.IsLetter) || TelefonoTextbox.Text.Any(char.IsPunctuation) || TelefonoTextbox.Text.Any(char.IsControl))
            {
                MessageBox.Show("Ingrese un teléfono válido sin letras ni símbolos", "Registro de usuarios",
                                MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            if (CorreoTextbox.Text.Length == 0)
            {
                MessageBox.Show("Ingrese un correo", "Registro de usuarios",
                                MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            if (CorreoTextbox.Text.Count(c => c == '@') != 1)
            {
                MessageBox.Show("Ingrese un correo válido \n(solo puede tener una arroba)", "Registro de usuarios",
                                MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            if (CorreoTextbox.Text.IndexOf("@") == 0)
            {
                MessageBox.Show("Ingrese un correo válido \n(debe poner un nombre antes de la arroba)", "Registro de usuarios",
                                MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            if (!CorreoTextbox.Text.EndsWith(".com"))
            {
                MessageBox.Show("Ingrese un correo válido \n(deber terminar en .com)", "Registro de usuarios",
                                MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            if (CorreoTextbox.Text.EndsWith("@.com"))
            {
                MessageBox.Show("Ingrese un correo válido \n(no tiene terminación)", "Registro de usuarios",
                                MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            return true;
        }

        private bool ValidarClave()
        {
            if(ClaveTextbox.Password.Length < 8)
            {
                LongitudTextbox.Visibility = Visibility.Visible;
                return false;
            }
            LongitudTextbox.Visibility = Visibility.Hidden;

            if(ClaveTextbox.Password.Length != 0 && !ClaveTextbox.Password.Equals(ConfirmarClaveTextbox.Password))
            {
                CoincidenciaLabel.Visibility = Visibility.Visible;
                return false;
            }
            CoincidenciaLabel.Visibility = Visibility.Hidden;


            return true;
        }
        private void Correo_TextChanged(object sender, RoutedEventArgs e)
        {
            if(CorreoTextbox.Text.Contains(" "))
            {
                int caretIndex = CorreoTextbox.CaretIndex;
                CorreoTextbox.Text = CorreoTextbox.Text.Replace(" ", "");
                CorreoTextbox.CaretIndex = caretIndex - 1;
            }
        }

        private void ClaveChanged(object sender, RoutedEventArgs e)
        {
            ValidarClave();
        }

        private void ConfirmarClaveChanged(object sender, RoutedEventArgs e)
        {
            ValidarClave();
        }
    }
}
