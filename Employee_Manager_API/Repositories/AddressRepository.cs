using Employee_Manager_API.DbClass;
using Employee_Manager_API.Interfaces;
using Employee_Manager_Models;

namespace Employee_Manager_API.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly AppDbContext _context;
        public AddressRepository(AppDbContext context)
        {
            _context = context;
        }
        public bool CreateAddress(Address add)
        {
            var department = _context.Tbl_Address.Add(add);
            return Save();
        }

        public bool DeleteAddress(Address add)
        {
            _context.Remove(add);
            return Save();
        }

        public bool AddressExists(int addId)
        {
            return _context.Tbl_Address.Any(p => p.Id == addId);
        }

        public ICollection<Address> GetAllAddress()
        {
            return _context.Tbl_Address.OrderBy(p => p.Id).ToList();
        }

        public Address GetAddress(int id)
        {
            return _context.Tbl_Address.Where(p => p.Id == id).FirstOrDefault();
        }


        public bool UpdateAddress(Address add)
        {
            _context.Update(add);
            return Save();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
