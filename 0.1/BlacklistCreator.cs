using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;

namespace _0._1
{
    class BlacklistCreator
    {
        private static readonly string SOCIAL_HOSTS_LIST_URL = "https://raw.githubusercontent.com/StevenBlack/hosts/master/alternates/social/hosts";
        private static readonly string ADS_HOSTS_LIST_URL = "https://raw.githubusercontent.com/AdAway/adaway.github.io/master/hosts.txt";
        private static readonly string GAMBLING_HOSTS_LIST_URL = "https://raw.githubusercontent.com/StevenBlack/hosts/master/alternates/gambling/hosts";

        public static IEnumerable<string> getAsHosts()
        {
            List<string> hosts = new List<string>();
            IEnumerable<string> categorizeHosts = new[] { "" };

            hosts.AddRange(getSafeSearchHosts());
            hosts.AddRange(getSafeYoutubeHosts());

            List<string> blacklist = Resources.profile.Default.customBlacklist;

            //turn the urls to loop hosts (blocked)
            foreach (string url in blacklist)
            {
                hosts.Add("0.0.0.0 " + url + " www." + url + " https://" + url + " https://www." + url);
            }

            if (FilteringSystem.IsSocialBlocked())
                categorizeHosts = categorizeHosts.Concat(getSocialHosts());

            if (FilteringSystem.IsAdsBlocked())
                categorizeHosts = categorizeHosts.Concat(getAdsHosts());

            if (FilteringSystem.IsGamblingBlocked())
                categorizeHosts = categorizeHosts.Concat(getGamblingHosts());

            return hosts.Concat(categorizeHosts);
        }

        private static List<string> getSafeSearchHosts()
        {
            //string safeGoogle = Resources.profile.Default.strict_search ? "216.239.38.120" : "216.239.38.119";
            string safeGoogle = "216.239.38.120";
            //string safeBing = Resources.profile.Default.strict_search ? "204.79.197.220" : "204.79.197.200";
            string safeBing = "204.79.197.220";

            List<string> hosts = new List<string>();
            hosts.Add("# Google Safe Search -----------------");
            hosts.Add(safeGoogle + " www.google.com google.com http://www.google.com https://www.google.com");
            hosts.Add("# Bing Safe Search -------------------");
            hosts.Add(safeBing + " www.bing.com bing.com http://www.bing.com https://www.bing.com");

            return hosts;
        }

        private static List<string> getSafeYoutubeHosts()
        {
            List<string> hosts = new List<string>();
            string safeYoutubeHost = Resources.profile.Default.strict_search ? "216.239.38.120" : "216.239.38.119";

            List<string> youtubeUrls = new List<string>();
            youtubeUrls.Add("youtube.com");
            youtubeUrls.Add("m.youtube.com");
            youtubeUrls.Add("youtubei.googleapis.com");
            youtubeUrls.Add("youtube.googleapis.com");
            youtubeUrls.Add("youtube-nocookie.com");

            foreach (string url in youtubeUrls)
            {
                hosts.Add(safeYoutubeHost + " " + url + " www." + url + " https://" + url + " https://www." + url);
            }

            return hosts;
        }

        public static String[] getSocialHosts()
        {
            try
            {
                WebClient client = new WebClient();
                Stream stream = client.OpenRead(SOCIAL_HOSTS_LIST_URL);
                StreamReader reader = new StreamReader(stream);
                string hostsString = reader.ReadToEnd().Replace("\n", Environment.NewLine);
                return new[] { "======================Social Blocking======================" + Environment.NewLine + hostsString.Substring(hostsString.IndexOf("# Hosts contributed by Steven Black")) };
            }
            catch
            {
                return new []{ "" };
            }
        }
        public static String[] getGamblingHosts()
        {
            try
            {
                WebClient client = new WebClient();
                Stream stream = client.OpenRead(GAMBLING_HOSTS_LIST_URL);
                StreamReader reader = new StreamReader(stream);
                string hostsString = reader.ReadToEnd().Replace("\n", Environment.NewLine);
                return new[] { "======================Gambeling Blocking======================" + Environment.NewLine + hostsString.Substring(hostsString.IndexOf("# Hosts contributed by Steven Black")) };
            }
            catch
            {
                return new[] { "" };
            }
        }
        public static String[] getAdsHosts()
        {
            try
            {
                WebClient client = new WebClient();
                Stream stream = client.OpenRead(ADS_HOSTS_LIST_URL);
                StreamReader reader = new StreamReader(stream);
                string hostsString = reader.ReadToEnd().Replace("\n", Environment.NewLine);
                return new[] { "======================Advertisements Blocking======================" + Environment.NewLine + hostsString };
            }
            catch
            {
                return new[] { "" };
            }
        }
    }
}
