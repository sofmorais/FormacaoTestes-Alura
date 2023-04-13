using Alura.ByteBank.Dados.Contexto;
using Alura.ByteBank.Dominio.Entidades;
using Alura.ByteBank.Dominio.Interfaces.Repositorios;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Alura.ByteBank.Dados.Repositorio
{
    public class ClienteRepositorio : IClienteRepositorio
    {
        private readonly ByteBankContexto _contexto;
        public ClienteRepositorio()
        {
            _contexto = new ByteBankContexto();
        }
        public bool Adicionar(Cliente cliente)
        {
            try
            {
                _contexto.Clientes.Add(cliente);
                _contexto.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao adicionar cliente: {ex.Message}.");
            }
        }

        public bool Atualizar(int id, Cliente cliente)
        {
            
            try
            {
                if (id != cliente.Id)
                {
                    throw new Exception($"Erro ao atualizar cliente com Id: {id}.");
                }

                _contexto.Entry(cliente).State = EntityState.Modified;
                _contexto.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao atualizar: {ex.Message}.");
            }
        }

        public bool Excluir(int id)
        {
            var cliente = _contexto.Clientes.FirstOrDefault(p => p.Id == id);

            try
            {
                if (cliente == null)
                {
                    throw new Exception($"Erro ao excluir cliente com Id: {id}.");
                }

                _contexto.Clientes.Remove(cliente);
                _contexto.SaveChanges();
                
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao excluir cliente: {ex.Message}.");
            }
        }

        public Cliente ObterPorId(int id)
        {
            try
            {
                var cliente = _contexto.Clientes.FirstOrDefault(p => p.Id == id);

                if(cliente == null)
                {
                    throw new Exception($"Erro ao obter cliente com Id: {id}.");
                }
                return cliente;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter cliente: {ex.Message}.") ;
            }
        }

        public Cliente ObterPorGuid(Guid guid)
        {
            try
            {
                var cliente = _contexto.Clientes.FirstOrDefault(p => p.Identificador == guid);
                
                if (cliente == null)
                {
                    throw new Exception($"Erro ao obter cliente com Guid: {guid}.");
                }
                return cliente;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter cliente: {ex.Message}.");
            }
        }

        public List<Cliente> ObterTodos()
        {
            try
            {
                return _contexto.Clientes.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter clientes: {ex.Message}.");
            }
        }

        public void Dispose()
        {
            _contexto.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
