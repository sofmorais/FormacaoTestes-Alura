using Alura.ByteBank.Dados.Repositorio;
using Alura.ByteBank.Dominio.Entidades;
using Alura.ByteBank.Dominio.Interfaces.Repositorios;
using Alura.ByteBank.Infraestrutura.Testes.Servico;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace Alura.ByteBank.Infraestrutura.Testes
{
    public class AgenciaRepositorioTestes
    {
        private readonly IAgenciaRepositorio _agenciaRepositorio;
        public AgenciaRepositorioTestes()
        {
            var servico = new ServiceCollection();
            servico.AddTransient<IAgenciaRepositorio, AgenciaRepositorio>();

            var provedor = servico.BuildServiceProvider();
            _agenciaRepositorio = provedor.GetService<IAgenciaRepositorio>();
        }

        [Fact]
        public void TestaAdicionarAgenciaBaseDados()
        {
            string nome = "Agencia Guarapari";
            Guid identificador = Guid.NewGuid();
            string endereco = "Rua 7 de Setembro, 123";
            int numero = 1234;

            var agencia = new Agencia()
            {
                Nome = nome,
                Identificador = identificador,
                Endereco = endereco,
                Numero = numero
            };

            var retorno = _agenciaRepositorio.Adicionar(agencia);

            Assert.True(retorno);
        }

        [Fact]
        public void TestaAdicionarAgenciaMock()
        {
            //Arrange
            var agencia = new Agencia()
            {
                Nome = "Agencia Bárbara de Alencar",
                Identificador = Guid.NewGuid(),
                Id = 4,
                Endereco = "Rua Bárbara de Alencar, 136",
                Numero = 6497
            };

            var repositorioMock = new ByteBankRepositorio();

            //Act
            var retorno = repositorioMock.AdicionarAgencia(agencia);

            //Assert
            Assert.True(retorno);
        }

        [Fact]
        public void TestaAtualizarAgencia()
        {
            var agencia = _agenciaRepositorio.ObterPorId(1);
            agencia.Nome = "Agencia Central Park";

            var atualizaAgencia = _agenciaRepositorio.Atualizar(1, agencia);

            Assert.True(atualizaAgencia);
        }

        [Fact]
        public void TestaExcluirAgencia()
        {
            //Arrange
            //Act
            var excluirAgencia = _agenciaRepositorio.Excluir(2);
            
            //Assert
            Assert.True(excluirAgencia);
        }

        [Fact]
        public void TestaObterAgenciaPorId()
        {
            //Arrange
            //Act
            var agencia = _agenciaRepositorio.ObterPorId(1);
            
            //Assert
            Assert.NotNull(agencia);
        }

        [Fact]
        public void TestaObterAgenciaPorGuid()
        {
            //Arrange
            var obterIdAgencia = _agenciaRepositorio.ObterPorId(1);

            //Act
            var obterGuidAgencia = _agenciaRepositorio.ObterPorGuid(obterIdAgencia.Identificador);

            //Assert
            Assert.Equal(obterIdAgencia.Identificador, obterGuidAgencia.Identificador);
        }

        [Fact]
        public void TestaObterTodasAgencias()
        {
            //Arrange
            //Act
            List<Agencia> listAgencias = _agenciaRepositorio.ObterTodos();
            
            //Assert
            Assert.NotNull(listAgencias);
            Assert.Equal(2, listAgencias.Count);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void TestaObterVariasAgenciasPorId(int id)
        {
            //Arrange
            //Act
            var agencia = _agenciaRepositorio.ObterPorId(id);
            
            //Assert
            Assert.NotNull(agencia);
        }

        [Fact]
        public void TestObterAgenciaMock() 
        {
            //Arrange
            var byteBankRepositorioMock = new Mock<IByteBankRepositorio>();
            var mock = byteBankRepositorioMock.Object;
            
            //Act
            var lista = mock.BuscarAgencias();

            //Assert
            byteBankRepositorioMock.Verify(b => b.BuscarAgencias());
        }

        //Testando exceptions
        [Fact]
        public void TestaExceptionObterAgenciaPorId()
        {
            //Arrange
            //Act
            //Assert
            Assert.Throws<Exception>(() => _agenciaRepositorio.ObterPorId(33));
        }
    }
}