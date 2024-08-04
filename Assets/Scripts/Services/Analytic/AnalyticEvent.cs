using System;

namespace Services.Analytic
{
    [Serializable]
    public struct AnalyticEvent
    {
        public readonly string type;
        public readonly string data;

        public AnalyticEvent(string data, string type)
        {
            this.data = data;
            this.type = type;
        }
    }
}