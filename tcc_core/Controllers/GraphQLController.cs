using HotChocolate.Execution;
using HotChocolate.Language;
using Microsoft.AspNetCore.Mvc;

namespace tcc_core.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class GraphQLController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public readonly IRequestExecutor _requestExecutor;

        public GraphQLController(IHttpContextAccessor httpContextAccessor, IRequestExecutor requestExecutor)
        {
            _httpContextAccessor = httpContextAccessor;
            _requestExecutor = requestExecutor;
        }

        /*
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] GraphQLRequest request)
        {
            IReadOnlyQueryRequest queryRequest = QueryRequestBuilder.New()
                .SetQuery(request.Query)
                .SetOperationName(request.OperationName)
                .SetVariables(request.Variables)
                .Create();

            IExecutionResult executionResult =
                await _requestExecutor.ExecuteAsync(queryRequest, _httpContextAccessor.HttpContext.RequestAborted);

            if (executionResult is IQueryResult queryResult)
            {
                return Ok(queryResult);
            }

            if (executionResult is IQueryError queryError)
            {
                return BadRequest(queryError);
            }

            return BadRequest();
        }
        */
    }

}
