using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Net;
using googleAPI;

namespace GeoCode {
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

	public class RootObject {
		public List<Result> results { get; set; }
		public string status { get; set; }
	}   

	public class GeoCodeRequest {
		static public RootObject Request (string address) {
			string url = "https://maps.googleapis.com/maps/api/geocode/json?address=" +
				Uri.EscapeUriString (address) +
				"&key=" + googleAPI.Requests._apiKey;;
			url = url.Replace (" ", "+");

			WebClient client = new WebClient();
			var response = client.DownloadString (url);

			RootObject result = JsonConvert.DeserializeObject<RootObject> (response.ToString());
			return result;
		}
	}
}
