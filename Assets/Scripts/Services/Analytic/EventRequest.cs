using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Services.Analytic
{
    [Serializable]
    public class EventRequest
    {
        [JsonProperty("events")] public List<AnalyticEvent> QueueEvents { get; private set; }

        public EventRequest()
        {
            QueueEvents = new List<AnalyticEvent>();
        }

        public void AddEvent(AnalyticEvent analyticEvent)
        {
            QueueEvents.Add(analyticEvent);
        }

        public void ResetEvents()
        {
            QueueEvents = new List<AnalyticEvent>();
        }

        public bool IsEventQueueEmpty()
        {
            return QueueEvents.Count == 0;
        }
        
    }
}