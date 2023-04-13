using Alura.ByteBank.Dados.Contexto;
using System;
using Xunit;

namespace Alura.ByteBank.Infraestrutura.Testes
{
    public class ByteBankContextoTestes
    {
        [Fact]
        public void TestaConexaoContextoComBDMySQL()
        {
            var contexto = new ByteBankContexto();
            bool conectado;
            try
            { 
                conectado = contexto.Database.CanConnect();
            }
            catch (Exception)
            {

                throw new Exception("Não foi possível conectar com a base de dados.");
            }

            Assert.True(conectado);
        }
    }
}
