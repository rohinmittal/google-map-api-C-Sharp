using System;
using System.Collections.Generic;

using GoogleGeoCode;
using GoogleRadarSearch;
using GooglePlaceDetail;
using GoogleLocalTime;
using GoogleDistanceMatrix;

namespace apiRequest {
	class apiRequest {
		public static string _apiKey = "<key>";

		public static void Main (string[] args) {
			if (args.Length != 2) {
				Console.WriteLine ("Error");
				return;
			}

			// geocode
			GeoCode gcObj = new GeoCode (_apiKey);
			GoogleGeoCode.Location location = gcObj.GeoCodeAddress (args [0]);

			// timezone api to find local time.
			LocalTime ltObj = new LocalTime(_apiKey);
			DateTime localTime = ltObj.GetLocalTime (location.lat, location.lng);
			Console.WriteLine (localTime);

			// radar search to find placeIDs of nearby places
			RadarSearch rsObj = new RadarSearch (_apiKey);
			List<GoogleRadarSearch.Result> placeIDs = rsObj.PlaceIDs (location.lat, location.lng, 16094, args[1]);

			// for each place id, find the details
			PlaceDetail pdObj = new PlaceDetail (_apiKey);
			for(int i = 0 ; i < placeIDs.Count; ++ i) {
				GooglePlaceDetail.Result info = pdObj.PlaceInfo (placeIDs[i].place_id);
				Console.WriteLine (info.name);
			}

			// distance matrix to compute the distance based on mode and other factors
			//DistanceMatrix dmObj = new DistanceMatrix (_apiKey);
			//DistanceMatrixObject response = dmObj.DistanceMatrixes ();
		}   
	}   
}