using Employee_Manager_Models;

namespace Employee_Manager_API.Interfaces
{
    public interface IAddressRepository
    {
        ICollection<Address> GetAllAddress();
        Address GetAddress(int id);
        bool AddressExists(int addId);
        bool CreateAddress(Address add);
        bool UpdateAddress(Address add);
        bool DeleteAddress(Address add);
        bool Save();
    }
}
