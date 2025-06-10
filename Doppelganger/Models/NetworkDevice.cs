using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Doppelganger.Models
{
    public class NetworkDevice : INotifyPropertyChanged
    {
        private string _status;
        private long _responseTime;
        private string _signalStrength;
        private string _location;
        private string _dhcpInfo;
        private string _speedTest;
        private string _macAddress;
        private string _vulnerabilityDetails;

        public string IPAddress { get; set; }
        public string HostName { get; set; }
        public string DeviceType { get; set; }
        public string Manufacturer { get; set; }
        public List<string> OpenPorts { get; set; } = new List<string>();
        public string SecurityRisk { get; set; }
        public DateTime LastSeen { get; set; }

        public string MacAddress
        {
            get => _macAddress;
            set
            {
                _macAddress = value;
                OnPropertyChanged(nameof(MacAddress));
            }
        }

        public long ResponseTime
        {
            get => _responseTime;
            set
            {
                _responseTime = value;
                OnPropertyChanged(nameof(ResponseTime));
            }
        }

        public string Status
        {
            get => _status;
            set
            {
                _status = value;
                OnPropertyChanged(nameof(Status));
            }
        }

        public string SignalStrength
        {
            get => _signalStrength;
            set
            {
                _signalStrength = value;
                OnPropertyChanged(nameof(SignalStrength));
            }
        }

        public string Location
        {
            get => _location;
            set
            {
                _location = value;
                OnPropertyChanged(nameof(Location));
            }
        }

        public string DhcpInfo
        {
            get => _dhcpInfo;
            set
            {
                _dhcpInfo = value;
                OnPropertyChanged(nameof(DhcpInfo));
            }
        }

        public string SpeedTest
        {
            get => _speedTest;
            set
            {
                _speedTest = value;
                OnPropertyChanged(nameof(SpeedTest));
            }
        }

        public string VulnerabilityDetails
        {
            get => _vulnerabilityDetails;
            set
            {
                _vulnerabilityDetails = value;
                OnPropertyChanged(nameof(VulnerabilityDetails));
            }
        }

        // Propriétés calculées pour l'affichage
        public string ServicesDisplay => string.Join(", ", OpenPorts.Take(3)) +
                                        (OpenPorts.Count > 3 ? $" (+{OpenPorts.Count - 3})" : "");

        public void UpdateFrom(NetworkDevice other)
        {
            if (other == null) return;

            if (!string.IsNullOrEmpty(other.HostName))
                HostName = other.HostName;

            if (!string.IsNullOrEmpty(other.DeviceType))
                DeviceType = other.DeviceType;

            if (!string.IsNullOrEmpty(other.Manufacturer))
                Manufacturer = other.Manufacturer;

            if (!string.IsNullOrEmpty(other.MacAddress))
                MacAddress = other.MacAddress;

            if (!string.IsNullOrEmpty(other.SignalStrength))
                SignalStrength = other.SignalStrength;

            if (!string.IsNullOrEmpty(other.Location))
                Location = other.Location;

            if (!string.IsNullOrEmpty(other.DhcpInfo))
                DhcpInfo = other.DhcpInfo;

            if (!string.IsNullOrEmpty(other.SpeedTest))
                SpeedTest = other.SpeedTest;

            if (other.OpenPorts?.Count > 0)
            {
                OpenPorts.AddRange(other.OpenPorts.Where(p => !OpenPorts.Contains(p)));
                OnPropertyChanged(nameof(ServicesDisplay));
            }

            ResponseTime = other.ResponseTime;
            Status = other.Status;
            LastSeen = other.LastSeen;
            SecurityRisk = other.SecurityRisk;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}