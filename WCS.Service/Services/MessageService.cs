

using WCS.ClassDomain.Domains;
using WCS.DataLayer.Contex;
using WCS.InterfaceService.Interfaces;
using WCS.Service.ServiceBase;

namespace WCS.Service.Services
{
    public class MessageService : BaseService<Message>, IMessageService
    {
        private readonly ContextWCS _ContextWCS;
        public MessageService(ContextWCS ContextWCS) : base(ContextWCS)
        {
            _ContextWCS = ContextWCS;
        }

    }
}
