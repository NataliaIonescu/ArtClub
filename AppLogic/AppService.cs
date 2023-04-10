using ArtClub.DataAccess.Abstractions;
using ArtClub.DataAccess.Model;

namespace ArtClub.AppLogic
{
    public class AppService
    {
        private readonly IAppRepository appRepository;
        public AppService(IAppRepository appRepository)
        {
            this.appRepository = appRepository;
        }

        //public IEnumerable<Report> GetProducts()
        //{
        //    return reportRepository.GetAll();
        //}

    }
}


