using Alura.Estacionamento.Alura.Estacionamento.Modelos;
using Alura.Estacionamento.Modelos;
using Xunit.Abstractions;

namespace Alura.Estacionamento.Testes
{
    public class PatioTeste : IDisposable
    {
        public ITestOutputHelper SaidaConsoleTeste;
        private Veiculo veiculo;
        private Patio estacionamento;
        private Operador operador;

        public PatioTeste(ITestOutputHelper _saidaConsoleTeste)
        {
            SaidaConsoleTeste = _saidaConsoleTeste;
            SaidaConsoleTeste.WriteLine("Executando o construtor.");

            veiculo = new Veiculo();
            estacionamento = new Patio();
            operador = new Operador();

            operador.Nome = "Júlio Lima";
            estacionamento.OperadorPatio = operador;
        }
        
        [Fact]
        public void ValidaFaturamentoUmVeiculo()
        {
            //Arrange
            veiculo.Proprietario = "Sofia Morais";
            veiculo.Placa = "ABC-0909";
            veiculo.Modelo = "Celta";
            veiculo.Cor = "Prata";
            veiculo.Tipo = TipoVeiculo.Automovel;

            //Registrando infos para faturamento
            estacionamento.RegistrarEntradaVeiculo(veiculo);
            estacionamento.RegistrarSaidaVeiculo(veiculo.Placa);

            //Act
            double faturamento = estacionamento.TotalFaturado();

            //Assert
            Assert.Equal(2, faturamento);
        }

        [Theory]
        [InlineData("João Silva", "ASD-1999", "Palio", "Prata")]
        [InlineData("Maria Silva", "POT-2378", "Cobalt", "Preto")]
        [InlineData("José Silva", "FHG-9154", "Classic", "Branco")]
        public void ValidaFaturamentoVariosVeiculos(string proprietario,
                                                      string placa,
                                                      string modelo,
                                                      string cor)
        {
            //Arrange
            veiculo.Proprietario = proprietario;
            veiculo.Placa = placa;
            veiculo.Cor = cor;
            veiculo.Modelo = modelo;

            estacionamento.RegistrarEntradaVeiculo(veiculo);
            estacionamento.RegistrarSaidaVeiculo(veiculo.Placa);

            //Act
            double faturamento = estacionamento.TotalFaturado();
            //Assert
            Assert.Equal(2, faturamento);
        }

        [Theory]
        [InlineData("João Silva", "ASD-1999", "Palio", "Prata")]
        public void LocalizaVeiculoPelaPlaca(string proprietario, 
                                              string placa,
                                              string modelo,
                                              string cor)
        {
            //Arrange
            veiculo.Proprietario = proprietario;
            veiculo.Placa = placa;
            veiculo.Modelo = modelo;
            veiculo.Cor = cor;
            veiculo.IdTicket = new Guid().ToString();

            estacionamento.RegistrarEntradaVeiculo(veiculo);

            //Act
            var consultaTicket = estacionamento.PesquisaVeiculo(veiculo.IdTicket);

            //Assert
            //Assert.Contains($"Identificador: {veiculo.IdTicket}", consultaTicket.Ticket);
            Assert.Equal(veiculo.IdTicket, consultaTicket.IdTicket);
        }

        [Fact]
        public void AlterarDadosVeiculo()
        {
            veiculo.Proprietario = "José Silva";
            veiculo.Placa = "ZXC-5678";
            veiculo.Modelo = "HB20";
            veiculo.Cor = "Verde";

            estacionamento.RegistrarEntradaVeiculo(veiculo);

            Veiculo veiculoCorAlterada = new Veiculo();
            veiculoCorAlterada.Proprietario = "José Silva";
            veiculoCorAlterada.Placa = "ZXC-5678";
            veiculoCorAlterada.Modelo = "HB20";
            veiculoCorAlterada.Cor = "Prata"; //Dado alterado

            Veiculo veiculoAlterado = estacionamento.AlterarDadosVeiculo(veiculoCorAlterada);

            Assert.Equal(veiculoAlterado.Cor, veiculoCorAlterada.Cor);
        }

        public void Dispose()
        {
            SaidaConsoleTeste.WriteLine("Dispose invocado.");
        }
    }
}
