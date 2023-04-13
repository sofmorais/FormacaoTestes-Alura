using Alura.ByteBank.Dados.Contexto;
using Alura.ByteBank.Dominio.Entidades;
using Alura.ByteBank.Dominio.Interfaces.Repositorios;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Alura.ByteBank.Dados.Repositorio
{
    public class AgenciaRepositorio : IAgenciaRepositorio
    {
        private readonly ByteBankContexto _contexto;
        public AgenciaRepositorio()
        {
            _contexto = new ByteBankContexto();
        }

        public bool Adicionar(Agencia agencia)
        {
            try
            {
                _contexto.Agencias.Add(agencia);
                _contexto.SaveChanges();

                return true;
            }
            catch(Exception ex)
            {
                throw new Exception($"Erro ao adicionar agência: {ex.Message}.");
            }
        }

        public bool Atualizar(int id, Agencia agencia)
        {
            try
            {
                if (id != agencia.Id)
                {
                    throw new Exception($"Erro ao atualizar agência com Id: {id}.");
                }

                _contexto.Entry(agencia).State = EntityState.Modified;
                _contexto.SaveChanges();
                
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao atualizar agência: {ex.Message}.");
            }
        }

        public bool Excluir(int id)
        {
            var agencia = _contexto.Agencias.FirstOrDefault(p => p.Id == id);

            try
            {
                if (agencia == null)
                {
                    throw new Exception($"Erro ao excluir agência com Id: {id}.");
                }

                _contexto.Agencias.Remove(agencia);
                _contexto.SaveChanges();
               
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao excluir agência: {ex.Message}.");
            }
        }

        public Agencia ObterPorId(int id)
        {
            try
            {
                var agencia = _contexto.Agencias.FirstOrDefault(p => p.Id == id);
                
                if(agencia == null)
                {
                    throw new Exception($"Erro ao obter agência com Id: {id}");
                }

                return agencia;
            }
            catch(Exception ex) 
            {
               throw new Exception($"Erro ao obter agência: {ex.Message}.");
            }
        }

        public Agencia ObterPorGuid(Guid guid)
        {
            try
            {
                var agencia = _contexto.Agencias.FirstOrDefault(p => p.Identificador == guid);
                
                if (agencia == null)
                {
                    throw new Exception($"Erro ao obter agência com Guid: {guid}.");
                }

                return agencia;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter agência: {ex.Message}.");
            }
        }

        public List<Agencia> ObterTodos()
        {
            try
            {
                return _contexto.Agencias.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter agências: {ex.Message}.");
            }
        }

        public void Dispose()
        {
            _contexto.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
