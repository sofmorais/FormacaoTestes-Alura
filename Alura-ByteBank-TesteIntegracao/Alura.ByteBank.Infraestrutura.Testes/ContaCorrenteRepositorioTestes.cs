using Alura.ByteBank.Dados.Repositorio;
using Alura.ByteBank.Dominio.Entidades;
using Alura.ByteBank.Dominio.Interfaces.Repositorios;
using Alura.ByteBank.Infraestrutura.Testes.DTO;
using Alura.ByteBank.Infraestrutura.Testes.Servico;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace Alura.ByteBank.Infraestrutura.Testes
{
    public class ContaCorrenteRepositorioTestes
    {
        private readonly IContaCorrenteRepositorio _contaCorrenteRepositorio;

        public ContaCorrenteRepositorioTestes()
        {
            var servico = new ServiceCollection();
            servico.AddTransient<IContaCorrenteRepositorio, ContaCorrenteRepositorio>();

            var provedor = servico.BuildServiceProvider();
            _contaCorrenteRepositorio = provedor.GetRequiredService<IContaCorrenteRepositorio>();
        }

        [Fact]
        public void TestaAdicionarContaCorrenteBancoDados()
        {
            //Arrange
            var contaCorrente = new ContaCorrente()
            {
                Saldo = 10,
                Identificador = Guid.NewGuid(),
                Cliente = new Cliente()
                {
                    Nome = "Kent Nelson",
                    CPF = "486.074.980-45",
                    Identificador = Guid.NewGuid(),
                    Profissao = "Bancário",
                    Id = 1
                },
                Agencia = new Agencia()
                {
                    Nome = "Agencia Central Coast City",
                    Identificador = Guid.NewGuid(),
                    Id = 1,
                    Endereco = "Rua das Flores, 25",
                    Numero = 147
                }
            };

            //Act
            var retorno = _contaCorrenteRepositorio.Adicionar(contaCorrente);

            //Assert
            Assert.True(retorno);
        }

        [Fact]
        public void TestaObterTodasContasCorrentes()
        {
            //Arrange
            //Act
            List<ContaCorrente> contaCorrente = _contaCorrenteRepositorio.ObterTodos();
            //Assert
            Assert.NotNull(contaCorrente);
        }

        [Fact]
        public void TestaObterAgenciaPorId()
        {
            //Arrange
            //Act
            var contaCorrente = _contaCorrenteRepositorio.ObterPorId(1);
            //Assert
            Assert.NotNull(contaCorrente);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void TestaObterVariasAgenciasPorId(int id)
        {
            //Arrange
            //Act
            var contaCorrente = _contaCorrenteRepositorio.ObterPorId(id);
            //Assert
            Assert.NotNull(contaCorrente);
        }

        [Fact]
        public void TestaAtualizaSaldoContaCorrente()
        {
            //Arrange
            var conta = _contaCorrenteRepositorio.ObterPorId(1);
            double saldoNovo = 15;
            conta.Saldo = saldoNovo;

            //Act
            var atualizaSaldo = _contaCorrenteRepositorio.Atualizar(conta.Id, conta);
            
            //Assert
            Assert.True(atualizaSaldo);
        }

        [Fact]
        public void TestaConsultaPix()
        {
            //Arrange
            var guid = new Guid("a0b80d53-c0dd-4897-ab90-c0615ad80d5a");
            var pix = new PixDTO()
            {
                Chave = guid,
                Saldo = 10
            };

            var pixRepositorioMock = new Mock<IPixRepositorio>();
            pixRepositorioMock.Setup(x => x.ConsultaPix(It.IsAny<Guid>())).Returns(pix);

            var mock = pixRepositorioMock.Object;
            
            //Act
            var saldo = mock.ConsultaPix(guid).Saldo;
            
            //Assert
            Assert.Equal(10, saldo);
        }

    }
}
