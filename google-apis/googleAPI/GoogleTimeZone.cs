using System;
using System.Net;
using Newtonsoft.Json;

namespace GoogleLocalTime {
	public class LocalTimeObject {
		public int dstOffset { get; set;}
		public int rawOffset { get; set;}
		public string status { get; set;}
		public string timeZoneId { get; set;}
		public string timeZoneName { get; set;}
	}  

	public class LocalTime {
		private string _apiKey = "";

		public LocalTime(string key) {
			_apiKey = key;
		}
			
		private LocalTimeObject Request(double lat, double lng) {
			string url = "https://maps.googleapis.com/maps/api/timezone/json?location=" + 
				lat.ToString() + "," +
				lng.ToString() + 
				"&timestamp=0" +
				"&key=" + _apiKey;

			WebClient client = new WebClient();
			var response = client.DownloadString (url);
			LocalTimeObject result = JsonConvert.DeserializeObject<LocalTimeObject> (response.ToString());
			return result;
		}

		public DateTime GetLocalTime(double lat, double lng) {
			DateTime utcTime = DateTime.UtcNow;
			try {
				LocalTimeObject timeZone = Request (lat, lng);
				TimeZoneInfo timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(timeZone.timeZoneId);
				DateTime lTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, timeZoneInfo);
				utcTime = lTime;
			}   
			catch (TimeZoneNotFoundException) {
				Console.WriteLine ("Error");
			}   

			// if there is some exception, return utc time only. 
			return utcTime;
		}  
	}
}

