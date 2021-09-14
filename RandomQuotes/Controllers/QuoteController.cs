using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RandomQuotes.Data;
using RandomQuotes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RandomQuotes.Controllers
{
    public class QuoteController : Controller
    {
        EntitiesContext dbcontext = new EntitiesContext();

        public async Task<IActionResult> Index(int pg = 1)
        {
            var quotesResult = await dbcontext.Quotes.OrderBy(emp => Guid.NewGuid()).ToListAsync();

            const int pageSize = 2;
            if (pg < 1)
                pg = 1;
            int quotesCount = quotesResult.Count();
            var pager = new Pager(quotesCount, pg, pageSize);
            int quoteSkip = (pg - 1) * pageSize;
            var quotesWithPagination = quotesResult.Skip(quoteSkip).Take(pager.PageSize).ToList();
            this.ViewBag.Pager = pager;
            return View(quotesWithPagination);
        }
        
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task <IActionResult> Add(QuotesViewModel quotesModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    //ModelState.Clear();
                    return View();
                }
                else
                {
                    await dbcontext.Quotes.AddAsync(quotesModel);
                    await dbcontext.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", ex.Message);
                return View();
            }
        }

        public async Task <IActionResult> Edit(int id)
        {
            try
            {
                var result = await dbcontext.Quotes.Where(q => q.Id == id).FirstOrDefaultAsync();
                return View("Edit", result);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
        }

        [HttpPost]
        public async Task <IActionResult> Edit(QuotesViewModel edit)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    dbcontext.Quotes.Update(edit);
                    await dbcontext.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                else
                {
                    return View("Edit", edit);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Edit", edit);
            }
        }
        
        
        [HttpPost]
        public async Task <IActionResult> Delete(int id)
        {
            try
            {
                var deleteQuote = await dbcontext.Quotes.Where(q => q.Id == id).FirstOrDefaultAsync();
                if (deleteQuote != null )
                {
                    dbcontext.Quotes.Remove(deleteQuote);
                    await dbcontext.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                else
                {
                    return View();
                }              
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
        }

    }
}
