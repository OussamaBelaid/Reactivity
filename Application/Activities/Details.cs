﻿namespace Application.Activities
{
    using Domain;
    using MediatR;
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
        public class Query : IRequest<Activity>
        {
            /// <summary>
            /// Gets or sets the Id
            /// </summary>
            public Guid Id { get; set; }
        }

        /// <summary>
        /// Defines the <see cref="Handler" />
        /// </summary>
        public class Handler : IRequestHandler<Query, Activity>
        {
            /// <summary>
            /// Defines the _context
            /// </summary>
            private readonly DataContext _context;

            /// <summary>
            /// Initializes a new instance of the <see cref="Handler"/> class.
            /// </summary>
            /// <param name="context">The context<see cref="DataContext"/></param>
            public Handler(DataContext context)
            {
                this._context = context;
            }

            /// <summary>
            /// The Handle
            /// </summary>
            /// <param name="request">The request<see cref="Query"/></param>
            /// <param name="cancellationToken">The cancellationToken<see cref="CancellationToken"/></param>
            /// <returns>The <see cref="Task{Activity}"/></returns>
            public async Task<Activity> Handle(Query request, CancellationToken cancellationToken)
            {
                var acitivity = await _context.Activities.FindAsync(request.Id);

                return acitivity;
            }
        }
    }
}
