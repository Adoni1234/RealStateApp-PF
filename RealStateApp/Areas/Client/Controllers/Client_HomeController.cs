using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealStateApp.Core.Application.Interfaces;
using RealStateApp.Core.Application.ViewModels.Property;
using RealStateApp.Core.Domain.Common;

namespace RealStateApp.Areas.Client.Controllers;

[Area("Client")]
[Authorize(Roles = nameof(Roles.Client))]
public class HomeController(IMapper mapper, IPropertyService propertyService) : Controller
{
    public async Task<IActionResult> Index()
    {
        var clientId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var properties = await propertyService.GetAllPurchasedByClientAsync(clientId);
        var vm = mapper.Map<List<PropertyViewModel>>(properties);
        return View(vm);
    }
}