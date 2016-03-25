using System;
using System.Collections.Generic;
using googleAPI;

namespace apiRequest {
	class apiRequest {
		public static void Main (string[] args) {
			List<RadarSearch.Result> results = Requests.nearbyPlaceIDs (args[0], 16094, args[1]);
			for(int i = 0 ; i < results.Count; ++ i) {
				placeDetail detail = Requests.findPlaceDetail (results[i].place_id);
				Console.WriteLine (detail.name);
			}   
		}   
	}   
}