using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace benjaminshi.gs1
{
    public class GCPLength
    {
        /// <summary>
        /// The download dir & path for gcpprefixformatlist.json 
        /// </summary>
        protected static string downloadDirPath = @"gs1.download";
        protected static string downloadFileName = @"gcpprefixformatlist.json";

        /// <summary>
        /// The property to return the full relative path of the download gcpprefixformatlist.json file
        /// </summary>
        protected static string DownloadFilePath { 
            get
            {
                try
                {
                    if (!Directory.Exists(downloadDirPath))
                    {
                        Directory.CreateDirectory(downloadDirPath);
                    }

                    return downloadDirPath + Path.DirectorySeparatorChar + downloadFileName;
                }
                catch
                {
                    return "";
                }
            }
        }

        /// <summary>
        /// Return the url of gcpprefixformatlist.json in www.gs1.org
        /// </summary>
        protected static string DownloadURL
        {
            get
            {
                return @"https://www.gs1.org/docs/gcp_length/gcpprefixformatlist.json";
            }
        }


        /// <summary>
        /// Download GCP Length Definition file Task
        /// </summary>
        /// <returns>whether download successed</returns>
        protected static async Task<bool> DownloadTask()
        {
            bool isOK = false;
            
            try
            {
                using (var client = new HttpClient())
                {
                    using (var result = await client.GetAsync(DownloadURL))
                    {
                        if (result.IsSuccessStatusCode)
                        {
                            byte[] data = await result.Content.ReadAsByteArrayAsync();

                            File.WriteAllBytes(DownloadFilePath, data);

                            isOK = true;
                        }
                    }
                }
            }
            catch
            {
                isOK = false;
            }

            if (isOK)
            {
                isOK = File.Exists(DownloadFilePath);
            }
            
            return isOK;
        }

        /// <summary>
        /// Download GCP Length Definition file
        /// </summary>
        /// <returns>whether download successed</returns>
        public static bool Download()
        {
            return DownloadTask().GetAwaiter().GetResult();
        }

        /// <summary>
        /// Refresh/reload the data from the GCP Length Table (JSON Version)
        /// </summary>
        /// <returns>true if successed, or false if failed.</returns>
        public static bool Refresh()
        {
            bool isOK = false;

            string downloadFile = DownloadFilePath;

            try
            {
                if (DownloadFilePath.Length <= 0) return false;

                if (!File.Exists(DownloadFilePath))
                {
                    Download();
                }
                if (File.Exists(DownloadFilePath))
                {
                    string text = File.ReadAllText(DownloadFilePath, Encoding.UTF8);
                    var def = JsonSerializer.Deserialize<JsonObject>(text);

                    if (null == def) return false;
                    if (!def.ContainsKey("GCPPrefixFormatList")) return false;

                    var GCPPrefixFormatList = def["GCPPrefixFormatList"];
                    if (null == GCPPrefixFormatList) return false;

                    var entry = GCPPrefixFormatList["entry"];
                    if (null == entry) return false;


                    string key;
                    int value;
                    int count = 0, count_ok = 0;
                    JsonNode? keyNode, valueNode;

                    if (null == _TableGCPLength)
                    {
                        _TableGCPLength = new Dictionary<string, int>();
                    }
                    _TableGCPLength.Clear();

                    foreach (var GCPPrefixFormat in entry.AsArray())
                    {
                        if (null != GCPPrefixFormat)
                        {
                            ++count;

                            keyNode = GCPPrefixFormat["prefix"];
                            valueNode = GCPPrefixFormat["gcpLength"];
                            if ((null != keyNode) && (null != valueNode))
                            {
                                key = keyNode.ToString();
                                value = int.Parse(valueNode.ToString());

                                _TableGCPLength.Add(key, value);

                                ++count_ok;
                            }
                        }
                    }

                    isOK = true;
                }
            }
            catch
            {
                isOK = false;
            }


            return isOK;
        }

        /// <summary>
        /// The variable used to stored the GCPLenth table
        /// </summary>
        protected static Dictionary<string, int>? _TableGCPLength = null;

        /// <summary>
        /// The property returned the GCPLenth table
        /// </summary>
        protected static Dictionary<string, int>? TableGCPLength
        {
            get
            {
                if (null == _TableGCPLength)
                {
                    Refresh();
                }

                return _TableGCPLength;
            }
        }

        /// <summary>
        /// Check whether a prefix existed in the GCP Length Table
        /// </summary>
        /// <param name="prefix">The code used to be detected</param>
        /// <returns>if the "prefix" can be found in the GCP Length Table</returns>
        public static bool Exists(string prefix)
        {
            bool isExisted = false;

            var table = TableGCPLength;

            if (null != table)
            {
                for (int len = 1;len <= prefix.Length;++len)
                {
                    string str = prefix.Substring(0,len);

                    if (Regex.IsMatch(@"[^\d]", str))
                    {
                        break;
                    }
                    else if (str.Length > 12)
                    {
                        break;
                    }
                    else if (table.ContainsKey(str))
                    {
                        isExisted = true;
                        break;
                    }
                }
            }

            return isExisted; 
        }

        /// <summary>
        /// GS1 Company Prefix Length detection
        /// </summary>
        /// <param name="prefix">Searchable codes like GCP, GTIN, etc.</param>
        /// <returns>GS1 Company Prefix Length, or 0 if cannot find a GCP Length based on the "prefix".</returns>
        public static int Find(string prefix)
        {
            int result = 0;
            string str = "";

            result = Find(prefix, out str);

            return result;
        }

        /// <summary>
        /// GS1 Company Prefix Length detection
        /// </summary>
        /// <param name="prefix">Searchable codes like GCP, GTIN, etc.</param>
        /// <param name="realPrefix">Output actual prefix when found, or ""</param>
        /// <returns>GS1 Company Prefix Length, or 0 if cannot find a GCP Length based on the "prefix".</returns>
        public static int Find(string prefix, out string realPrefix)
        {
            int result = 0;

            realPrefix = "";

            var table = TableGCPLength;

            if (null != table)
            {
                for (int len = 1; len <= prefix.Length; ++len)
                {
                    string str = prefix.Substring(0, len);

                    if (Regex.IsMatch(@"[^\d]", str))
                    {
                        break;
                    }
                    else if (str.Length > 12)
                    {
                        break;
                    }
                    else if (table.ContainsKey(str))
                    {
                        result = table[str];

                        realPrefix = str;

                        break;
                    }
                }
            }

            return result;
        }
    }
}