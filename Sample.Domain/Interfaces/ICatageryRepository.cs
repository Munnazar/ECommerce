using Sample.Domain.Models;
using Sample.Models.Requests.ContactUs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Domain.Interfaces
{
    public interface ICatageryRepository
    {
        Task<List<Catagery>> GetCatageries();
        Task<List<Catagery>> GetCatageryById(int id);
        Task<bool> AddCatagery(CatageryRequest request);
        Task<bool> UpDateCatagery(int id, CatageryRequest request);
        Task<bool> DeleteCatagery(int id);
    }
}
