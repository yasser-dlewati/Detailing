using Detailing.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Detailing.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public abstract class DetailingControllerBase<T> : ControllerBase where T : IModel
    {
        private readonly IConfiguration _config;
        private IDatabaseService _dbservice;
        protected IEntityRepository<T> _repoService { get; set; }

        public DetailingControllerBase(IConfiguration config, IDatabaseService dbservice, IEntityRepository<T> repoService)
        {
            _config = config;
            dbservice.ConnectionString = config.GetConnectionString("localMysqlConnectionstring");
            _dbservice = dbservice;
            _repoService = repoService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var modelsList = _repoService.GetAll();
            return Ok(modelsList);
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            var model = _repoService.GetById(id);
            return model != null ? Ok(model) : NotFound();
        }


        [HttpPost]
        public IActionResult Post([FromBody] T newModel)
        {
            var isModelInserted = _repoService.TryInsert(newModel, out var id);
            newModel.Id = id;
            return isModelInserted ? CreatedAtAction(nameof(Get), newModel) : BadRequest();
        }

        [HttpPut]
        public IActionResult Put([FromBody] T modelToUpdate)
        {
            var isModelUpdated = _repoService.TryUpdate(modelToUpdate);
            return isModelUpdated ? Ok(modelToUpdate) : BadRequest();
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var isModelDeleted = _repoService.TryDelete(id);
            return isModelDeleted ? NoContent() : BadRequest();
        }

    }
}