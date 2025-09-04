using NationalPark_1144_mvc.Models;
using NationalPark_1144_mvc.Repository.IRepository;

namespace NationalPark_1144_mvc.Repository
{
    public class NationalParkRepository : Repository<NationalPark>, INationalParkRepository
    {
        private readonly IHttpClientFactory _clientFactory;
        public NationalParkRepository(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }

       
    }
}
