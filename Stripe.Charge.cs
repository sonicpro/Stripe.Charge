using System;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;

internal static class program
{
	public static void Main(string[] args)
	{
		if(args.Length != 1)
		{
			string moduleName = Path.GetFileNameWithoutExtension(System.Reflection.Assembly.GetEntryAssembly().Location);
			string usage = "Usage:{0}{1} secrettoken";
			Console.WriteLine(usage, Environment.NewLine, moduleName);
			return;
		}
		
		const string host = "api.stripe.com";
		const string resource = "/v1/charges";
		const string amount = "400"; // Has not used so far.
		
		Uri uri = new Uri(String.Format("{0}{1}{2}{3}", Uri.UriSchemeHttps, Uri.SchemeDelimiter, host, resource));
		NetworkCredential credentials = new NetworkCredential(args[0], String.Empty);
		
		HttpWebRequest req = (HttpWebRequest)WebRequest.Create(uri);
		req.Credentials = credentials;
		req.UserAgent = "curl/7.39.0";
		req.Accept = "*/*";

		WebResponse res = req.GetResponse();
		
		Console.WriteLine(getResponse(res));
	}
	
	private static string getResponse(WebResponse response)
	{
		Stream respStream = response.GetResponseStream();
		using(StreamReader sr = new StreamReader(respStream, Encoding.UTF8))
		{
			return sr.ReadToEnd();
		}
	}
}