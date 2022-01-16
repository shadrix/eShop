using MVC.Services.Interfaces;
using MVC.ViewModels.CatalogViewModels;
using MVC.ViewModels.Pagination;

namespace MVC.Controllers;

public class CatalogController : Controller
{
    private ICatalogService _catalogService;

    public CatalogController(ICatalogService catalogService) =>
        _catalogService = catalogService;
    
    public async Task<IActionResult> Index(int? brandFilterApplied, int? typesFilterApplied, int? page, int? itemsPage)
    {
        var catalog = await _catalogService.GetCatalogItems(page ?? 0, itemsPage ?? 9, brandFilterApplied, typesFilterApplied);
        var vm = new IndexViewModel()
        {
            CatalogItems = catalog.Data,
            Brands = await _catalogService.GetBrands(),
            Types = await _catalogService.GetTypes(),
            PaginationInfo = new PaginationInfo()
            {
                ActualPage = page ?? 0,
                ItemsPerPage = catalog.Data.Count,
                TotalItems = catalog.Count,
                TotalPages = (int)Math.Ceiling((decimal)((decimal)catalog.Count / itemsPage))
            }
        };

        vm.PaginationInfo.Next = (vm.PaginationInfo.ActualPage == vm.PaginationInfo.TotalPages - 1) ? "is-disabled" : "";
        vm.PaginationInfo.Previous = (vm.PaginationInfo.ActualPage == 0) ? "is-disabled" : "";

        return View(vm);
    }
}