using Alura.Estacionamento.Alura.Estacionamento.Modelos;
using Alura.Estacionamento.Modelos;
using System.Globalization;
using Xunit.Abstractions;

namespace Alura.Estacionamento.Testes
{
    public class VeiculoTeste : IDisposable
    {
        public ITestOutputHelper SaidaConsoleTeste;
        private Veiculo veiculo;

        // Usando o construtor da classe teste podemos criar uma vari�vel global para reutilizar nos testes
        public VeiculoTeste(ITestOutputHelper _saidaConsoleTeste)
        {
            SaidaConsoleTeste = _saidaConsoleTeste;
            SaidaConsoleTeste.WriteLine("Executando o construtor.");

            veiculo = new Veiculo();
        }

        //� uma boa pr�tica utilizar um padrão para o desenvolvimentos dos testes, pois facilita a manuten��o e leitura do c�digo
        //O padr�o AAA deixa mais claro o que est� sendo testado e os resultado
        [Fact]
        [Trait("Funcionalidade", "Acelerar")]
        public void TestaVeiculoAcelerar()
        {
            //Arrange: Prepara��o do cen�rio de teste, iniciar vari�veis, objetos
            //var veiculo = new Veiculo();
            //Act: Definir o m�todo que será testado
            veiculo.Acelerar(10);
            //Assert: Valida��o do resultado obtido com o esperado de acordo com o m�todo
            Assert.Equal(100, veiculo.VelocidadeAtual);
        }

        [Fact]
        //Propriedade Trait permite agrupar testes criando uma categoria e atribuindo um valor
        [Trait("Funcionalidade", "Frear")]
        public void TestaVeiculoFrear()
        {
            //Act
            veiculo.Frear(10);
            //Assert
            Assert.Equal(-150, veiculo.VelocidadeAtual);
        }

        [Fact]
        public void TestaTipoVeiculo()
        {
            Assert.Equal(TipoVeiculo.Automovel, veiculo.Tipo);
        }

        [Fact]
        public void ValidaNomeProprietario()
        {
            veiculo.Proprietario = "Sofia Morais";

            string dadosFichaVeiculo = veiculo.ToString();

            Assert.Contains(veiculo.Proprietario, dadosFichaVeiculo);
        }

        [Fact]
        public void ObterDadoFichaDoVeiculo()
        {
            veiculo.Proprietario = "Pedro Silva";
            veiculo.Placa = "JIT-7865";
            veiculo.Modelo = "Corolla";
            veiculo.Cor = "Prata";
            veiculo.Tipo = TipoVeiculo.Automovel;

            string dadosFichaVeiculo = veiculo.ToString();

            Assert.Contains("Tipo do veículo", dadosFichaVeiculo);
        }

        //Testes que retornam alguma exeception
        [Fact]
        public void ValidaNomeProprietario_RetornaException()
        {
            //Arrange
            string nomeProprietario = "An";
            //Assert
            Assert.Throws<FormatException>(
               //Act
               () => new Veiculo(nomeProprietario)
           );
        }

        [Fact]
        public void ValidaPlacaVeiculoSemHifen_RetornaException()
        {
            //Arrange
            string placa = "ABCD1234";
            //Assert
            var mensagem = Assert.Throws<FormatException>(
               //Act
               () => veiculo.Placa = placa
           ); ;

            Assert.Equal("O 4° caractere deve ser um hífen.", mensagem.Message);
        }

        public void Dispose()
        {
            SaidaConsoleTeste.WriteLine("Dispose invocado.");
        }
    }
}