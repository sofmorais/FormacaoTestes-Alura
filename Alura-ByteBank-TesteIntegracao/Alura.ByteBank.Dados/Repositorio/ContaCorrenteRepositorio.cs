using Alura.ByteBank.Dados.Contexto;
using Alura.ByteBank.Dominio.Entidades;
using Alura.ByteBank.Dominio.Interfaces.Repositorios;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Alura.ByteBank.Dados.Repositorio
{
    public class ContaCorrenteRepositorio: IContaCorrenteRepositorio
    {
        private readonly ByteBankContexto _contexto;
        public ContaCorrenteRepositorio()
        {
            _contexto = new ByteBankContexto();
        }
        public bool Adicionar(ContaCorrente conta)
        {
            try
            {    //https://docs.microsoft.com/pt-br/ef/core/change-tracking/identity-resolution            
                _contexto.ContaCorrentes.Update(conta);
                _contexto.SaveChanges();

                return true;
            }
            catch(Exception ex)
            {
                throw new Exception($"Erro ao adicionar conta corrente: {ex.Message}.");
            }
        }

        public bool Atualizar(int id, ContaCorrente conta)
        {
            try
            {
                if (id != conta.Id)
                {
                    throw new Exception($"Erro ao atualizar conta corrente com Id: {id}.");
                }

                _contexto.Entry(conta).State = EntityState.Modified;
                _contexto.SaveChanges();
                
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao atualizar conta corrente: {ex.Message}.");
            }
        }

        public bool Excluir(int id)
        {
            var conta = _contexto.ContaCorrentes.FirstOrDefault(p => p.Id == id);

            try
            {
                if (conta == null)
                {
                    throw new Exception($"Erro ao excluir conta corrente com Id: {id}.");
                }

                _contexto.ContaCorrentes.Remove(conta);
                _contexto.SaveChanges();
                
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao excluir conta corrente: {ex.Message}.");
            }
        }

        public ContaCorrente ObterPorId(int id)
        {
            try
            {
                var conta = _contexto.ContaCorrentes.Include(c => c.Cliente)
                                                    .Include(x => x.Agencia).FirstOrDefault(p => p.Id == id);
                
                if (conta == null)
                {
                    throw new Exception($"Erro ao obter conta corrente com Id: {id}.");
                }
                return conta;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter conta corrente: {ex.Message}.");
            }
        }

        public ContaCorrente ObterPorGuid(Guid guid)
        {
            try
            {
                var conta = _contexto.ContaCorrentes.Include(c=>c.Cliente)
                                                    .Include(x=>x.Agencia).FirstOrDefault(p => p.Identificador == guid);
                
                if (conta == null)
                {
                    throw new Exception($"Erro ao obter conta com Guid: {guid}.");
                }
                return conta;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter conta corrente: {ex.Message}.");
            }
        }
        public List<ContaCorrente> ObterTodos()
        {
            try
            {
                return _contexto.ContaCorrentes.Include(c => c.Cliente)
                                               .Include(x => x.Agencia).ToList();
            }
            catch(Exception ex)
            {
                throw new Exception($"Erro ao obter conta corrente: {ex.Message}.");
            }
        }

        public void Dispose()
        {
            _contexto.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
