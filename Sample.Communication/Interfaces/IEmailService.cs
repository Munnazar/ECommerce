using Sample.Models.ServiceRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Communication.Interfaces
{
    public interface IEmailService
    {
        public Task<bool> SendEmailAsync(MailRequest mailRequest);

    }
}
