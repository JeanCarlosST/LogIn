using LogIn.DAL;
using LogIn.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace LogIn.BLL
{
    public class UsuariosBLL
    {
        public static bool Guardar(Usuarios usuario)
        {
            if (!Existe(usuario.UsuarioId))
                return Insertar(usuario);
            else
                return Modificar(usuario);
        }
        private static bool Insertar(Usuarios usuario)
        {
            Contexto contexto = new Contexto();
            bool found = false;

            try
            {
                contexto.Usuarios.Add(usuario);
                found = contexto.SaveChanges() > 0;
            }
            catch
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return found;
        }
        public static bool Modificar(Usuarios usuario)
        {
            Contexto contexto = new Contexto();
            bool found = false;

            try
            {
                contexto.Entry(usuario).State = EntityState.Modified;
                found = contexto.SaveChanges() > 0;
            }
            catch
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return found;
        }
        public static bool Eliminar(int id)
        {
            Contexto contexto = new Contexto();
            bool found = false;

            try
            {
                var usuario = contexto.Usuarios.Find(id);

                if (usuario != null)
                {
                    contexto.Usuarios.Remove(usuario);
                    found = contexto.SaveChanges() > 0;
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return found;
        }
        public static Usuarios Buscar(int id)
        {
            Contexto contexto = new Contexto();
            Usuarios usuario;

            try
            {
                usuario = contexto.Usuarios.Find(id);
            }
            catch
            {
                throw;

            }
            finally
            {
                contexto.Dispose();
            }

            return usuario;
        }

        public static Usuarios Buscar(string user, string clave)
        {
            Contexto contexto = new Contexto();
            Usuarios usuario;

            try
            {
                usuario = contexto.Usuarios.Where(u => (u.Telefono == user || u.Email == user) && u.Clave == clave).FirstOrDefault();
            }
            catch
            {
                throw;

            }
            finally
            {
                contexto.Dispose();
            }

            return usuario;
        }

        public static bool Existe(int id)
        {
            Contexto contexto = new Contexto();
            bool found = false;

            try
            {
                found = contexto.Usuarios.Any(u => u.UsuarioId == id);
            }
            catch
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return found;
        }

        public static List<Usuarios> GetList(Expression<Func<Usuarios, bool>> criterio)
        {
            List<Usuarios> list = new List<Usuarios>();
            Contexto contexto = new Contexto();

            try
            {
                list = contexto.Usuarios.Where(criterio).AsNoTracking().ToList<Usuarios>();
            }
            catch
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return list;
        }

    }
}
