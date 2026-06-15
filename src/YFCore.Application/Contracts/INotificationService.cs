using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YFCore.Application.Contracts
{
    public interface INotificationService
    {
        Task SendAsync(string token, string title, string message);
    }
}