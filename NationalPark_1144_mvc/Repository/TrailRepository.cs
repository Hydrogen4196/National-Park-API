using NationalPark_1144_mvc.Models;
using NationalPark_1144_mvc.Repository.IRepository;

namespace NationalPark_1144_mvc.Repository
{
    public class TrailRepository : Repository<Trail>, ITrailRepository
    {
        private readonly IHttpClientFactory _clientFactory;
        public TrailRepository(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }

    }
}
