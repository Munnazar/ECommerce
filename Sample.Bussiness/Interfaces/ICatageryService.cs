using Sample.Domain.Models;
using Sample.Models.Requests.ContactUs;
using Sample.Models.Responces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Bussiness.Interfaces
{
    public interface ICatageryService 
    {
        Task<ApiResponce<List<Catagery>>> GetCatageries();
        Task<ApiResponce<List<Catagery>>> GetCatageryById(int id);
        Task<ApiResponce<bool>> AddCatagery(CatageryRequest request);
        Task<ApiResponce<bool>> UpDateCatagery(int id, CatageryRequest request);
        Task<ApiResponce<bool>> DeleteCatagery(int id);
    }
}
