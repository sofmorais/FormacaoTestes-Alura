using Alura.ByteBank.Dados.Repositorio;
using Alura.ByteBank.Dominio.Entidades;
using Alura.ByteBank.Dominio.Interfaces.Repositorios;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using Xunit;

namespace Alura.ByteBank.Infraestrutura.Testes
{
    public class ClienteRepositorioTestes
    {
        private readonly IClienteRepositorio _clienteRepositorio;

        public ClienteRepositorioTestes()
        {
            var servico = new ServiceCollection();
            servico.AddTransient<IClienteRepositorio, ClienteRepositorio>();

            var provedor = servico.BuildServiceProvider();
            _clienteRepositorio = provedor.GetRequiredService<IClienteRepositorio>();
        }

        [Fact]
        public void TestaObterTodosClientes()
        {
            //Arrange
            //Act
            List<Cliente> listaClientes = _clienteRepositorio.ObterTodos();
            //Assert
            Assert.NotNull(listaClientes);
            Assert.Equal(3, listaClientes.Count);
        }

        [Fact]
        public void TestaObterClientePorId()
        {
            //Arrange
            //Act
            var cliente = _clienteRepositorio.ObterPorId(1);
            //Assert
            //Assert.Equal(clienteUm.Id, cliente.Id);
            Assert.NotNull(cliente);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void TestaObterVariosClientePorId(int id)
        {
            //Arrange
            //Act
            var cliente = _clienteRepositorio.ObterPorId(id);
            //Assert
            Assert.NotNull(cliente);
        }

    }
}
