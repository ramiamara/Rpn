using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RpnApi.Models;
using RpnApi.Services;
using RpnApi.Shared;

namespace RpnApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RpnController : ControllerBase
    {
        private readonly IStackService _stackService;
        private readonly IOperandService _operandService;
        private readonly ICalculateService _calculateService;

        public RpnController(IStackService stackService,
            IOperandService operandService,
            ICalculateService calculateService)
        {
            _stackService = stackService;
            _operandService = operandService;
            _calculateService = calculateService;
        }

        /// <summary>
        /// List all the operand
        /// </summary>
        /// <returns></returns>
        [HttpGet("op")]
        public ActionResult Operands()
        {
            var result = _operandService.Get();
            return JsonContentResult.Success(result);
        }

        /// <summary>
        /// List the available stack
        /// </summary>
        /// <returns></returns>
        [HttpGet("stack")]
        public ActionResult Stacks()
        {
            var result = _stackService.Get();
            return JsonContentResult.Success(result);
        }

        /// <summary>
        /// Get a stack
        /// </summary>
        /// <response code="200">Return new stack</response>
        /// <response code="404"> stack_id not found</response>
        /// <param name="stack_id"></param>  
        [HttpGet("stack/{stack_id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Stack(int stack_id)
        {
            var result = _stackService.Get(stack_id);
            return JsonContentResult.Success(result);
        }

        /// <summary>
        /// Create a new stack
        /// </summary>
        /// <returns></returns>
        [HttpPost("stack")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult AddStack(StackRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(request);
            }

            var result = _stackService.Add(request.stacks);
            return JsonContentResult.Success(result);
        }

        /// <summary>
        /// Push new value to stack
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Return new stack</response>
        /// <response code="400"> </response>
        /// <response code="404"> stack_id not found</response>
        /// <param name="stack_id"></param> 
        /// <param name="entry"></param>
        [HttpPost("stack/{stack_id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult AddEntry(int stack_id, [FromBody] Entry entry)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(entry);
            }
            var result = _stackService.AddEntry(stack_id, entry);
            return result == null ? NotFound("stack_id not found") : JsonContentResult.Success(result);
        }

        /// <summary>
        /// Push new value to stack
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Return new stack</response>
        /// <response code="400"> </response>
        /// <response code="404"> stack_id not found</response>
        /// <param name="op"></param> 
        /// <param name="stack_id"></param>
        [HttpPost("op/{op}/stack/{stack_id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult Calculate(string op, int stack_id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(op);
            }
            var result = _calculateService.Calculate(stack_id, op);
            return result == null ? NotFound("stack_id not found") : JsonContentResult.Success(result);
        }

        /// <summary>
        /// Delete a stack
        /// </summary>
        /// <response code="204">Stack deleted -> return no element</response>
        /// <response code="400"> </response>
        /// <param name="stack_id"></param>        
        [HttpDelete("stack/{stack_id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int stack_id)
        {
            var deleted = _stackService.Delete(stack_id);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
