using Alura.Estacionamento.Alura.Estacionamento.Modelos;
using Alura.Estacionamento.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Alura.Estacionamento.Modelos
{
    public class Patio
    {
        //Campos
        private List<Veiculo> veiculos;
        private double faturado;
        private Operador _operador;

        //Propriedades
        public Patio()
        {
            faturado = 0;
            veiculos = new List<Veiculo>();
        }

        public List<Veiculo> Veiculos { get => veiculos; set => veiculos = value; }       
        public double Faturado { get => faturado; set => faturado = value; }
        public Operador OperadorPatio { get => _operador; set => _operador = value; }

        //Métodos
        public double TotalFaturado()
        {
            return this.Faturado;
        }

        public string MostrarFaturamento()
        {
            string totalfaturado = String.Format("Total faturado até o momento :::::::::::::::::::::::::::: {0:c}", this.TotalFaturado());
            return totalfaturado;
        }

        public void RegistrarEntradaVeiculo(Veiculo veiculo)
        {
            veiculo.HoraEntrada = DateTime.Now;
            GerarTicket(veiculo);
            Veiculos.Add(veiculo);            
        }

        public string RegistrarSaidaVeiculo(String placa)
        {
            Veiculo procurado = null;
            string informacao = string.Empty;

            foreach (Veiculo veiculo in Veiculos)
            {
                if (veiculo.Placa == placa)
                {
                    veiculo.HoraSaida = DateTime.Now;
                    TimeSpan tempoPermanencia = veiculo.HoraSaida - veiculo.HoraEntrada;
                    double valorASerCobrado = 0;
                    if (veiculo.Tipo == TipoVeiculo.Automovel)
                    {
                        /// o método Math.Ceiling(), aplica o conceito de teto da matemática onde o valor máximo é o inteiro imediatamente posterior a ele.
                        /// Ex.: 0,9999 ou 0,0001 teto = 1
                        /// Obs.: o conceito de chão é inverso e podemos utilizar Math.Floor();
                        valorASerCobrado = Math.Ceiling(tempoPermanencia.TotalHours) * 2;
                    }

                    if (veiculo.Tipo == TipoVeiculo.Motocicleta)
                    {
                        valorASerCobrado = Math.Ceiling(tempoPermanencia.TotalHours) * 1;
                    }
                    informacao = string.Format("Hora de entrada: {0: HH: mm: ss}\n " +
                                             "Hora de saída: {1: HH:mm:ss}\n "      +
                                             "Permanência: {2: HH:mm:ss} \n "       +
                                             "Valor a pagar: {3:c}", veiculo.HoraEntrada, veiculo.HoraSaida, new DateTime().Add(tempoPermanencia), valorASerCobrado);
                    procurado = veiculo;
                    this.Faturado = this.Faturado + valorASerCobrado;
                    break;
                }

            }
            if (procurado != null)
            {
                this.Veiculos.Remove(procurado);
            }
            else
            {
                return "Não foi possível encontrar nenhum veículo com a placa informada.";
            }

            return informacao;
        }

        public Veiculo PesquisaVeiculo(string idTicket)
        {
            var veiculoEncontrado = (from veiculo in Veiculos
                                     where veiculo.IdTicket == idTicket
                                     select veiculo).SingleOrDefault();
            
            return veiculoEncontrado;
        }

        public Veiculo AlterarDadosVeiculo(Veiculo veiculoAlterado)
        {
            var veiculoTemp = (from veiculo in Veiculos
                                     where veiculo.Placa == veiculoAlterado.Placa
                                     select veiculo).SingleOrDefault();

            veiculoTemp.AlterarDados(veiculoAlterado);

            return veiculoTemp;
        }

        private string GerarTicket(Veiculo veiculo)
        {
            veiculo.IdTicket = new Guid().ToString().Substring(0, 5);

            string ticket = "### Ticket Estacionamento ###" +
                            $">>> Operador: {OperadorPatio.Nome}" +
                            $">>> Identificador: {veiculo.IdTicket}" +
                            $">>> Data/Hora de Entrada: {DateTime.Now}" +
                            $">>> Tipo de automóvel: {veiculo.Tipo}" +
                            $">>> Placa do veículo: {veiculo.Placa}";

            veiculo.Ticket = ticket;
            return ticket;
        }
    }
}
