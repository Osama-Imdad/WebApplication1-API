using DataAccessLayers;

namespace WebApplication1_API.Repository
{
    public interface IEmpolyeeRepository
    {
        Task<IEnumerable<Empolyee>> GetAllEmpoloyees();
        Task<Empolyee> GetEmployee(int Id);
        Task<Empolyee> AddEmpoloyees(Empolyee employee);
        Task<Empolyee> UpdateEmpolyee(Empolyee employee);
        Task<Empolyee> DeleteEmployee(int Id);
        Task<IEnumerable<Empolyee>> SearchEmpoloyeeByName(string name);
    }
}
