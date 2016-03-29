using System;
using System.Net;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace GoogleDistanceMatrix {
	public class Distance {
		public string text { get; set;}
		public string value { get; set;}
	}

	public class Duration {
		public string text { get; set;}
		public string value { get; set;}
	}

	public class Elements {
		public Distance distance { get; set; }
		public Duration duration { get; set; }
		public string status { get; set;}
	}

	public class Rows {
		public List<Elements> elements { get; set; }
	}

	public class DistanceMatrixObject {
		public List<string> destination_addresses { get; set;}
		public List<string> origin_addresses { get; set;}
		public List<Rows> rows { get; set;}
		public string status { get; set;}
	}

	public class DistanceMatrix {
		private string _apiKey = "";

		public DistanceMatrix(string key) {
			_apiKey = key;
		}

		private DistanceMatrixObject Request (List<String> origins, List<String> destinations, string mode) {
			string originURL = "";
			for (int i = 0; i < origins.Count; ++i) {
				originURL = originURL + Uri.EscapeUriString (origins [i]) + "|";  
			}

			string destURL = "";
			for (int i = 0; i < origins.Count; ++i) {
				destURL = destURL + Uri.EscapeUriString (destinations [i]) + "|";  
			}

			string url = "https://maps.googleapis.com/maps/api/distancematrix/json?" +
				"units=imperial" +
				"origins=" + originURL +
				"&destinations=" + destURL +
				"&mode=" + mode +
				"&key=" + _apiKey;
			Console.WriteLine (url);
			WebClient client = new WebClient();
			var response = client.DownloadString (url);

			DistanceMatrixObject result = JsonConvert.DeserializeObject<DistanceMatrixObject> (response.ToString());
			return result;
		}

		public DistanceMatrixObject DistanceMatrixes(List<String> origins, List<String> destinations, string mode) {
			return Request(origins, destinations, mode);
		}
	}
}

