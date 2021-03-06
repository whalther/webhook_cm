﻿using System.Collections.Generic;
namespace Domain.DTOs
{
    public class Texto
    {
        public List<string> texto { get; set; }
    }

    public class Intent
    {
        public string name { get; set; }
        public string displayName { get; set; }
    }

    public class QueryResult
    {
        public string queryText { get; set; }
        public string action { get; set; }
        public Dictionary<string, string> parameters { get; set; }
        public bool allRequiredParamsPresent { get; set; }
        public Intent intent { get; set; }
    }

    public class RequestDto
    {
        public string responseId { get; set; }
        public QueryResult queryResult { get; set; }
        public string session { get; set; }
    }
}
