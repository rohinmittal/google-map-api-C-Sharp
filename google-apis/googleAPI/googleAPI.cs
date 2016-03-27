using System;
using System.Net;
using GeoCode;
using RadarSearch;
using PlaceDetail;
using TimeZone;
using System.Collections.Generic;

namespace googleAPI {
	class placeDetail {
		public placeDetail(string _placeID, string _name, string _address, string _phoneNumber, string _website) {
			this.placeID = _placeID;
			this.address = _address;
			this.name = _name;
			this.phoneNumber = _phoneNumber;
			this.website = _website;
		}   

		public string placeID { get; set; }
		public string name { get; set; }
		public string address { get; set; }
		public string phoneNumber { get; set; }
		public string website { get; set; }
	}   

	class Requests {    
		public static string _apiKey = "<key>";

		// accepts an address and returns its geocoded lat,lng tuple
		public static void geoCode(string address, out double lat, out double lng) {
			GeoCode.RootObject response = GeoCodeRequest.Request(address);
			lat = response.results[0].geometry.location.lat;
			lng = response.results[0].geometry.location.lng;
		}   

		// accepts an address and returns its geocoded lat,lng tuple
		public static List<RadarSearch.Result> nearbyPlaceIDs(string address, int radius, string placeType) {

			// find the lng/lat of address
			double lng, lat;
			geoCode (address, out lat, out lng);

			RadarSearch.RootObject response = RadarSearchRequest.Request(lat, lng, radius, placeType);
			return response.results;
		}   

		public static placeDetail findPlaceDetail(string placeID) {
			PlaceDetail.RootObject response = PlaceDetailRequest.Request(placeID);

			placeDetail place = new placeDetail (response.result.place_id, response.result.name, response.result.formatted_address, response.result.formatted_phone_number, response.result.website);
			return place;
		}

		public static DateTime getLocalTime(double lat, double lng) {
			DateTime utcTime = DateTime.UtcNow;
			try {
				string timeZoneID = TimeZoneRequest.Request (lat, lng);
				TimeZoneInfo timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(timeZoneID);
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
