using Newtonsoft.Json;
using starbase_nexus_api.Constants;
using starbase_nexus_api.Models.Yolol.YololProject.FetchConfig;
using starbase_nexus_api.Models.Yolol.YololProject.Fetched;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace starbase_nexus_api.Services.Yolol
{
    public class FetchConfigService : IFetchConfigService
    {
        public async Task<FetchConfigValidationResult> ValidateFetchConfig(string fetchConfigUri)
        {
            FetchConfigValidationResult result = new FetchConfigValidationResult();

            string responseBody;
            try
            {
                WebRequest request = WebRequest.Create(fetchConfigUri);
                WebResponse response = await request.GetResponseAsync();
                using (Stream dataStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(dataStream);
                    responseBody = reader.ReadToEnd();
                }
                response.Close();

            }
            catch (WebException ex)
            {
                var exResponse = (HttpWebResponse)ex.Response;
                AddResultError(result, "FetchConfigUri", $"{(int)exResponse.StatusCode} Loading the fetch config failed: {ex.Message}");
                return result;
            }

            if (responseBody.Length > InputSizes.MULTILINE_TEXT_MAX_LENGTH)
            {
                AddResultError(result, "FetchConfig", $"File too big: {responseBody.Length}/{InputSizes.MULTILINE_TEXT_MAX_LENGTH} bytes.");
                return result;
            }

            FetchConfig fetchConfig;

            try
            {
                fetchConfig = JsonConvert.DeserializeObject<FetchConfig>(responseBody);
                result.FetchConfig = fetchConfig;
            }
            catch (Exception jsonEx)
            {
                AddResultError(result, "FetchConfigUri", jsonEx.Message);
                return result;
            }

            if (fetchConfig.Docs == null)
            {
                AddResultError(result, "Docs", "Docs is empty");
            }
            else
            {
                await TestUri(result, fetchConfig.Docs.AbsoluteUri, "docs");
            }

            if (fetchConfig.Scripts.Count == 0)
            {
                AddResultError(result, "Scripts", "Scripts is empty");
            }
            else
            {
                foreach(FetchConfigScript script in fetchConfig.Scripts)
                {
                    await TestUri(result, script.Uri.AbsoluteUri, script.Uri.AbsoluteUri);

                    if (script.Name != null)
                    {
                        if (script.Name.Length > InputSizes.SCRIPT_NAME_MAX_LENGTH)
                        {
                            AddResultError(result, script.Uri.AbsoluteUri, $"Name field too long: {script.Name.Length}/{InputSizes.SCRIPT_NAME_MAX_LENGTH} chars.");
                        }
                    }
                }
            }

            return result;
        }

        private async Task TestUri(FetchConfigValidationResult result, string uri, string prefix)
        {
            try
            {
                new Uri(uri, UriKind.Absolute);
            }
            catch (Exception ex) {
                AddResultError(result, prefix, ex.Message);
                return;
            }

            try
            {
                WebRequest request = WebRequest.Create(uri);
                WebResponse response = await request.GetResponseAsync();
                string responseBody;
                using (Stream dataStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(dataStream);
                    responseBody = reader.ReadToEnd();
                    Console.WriteLine(responseBody);
                }
                response.Close();
                if (responseBody == null || responseBody.Length == 0 )
                {
                    AddResultError(result, prefix, "File empty");
                    return;
                }
                else if (responseBody.Length > InputSizes.MULTILINE_TEXT_MAX_LENGTH)
                {
                    AddResultError(result, uri, $"File too big: {responseBody.Length}/{InputSizes.MULTILINE_TEXT_MAX_LENGTH} bytes.");
                }
                AddResultSuccess(result, prefix, $"{responseBody.Length} bytes");
            }
            catch (WebException ex)
            {
                var exResponse = (HttpWebResponse)ex.Response;
                AddResultError(result, prefix, $"{(int)exResponse.StatusCode} {exResponse.StatusDescription}");
                exResponse.Close();
            }
        }

        public async Task<FetchedYololProject> LoadProjectByFetchConfig(FetchConfig fetchConfig)
        {
            FetchedYololProject project = new FetchedYololProject();

            project.Documentation = await GetExternalFileContents(fetchConfig.Docs);

            foreach(var script in fetchConfig.Scripts)
            {
                project.Scripts.Add(new FetchedYololScript
                {
                    Name = script.Name,
                    Code = await GetExternalFileContents(script.Uri)
                });
            }

            return project;
        }

        private void AddResultSuccess(FetchConfigValidationResult result, string key, string message)
        {
            if (!result.Successes.ContainsKey(key))
            {
                result.Successes[key] = new System.Collections.Generic.List<string>();
            }

            result.Successes[key].Add(message);
        }

        private void AddResultError(FetchConfigValidationResult result, string key, string message)
        {
            if (!result.Errors.ContainsKey(key))
            {
                result.Errors[key] = new System.Collections.Generic.List<string>();
            }

            result.Errors[key].Add(message);
        }

        private async Task<string> GetExternalFileContents(Uri uri)
        {
            WebRequest request = WebRequest.Create(uri);
            WebResponse response = await request.GetResponseAsync();
            string responseBody;
            using (Stream dataStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(dataStream);
                responseBody = reader.ReadToEnd();
            }
            response.Close();
            return responseBody;
        }
    }
}
