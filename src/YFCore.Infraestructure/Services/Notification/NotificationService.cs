using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using YFCore.Application.Contracts;

namespace YFCore.Infraestructure.Services.Notification
{
    public class NotificationService : INotificationService
    {
        public Task SendAsync(string token, string title, string message)
        {
            Console.WriteLine($"Notification sent to token: {token}, title: {title}, message: {message}");
            return Task.CompletedTask;}
    }
}