using Detailing.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Detailing.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public abstract class DetailingControllerBase<T> : ControllerBase where T : IModel
    {
        private readonly IConfiguration _config;
        private readonly IDatabaseService _dbservice;
        private readonly IModelManager<T> _manager;

        public DetailingControllerBase(IConfiguration config, IDatabaseService dbservice, IModelManager<T> manager)
        {
            _config = config;
            dbservice.ConnectionString = config.GetConnectionString("localMysqlConnectionstring");
            _dbservice = dbservice;
            _manager = manager;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var modelsList = _manager.GetAll();
            return Ok(modelsList);
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            var model = _manager.GetById(id);
            return model != null ? Ok(model) : NotFound();
        }


        [HttpPost]
        public IActionResult Post([FromBody] T newModel)
        {
            var isModelInserted = _manager.TryInsert(newModel, out var id);
            newModel.Id = id;
            return isModelInserted ? CreatedAtAction(nameof(Get), newModel) : BadRequest();
        }

        [HttpPut]
        public IActionResult Put([FromBody] T modelToUpdate)
        {
            var isModelUpdated = _manager.TryUpdate(modelToUpdate);
            return isModelUpdated ? Ok(modelToUpdate) : BadRequest();
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var isModelDeleted = _manager.TryDelete(id);
            return isModelDeleted ? NoContent() : BadRequest();
        }

    }
}