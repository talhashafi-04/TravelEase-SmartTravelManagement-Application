using System;
using System.Drawing;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TravelEase
{
    // Add this class to your project
    public class GoogleMapsForm : Form
    {
        private WebBrowser webBrowser;
        private string apiKey = "YOUR_GOOGLE_MAPS_API_KEY"; // Replace with your Google Maps API key

        // Destination coordinates or location name
        private string destinationName;
        private double? latitude;
        private double? longitude;

        public GoogleMapsForm(string destinationName, double? latitude = null, double? longitude = null)
        {
            this.destinationName = destinationName;
            this.latitude = latitude;
            this.longitude = longitude;

            InitializeMapForm();
            LoadMap();
        }

        private void InitializeMapForm()
        {
            this.Text = "Destination Map";
            this.Size = new Size(1000, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Icon = SystemIcons.Information;

            webBrowser = new WebBrowser
            {
                Dock = DockStyle.Fill,
                ScriptErrorsSuppressed = true
            };

            this.Controls.Add(webBrowser);
        }

        private async void LoadMap()
        {
            try
            {
                // If we don't have coordinates, try to geocode the destination name
                if (!latitude.HasValue || !longitude.HasValue)
                {
                    var coordinates = await GeocodeLocationAsync(destinationName);
                    if (coordinates != null)
                    {
                        latitude = coordinates.Value.latitude;
                        longitude = coordinates.Value.longitude;
                    }
                }

                string html;

                if (latitude.HasValue && longitude.HasValue)
                {
                    // Load map with coordinates
                    html = CreateMapHtml(latitude.Value, longitude.Value);
                }
                else
                {
                    // If geocoding failed, use the name as a search term
                    html = CreateSearchMapHtml(destinationName);
                }

                webBrowser.DocumentText = html;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading map: {ex.Message}", "Map Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string CreateMapHtml(double lat, double lng)
        {
            string htmlTemplate = @"
            <!DOCTYPE html>
            <html>
            <head>
                <meta charset='utf-8'>
                <title>Destination Map</title>
                <style>
                    html, body, #map {
                        height: 100%;
                        margin: 0;
                        padding: 0;
                    }
                </style>
            </head>
            <body>
                <div id='map'></div>
                <script>
                    function initMap() {
                        var destination = {lat: " + lat.ToString(System.Globalization.CultureInfo.InvariantCulture) + @", lng: " + lng.ToString(System.Globalization.CultureInfo.InvariantCulture) + @"};
                        var map = new google.maps.Map(document.getElementById('map'), {
                            zoom: 13,
                            center: destination,
                            mapTypeId: 'hybrid',
                            mapTypeControl: true,
                            fullscreenControl: true,
                            streetViewControl: true,
                            zoomControl: true
                        });
                        
                        var marker = new google.maps.Marker({
                            position: destination,
                            map: map,
                            title: '" + destinationName.Replace("'", "\\'") + @"',
                            animation: google.maps.Animation.DROP
                        });
                        
                        var infowindow = new google.maps.InfoWindow({
                            content: '<div style=""width:250px; padding:10px;"">' +
                                     '<h3 style=""margin-top:0;"">" + destinationName.Replace("'", "\\'") + @"</h3>' +
                                     '<p>Explore this amazing destination!</p>' +
                                     '<p><a href=""https://www.google.com/maps/dir/?api=1&destination=" + lat.ToString(System.Globalization.CultureInfo.InvariantCulture) + "," + lng.ToString(System.Globalization.CultureInfo.InvariantCulture) + @""" target=""_blank"">Get directions</a></p>' +
                                     '</div>'
                        });
                        
                        marker.addListener('click', function() {
                            infowindow.open(map, marker);
                        });
                        
                        // Open info window by default
                        infowindow.open(map, marker);
                    }
                </script>
                <script async defer
                    src='https://maps.googleapis.com/maps/api/js?key=" + apiKey + @"&callback=initMap'>
                </script>
            </body>
            </html>";

            return htmlTemplate;
        }

        private string CreateSearchMapHtml(string searchTerm)
        {
            string encodedSearch = HttpUtility.UrlEncode(searchTerm);

            string htmlTemplate = @"
            <!DOCTYPE html>
            <html>
            <head>
                <meta charset='utf-8'>
                <title>Search Map</title>
                <style>
                    html, body, #map {
                        height: 100%;
                        margin: 0;
                        padding: 0;
                    }
                    #search-box {
                        position: absolute;
                        top: 10px;
                        left: 10px;
                        padding: 10px;
                        background-color: white;
                        border-radius: 3px;
                        box-shadow: 0 2px 6px rgba(0,0,0,0.3);
                        z-index: 5;
                    }
                </style>
            </head>
            <body>
                <div id='map'></div>
                <script>
                    function initMap() {
                        var map = new google.maps.Map(document.getElementById('map'), {
                            zoom: 3,
                            center: {lat: 0, lng: 0},
                            mapTypeId: 'roadmap',
                            mapTypeControl: true,
                            fullscreenControl: true,
                            streetViewControl: true,
                            zoomControl: true
                        });
                        
                        // Create search box
                        var input = document.createElement('input');
                        input.id = 'pac-input';
                        input.className = 'controls';
                        input.type = 'text';
                        input.placeholder = 'Search for places';
                        input.value = '" + searchTerm.Replace("'", "\\'") + @"';
                        
                        var searchBox = document.createElement('div');
                        searchBox.id = 'search-box';
                        searchBox.appendChild(input);
                        map.controls[google.maps.ControlPosition.TOP_LEFT].push(searchBox);
                        
                        // Set up search functionality
                        var searchBox = new google.maps.places.SearchBox(input);
                        
                        map.addListener('bounds_changed', function() {
                            searchBox.setBounds(map.getBounds());
                        });
                        
                        var markers = [];
                        searchBox.addListener('places_changed', function() {
                            var places = searchBox.getPlaces();
                            
                            if (places.length == 0) {
                                return;
                            }
                            
                            // Clear existing markers
                            markers.forEach(function(marker) {
                                marker.setMap(null);
                            });
                            markers = [];
                            
                            var bounds = new google.maps.LatLngBounds();
                            places.forEach(function(place) {
                                if (!place.geometry) {
                                    console.log('Returned place contains no geometry');
                                    return;
                                }
                                
                                var marker = new google.maps.Marker({
                                    map: map,
                                    title: place.name,
                                    position: place.geometry.location,
                                    animation: google.maps.Animation.DROP
                                });
                                
                                markers.push(marker);
                                
                                var infowindow = new google.maps.InfoWindow({
                                    content: '<div style=""width:250px; padding:10px;"">' +
                                             '<h3 style=""margin-top:0;"">' + place.name + '</h3>' +
                                             '<p>' + (place.formatted_address || '') + '</p>' +
                                             '<p><a href=""https://www.google.com/maps/place/?q=place_id:' + place.place_id + '"" target=""_blank"">View on Google Maps</a></p>' +
                                             '</div>'
                                });
                                
                                marker.addListener('click', function() {
                                    infowindow.open(map, marker);
                                });
                                
                                if (place.geometry.viewport) {
                                    bounds.union(place.geometry.viewport);
                                } else {
                                    bounds.extend(place.geometry.location);
                                }
                            });
                            
                            map.fitBounds(bounds);
                        });
                        
                        // Trigger search on load
                        google.maps.event.trigger(input, 'focus');
                        google.maps.event.trigger(input, 'keydown', {
                            keyCode: 13  // Enter key
                        });
                    }
                </script>
                <script async defer
                    src='https://maps.googleapis.com/maps/api/js?key=" + apiKey + @"&libraries=places&callback=initMap'>
                </script>
            </body>
            </html>";

            return htmlTemplate;
        }

        private async Task<(double latitude, double longitude)?> GeocodeLocationAsync(string locationName)
        {
            try
            {
                string encodedLocation = HttpUtility.UrlEncode(locationName);
                string url = $"https://maps.googleapis.com/maps/api/geocode/json?address={encodedLocation}&key={apiKey}";

                using (WebClient client = new WebClient())
                {
                    string json = await client.DownloadStringTaskAsync(url);
                    JObject response = JObject.Parse(json);

                    if (response["status"].ToString() == "OK")
                    {
                        var location = response["results"][0]["geometry"]["location"];
                        double lat = (double)location["lat"];
                        double lng = (double)location["lng"];

                        return (lat, lng);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Geocoding error: {ex.Message}");
            }

            return null;
        }
    }
}

//private string GetGoogleMapsApiKey()
//{
//    // Store your API key in app settings, config file, or user settings

//    // Avoid hardcoding the key in source code

//    // Option 1: App settings
//    // return Properties.Settings.Default.GoogleMapsApiKey;

//    // Option 2: Config file
//    // return ConfigurationManager.AppSettings["GoogleMapsApiKey"];

//    // For testing only (replace with your actual key)
//    return "YOUR_GOOGLE_MAPS_API_KEY";
//}