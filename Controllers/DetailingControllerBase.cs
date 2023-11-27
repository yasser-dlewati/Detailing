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

        public DetailingControllerBase(IServiceProvider serviceProvider)
        {
            _config = serviceProvider.GetService<IConfiguration>() ?? throw new ArgumentNullException(nameof(serviceProvider));
            var dbservice = serviceProvider.GetService<IDatabaseService>() ?? throw new ArgumentNullException(nameof(serviceProvider));
            dbservice.ConnectionString = _config.GetConnectionString("localMysqlConnectionstring");
            _dbservice = dbservice;
            _manager = serviceProvider.GetService<IModelManager<T>>() ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        [HttpGet]
        public virtual async Task<IActionResult> GetAsync()
        {
            var modelsList = await _manager.GetAllAsync();
            return Ok(modelsList);
        }

        [HttpGet("{id:int}")]
        public virtual async Task<IActionResult> GetAsync(int id)
        {
            var model = await _manager.GetByIdAsync(id);
            return model != null ? Ok(model) : NotFound();
        }

        [HttpPost]
        public virtual async Task<IActionResult> PostAsync([FromBody] T newModel)
        {
            var isModelInserted = _manager.TryInsert(ref newModel);
            return isModelInserted ? CreatedAtAction(nameof(GetAsync), newModel) : BadRequest();
        }

        [HttpPut]
        public virtual async Task<IActionResult> PutAsync([FromBody] T modelToUpdate)
        {
            var isModelUpdated = await _manager.TryUpdateAsync(modelToUpdate);
            return isModelUpdated ? Ok(modelToUpdate) : BadRequest();
        }

        [HttpDelete("{id:int}")]
        public virtual async Task<IActionResult> DeleteAsync(int id)
        {
            var isModelDeleted = await _manager.TryDeleteAsync(id);
            return isModelDeleted ? NoContent() : BadRequest();
        }
    }
}