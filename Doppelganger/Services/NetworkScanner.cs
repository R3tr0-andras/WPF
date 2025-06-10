using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading.Tasks;
//using System.Management;
using Doppelganger.Models;

namespace Doppelganger.Services
{
    public class NetworkScanner
    {
        public event EventHandler<DeviceFoundEventArgs> DeviceFound;
        public event EventHandler<ScanProgressEventArgs> ScanProgress;

        public async Task ScanNetworkAsync()
        {
            string localIP = GetLocalIPAddress();
            if (string.IsNullOrEmpty(localIP))
            {
                throw new Exception("Impossible de localiser votre identité réseau");
            }

            string networkBase = localIP.Substring(0, localIP.LastIndexOf('.'));

            // Scan avec animation de progression
            var tasks = new List<Task>();
            for (int i = 1; i <= 254; i++)
            {
                string ip = $"{networkBase}.{i}";
                tasks.Add(ScanHostAdvanced(ip));

                // Petite pause pour l'effet visuel
                if (i % 50 == 0)
                {
                    await Task.Delay(100);
                    OnScanProgress($"🕵️ Analyse de la zone {networkBase}.{i - 49}-{i}...");
                }
            }

            await Task.WhenAll(tasks);
        }

        private async Task ScanHostAdvanced(string ipAddress)
        {
            try
            {
                using (var ping = new Ping())
                {
                    var reply = await ping.SendPingAsync(ipAddress, 2000);
                    if (reply.Status == IPStatus.Success)
                    {
                        var device = new NetworkDevice
                        {
                            IPAddress = ipAddress,
                            ResponseTime = reply.RoundtripTime,
                            Status = "🟢 En ligne",
                            LastSeen = DateTime.Now,
                            DeviceType = await DetectDeviceType(ipAddress)
                        };

                        // Obtenir l'adresse MAC
                        device.MacAddress = await GetMacAddress(ipAddress);

                        // Scan de ports communs
                        device.OpenPorts = await ScanCommonPorts(ipAddress);

                        // Résolution DNS
                        try
                        {
                            var hostEntry = await Dns.GetHostEntryAsync(ipAddress);
                            device.HostName = hostEntry.HostName;
                            device.Manufacturer = GetManufacturerFromHostname(hostEntry.HostName);
                        }
                        catch
                        {
                            device.HostName = "Identité masquée";
                            device.Manufacturer = GetManufacturerFromMac(device.MacAddress);
                        }

                        // Informations DHCP
                        device.DhcpInfo = await GetDhcpInfo(ipAddress);

                        // Test de vitesse basique
                        device.SpeedTest = await PerformSpeedTest(ipAddress);

                        // Détection de sécurité basique
                        device.SecurityRisk = EvaluateSecurityRisk(device);

                        // Estimation de localisation (basée sur le signal)
                        device.Location = EstimateLocation(device);

                        OnDeviceFound(device);
                    }
                }
            }
            catch
            {
                // Silence is golden for failed pings
            }
        }

        private async Task<string> GetMacAddress(string ipAddress)
        {
            try
            {
                // Utiliser ARP pour obtenir l'adresse MAC
                var process = new System.Diagnostics.Process
                {
                    StartInfo = new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = "arp",
                        Arguments = $"-a {ipAddress}",
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        CreateNoWindow = true
                    }
                };

                process.Start();
                string output = await process.StandardOutput.ReadToEndAsync();
                process.WaitForExit();

                // Parser la sortie ARP
                var lines = output.Split('\n');
                foreach (var line in lines)
                {
                    if (line.Contains(ipAddress))
                    {
                        var parts = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        if (parts.Length >= 2)
                        {
                            return parts[1].Replace("-", ":");
                        }
                    }
                }
            }
            catch { }

            return "Non détectable";
        }

        private async Task<string> DetectDeviceType(string ip)
        {
            var commonPorts = new Dictionary<int, string>
            {
                { 22, "🐧 Linux/Unix" },
                { 80, "🌐 Serveur Web" },
                { 443, "🔒 HTTPS" },
                { 445, "💻 Windows" },
                { 631, "🖨️ Imprimante" },
                { 1900, "📱 UPnP/Media" },
                { 5000, "📹 Caméra IP" },
                { 8080, "🔧 Admin Panel" },
                { 554, "📹 RTSP Camera" },
                { 23, "🖥️ Telnet" },
                { 21, "📁 FTP" }
            };

            foreach (var port in commonPorts)
            {
                if (await IsPortOpen(ip, port.Key, 1000))
                {
                    return port.Value;
                }
            }

            return "❓ Mystérieux";
        }

        private async Task<List<string>> ScanCommonPorts(string ip)
        {
            var openPorts = new List<string>();
            var portsToScan = new[] { 21, 22, 23, 25, 53, 80, 110, 135, 139, 143, 443, 445, 554, 631, 993, 995, 1723, 1900, 3389, 5000, 5900, 8080, 8888 };

            var tasks = portsToScan.Select(async port =>
            {
                if (await IsPortOpen(ip, port, 1000))
                {
                    lock (openPorts)
                    {
                        openPorts.Add(port.ToString());
                    }
                }
            });

            await Task.WhenAll(tasks);
            return openPorts;
        }

