using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Alura.ByteBank.Dominio.Entidades
{
    public class Agencia
    {
        [Key]
        public int Id { get; set; }
        private int _numero;
        public int Numero { 
             get { return _numero; } 
             set { 
                 if(value <= 0)
                {
                    throw new Exception("O campo número da agência não pode ser 0.");
                }
                _numero = value;
             } 
        }
        private String _nome;
        public String Nome {
            get { return _nome; }
            set { 
               if(value.Length <3)
                {
                    throw new Exception("O nome da agência deve possuir pelo menos 3 caracteres.");
                }
                _nome = value;
            } 
        }
        private String _endereco;
        public String Endereco
        {
            get { return _endereco; }
            set
            {
                if (value.Length < 10)
                {
                    throw new Exception("O endereço deve possuir pelo menos 10 caracteres.");
                }
                _endereco = value;
            }
        }
        public virtual ICollection<ContaCorrente> Contas { get; set; }
        public Guid Identificador { get; set; }
        public Agencia()
        {
            Contas = new Collection<ContaCorrente>();            
        }
    }
}
