using System;

namespace ConnectionManager
{
    public class UbntClient
    {
        private SSHClient _client = null;

        //---------------------------
        // Set SSH Client
        //---------------------------
        public void SetSSHClient(SSHClient client)
        {
            _client = client;
        }

        //---------------------------
        // Get Signal Strength
        //---------------------------
        public int GetSignal() {
            if (_client != null)
            {
                string result = _client.Command("mca-status | grep signal");
                if (result != "")
                {
                    return Convert.ToInt32(result.TrimEnd('\r', '\n').Split('=')[1]);
                }
                return -100;
            }
            return -100;
        }
        
        //---------------------------
        // Get Noise Floor
        //---------------------------
        public int GetNoiseFloor()
        {
            if (_client != null)
            {
                string result = _client.Command("mca-status | grep noise");
                if (result != "")
                {
                    return Convert.ToInt32(result.TrimEnd('\r', '\n').Split('=')[1]);
                }
                return -100;
            }
            return -100;
        }
        
        //---------------------------
        // Get Transmit CCQ
        //---------------------------
        public int GetTransmitCCQ()
        {
            if (_client != null)
            {
                string result = _client.Command("mca-status | grep ccq");
                if (result != "")
                {
                    return Convert.ToInt32(result.TrimEnd('\r', '\n').Split('=')[1]) / 10;
                }
                return 0;
            }
            return 0;
        }
        
        //---------------------------
        // Get Base Station SSID
        //---------------------------
        public string GetBaseSSID()
        {
            if (_client != null)
            {
                string result = _client.Command("mca-status | grep essid");
                if (result != "")
                {
                    return result.TrimEnd('\r', '\n').Split('=')[1];
                }
                return "";
            }
            return "";
        }
        
        //---------------------------
        // Get AP MAC
        //---------------------------
        public string GetApMAC()
        {
            if (_client != null)
            {
                string result = _client.Command("mca-status | grep apMac");
                if (result != "")
                {
                    return result.TrimEnd('\r', '\n').Split('=')[1];
                }
                return "";
            }
            return "";
        }
        
        //---------------------------
        // Get WLAN IP Address
        //---------------------------
        public string GetWlanIpAddress()
        {
            if (_client != null)
            {
                string result = _client.Command("mca-status | grep wlanIpAddress");
                if (result != "")
                {
                    return result.TrimEnd('\r', '\n').Split('=')[1];
                }
                return "";
            }
            return "";
        }
        
        //---------------------------
        // Get Frequency
        //---------------------------
        public string GetFrequency()
        {
            if (_client != null)
            {
                string result = _client.Command("mca-status | grep freq");
                if (result != "")
                {
                    return result.TrimEnd('\r', '\n').Split('=')[1];
                }
                return "";
            }
            return "";
        }
        
        //---------------------------
        // Get Channel
        //---------------------------
        public string GetChannel()
        {
            string[] channels = { "2412", "2417", "2422", "2427", "2432", "2437", "2442",
                                  "2447", "2452", "2457", "2462", "2467", "2472", "2484"};
            return (Array.IndexOf(channels, GetFrequency()) + 1).ToString();
        }
        
        //---------------------------
        // Get ACK Timeout
        //---------------------------
        public string GetAckTimeout()
        {
            if (_client != null)
            {
                string result = _client.Command("mca-status | grep ackTimeout");
                if (result != "")
                {
                    return result.TrimEnd('\r', '\n').Split('=')[1];
                }
                return "";
            }
            return "";
        }
        
        //---------------------------
        // Get TX Rate
        //---------------------------
        public string GetTxRate()
        {
            if (_client != null)
            {
                string result = _client.Command("mca-status | grep wlanTxRate");
                if (result != "")
                {
                    return (result.TrimEnd('\r', '\n').Split('=')[1]).Split('.')[0];
                }
                return "";
            }
            return "";
        }
        
        //---------------------------
        // Get RX Rate
        //---------------------------
        public string GetRxRate()
        {
            if (_client != null)
            {
                string result = _client.Command("mca-status | grep wlanRxRate");
                if (result != "")
                {
                    return (result.TrimEnd('\r', '\n').Split('=')[1]).Split('.')[0];
                }
                return "";
            }
            return "";
        }
        
        //---------------------------
        // Get Uptime
        //---------------------------
        public string GetUptime()
        {
            if (_client != null)
            {
                string result = _client.Command("mca-status | grep uptime");
                if (result != "")
                {
                    return result.TrimEnd('\r', '\n').Split('=')[1];
                }
                return "";
            }
            return "";
        }
        
        //---------------------------
        // Get Formatted Uptime
        //---------------------------
        public string GetUptimeFormatted()
        {
            if (_client != null)
            {
                TimeSpan ts = TimeSpan.FromSeconds(Convert.ToDouble(GetUptime()));
                if (ts.Days > 0)
                {
                    return ts.ToString(@"d' day(-s) 'hh\:mm\:ss");
                }
                else
                {
                    return ts.ToString(@"hh\:mm\:ss");
                }
            }
            return "";
        }
    }
}
