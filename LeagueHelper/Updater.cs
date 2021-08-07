using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LeagueHelper
{
    public class Updater
    {

#if DEBUG
        private const String updateLink = "E:\\Programming\\C#\\LeagueHelper\\LeagueHelper\\versions.json";
#else
        private const String updateLink = "https://raw.githubusercontent.com/jacklee-code/LeagueHelper/master/LeagueHelper/versions.json";
#endif

        private HttpClient httpClient;

        public Updater()
        {
            httpClient = new HttpClient();
        }

        public async Task<Version> CheckUpdate(String currentVersion)
        {
            bool result = false;
            List<Version> versions = await GetVersions();
            int i = 0;
            for (; i < versions.Count; i++)
                if (new System.Version(currentVersion).CompareTo(new System.Version(versions[i].versionNumber)) < 0)
                {
                    result = true;
                    break;
                }

                    
            if (!result)
                return null;
            Version version = new Version()
            {
                versionNumber = versions[versions.Count - 1].versionNumber,
                date = versions[versions.Count - 1].date,
                url = versions[versions.Count - 1].url,
                changelog = ""
            };      
            for (; i < versions.Count; i++)
                version.changelog += versions[i].changelog + '\n';
            return version;
        }

        public async Task<List<Version>> GetVersions()
        {
            List<Version> versions;
#if DEBUG
            using (var fs = new FileStream(updateLink, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var sr = new StreamReader(fs, Encoding.UTF8))
            {
                String json = sr.ReadToEnd();
                versions = JsonConvert.DeserializeObject<List<Version>>(json);
                return versions;
            }
#else
            try
            {
                String json = await httpClient.GetStringAsync(updateLink);
                versions = JsonConvert.DeserializeObject<List<Version>>(json);
                return versions;
            }
            catch { return new List<Version>(); }

#endif
        }

        public async void WriteNewVersionToJson(Version version)
        {
            List<Version> versions = await GetVersions();
            versions.Add(version);
            using (var fs = new FileStream(updateLink, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
            using (var sr = new StreamWriter(fs,Encoding.UTF8))
            {
                String json = JsonConvert.SerializeObject(versions, Formatting.Indented);
                sr.Write(json);
            }
        }

    }

    public class Version
    {
        public String versionNumber { get; set; }
        public String changelog { get; set; }
        public String date { get; set; }
        public String url { get; set; }
    }
}
