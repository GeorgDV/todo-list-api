using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using KlientRakendus.Models;
using KlientRakendus.Data;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using KlientRakendus.Data.Models;

namespace KlientRakendus.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;


        public HomeController(ILogger<HomeController> logger, ApplicationDbContext dbContext, IMapper mapper)
        {
            _logger = logger;
            _dbContext = dbContext;
            _mapper = mapper;
        }



        public async Task<ActionResult> Index()
        {
            var tasks = await _dbContext.Tasks
                                  .ToListAsync();

            if (tasks == null)
            {
                return NotFound();
            }

            var model = _mapper.Map<List<TaskModel>>(tasks);

            return View(model);
        }



        // GET: Home/Create
        public ActionResult Create()
        {
            var model = new TaskCreateModel();

            model.Referer = Request.Headers["Referer"].ToString();
            return View(model);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TaskCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var task = new TaskModel();
            _mapper.Map(model, task);

            try
            {
                _dbContext.Tasks.Add(task);
                await _dbContext.SaveChangesAsync();
            }
            catch
            {
                return NotFound();
            }

            string prevPage = model.Referer;  // Request.Headers["Referer"].ToString();
            return Redirect(prevPage);
        }



        // GET: Home/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _dbContext.Tasks.FindAsync(id);

            if (task == null)
            {
                return NotFound();
            }

            var model = _mapper.Map<TaskEditModel>(task);

            model.Referer = Request.Headers["Referer"].ToString();
            return View(model);
        }

        // POST: Home/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, TaskCreateModel model)
        {
            var task = await _dbContext.Tasks
                                       .FirstOrDefaultAsync(t => t.Id == id);

            if (task == null)
            {
                return NotFound();
            }

            try
            {
                _dbContext.Tasks.Remove(task);
                await _dbContext.SaveChangesAsync();
            }
            catch
            {
                return NotFound();
            }

            string prevPage = model.Referer;
            return Redirect(prevPage);
        }




        // GET: Home/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _dbContext.Tasks.FindAsync(id);

            if (task == null)
            {
                return NotFound();
            }

            var model = _mapper.Map<TaskEditModel>(task);

            model.Referer = Request.Headers["Referer"].ToString();
            return View(model);
        }

        // POST: Home/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TaskEditModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            
            var task = await _dbContext.Tasks
                                       .FindAsync(id);

            if (task == null || task.Id != id)
            {
                return NotFound();
            }
            if (!TaskExists(task.Id))
            {
                return NotFound();
            }

            _mapper.Map(model, task);

            try
            {
                _dbContext.Tasks.Update(task);
                await _dbContext.SaveChangesAsync();
            }
            catch
            {
                return NotFound();
            }

            string prevPage = model.Referer;
            return Redirect(prevPage);
        }

        public IActionResult Documentation()
        {
            return View();
        }

        public IActionResult Login()
        {
            return RedirectToAction("Index");
        }

        public IActionResult Register()
        {
            return RedirectToAction("Index");
        }

        public bool TaskExists(int id)
        {
            return _dbContext.Tasks.Any(i => i.Id == id);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
