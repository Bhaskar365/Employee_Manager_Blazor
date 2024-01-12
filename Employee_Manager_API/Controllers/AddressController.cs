using Employee_Manager_API.Interfaces;
using Employee_Manager_Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Employee_Manager_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressRepository _addressRespository;
        public AddressController(IAddressRepository addressRespository)
        {
            _addressRespository = addressRespository;
        }

        [HttpGet("GetAllAddress")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Address>))]
        public IActionResult GetAllAddress()
        {
            var address = _addressRespository.GetAllAddress();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (address == null)
                return NotFound();

            return Ok(address);
        }

        [HttpGet("{addressId}")]
        [ProducesResponseType(200, Type = typeof(Address))]
        [ProducesResponseType(400)]
        public IActionResult GetAddress(int addressId)
        {
            if (!_addressRespository.AddressExists(addressId))
                return NotFound();

            var address = _addressRespository.GetAddress(addressId);

            if (address == null)
                return NotFound();

            return Ok(address);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateDepartment([FromBody] Address address)
        {
            if (address == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_addressRespository.CreateAddress(address))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully created");
        }

        [HttpPut("{addId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateDepartment(int addId, [FromBody] Address add)
        {
            if (add == null)
                return BadRequest(ModelState);

            if (addId != add.Id)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest();

            if (!_addressRespository.UpdateAddress(add))
            {
                ModelState.AddModelError("", "Something went wrong while updating department");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{addId}")]
        public IActionResult DeleteDepartment(int addId)
        {
            var addressToDelete = _addressRespository.GetAddress(addId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_addressRespository.DeleteAddress(addressToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting departments");
            }
            return NoContent();
        }
    }
}