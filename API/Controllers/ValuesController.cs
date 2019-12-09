namespace API.Controllers
{
    using Domain;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Persistence;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="ValuesController" />
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        /// <summary>
        /// Defines the _context
        /// </summary>
        private readonly DataContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValuesController"/> class.
        /// </summary>
        /// <param name="context">The context<see cref="DataContext"/></param>
        public ValuesController(DataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// The Get
        /// </summary>
        /// <returns>The <see cref="Task{ActionResult{IEnumerable{Value}}}"/></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Value>>> Get()
        {
            var values = await _context.Values.ToListAsync();
            return Ok(values);
        }

        /// <summary>
        /// The Get
        /// </summary>
        /// <param name="id">The id<see cref="int"/></param>
        /// <returns>The <see cref="Task{ActionResult{string}}"/></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<string>> Get(int id)
        {
            var value = await _context.Values.FindAsync(id);
            return Ok(value);
        }

        /// <summary>
        /// The Post
        /// </summary>
        /// <param name="value">The value<see cref="string"/></param>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        /// <summary>
        /// The Put
        /// </summary>
        /// <param name="id">The id<see cref="int"/></param>
        /// <param name="value">The value<see cref="string"/></param>
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        /// <summary>
        /// The Delete
        /// </summary>
        /// <param name="id">The id<see cref="int"/></param>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
      
    
}
}
