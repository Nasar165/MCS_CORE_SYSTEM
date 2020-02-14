using System;
using System.Collections.Generic;
using System.IO;
using api.Middleware.Interface;
using Components.Interface;
using Components.Logger.Interface;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace api.Middleware
{
    public class OWASPHeaderPolicy : IHeaderPolicy
    {
        private IFileWriter FileWriter { get; }
        private IHeaderDictionary Header { get; set; }
        private ILogger EventLogger { get; }
        public OWASPHeaderPolicy(IFileWriter fileWriter, ILogger eventLogger)
        {
            FileWriter = fileWriter;
            EventLogger = eventLogger;
        }
        private void AddSecurityToHeader(string key, string value)
        {
            Header.Add(key, value);
        }

        private void RemoveVaulnerabiltiesFromHeader(string key, string value)
        {
            Header.Remove("Server");
        }

        private IReadOnlyCollection<Policy> GetPolicies()
        {
            var filePath = $"{Directory.GetCurrentDirectory()}/Middleware/owasp.json";
            var textFile = FileWriter.ReadTextFromFile(filePath);
            return JsonConvert.DeserializeObject<List<Policy>>(textFile);
        }

        public void AddPolicyToHeader(IHeaderDictionary header)
        {
            try
            {
                Header = header;
                foreach (var policy in GetPolicies())
                {
                    if (policy.AddToHeader)
                        AddSecurityToHeader(policy.Key, policy.Value);
                }
            }
            catch (Exception error)
            {
                EventLogger.LogEvent(error);
            }
        }
    }
}