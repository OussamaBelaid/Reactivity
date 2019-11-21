namespace Application.Activities
{
    using Application.DTO;
    using AutoMapper;
    using Domain;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using Persistence;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="List" />
    /// </summary>
    public class List
    {
        /// <summary>
        /// Defines the <see cref="Query" />
        /// </summary>
        public class Query : IRequest<List<ActivityDto>>
        {
        }

        /// <summary>
        /// Defines the <see cref="Handler" />
        /// </summary>
        public class Handler : IRequestHandler<Query, List<ActivityDto>>
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
            /// <param name="logger">The logger<see cref="ILogger{List}"/></param>
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
            /// <returns>The <see cref="Task{List{Activity}}"/></returns>
            public async Task<List<ActivityDto>> Handle(Query request, CancellationToken cancellationToken)
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
                var activities = await _context.Activities.ToListAsync(cancellationToken);
                var activitiesResult = _mapper.Map<List<Activity>, List<ActivityDto>>(activities);
                return activitiesResult;
            }
        }
    }
}
