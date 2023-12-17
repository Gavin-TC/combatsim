using Combatsim.Client;

namespace Combatsim
{
	public class Program
	{
		public static void Main(string[] args)
		{
			Client.Client client = new Client.Client();
			client.startGame();
		}
	}
}