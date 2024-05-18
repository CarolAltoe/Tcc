using Microsoft.AspNetCore.Mvc;
using tcc_core.Query;
using HotChocolate.Execution;

namespace tcc_core.Controllers
{
    [Route("graphql")]
    [ApiController]
    public class GraphQLController : ControllerBase
    {
        private readonly IServiceProvider _serviceProvider;

        public GraphQLController(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] string query)
        {
            var schema = new SchemaBuilder()
                .AddQueryType<TesteQuery>()
                .AddServices(_serviceProvider)
                .Create();

            var executor = schema.MakeExecutable();

            var result = await executor.ExecuteAsync(query);
            return Ok(result);
        }
    }
}