using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_Banco_De_Sangre
{
    class Paciente
    {
        private int id, generoint, edad, tiposangreint, tiporhint;
        private string nombres, apellidos, generostring, tiposangrestring, tiporhstring;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Nombres
        {
            get { return nombres; }
            set { nombres = value; }
        }

        public string Apellidos
        {
            get { return apellidos; }
            set { apellidos = value; }
        }

        public int GeneroInt
        {
            get { return generoint; }
            set { generoint = value; }
        }

        public string GeneroString
        {
            get { return generostring; }
            set { generostring = value; }
        }

        public int Edad
        {
            get { return edad; }
            set { edad = value; }
        }

        public int TipoSangreInt
        {
            get { return tiposangreint; }
            set { tiposangreint = value; }
        }

        public string TipoSangreString
        {
            get { return tiposangrestring; }
            set { tiposangrestring = value; }
        }

        public int TipoRHInt
        {
            get { return tiporhint; }
            set { tiporhint = value; }
        }

        public string TipoRHString
        {
            get { return tiporhstring; }
            set { tiporhstring = value; }
        }
    }
}
