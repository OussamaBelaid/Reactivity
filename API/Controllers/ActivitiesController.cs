﻿namespace API.Controllers
{
    using Application.Activities;
    using Application.DTO;
    using Domain;
    using MediatR;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using static Application.Activities.List;

    /// <summary>
    /// Defines the <see cref="ActivitiesController" />
    /// </summary>

    public class ActivitiesController : BaseController
    {
       

        /// <summary>
        /// The List
        /// </summary>
        /// <param name="ct">The ct<see cref="CancellationToken"/></param>
        /// <returns>The <see cref="Task{ActionResult{List{Activity}}}"/></returns>
        [HttpGet]
        public async Task<ActionResult<ActivityEnvelope>> List(int? limit, int? offset,bool isGoing,bool isHost,DateTime? startDate)
        {
            return await Mediator.Send(new List.Query(limit,offset,isGoing,isHost,startDate));
        }

        /// <summary>
        /// The Details
        /// </summary>
        /// <param name="id">The id<see cref="Guid"/></param>
        /// <returns>The <see cref="Task{ActionResult{Activity}}"/></returns>
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<ActivityDto>> Details(Guid id)
        {
            return await Mediator.Send(new Details.Query { Id = id });
        }

        /// <summary>
        /// The Create
        /// </summary>
        /// <param name="command">The command<see cref="Create.Command"/></param>
        /// <returns>The <see cref="Task{ActionResult{Unit}}"/></returns>
        [HttpPost]
        public async Task<ActionResult<Unit>> Create(Create.Command command)
        {
            return await Mediator.Send(command);
        }

        /// <summary>
        /// The Edit
        /// </summary>
        /// <param name="id">The id<see cref="Guid"/></param>
        /// <param name="command">The command<see cref="Edit.Command"/></param>
        /// <returns>The <see cref="Task{ActionResult{Unit}}"/></returns>
        [HttpPut("{id}")]
        [Authorize(Policy ="IsActivityHost")]
        public async Task<ActionResult<Unit>> Edit(Guid id, Edit.Command command)
        {
            command.Id = id;
            return await Mediator.Send(command);
        }

        /// <summary>
        /// The Delete
        /// </summary>
        /// <param name="id">The id<see cref="Guid"/></param>
        /// <returns>The <see cref="Task{ActionResult{Unit}}"/></returns>
        [HttpDelete("{id}")]
        [Authorize(Policy = "IsActivityHost")]
        public async Task<ActionResult<Unit>> Delete(Guid id)
        {
            return await Mediator.Send(new Delete.Command { Id = id });
        }

        [HttpPost("{id}/attend")]
        public async Task<ActionResult<Unit>> Attend (Guid id)
        {
            return await Mediator.Send(new Attend.Command{Id=id});
        }
        [HttpDelete("{id}/attend")]
        public async Task<ActionResult<Unit>> Unattend (Guid id)
        {
            return await Mediator.Send(new Unattend.Command { Id = id });
        }
    }
}
