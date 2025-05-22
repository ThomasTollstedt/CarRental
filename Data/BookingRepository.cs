using CarRental.Controllers;
using CarRental.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Data
{
    public class BookingRepository : IBooking
    {
        private readonly ApplicationDbContext _context;

        public BookingRepository (ApplicationDbContext context)
        {
            _context = context;
        }

        public Booking Create(Booking booking)
        {
            _context.Bookings.Add(booking);
            var car = _context.Cars.Find(booking.CarId);
            if (car != null)
            {
                car.Available = false; // Markera bilen som ej tillgänglig
            }
            _context.SaveChanges();
            return booking;
            
        }

        public void DeleteBooking(int id)
        {
            var booking = _context.Bookings.Find(id);
            if (booking != null)
            {
                var car = _context.Cars.Find(booking.CarId);                              
                if (car != null)
                {
                    car.Available = true; // Markera bilen som tillgänglig igen
                }
                _context.Bookings.Remove(booking);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Booking> GetAllBookings()
        {
            return _context.Bookings.Include(b => b.Car).OrderByDescending(b => b.Id).ToList();                           
               
        }

        public Booking GetBookingById(int id)
        {
            var booking = _context.Bookings.Include(b => b.Car).FirstOrDefault(b => b.Id == id);
            if (booking != null) 
            {
                return booking;
            }
            return null; 
        }
    }
}
