using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO
{
    public class ActivityDto
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
        public DateTime Date { get; set; }

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
        [JsonProperty("attendees")] 
        public ICollection<AttendeeDto> UserActivities { get; set; }
    }
}
