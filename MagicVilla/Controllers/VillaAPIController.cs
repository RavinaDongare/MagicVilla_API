using MagicVilla.Data;
using MagicVilla.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaAPIController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IEnumerable<VillaDTO> GetVillas()
        {

            return VillaStore.VillaList;

        }
        [HttpGet("Id:int", Name = "GetVillas")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetVillas(int Id)
        {

            if (Id == 0)
            {
                return BadRequest();
            }
            var Villa = VillaStore.VillaList.FirstOrDefault(data => data.Id == Id);
            if (Villa == null)
            {
                return NotFound();
            }
            return Ok(Villa);
        }

        [HttpPost]
        public ActionResult<VillaDTO> CreateVillas([FromBody] VillaDTO villaDTO)
        {
            if (!ModelState.IsValid) {
            
            return BadRequest(villaDTO);
            }
            if (villaDTO == null)
            {
                return BadRequest(villaDTO);
            }
            if (villaDTO.Id > 0)
            {
                return BadRequest(villaDTO);
            }
            if(VillaStore.VillaList.FirstOrDefault(data=>data.Name.ToLower() == villaDTO.Name.ToLower())!=null)
            {
                ModelState.AddModelError("CustomError","Vill Already Exist");
                return BadRequest(ModelState);    
            }

             villaDTO.Id = VillaStore.VillaList.OrderByDescending(data => data.Id).FirstOrDefault().Id + 1;
            VillaStore.VillaList.Add(villaDTO);
            return CreatedAtRoute("GetVillas",new { Id=villaDTO.Id }, villaDTO);

        }
        [HttpDelete("Id:int")]
        public ActionResult<VillaDTO> DalateVillas( int IDs )
        {
           int ID= IDs;

           
            if (ID == 0)
            {
                return BadRequest(ID);
            }
            if (ID < 0)
            {
                return BadRequest(ID);
            }

             var Villa = VillaStore.VillaList.FirstOrDefault(data => data.Id == ID);
            if (Villa == null) { return BadRequest(ID); }
           VillaStore.VillaList.Remove(Villa);               
                
              
            return CreatedAtRoute("GetVillas", ID);


        }



        [HttpPut("Id:int", Name = "UpdateVillas")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult UpdateVillas(int id,[FromBody] VillaDTO villaDTO)
        {

            if (villaDTO.Id == 0 || id !=villaDTO.Id)
            {
                return BadRequest();
            }
            var Villa = VillaStore.VillaList.FirstOrDefault(data => data.Id == villaDTO.Id);
            if (Villa == null)
            {
                return NotFound();
            }
            Villa.Name = villaDTO.Name;
         
            return Ok(Villa);
        }


    }
    }
