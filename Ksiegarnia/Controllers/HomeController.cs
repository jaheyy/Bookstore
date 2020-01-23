using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Ksiegarnia.Models;
using System.Net.Mail;
using System.Net;

namespace Ksiegarnia.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult SendEmail(string subject, string body, string name, string email)
        {
            MailMessage message = new MailMessage();
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = false;
            message.From = new MailAddress("ksiegarniazajeciowa@gmail.com", name);
            message.To.Add("ksiegarniazajeciowa@gmail.com");
            message.ReplyToList.Add(email);

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            smtp.Port = 587;

            NetworkCredential networkCredential = new NetworkCredential("ksiegarniazajeciowa@gmail.com", "Qwerty123.");
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = networkCredential;

            smtp.Send(message);

            return View("Contact");
        }
    }
}
