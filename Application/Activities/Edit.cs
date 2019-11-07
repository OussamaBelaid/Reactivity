namespace Application.Activities
{
    using MediatR;
    using Persistence;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="Edit" />
    /// </summary>
    public class Edit
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

            /// <summary>
            /// Gets or sets the Title
            /// </summary>
            public string Title { get; set; }

            /// <summary>
            /// Gets or sets the Category
            /// </summary>
            public string Category { get; set; }

            /// <summary>
            /// Gets or sets the Date
            /// </summary>
            public DateTime? Date { get; set; }

            /// <summary>
            /// Gets or sets the Description
            /// </summary>
            public string Description { get; set; }

            /// <summary>
            /// Gets or sets the City
            /// </summary>
            public string City { get; set; }

            /// <summary>
            /// Gets or sets the Venue
            /// </summary>
            public string Venue { get; set; }
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

                activity.Title = request.Title ?? activity.Title;
                activity.Description = request.Description ?? activity.Description;
                activity.Category = request.Category ?? activity.Category;
                activity.Date = request.Date ?? activity.Date;
                activity.City = request.City ?? activity.City;
                activity.Venue = request.Venue ?? activity.Venue;

                var succes = await _context.SaveChangesAsync() > 0;

                if (succes) return Unit.Value;

                throw new Exception("Probleme update");
            }
        }
    }
}
