using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Assignment.Data;
using Assignment.Models;
using Assignment.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace Assignment.Controllers
{
    public class AddressBooksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AddressBooksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AddressBooks
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.AddressBook.Include(a => a.AddressType);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: AddressBooks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var addressBook = await _context.AddressBook
                .Include(a => a.AddressType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (addressBook == null)
            {
                return NotFound();
            }

            return View(addressBook);
        }

        // GET: AddressBooks/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["AddressTypeId"] = new SelectList(_context.Set<AddressType>(), "Id", "AddressTypeName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddressVM model)
        {
            if (ModelState.IsValid)
            {
                //AddressBook addressBook = new AddressBook();
                if (model!=null)
                {
                    foreach (var address in model.AddressBooks)
                    {
                        //addressBook.AddressBooks.Add(address);
                        _context.AddressBook.Add(address);
                    }
                    await _context.SaveChangesAsync();
                    return View();
                }                
            }

            ViewData["AddressTypeId"] = new SelectList(_context.Set<AddressType>(), "Id", "AddressTypeName");
            return View(model);
        }

        // GET: AddressBooks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var addressBook = await _context.AddressBook.FindAsync(id);
            if (addressBook == null)
            {
                return NotFound();
            }
            ViewData["AddressTypeId"] = new SelectList(_context.Set<AddressType>(), "Id", "AddressTypeName", addressBook.AddressTypeId);
            return View(addressBook);
        }

        // POST: AddressBooks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Address,AddressTypeId,Date,Time,Remarks")] AddressBook addressBook)
        {
            if (id != addressBook.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(addressBook);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AddressBookExists(addressBook.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AddressTypeId"] = new SelectList(_context.Set<AddressType>(), "Id", "AddressTypeName", addressBook.AddressTypeId);
            return View(addressBook);
        }

        // GET: AddressBooks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var addressBook = await _context.AddressBook
                .Include(a => a.AddressType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (addressBook == null)
            {
                return NotFound();
            }

            return View(addressBook);
        }

        // POST: AddressBooks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var addressBook = await _context.AddressBook.FindAsync(id);
            _context.AddressBook.Remove(addressBook);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AddressBookExists(int id)
        {
            return _context.AddressBook.Any(e => e.Id == id);
        }
        public ActionResult Create1()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create1(AddressVM addressVM)
        {
            foreach (var item in addressVM.AddressBooks)
            {
                _context.AddressBook.Add(item);                
            }
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<JsonResult> SearchByKeyword(string myKeyword)
        {
            var allAddress = await _context.AddressBook.ToListAsync();
            if (myKeyword!=null)
            {
                myKeyword = myKeyword.Trim().ToLower();
                allAddress = allAddress.Where(a => a.Title.ToLower().Contains(myKeyword) || a.Remarks.ToLower().Contains(myKeyword) || a.Address.ToLower().Contains(myKeyword)).ToList();
            }
            return Json(allAddress);
        }
    }
}
