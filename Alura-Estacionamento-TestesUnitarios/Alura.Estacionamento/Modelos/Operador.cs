using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alura.Estacionamento.Modelos
{
    public class Operador
    {
        private string _idOperador;
        private string _nome;

        public string Nome { get => _nome; set => _nome = value; }
        public string IdOperador { get => _idOperador; set => _idOperador = value; }

        public Operador()
        {
            IdOperador = new Guid().ToString().Substring(0, 8);
        }

        public override string ToString()
        {
            return $"Operador: {Nome}" +
                   $"Id Operador: {IdOperador}";
        }
    }
}
