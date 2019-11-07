namespace API.Controllers
{
    using Application.Activities;
    using Domain;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="ActivitiesController" />
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ActivitiesController : ControllerBase
    {
        /// <summary>
        /// Defines the _mediator
        /// </summary>
        private readonly IMediator _mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="ActivitiesController"/> class.
        /// </summary>
        /// <param name="mediator">The mediator<see cref="IMediator"/></param>
        public ActivitiesController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        /// <summary>
        /// The List
        /// </summary>
        /// <param name="ct">The ct<see cref="CancellationToken"/></param>
        /// <returns>The <see cref="Task{ActionResult{List{Activity}}}"/></returns>
        [HttpGet]
        public async Task<ActionResult<List<Activity>>> List(CancellationToken ct)
        {
            return await _mediator.Send(new List.Query(), ct);
        }

        /// <summary>
        /// The Details
        /// </summary>
        /// <param name="id">The id<see cref="Guid"/></param>
        /// <returns>The <see cref="Task{ActionResult{Activity}}"/></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Activity>> Details(Guid id)
        {
            return await _mediator.Send(new Details.Query { Id = id });
        }

        /// <summary>
        /// The Create
        /// </summary>
        /// <param name="command">The command<see cref="Create.Command"/></param>
        /// <returns>The <see cref="Task{ActionResult{Unit}}"/></returns>
        [HttpPost]
        public async Task<ActionResult<Unit>> Create(Create.Command command)
        {
            return await _mediator.Send(command);
        }

        /// <summary>
        /// The Edit
        /// </summary>
        /// <param name="id">The id<see cref="Guid"/></param>
        /// <param name="command">The command<see cref="Edit.Command"/></param>
        /// <returns>The <see cref="Task{ActionResult{Unit}}"/></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> Edit(Guid id, Edit.Command command)
        {
            command.Id = id;
            return await _mediator.Send(command);
        }

        /// <summary>
        /// The Delete
        /// </summary>
        /// <param name="id">The id<see cref="Guid"/></param>
        /// <returns>The <see cref="Task{ActionResult{Unit}}"/></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Delete(Guid id)
        {
            return await _mediator.Send(new Delete.Command { Id = id });
        }
    }
}
