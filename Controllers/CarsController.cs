using CarRental.Data;
using CarRental.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Controllers
{
    [Authorize]
public class CarsController : Controller
    {
        private readonly ICar _carRepository;

        public CarsController(ICar carRepository)
        {
            _carRepository = carRepository;
        }

        // GET: CarsController  
        [Authorize(Roles = "Customer,Admin")] 
        public ActionResult Index()
        {
            var cars = _carRepository.ListAllCars(); // Hämtar alla bokningar  
            return View(cars);
        }

        // GET: CarsController/Details/5  
        public ActionResult Details(int id) // TAS BORT? FINNS EJ MED I KRAVSPEC/USER STORIES????
        {
            var car = _carRepository.GetCar(id);
            if (car == null)
            {
                return NotFound();
            }
            return View(car);
        }

        // GET: CarsController/Create  
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: CarsController/Create  
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(Car car)
        {
            {
                if (ModelState.IsValid)
                {
                    _carRepository.Add(car); // Skapar bokning  
                    TempData["Message"] = "Bil skapad!"; // Meddelande om att bilen skapats  
                    return RedirectToAction(nameof(Index));
                }
                return View(car);
            }
        }

        // GET: CarsController/Edit/5  
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CarsController/Edit/5  
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CarsController/Delete/5  
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CarsController/Delete/5  
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
