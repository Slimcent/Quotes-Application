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
        public IActionResult Index()
        {
            Random random = new Random();
            //var res = dbcontext.Quotes.AsNoTracking().OrderByDescending(q => q.Quote);
            var res = dbcontext.Quotes.OrderBy(emp => Guid.NewGuid()).AsNoTracking();
            return View(res);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(QuotesViewModel quotesModel)
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
                    dbcontext.Quotes.Add(quotesModel);
                    dbcontext.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {
                return View();
            }
        }

        public IActionResult Edit (int id)
        {
            try
            {
                var result = dbcontext.Quotes.Where(q => q.Id == id).FirstOrDefault();
                return View("Edit", result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public IActionResult Edit(QuotesViewModel edit)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    dbcontext.Quotes.Update(edit);
                    dbcontext.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    return View("Edit", edit);
                }
            }
            catch (Exception)
            {
                return View("Edit", edit);
            }
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            try
            {
                var deleteQuote = dbcontext.Quotes.Where(q => q.Id == id).FirstOrDefault();
                if (deleteQuote != null )
                {
                    dbcontext.Quotes.Remove(deleteQuote);
                    dbcontext.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    return View();
                }              
            }
            catch (Exception)
            {
                return View();
            }
        }

    }
}
