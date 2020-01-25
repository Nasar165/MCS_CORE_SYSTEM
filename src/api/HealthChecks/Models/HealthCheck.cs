using System;

namespace api.HealthChecks.Models
{
    public class HealthCheck
    {
        public string Status { get; set; }
        public string Component { get; set; }
        public string Description { get; set; }
        public TimeSpan Duration { get; set; }
    }
}