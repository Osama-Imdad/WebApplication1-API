using DataAccessLayers;
using Microsoft.EntityFrameworkCore;
using WebApplication1_API.DataContext;

namespace WebApplication1_API.Repository
{
    public class EmpolyeeRepository : IEmpolyeeRepository
    {

        //DB Context
        public readonly Application_DBContext _Context;
        public List<Empolyee> empolyees;
        public EmpolyeeRepository(Application_DBContext Context)
        {
            _Context = Context;
        }

        //Adding New Empolyee
        public async Task<Empolyee>AddEmpoloyees(Empolyee empolyee)
        {
         var data = await _Context.Empolyee.AddAsync(empolyee);
            await _Context.SaveChangesAsync();
            return data.Entity;
        }

        

        //Delete Empolyee
        public async Task<Empolyee> DeleteEmployee(int Id)

        {
            var data = await _Context.Empolyee.Where(a => a.Id == Id).FirstOrDefaultAsync();
            if (data != null)
            {
               _Context.Empolyee.Remove(data);
               await _Context.SaveChangesAsync();
                return data;
            }
            return null;
        }

        //Get Empolyee By Id
        public async Task<Empolyee>GetEmployee(int Id)
        {
            return await _Context.Empolyee.Where(a => a.Id == Id).FirstOrDefaultAsync();

        }

        //Get All Empolyyes List 
        public async Task<IEnumerable<Empolyee>> GetAllEmpoloyees()
        {
            
           return await _Context.Empolyee.ToListAsync();
        }


        //update Empolyee
       public async Task<Empolyee>UpdateEmpolyee(Empolyee empolyee)
        {
            var data = await _Context.Empolyee.FirstOrDefaultAsync(a => a.Id == empolyee.Id);
            if(data != null)
            {
                data.name = empolyee.name;
                data.designation = empolyee.designation;
                data.department = empolyee.department;
                data.contactNum = empolyee.contactNum;
                await _Context.SaveChangesAsync();
                return data;
            }
            return null;
        }


        //Sechring Empolyee by Name
        public async Task<IEnumerable<Empolyee>> SearchEmpoloyeeByName(string name)
        {
            IQueryable<Empolyee> query = _Context.Empolyee;
            if(!string.IsNullOrEmpty(name))
            {
                query = query.Where(a => a.name.Contains(name));
            }
            return await query.ToListAsync();
        }
    }
}
