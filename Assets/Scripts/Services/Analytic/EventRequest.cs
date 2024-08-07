using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Services.Analytic
{
    [Serializable]
    public class EventRequest
    {
        [JsonProperty("events")] private List<AnalyticEvent> _queueEvents;
        private List<List<AnalyticEvent>> _listQueueEvents;

        public EventRequest()
        {
            _queueEvents = new List<AnalyticEvent>();
            _listQueueEvents = new List<List<AnalyticEvent>>();
            _listQueueEvents.Add(_queueEvents);
        }

        /// <summary>
        ///     Add a new event to event list
        /// </summary>
        /// <param name="analyticEvent"></param>
        public void AddEvent(AnalyticEvent analyticEvent)
        {
            _listQueueEvents[_listQueueEvents.Count].Add(analyticEvent);
        }

        /// <summary>
        ///     Remove all events in first queue
        /// </summary>
        public void ResetEvents()
        {
            if (_listQueueEvents.Count >= 2)
            {
                _listQueueEvents.RemoveAt(0);
            }
            else
            {
                _queueEvents = new List<AnalyticEvent>();
                _listQueueEvents = new List<List<AnalyticEvent>> { _queueEvents };
            }
        }

        /// <summary>
        ///     Create a new queue without remove previous queue
        /// </summary>
        public void CreateNewQueue()
        {
            _listQueueEvents.Add(new List<AnalyticEvent>());
        }

        /// <summary>
        ///     Return availability of last event queue
        /// </summary>
        /// <returns></returns>
        public bool IsEventQueueEmpty()
        {
            return _listQueueEvents[_listQueueEvents.Count].Count == 0;
        }

        /// <summary>
        ///     Group last and previous queue into one
        /// </summary>
        public void GroupPreviousEvents()
        {
            if (_listQueueEvents.Count < 2) return;
            _listQueueEvents[^1].AddRange(_listQueueEvents[_listQueueEvents.Count]);
            _listQueueEvents.RemoveAt(_listQueueEvents.Count);
        }
    }
}