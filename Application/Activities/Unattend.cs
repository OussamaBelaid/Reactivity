﻿using Application.Errors;
using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Activities
{
    public class Unattend
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;
            private readonly IUserAccessor _accessor;

            public Handler(DataContext context, IUserAccessor accessor)
            {
                _context = context;
                _accessor = accessor;
            }
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var activity = await _context.Activities.FindAsync(request.Id);

                if (activity == null) throw new RestException(System.Net.HttpStatusCode.NotFound, new { Activity = "Could not find activity" });

                var user = await _context.Users.SingleOrDefaultAsync(x => x.UserName == _accessor.GetCurrentUsername());

                var attendance = await _context.UserActivities.SingleOrDefaultAsync(x => x.ActivityId == activity.Id && x.AppUserId == user.Id);

                if(attendance == null)
                {
                    return Unit.Value;
                }
                 if(attendance.IsHost)
                {
                    throw new RestException(System.Net.HttpStatusCode.BadRequest, new { Attendance = "You connot remove yourself as host" });
                   
                }
                _context.UserActivities.Remove(attendance);
                var success = await _context.SaveChangesAsync() > 0;

                if (success) return Unit.Value;
                throw new Exception("Probleme saving changes");



            }

            
        }
    }
}
