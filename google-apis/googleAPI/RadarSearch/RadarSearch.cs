using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Net;
using googleAPI;

namespace RadarSearch {

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

	public class RootObject {
		public List<Result> results { get; set; }
		public List<Result> html_attributions { get; set; }
		public string status { get; set; }
	}   

	public class RadarSearchRequest {

		static public RootObject Request (double lat, double lng, int radius, string placeType) {
			string url = "https://maps.googleapis.com/maps/api/place/radarsearch/json?location=" +
			             lat.ToString () + "," +
			             lng.ToString () +
			             "&radius=" + radius.ToString () +
			             "&keyword=indian&type=" +
			             placeType + "&key=" +
			             googleAPI.Requests._apiKey;
			
			WebClient client = new WebClient();
			var response = client.DownloadString (url);

			RootObject result = JsonConvert.DeserializeObject<RootObject> (response.ToString());

			// returns the placeId object
			return result;
		}
	}
}