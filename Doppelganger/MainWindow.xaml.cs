using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Linq;
using System.Net.Sockets;
using System.Windows.Media;
using System.Windows.Threading;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using System.Windows.Media.Animation;
using System.Reflection;
using Doppelganger.Models;
using Doppelganger.Services;

namespace Doppelganger
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public ObservableCollection<NetworkDevice> Devices { get; set; }
        private bool _isScanning;
        private string _statusMessage;
        private int _devicesFound;
        private DispatcherTimer _realTimeTimer;
        private NetworkScanner _networkScanner;
        private BluetoothScanner _bluetoothScanner;
        private WifiScanner _wifiScanner;
        private VulnerabilityScanner _vulnerabilityScanner;

        public bool IsScanning
        {
            get => _isScanning;
            set
            {
                _isScanning = value;
                OnPropertyChanged(nameof(IsScanning));
                OnPropertyChanged(nameof(CanScan));
            }
        }

        public bool CanScan => !IsScanning;

        public string StatusMessage
        {
            get => _statusMessage;
            set
            {
                _statusMessage = value;
                OnPropertyChanged(nameof(StatusMessage));
            }
        }

        public int DevicesFound
        {
            get => _devicesFound;
            set
            {
                _devicesFound = value;
                OnPropertyChanged(nameof(DevicesFound));
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            Devices = new ObservableCollection<NetworkDevice>();
            StatusMessage = "🕵️ Doppelganger est prêt à révéler les secrets de votre réseau...";

            InitializeServices();
            InitializeRealTimeUpdates();

            // Animation d'entrée
            var fadeIn = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(0.8));
            BeginAnimation(OpacityProperty, fadeIn);
        }

        private void InitializeServices()
        {
            _networkScanner = new NetworkScanner();
            _bluetoothScanner = new BluetoothScanner();
            _wifiScanner = new WifiScanner();
            _vulnerabilityScanner = new VulnerabilityScanner();

            // Abonnement aux événements
            _networkScanner.DeviceFound += OnDeviceFound;
            _networkScanner.ScanProgress += OnScanProgress;
            _bluetoothScanner.DeviceFound += OnDeviceFound;
            _wifiScanner.DeviceFound += OnDeviceFound;
            _vulnerabilityScanner.VulnerabilityFound += OnVulnerabilityFound;
        }

        private void InitializeRealTimeUpdates()
        {
            _realTimeTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(30)
            };
            _realTimeTimer.Tick += async (s, e) => await UpdateDevicesStatus();
        }

        #region Event Handlers pour les boutons

        private async void ScanButton_Click(object sender, RoutedEventArgs e)
        {
            await ScanNetwork();
        }

        private async void BluetoothScanButton_Click(object sender, RoutedEventArgs e)
        {
            await ScanBluetooth();
        }

        private async void WifiScanButton_Click(object sender, RoutedEventArgs e)
        {
            await ScanWifi();
        }

        private async void VulnScanButton_Click(object sender, RoutedEventArgs e)
        {
            await ScanVulnerabilities();
        }

        private async void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            if (!IsScanning)
            {
                await ScanNetwork();
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            Devices.Clear();
            DevicesFound = 0;
            StatusMessage = "🧹 Traces effacées - le réseau redevient mystérieux";
        }

        private async void ExportButton_Click(object sender, RoutedEventArgs e)
        {
            await ExportResults();
        }

        #endregion

        #region Méthodes de scan

        private async Task ScanNetwork()
        {
            IsScanning = true;
            Devices.Clear();
            DevicesFound = 0;
            StatusMessage = "🔍 Exploration des ombres numériques en cours...";

            try
            {
                await _networkScanner.ScanNetworkAsync();
                StartRealTimeMonitoring();
            }
            catch (Exception ex)
            {
                StatusMessage = $"💥 Erreur dans la matrice: {ex.Message}";
            }
            finally
            {
                IsScanning = false;
            }
        }

        private async Task ScanBluetooth()
        {
            IsScanning = true;
            StatusMessage = "📱 Détection des signaux Bluetooth...";

            try
            {
                await _bluetoothScanner.ScanBluetoothAsync();
                StatusMessage = "📱 Scan Bluetooth terminé";
            }
            catch (Exception ex)
            {
                StatusMessage = $"📱 Erreur Bluetooth: {ex.Message}";
            }
            finally
            {
                IsScanning = false;
            }
        }

        private async Task ScanWifi()
        {
            IsScanning = true;
            StatusMessage = "📶 Analyse des réseaux WiFi...";

            try
            {
                await _wifiScanner.ScanWifiAsync();
                StatusMessage = "📶 Scan WiFi terminé";
            }
            catch (Exception ex)
            {
                StatusMessage = $"📶 Erreur WiFi: {ex.Message}";
            }
            finally
            {
                IsScanning = false;
            }
        }

        private async Task ScanVulnerabilities()
        {
            if (Devices.Count == 0)
            {
                StatusMessage = "🛡️ Aucun appareil à analyser. Effectuez d'abord un scan réseau.";
                return;
            }

            IsScanning = true;
            StatusMessage = "🛡️ Analyse des vulnérabilités...";

            try
            {
                await _vulnerabilityScanner.ScanVulnerabilitiesAsync(Devices.ToList());
                StatusMessage = "🛡️ Analyse de sécurité terminée";
            }
            catch (Exception ex)
            {
                StatusMessage = $"🛡️ Erreur d'analyse: {ex.Message}";
            }
            finally
            {
                IsScanning = false;
            }
        }

        #endregion

        #region Event Handlers pour les scanners

        private void OnDeviceFound(object sender, DeviceFoundEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                var existingDevice = Devices.FirstOrDefault(d => d.IPAddress == e.Device.IPAddress ||
                                                                d.MacAddress == e.Device.MacAddress);
                if (existingDevice != null)
                {
                    // Mise à jour des informations
                    existingDevice.UpdateFrom(e.Device);
                }
                else
                {
                    Devices.Add(e.Device);
                    DevicesFound++;
                }
            });
        }

        private void OnScanProgress(object sender, ScanProgressEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                StatusMessage = e.Message;
            });
        }

        private void OnVulnerabilityFound(object sender, VulnerabilityFoundEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                var device = Devices.FirstOrDefault(d => d.IPAddress == e.IPAddress);
                if (device != null)
                {
                    device.SecurityRisk = e.Risk;
                    device.VulnerabilityDetails = e.Details;
                }
            });
        }

        #endregion

        private void StartRealTimeMonitoring()
        {
            _realTimeTimer.Start();
            StatusMessage += " 👁️ Surveillance active...";
        }

        private async Task UpdateDevicesStatus()
        {
            foreach (var device in Devices.ToList())
            {
                try
                {
                    using (var ping = new Ping())
                    {
                        var reply = await ping.SendPingAsync(device.IPAddress, 1000);
                        var wasOnline = device.Status.Contains("🟢");
                        var isOnline = reply.Status == IPStatus.Success;

                        if (wasOnline != isOnline)
                        {
                            device.Status = isOnline ? "🟢 En ligne" : "🔴 Hors ligne";
                            device.LastSeen = DateTime.Now;

                            ShowToastNotification($"Doppelganger {device.IPAddress} {(isOnline ? "est apparu" : "a disparu")}!");
                        }

                        if (isOnline)
                        {
                            device.ResponseTime = reply.RoundtripTime;
                        }
                    }
                }
                catch { }
            }
        }

        private void ShowToastNotification(string message)
        {
            StatusMessage = $"🔔 {message}";
        }

        private async Task ExportResults()
        {
            try
            {
                var exporter = new DataExporter();
                var filename = await exporter.ExportToJsonAsync(Devices.ToList());
                StatusMessage = $"📄 Rapport exporté: {filename}";
            }
            catch (Exception ex)
            {
                StatusMessage = $"❌ Erreur d'export: {ex.Message}";
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            _realTimeTimer?.Stop();
            base.OnClosed(e);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}