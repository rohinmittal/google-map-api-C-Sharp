using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Net;
using googleAPI;

namespace PlaceDetail
{
	public class AddressComponent {
		public string long_name { get; set; }
		public string short_name { get; set; }
		public List<string> types { get; set; }
	}   

	public class Time {
		public int day { get; set;}
		public string time { get; set;}
	}   

	public class Periods {
		public Time close { get; set; } 
		public Time open { get; set; } 
	}   

	public class OpeningHours {
		public bool open_now { get; set; }
		public List<Periods> periods { get; set; }
		public List<String> weekday_text { get; set; }
	}   

	public class Photos {
		public int height { get; set; }
		public int weight { get; set; }
		public List<String> html_attributions { get; set; }
		public string photo_reference { get; set; }
	}   

	public class Aspect {
		public double rating { get; set; }
		public string type  { get; set; } 
	}   

	public class Review {
		public List<Aspect> aspects { get; set; }
		public string author_name  { get; set; } 
		public string author_url  { get; set; } 
		public string language  { get; set; } 
		public string profule_photo_url  { get; set; } 
		public int rating  { get; set; }
		public string text  { get; set; }
		public int time  { get; set; }
	}

	public class Result {
		public List<AddressComponent> address_components { get; set; }
		public string adr_address { get; set; }
		public string formatted_address { get; set; }
		public string formatted_phone_number { get; set; }
		public Geometry geometry { get; set; }
		public string icon { get; set; }
		public string id { get; set; }
		public string international_phone_number { get; set; }
		public string name { get; set; }
		public OpeningHours opening_hours { get; set; }
		public List<Photos> photos { get; set; }
		public string place_id { get; set; }
		public double rating { get; set; }
		public string reference { get; set; }
		public List<Review> reviews { get; set;}
		public string scope { get; set; }
		public List<string> types { get; set; }
		public string url { get; set; }
		public string website { get; set; }
		public int user_ratings_total { get; set; }
		public int utc_offset { get; set; }
		public string vicinity { get; set; }
	}

	public class AccessPoints {
		public Location location { get; set; }
		public List<string> travel_modes { get; set; }
	}

	public class Geometry {
		public List<AccessPoints> access_points { get; set; }
		public Location location { get; set; }
	}
	public class Bounds {
		public Location northeast { get; set; }
		public Location southwest { get; set; }
	}

	public class Location {
		public double lat { get; set; }
		public double lng { get; set; }
	}

	public class RootObject {
		public Result result { get; set; }
		public List<String> html_attributions { get; set; }
		public string status { get; set; }
	}

	public class PlaceDetailRequest {

		static public RootObject Request (string placeID) {
			string url = "https://maps.googleapis.com/maps/api/place/details/json?" +
				"placeid=" + placeID +
				"&key=" + googleAPI.Requests._apiKey;
			WebClient client = new WebClient();
			var response = client.DownloadString (url);

			RootObject result = JsonConvert.DeserializeObject<RootObject> (response.ToString());

			return result;
		}
	}
}
