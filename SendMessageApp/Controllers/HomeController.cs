using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.NotificationHubs;
using Microsoft.Extensions.Logging;
using SendMessageApp.Models;

namespace SendMessageApp.Controllers
{
    public class HomeController : Controller
    {
        private NotificationHubClient _hub;
        private readonly ILogger<HomeController> _logger;

        private List<string> _messages;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _hub = NotificationHubClient.CreateClientFromConnectionString("Endpoint=sb://xfazurenotificationhubs.servicebus.windows.net/;SharedAccessKeyName=DefaultFullSharedAccessSignature;SharedAccessKey=rzpdLjKxq5IOB42XUYfz9+zwPbshw7kbioUvAyEnCkM=", "XFAzureNotificationHubs");

            _messages = new List<string>
            {
                "大したやつだ!",
                "頑張っているじゃないか!",
                "相変わらず天才だな",
                "すごいな!",
                "最高だぜ!",
                "センスいいね!",
                "やるじゃん!",
                "すごい,すごすぎる!",
                "尊敬するよ",
                "見習いたいものだ",
                "どうしてそんなにすごいの?",              
            };
        }

        //public async Task<IActionResult> Index()
        //{
        //    return View();
        //}
        
        public async Task<IActionResult> Index(int id)
        {
            if (id != 0)
            {
                var rand = new Random();
                var messageIndex = rand.Next(0, _messages.Count - 1);
                string message = _messages[messageIndex];
                var androidMessage = "{\"data\":{\"message\": \"" + message + "\"}}";
                await _hub.SendFcmNativeNotificationAsync(androidMessage);
            }

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
