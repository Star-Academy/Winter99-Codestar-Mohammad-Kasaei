using System;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;

namespace SearchWebApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DocumentController : Controller
    {
        private const string SplitRegex = @"[,]+";
        private const string OrRegex = @"[+].+";
        private const string NotRegex = @"[-].+";
        private static readonly Func<string, string> WordsTrimmer = w => w.Trim('-').Trim('+');

        private readonly IQueryEngine _queryEngine;
        private readonly IQueryBuilder _queryBuilder;

        public DocumentController(IQueryEngine queryEngine, IQueryBuilder queryBuilder)
        {
            _queryEngine = queryEngine;
            _queryBuilder = queryBuilder;
        }

        [HttpGet("{index}")]
        public IActionResult Query([FromRoute] string index, [FromQuery] string query)
        {
            SplitWordsToGroups(query, out var notWords, out var orWords, out var andWords);
            var elasticQuery = _queryBuilder.WordsToNestQueryObject(index, andWords, orWords, notWords);
            var result = _queryEngine
                .GetDocsAdvancedQuery(elasticQuery)
                .ToList();
            if (result.Any())
                return Json(result.ToList());
            return NotFound("No document found based on your query");
        }

        [HttpPost("{index}")]
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

        private static void SplitWordsToGroups(string queryString,
            out string[] notWords,
            out string[] orWords,
            out string[] andWords
        )
        {
            var allWords = Regex.Split(queryString, SplitRegex);
            var orTerms = allWords.Where(w => Regex.IsMatch(w, OrRegex)).ToList();
            var notTerms = allWords.Where(w => Regex.IsMatch(w, NotRegex)).ToList();
            var andTerms = allWords.Except(orTerms).Except(notTerms).ToList();

            notWords = notTerms.Select(WordsTrimmer).ToArray();
            orWords = orTerms.Select(WordsTrimmer).ToArray();
            andWords = andTerms.ToArray();
        }

        [HttpGet]
        [Route("ping")]
        public IActionResult Ping()
        {
            return _queryEngine.CheckConnection()
                ? Ok()
                : StatusCode(500, "Elastic server is unavailable");
        }
    }
}