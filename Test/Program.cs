using Newtonsoft.Json;
using Rhino.Compute;
using System.Windows;


namespace TestCompute
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			//Console.WriteLine("Hello World!");
			ComputeServer.WebAddress = "http://localhost:6500/";
			// ComputeServer.ApiKey = "";

			//var definitionName = "speckle-get-stream.gh";
			var definitionName = "sun-hours-study.gh";
			var definitionPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
			definitionPath = Path.GetDirectoryName(definitionPath);
			definitionPath = Path.Combine(definitionPath, definitionName);
			//definitionPath = "C:\\Users\\Nathan.R.Terranova\\Downloads\\sun-hours-study.gh";


			var trees = new List<GrasshopperDataTree>();

			var value1 = new GrasshopperObject(10);
			var param1 = new GrasshopperDataTree("Radius");
			param1.Add("0", new List<GrasshopperObject> { value1 });
			trees.Add(param1);

			var value2 = new GrasshopperObject(35);
			var param2 = new GrasshopperDataTree("Count");
			param2.Add("0", new List<GrasshopperObject> { value2 });
			trees.Add(param2);

			var value3 = new GrasshopperObject(35);
			var param3 = new GrasshopperDataTree("Length");
			param3.Add("0", new List<GrasshopperObject> { value3 });
			trees.Add(param3);

			var result = Rhino.Compute.GrasshopperCompute.EvaluateDefinition(definitionPath, trees);
			var data = result[0].InnerTree.First().Value[0].Data;
			var parsed = JsonConvert.DeserializeObject<Dictionary<string, string>>(data);
			var obj = Rhino.FileIO.File3dmObject.FromJSON(parsed);

			Console.WriteLine("Result: {0}", obj.GetType());

			Console.WriteLine("Press any key to continue...");
			Console.ReadKey();
		}
	}
}