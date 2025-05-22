using CarRental.Data;
using CarRental.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Controllers
{
    public class CarsController : Controller
    {
        private readonly ICar _carRepository;

        public CarsController(ICar carRepository)
        {
            _carRepository = carRepository;
        }

        // GET: CarsController
        public ActionResult Index()
        {
            var cars = _carRepository.ListAllCars(); // Hämtar alla bokningar
            return View(cars);
        }

        // GET: CarsController/Details/5
        public ActionResult Details(int id)
        {
            var car = _carRepository.GetCar(id);
            if (car == null)
            {
                return NotFound();
            }
            return View(car);
        }

        // GET: CarsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CarsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
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
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CarsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
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
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CarsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
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
