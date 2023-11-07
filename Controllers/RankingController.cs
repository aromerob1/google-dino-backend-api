using google_dino_backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace google_dino_backend.Controllers
{
    [ApiController]
    [Route("ranking")]
    public class RankingController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public RankingController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("top5")]
        public async Task<ActionResult<IEnumerable<Score>>> GetTop5()
        {
            var topScores = await _context.Scores
                .OrderByDescending(s => s.Value)
                .Take(5)
                .ToListAsync();

            return Ok(topScores);
        }

        [HttpPost]
        [Route("score")]
        public async Task<ActionResult<Score>> CreateScore(Score newScore)
        {
            _context.Scores.Add(newScore);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetScore), new { id = newScore.Id }, newScore);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Score>> GetScore(int id)
        {
            var score = await _context.Scores.FindAsync(id);

            if (score == null)
            {
                return NotFound();
            }

            return score;
        }

    }
}