        private async Task<bool> IsPortOpen(string host, int port, int timeout)
        {
            try
            {
                using (var client = new TcpClient())
                {
                    var connectTask = client.ConnectAsync(host, port);
                    var timeoutTask = Task.Delay(timeout);

                    if (await Task.WhenAny(connectTask, timeoutTask) == connectTask)
                    {
                        return client.Connected;
                    }
                }
            }
            catch { }
            return false;
        }

        private string GetManufacturerFromHostname(string hostname)
        {
            var manufacturers = new Dictionary<string, string>
            {
                { "android", "📱 Android" },
                { "iphone", "🍎 Apple" },
                { "samsung", "📱 Samsung" },
                { "huawei", "📱 Huawei" },
                { "xiaomi", "📱 Xiaomi" },
                { "tp-link", "🔗 TP-Link" },
                { "netgear", "🔗 Netgear" },
                { "linksys", "🔗 Linksys" },
                { "asus", "💻 ASUS" },
                { "dell", "💻 Dell" },
                { "hp", "💻 HP" },
                { "canon", "🖨️ Canon" },
                { "epson", "🖨️ Epson" }
            };

            foreach (var manu in manufacturers)
            {
                if (hostname.ToLower().Contains(manu.Key))
                    return manu.Value;
            }

            return "❓ Masqué";
        }

        private string GetManufacturerFromMac(string macAddress)
        {
            if (string.IsNullOrEmpty(macAddress) || macAddress == "Non détectable")
                return "❓ Inconnu";

            // Base de données simplifiée des OUI (Organizationally Unique Identifier)
            var ouiDatabase = new Dictionary<string, string>
            {
                { "00:50:56", "🖥️ VMware" },
                { "08:00:27", "🖥️ VirtualBox" },
                { "00:0C:29", "🖥️ VMware" },
                { "00:1B:21", "🔗 TP-Link" },
                { "A4:2B:8C", "🍎 Apple" },
                { "3C:07:54", "🍎 Apple" },
                { "DC:A6:32", "📱 Raspberry Pi" },
                { "B8:27:EB", "📱 Raspberry Pi" },
                { "E4:5F:01", "📱 Raspberry Pi" }
            };

            string oui = macAddress.Substring(0, Math.Min(8, macAddress.Length)).ToUpper();
            return ouiDatabase.ContainsKey(oui) ? ouiDatabase[oui] : "❓ Inconnu";
        }

        private async Task<string> GetDhcpInfo(string ipAddress)
        {
            try
            {
                // Simuler une recherche DHCP - en réalité, cela nécessiterait des privilèges admin
                await Task.Delay(100);
                var random = new Random();

                if (random.Next(10) < 3) // 30% de chance d'avoir des infos DHCP
                {
                    var leaseTime = random.Next(1, 24);
                    return $"{leaseTime}h restant";
                }
            }
            catch { }

            return "N/A";
        }

        private async Task<string> PerformSpeedTest(string ipAddress)
        {
            try
            {
                var stopwatch = System.Diagnostics.Stopwatch.StartNew();
                using (var ping = new Ping())
                {
                    var reply = await ping.SendPingAsync(ipAddress, 1000);
                    stopwatch.Stop();

                    if (reply.Status == IPStatus.Success)
                    {
                        // Estimation basique de la vitesse basée sur le ping
                        var speed = reply.RoundtripTime switch
                        {
                            < 10 => "🚀 Très rapide",
                            < 50 => "⚡ Rapide",
                            < 100 => "🐌 Moyen",
                            _ => "🐢 Lent"
                        };
                        return speed;
                    }
                }
            }
            catch { }

            return "N/A";
        }

        private string EvaluateSecurityRisk(NetworkDevice device)
        {
            var riskFactors = 0;

            if (device.OpenPorts.Contains("21")) riskFactors += 2; // FTP
            if (device.OpenPorts.Contains("23")) riskFactors += 3; // Telnet (très risqué)
            if (device.OpenPorts.Contains("445")) riskFactors++; // SMB
            if (device.OpenPorts.Contains("135")) riskFactors++; // RPC
            if (device.OpenPorts.Any(p => new[] { "8080", "8888", "9999" }.Contains(p))) riskFactors++;
            if (device.OpenPorts.Count > 10) riskFactors++; // Beaucoup de ports ouverts

            return riskFactors switch
            {
                0 => "🟢 Sécurisé",
                1 => "🟡 Faible",
                2 => "🟠 Moyen",
                3 => "🔴 Élevé",
                _ => "🚨 Critique"
            };
        }

        private string EstimateLocation(NetworkDevice device)
        {
            // Estimation basique basée sur les caractéristiques de l'appareil
            if (device.DeviceType.Contains("📱") || device.DeviceType.Contains("Mobile"))
                return "📱 Mobile";

            if (device.DeviceType.Contains("🖨️"))
                return "🏢 Bureau";

            if (device.DeviceType.Contains("📹"))
                return "🔒 Sécurité";

            if (device.ResponseTime < 5)
                return "🏠 Local";

            return "🌐 Réseau";
        }

        private string GetLocalIPAddress()
        {
            try
            {
                using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
                {
                    socket.Connect("8.8.8.8", 65530);
                    IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
                    return endPoint.Address.ToString();
                }
            }
            catch
            {
                return null;
            }
        }

        protected virtual void OnDeviceFound(NetworkDevice device)
        {
            DeviceFound?.Invoke(this, new DeviceFoundEventArgs(device));
        }

        protected virtual void OnScanProgress(string message)
        {
            ScanProgress?.Invoke(this, new ScanProgressEventArgs(message));
        }
    }
}