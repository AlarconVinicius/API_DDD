using Domain.Interfaces;
using Domain.Interfaces.InterfacesServices;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class ServiceMessage : IServiceMessage
    {
        private readonly IMessage _iMessage;
        public ServiceMessage(IMessage iMessage)
        {
            _iMessage = iMessage;
        }

        public async Task Adicionar(Message Objeto)
        {
            var validaTitulo = Objeto.ValidarPropriedadeString(Objeto.Titulo, "Titulo");
            if (validaTitulo)
            {
                Objeto.DataCadastro = DateTime.Now;
                Objeto.DataAlteracao = DateTime.Now;
                Objeto.Ativo = true;
                await _iMessage.Add(Objeto);
            }
        }

        public async Task Atualizar(Message Objeto)
        {
            var validaTitulo = Objeto.ValidarPropriedadeString(Objeto.Titulo, "Titulo");
            if (validaTitulo)
            {
                Objeto.DataAlteracao = DateTime.Now;
                await _iMessage.Update(Objeto);
            }
        }

        public async Task<List<Message>> ListarMessageAtivas()
        {
            return await _iMessage.ListarMessage(n => n.Ativo);
        }
    }
}
