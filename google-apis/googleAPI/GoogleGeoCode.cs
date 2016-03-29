using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Net;

namespace GoogleGeoCode {
	public class AddressComponent {
		public string long_name { get; set; }
		public string short_name { get; set; }
		public List<string> types { get; set; }
	}   

	public class Result {
		public List<AddressComponent> address_components { get; set; }
		public string formatted_address { get; set; }
		public Geometry geometry { get; set; }
		public string place_id { get; set; }
		public List<string> types { get; set; }
		public string partial_match {get; set; }
	}   

	public class Bounds {
		public Location northeast { get; set; }
		public Location southwest { get; set; }
	}   

	public class Location {
		public double lat { get; set; }
		public double lng { get; set; }
	}   

	public class Geometry {
		public Location location { get; set; }
		public Bounds bounds { get; set; }
		public string location_type { get; set; }
		public Bounds viewport { get; set; }
	}   

	public class GeoCodeObject {
		public List<Result> results { get; set; }
		public string status { get; set; }
	}   

	public class GeoCode {
		private string _apiKey = "";

		public GeoCode(string key) {
			_apiKey = key;
		}

		private GeoCodeObject Request (string address) {
			string url = "https://maps.googleapis.com/maps/api/geocode/json?address=" +
				Uri.EscapeUriString (address) +
				"&key=" + _apiKey;
			url = url.Replace (" ", "+");

			WebClient client = new WebClient();
			var response = client.DownloadString (url);

			GeoCodeObject result = JsonConvert.DeserializeObject<GeoCodeObject> (response.ToString());
			return result;
		}

		public Location GeoCodeAddress(string address) {
			GeoCodeObject response = Request(address);
			return response.results [0].geometry.location;
		}   
	}
}
