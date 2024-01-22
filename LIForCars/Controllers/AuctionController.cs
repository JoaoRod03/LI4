using LIForCars.Data.Interfaces;
using LIForCars.Models;
using Microsoft.AspNetCore.Mvc;

namespace LIForCars.Controllers
{
    [ApiController]
    [Route("api/Auction")]
    public class AuctionController : ControllerBase
    {
        private readonly IAuctionRepository _repository;

        public AuctionController(IAuctionRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Auction>> GetAll()
        {
            return Ok(_repository.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<Auction> GetById(int id)
        {
            var auction = _repository.GetById(id);
            if (auction == null)
            {
                return NotFound();
            }
            return Ok(auction);
        }

        [HttpPost("create")]
        public ActionResult Create([FromForm] Auction auction)
        {
            //if (_repository.CarIdExists(auction.CarId)) return BadRequest("CarId already exists");
            
            _repository.Create(auction);
            return CreatedAtAction(nameof(GetById), new { id = auction.Id }, auction);
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, [FromBody] Auction auction)
        {
            if (id != auction.Id)
            {
                return BadRequest();
            }

            _repository.Update(auction);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _repository.Delete(id);
            return NoContent();
        }
    }
}
