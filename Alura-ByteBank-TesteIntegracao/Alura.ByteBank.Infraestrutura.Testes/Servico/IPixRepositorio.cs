using Alura.ByteBank.Infraestrutura.Testes.DTO;
using System;

namespace Alura.ByteBank.Infraestrutura.Testes.Servico
{
    public interface IPixRepositorio
    {
        public PixDTO ConsultaPix(Guid pix);
    }
}
