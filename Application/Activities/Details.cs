namespace Application.Activities
{
    using Application.DTO;
    using Application.Errors;
    using AutoMapper;
    using Domain;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Persistence;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="Details" />
    /// </summary>
    public class Details
    {
        /// <summary>
        /// Defines the <see cref="Query" />
        /// </summary>
        public class Query : IRequest<ActivityDto>
        {
            /// <summary>
            /// Gets or sets the Id
            /// </summary>
            public Guid Id { get; set; }
        }

        /// <summary>
        /// Defines the <see cref="Handler" />
        /// </summary>
        public class Handler : IRequestHandler<Query, ActivityDto>
        {
            /// <summary>
            /// Defines the _context
            /// </summary>
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            /// <summary>
            /// Initializes a new instance of the <see cref="Handler"/> class.
            /// </summary>
            /// <param name="context">The context<see cref="DataContext"/></param>
            public Handler(DataContext context,IMapper mapper)
            {
                _context = context;
               _mapper = mapper;
            }

            /// <summary>
            /// The Handle
            /// </summary>
            /// <param name="request">The request<see cref="Query"/></param>
            /// <param name="cancellationToken">The cancellationToken<see cref="CancellationToken"/></param>
            /// <returns>The <see cref="Task{Activity}"/></returns>
            public async Task<ActivityDto> Handle(Query request, CancellationToken cancellationToken)
            {
               
                var activity = await _context.Activities.FindAsync(request.Id);
                if (activity == null)
                {
                    throw new RestException(System.Net.HttpStatusCode.NotFound, new { activity = "Not found" });
                }
                var activityReturns = _mapper.Map<Activity, ActivityDto>(activity);
                return activityReturns;
            }

           
        }
    }
}
