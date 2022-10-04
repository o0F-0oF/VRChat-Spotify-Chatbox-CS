using SharpOSC;
using System.Diagnostics;

class Program
{
	public static UDPSender sender = new UDPSender("127.0.0.1", 8000);
	public static string spotifySong = "";
	public static int delay = 1500; // 1 sec == 1000 ms

	static void Main(string[] args)
	{
		repeat();
		Console.ReadLine();
	}
	public static void getSongPlaying()
	{
		foreach (var proc in Process.GetProcessesByName("Spotify")) //get every process with name Spotify  
		{
			var title = proc.MainWindowTitle; //get the window title
			if (title != "") //check if the window title isn't blank
			{
				spotifySong = title;
				Console.WriteLine(title);
			}
		}
	}
	public static void sendMessage(string text) 
	{
		var message = new OscMessage("/chatbox/input", $"Currently Playing:\v{text}\v(Artist - Song)", true);
		Console.WriteLine("sent!");
		sender.Send(message);
	}
	public static async void repeat() 
	{
        while (true)
        {
			getSongPlaying();
			sendMessage(spotifySong);
			await Task.Delay(delay);
		}
	}
}