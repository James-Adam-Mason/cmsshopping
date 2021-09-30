using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CmsShoppingCart.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using CmsShoppingCart.Models;

namespace CmsShoppingCart.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PagesController : Controller
    {
        private readonly CmsShoppingCartContext context;

        public PagesController(CmsShoppingCartContext context) {
            this.context = context;
        }
        //Get /admin/pages
        public async Task< IActionResult> Index()
        {
            IQueryable<PagesController> pages = (IQueryable<PagesController>)(from p in context.Pages orderby p.Sorting select p);

            List<PagesController> pagesList = await pages.ToListAsync();

            

            return View(pagesList);

        }
        //Get admin/pages/details
        public async Task<IActionResult> Details(int id)
        {
            Page page = await context.Pages.FirstOrDefaultAsync(x => x.Id == id);
            //check if view is null
            if (page == null){
                return NotFound();
            }
            return View(page);

        }
        //Get /admin/pages/create
        public IActionResult Create() => View();

        //POST admin/pages/create
        [HttpPost]
        public async Task<IActionResult> Create(Page page)
        {
            if (ModelState.IsValid) {
                page.Slug = page.Title.ToLower().Replace("", "_");
                page.Sorting = 100;

                var slug = await context.Pages.FirstOrDefaultAsync(x => x.Slug == page.Slug);
                if (slug != null)
                {
                    ModelState.AddModelError("", "The Title Already Exists.");//if the page already exists 
                    return View(page);
                }
                //add the page
                context.Add(page);
                await context.SaveChangesAsync();

                //notification that the page has been created
                TempData["Succcess"] = "The page has been created and added";

                return RedirectToAction("Index");
            }
            return View(page);
        }
    }
}
