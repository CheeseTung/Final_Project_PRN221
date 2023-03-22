using Final_Project_PRN221.Models;
using Microsoft.EntityFrameworkCore;

namespace Final_Project_PRN221.ServiceData
{
    public class DataService
    {
        private readonly Project_PRN221Context _context;
        public DataService(Project_PRN221Context context)
        {
            _context = context;
        }

        public List<User> getAllUsers()
        {
            var query = _context.Users.Include(u => u.Room);
            List<User> users = (query.Where(u => u.Role == 1)).ToList();
            return users;
        }
    }
}
