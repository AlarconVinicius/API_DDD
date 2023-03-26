using AutoMapper;
using Domain.Interfaces;
using Entities.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPIs.Models;

namespace WebAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMapper _iMapper;
        private readonly IMessage _iMessage;

        public MessageController(IMapper iMapper, IMessage iMessage)
        {
            _iMapper = iMapper;
            _iMessage = iMessage;
        }

        [Authorize]
        [Produces("application/json")]
        [HttpPost("/api/Add")]
        public async Task<List<Notifies>> Add(MessageViewModel message)
        {
            message.UserId = await RetornarIdUsuarioLogado();
            var messageMap = _iMapper.Map<Message>(message);
            await _iMessage.Add(messageMap);
            return messageMap.Notificacoes;
        }

        [Authorize]
        [Produces("application/json")]
        [HttpPut("/api/Update")]
        public async Task<List<Notifies>> Update(MessageViewModel message)
        {
            var messageMap = _iMapper.Map<Message>(message);
            await _iMessage.Update(messageMap);
            return messageMap.Notificacoes;
        }

        [Authorize]
        [Produces("application/json")]
        [HttpDelete("/api/Delete")]
        public async Task<List<Notifies>> Delete(MessageViewModel message)
        {
            var messageMap = _iMapper.Map<Message>(message);
            await _iMessage.Delete(messageMap);
            return messageMap.Notificacoes;
        }

        [Authorize]
        [Produces("application/json")]
        [HttpGet("/api/GetEntityById")]
        public async Task<MessageViewModel> GetEntityById(Message message)
        {
            message = await _iMessage.GetEntityById(message.Id);
            var messageMap = _iMapper.Map<MessageViewModel>(message);
            return messageMap;
        }

        [Authorize]
        [Produces("application/json")]
        [HttpGet("/api/List")]
        public async Task<List<MessageViewModel>> List()
        {
            var messages = await _iMessage.List();
            var messagesMap = _iMapper.Map<List<MessageViewModel>>(messages);
            return messagesMap;
        }
        private async Task<string> RetornarIdUsuarioLogado()
        {
            if (User != null)
            {
                var idUser = User.FindFirst("idUser");
                return idUser.Value;
            }
            return String.Empty;
        }

    }
}
