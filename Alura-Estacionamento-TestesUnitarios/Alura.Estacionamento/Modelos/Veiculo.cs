using Alura.Estacionamento.Alura.Estacionamento.Modelos;
using System;
using System.Globalization;

namespace Alura.Estacionamento.Modelos
{
    public class Veiculo
    {
        //Campos    
        private string _placa;
        private string _proprietario;
        private TipoVeiculo _tipo;
        private string _ticket;

        //Propriedades   
        public Veiculo()
        {

        }

        public Veiculo(string proprietario)
        {
            Proprietario = proprietario;
        }

        public string Proprietario
        {
            get
            {
                return _proprietario;
            }
            set
            {
                if (value.Length < 3)
                {
                    throw new FormatException("O nome do proprietário precisa ter pelo menos 3 caracteres.");
                }

                _proprietario = value;
            }
        }

        public string Placa
        {
            get
            {
                return _placa;
            }
            set
            {
                //Checa se o valor possui pelo menos 8 caracteres
                if (value.Length != 8)
                {
                    throw new FormatException("A placa deve possuir 8 caracteres.");
                }
                for (int i = 0; i < 3; i++)
                {
                    //Checa se os 3 primeiros caracteres são números
                    if (char.IsDigit(value[i]))
                    {
                        throw new FormatException("Os 3 primeiros caracteres devem ser letras!");
                    }
                }
                //Checa o hífen
                if (value[3] != '-')
                {
                    throw new FormatException("O 4° caractere deve ser um hífen.");
                }
                //Checa se os 3 primeiros caracteres são números
                for (int i = 4; i < 8; i++)
                {
                    if (!char.IsDigit(value[i]))
                    {
                        throw new FormatException("Do 5º ao 8º caractere deve ter um número!");
                    }
                }
                _placa = value;
            }
        }
        /// <summary>
        /// { get; set; } cria uma propriedade automática, ou seja,
        /// durante a compilação, é gerado um atributo para armazenar
        /// o valor da propriedade e os metodos get e set, respectivamente,
        /// lêem e escrevem diretamente no atributo gerado, sem
        /// qualquer validação. É um recurso útil, pois as propriedades
        /// permitem fazer melhor uso do recurso de Reflection do .Net
        /// Framework, entre outros benefícios.
        /// </summary>
        public string Modelo { get; set; }        
        public string Cor { get; set; }
        public TipoVeiculo Tipo { get => _tipo; set => _tipo = value; }
        public double VelocidadeAtual { get; set; }
        public DateTime HoraEntrada { get; set; }
        public DateTime HoraSaida { get; set; }
        public string IdTicket { get; set; }
        public string Ticket { get => _ticket; set => _ticket = value; }

        //Métodos
        public void Acelerar(int tempoSeg)
        {
            this.VelocidadeAtual += (tempoSeg * 10);
        }

        public void Frear(int tempoSeg)
        {
            this.VelocidadeAtual -= (tempoSeg * 15);
        }

        internal void AlterarDados(Veiculo veiculoAlterado)
        {
            Proprietario = veiculoAlterado.Proprietario;
            Modelo = veiculoAlterado.Modelo;
            Cor = veiculoAlterado.Cor;
        }

        //ToString converte um objeto em sua representação de cadeia de caracteres para que ele seja adequado para exibição
        public override string ToString()
        {
            return $"Ficha do veículo:\n " +
                    $"Proprietário: {Proprietario}\n" +
                    $"Placa: {Placa}\n" +
                    $"Modelo: {Modelo}\n" +
                    $"Cor: {Cor}\n" +
                    $"Tipo do veículo: {Tipo}\n";
        }
    }
}
