using JS.Chat.Domain.Interfaces;
using JS.Chat.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Security.Claims;
using System.Threading.Tasks;

namespace JS.Chat.Presentation.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            var messages = await _unitOfWork.Messages.GetMessages();
            return View(messages);
        }

        [HttpPost]
        public IActionResult SaveMessage(string message)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var userId = claim.Value;
            var tMessage = new TMessage
            {
                Message = message,
                DateMessage = DateTime.Now,
                UserId = Guid.Parse(userId),
                UserName = User.Identity.Name
            };
            _unitOfWork.Messages.AddMessage(tMessage);
            _unitOfWork.Complete();
            return Ok();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
