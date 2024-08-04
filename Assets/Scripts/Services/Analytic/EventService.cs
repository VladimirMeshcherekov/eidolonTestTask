using System;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Services.JsonSave;
using UnityEngine;
using Utilities;

namespace Services.Analytic
{
    public class EventService : MonoBehaviour
    {
        [SerializeField] private int requestCooldown;
        [SerializeField] private string analyticServerUrl;

        private JsonSaveService _saveService;
        private string _saveFilePath;
        private EventRequest _eventRequest;
        private AsyncTimer _sendEventTimer;
        private readonly HttpClient _client = new();

        private void Start()
        {
            _sendEventTimer = new AsyncTimer(TrySendRequest, requestCooldown, true);
            _saveFilePath = Application.persistentDataPath + "/AnalyticEvents.json";
            _saveService = new JsonSaveService(_saveFilePath);

            if (_saveService.TryToLoad(out _eventRequest) == false) _eventRequest = new EventRequest();
        }

        public void TrackEvent(string type, string data)
        {
            if (!_sendEventTimer.IsTimerTicking) _sendEventTimer.StartTimer();

            _eventRequest.AddEvent(new AnalyticEvent(type, data));
            _saveService.Save(JsonConvert.SerializeObject(_eventRequest));
        }

        private async void TrySendRequest()
        {
            if (_eventRequest.QueueEvents.Capacity == 0) return;

            var content = new StringContent(JsonConvert.SerializeObject(_eventRequest), Encoding.UTF8,
                "application/json");

            try
            {
                var response = await _client.PostAsync(analyticServerUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    _eventRequest.ResetEvents();
                    _sendEventTimer.StopTimer();
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to send analytics data: {ex.Message}");
            }
        }
    }
}