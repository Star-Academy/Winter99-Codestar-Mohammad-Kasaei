using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SearchWebApplication.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class DocumentController : Controller
    {
        private readonly IQueryEngine _queryEngine;
        private readonly IQueryBuilder _queryBuilder;
        private readonly IApiQueryParser _apiQueryParser;
        private readonly IConnectionChecker _connectionChecker;

        public DocumentController(IQueryEngine queryEngine, IQueryBuilder queryBuilder, IApiQueryParser apiQueryParser,
            IConnectionChecker connectionChecker)
        {
            _queryEngine = queryEngine;
            _queryBuilder = queryBuilder;
            _apiQueryParser = apiQueryParser;
            _connectionChecker = connectionChecker;
        }

        [HttpGet("{index}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Query([FromRoute] string index, [FromQuery] string query)
        {
            _apiQueryParser.SplitWordsToGroups(query, out var notWords, out var orWords, out var andWords);
            var elasticQuery = _queryBuilder.WordsToNestQueryObject(index, andWords, orWords, notWords);
            var result = _queryEngine
                .GetDocsAdvancedQuery(elasticQuery)
                .ToList();
            if (result.Any())
                return Json(result.ToList());
            return NotFound("No document found based on your query");
        }

        [HttpPost("{index}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Index([FromRoute] string index, [FromBody] Document document)
        {
            try
            {
                _queryEngine.Index(index, document);
                return StatusCode(201, "Document indexed");
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Internal server exception {e.Message}");
            }
        }

        [HttpGet]
        [Route("ping")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Ping()
        {
            return _connectionChecker.CheckConnection()
                ? Ok()
                : StatusCode(500, "Elastic server is unavailable");
        }
    }
}