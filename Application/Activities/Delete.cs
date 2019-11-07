namespace Application.Activities
{
    using MediatR;
    using Persistence;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="Delete" />
    /// </summary>
    public class Delete
    {
        /// <summary>
        /// Defines the <see cref="Command" />
        /// </summary>
        public class Command : IRequest
        {
            /// <summary>
            /// Gets or sets the Id
            /// </summary>
            public Guid Id { get; set; }
        }

        /// <summary>
        /// Defines the <see cref="Handler" />
        /// </summary>
        public class Handler : IRequestHandler<Command>
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
            /// <param name="request">The request<see cref="Command"/></param>
            /// <param name="cancellationToken">The cancellationToken<see cref="CancellationToken"/></param>
            /// <returns>The <see cref="Task{Unit}"/></returns>
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var activity = await _context.Activities.FindAsync(request.Id);
                if (activity == null)
                {
                    throw new Exception("Could not find activity");
                }
                _context.Remove(activity);
                var succes = await _context.SaveChangesAsync() > 0;

                if (succes) return Unit.Value;

                throw new Exception("Probleme delete");
            }
        }
    }
}
