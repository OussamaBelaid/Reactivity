namespace Application.Activities
{
    using Application.DTO;
    using Application.Interfaces;
    using AutoMapper;
    using Domain;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using Persistence;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="List" />
    /// </summary>
    public class List
    {
        public class ActivityEnvelope
        {
            public List<ActivityDto> Activities { get; set; }
            public int ActivityCount { get; set; }
        }
        /// <summary>
        /// Defines the <see cref="Query" />
        /// </summary>
        public class Query : IRequest<ActivityEnvelope>
        {
            public int? Limit { get; set; }
            public int? Offset { get; set; }
            public bool IsGoing { get; set; }
            public bool IsHost { get; set; }
            public DateTime? StartDate { get; set; }
            public Query(int? limit,int? offset,bool isGoing,bool isHost,DateTime? startDate)
            {
                Limit = limit;
                Offset = offset;
                IsGoing = isGoing;
                IsHost = isHost;
                StartDate = startDate ?? DateTime.Now;
            }
        }

        /// <summary>
        /// Defines the <see cref="Handler" />
        /// </summary>
        public class Handler : IRequestHandler<Query,ActivityEnvelope>
        {
            /// <summary>
            /// Defines the _context
            /// </summary>
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            private readonly IUserAccessor _userAccessor;



            /// <summary>
            /// Initializes a new instance of the <see cref="Handler"/> class.
            /// </summary>
            /// <param name="context">The context<see cref="DataContext"/></param>
            /// <param name="logger">The logger<see cref="ILogger{List}"/></param>
            public Handler(DataContext context,IMapper mapper,IUserAccessor userAccessor)
            {
               _context = context;
                _mapper = mapper;
                _userAccessor = userAccessor;
            }

            /// <summary>
            /// The Handle
            /// </summary>
            /// <param name="request">The request<see cref="Query"/></param>
            /// <param name="cancellationToken">The cancellationToken<see cref="CancellationToken"/></param>
            /// <returns>The <see cref="Task{List{Activity}}"/></returns>
            public async Task<ActivityEnvelope> Handle(Query request, CancellationToken cancellationToken)
            {

                //try
                //{
                //    for (var i=0;i<10;i++)
                //    {
                //        cancellationToken.ThrowIfCancellationRequested();
                //        await Task.Delay(1000, cancellationToken);
                //        _logger.LogInformation($"Task {i} has completed");

                //    }
                //}
                //catch (Exception ex) when (ex is TaskCanceledException)
                //{

                //    _logger.LogInformation("Task was canceled");
                //}
                var queryable = _context.Activities
                    .Where(x=> x.Date >= request.StartDate)
                    .OrderBy(x=> x.Date)
                    .AsQueryable();

                if(request.IsGoing && !request.IsHost)
                {
                    queryable = queryable.Where(x => x.UserActivities.Any(a => a.AppUser.UserName == _userAccessor.GetCurrentUsername() && a.IsHost));
                }
                var activities = await queryable.Skip(request.Offset ?? 0).Take(request.Limit ?? 3).ToListAsync();

                return new ActivityEnvelope
                {
                    Activities = _mapper.Map<List<Activity>, List<ActivityDto>>(activities),
                    ActivityCount = queryable.Count()
                };
                
            }
        }
    }
}
