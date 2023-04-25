using ApiDemo1.Data;
using ApiDemo1.Models.DTO;
using DemoAPI;
using Microsoft.AspNetCore.Mvc;
namespace controller{

    [ApiController]
    [Route("/apiVilla")]
    public class VillaController : ControllerBase{
        private object villaDTO;

        [HttpGet]
        [Route("")]
        public ActionResult<VillaDTO> GetVillas()
        {
            return Ok (VillaStore.villaList);
        }

         [HttpGet("{id:int}",Name="GetVilla")]
         [ProducesResponseType(StatusCodes.Status200OK)]
         [ProducesResponseType(StatusCodes.Status400BadRequest)]
         [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult <VillaDTO>GetVillas( int id )
        {
          if (id==0){
            return BadRequest();
          }

          var  villa = VillaStore.villaList.FirstOrDefault(u=>u.Id==id);
           if (villa == null){
            return NotFound();
           }
            return  Ok (villa);
        }
         
          [HttpPost]

            public ActionResult<VillaDTO> CreateVilla([FromBody]VillaDTO villaDTO)
          {
            if(villaDTO==null)
            {
                return BadRequest(villaDTO);
            }
            if (villaDTO.Id>0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            villaDTO.Id= VillaStore.villaList.OrderByDescending(u=>u.Id).FirstOrDefault().Id+1;
            VillaStore.villaList.Add(villaDTO);
            
            return CreatedAtRoute("GetVilla",new {id =villaDTO.Id},villaDTO);

          }
       
       
       
        }

    }
