using System;
using Doppelganger.Models;

namespace Doppelganger
{
    public class DeviceFoundEventArgs : EventArgs
    {
        public NetworkDevice Device { get; }

        public DeviceFoundEventArgs(NetworkDevice device)
        {
            Device = device;
        }
    }

    public class ScanProgressEventArgs : EventArgs
    {
        public string Message { get; }
        public int Progress { get; }

        public ScanProgressEventArgs(string message, int progress = 0)
        {
            Message = message;
            Progress = progress;
        }
    }

    public class VulnerabilityFoundEventArgs : EventArgs
    {
        public string IPAddress { get; }
        public string Risk { get; }
        public string Details { get; }

        public VulnerabilityFoundEventArgs(string ipAddress, string risk, string details)
        {
            IPAddress = ipAddress;
            Risk = risk;
            Details = details;
        }
    }
}