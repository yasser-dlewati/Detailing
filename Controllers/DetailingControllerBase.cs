using Detailing.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Detailing.Controllers
{

    [ApiController]
    public abstract class DetailingControllerBase<T> : ControllerBase
    {
        private readonly IConfiguration _config;
        private IDatabaseService _dbservice;
        protected IEntityRepository<T> _repoService { get; set; }

        public DetailingControllerBase(IConfiguration config, IDatabaseService dbservice)
        {
            _config = config;
            dbservice.ConnectionString = config.GetConnectionString("localMysqlConnectionstring");
            _dbservice = dbservice;
        }

    }
}