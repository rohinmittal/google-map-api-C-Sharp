using System;
using googleAPI;
using System.Net;
using Newtonsoft.Json;

namespace TimeZone {

	public class RootObject {
		public int dstOffset { get; set;}
		public int rawOffset { get; set;}
		public string status { get; set;}
		public string timeZoneId { get; set;}
		public string timeZoneName { get; set;}
	}  

	public class TimeZoneRequest {
		public static string Request(double lat, double lng) {
			string url = "https://maps.googleapis.com/maps/api/timezone/json?location=" + 
				lat.ToString() + "," +
				lng.ToString() + 
				"&timestamp=0" +
				"&key=" + googleAPI.Requests._apiKey;

			WebClient client = new WebClient();
			var response = client.DownloadString (url);
			RootObject result = JsonConvert.DeserializeObject<RootObject> (response.ToString());
			return result.timeZoneId;
		}   
	}
}

