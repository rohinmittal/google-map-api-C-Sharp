using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Net;

namespace GoogleRadarSearch {

	public class Result {
		public Geometry geometry { get; set; }
		public string id { get; set; }
		public string place_id { get; set; }
		public string reference { get; set; }
	}   

	public class Geometry {
		public Location location { get; set; }
	}   

	public class Location {
		public double lat { get; set; }
		public double lng { get; set; }
	}   

	public class RadarSearchObject {
		public List<Result> results { get; set; }
		public List<Result> html_attributions { get; set; }
		public string status { get; set; }
	}   

	public class RadarSearch {
		private string _apiKey = "";

		public RadarSearch(string key) {
			_apiKey = key;
		}

		private RadarSearchObject Request (double lat, double lng, int radius, string placeType) {
			string url = "https://maps.googleapis.com/maps/api/place/radarsearch/json?location=" +
			             lat.ToString () + "," +
			             lng.ToString () +
			             "&radius=" + radius.ToString () +
			             "&keyword=indian&type=" +
			             placeType + "&key=" + _apiKey;
			
			WebClient client = new WebClient();
			var response = client.DownloadString (url);

			RadarSearchObject result = JsonConvert.DeserializeObject<RadarSearchObject> (response.ToString());
			return result;
		}

		public List<Result> PlaceIDs(double lat, double lng, int radius, string placeType) {
			RadarSearchObject response = Request(lat, lng, radius, placeType);
			return response.results;
		}  
	}
}