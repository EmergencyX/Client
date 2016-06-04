using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.NetworkInformation;
using System.Net;
using Grpc.Core;
using Grpc.Core.Logging;
using EmergencyXService;

namespace EmergencyX_Client
{
	public class Login
	{

		public void Test()
		{
			//Some ping debug test stuff
			//
			Ping testPing = new Ping();
						
			string dataBuffer = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
			byte[] buffer = Encoding.ASCII.GetBytes(dataBuffer);
			int timeout = 10000;

			PingReply ereg = testPing.Send("beta.emergencyx.de", timeout, buffer); 

			if(ereg.Status == IPStatus.Success)
			{
				MessageBox.Show("Success");
			}
			else
			{
				MessageBox.Show(ereg.Status.ToString());
			}
			//
			// end debug test stuff

			// SSL Crt (should been placed in Solution Dir with Build Option Copy always)
			//
			SslCredentials cred = new SslCredentials(File.ReadAllText("server.crt"));
			
			// new Channel and then a new client based on that Channel
			//
			var connectionChannel = new Channel("beta.emergencyx.de:50051/EmergencyExplorerService", cred);
			var emx = EmergencyExplorerService.NewClient(connectionChannel);

			// For testing only hardcoded login informations
			//
			LoginRequest request = new LoginRequest { Username = "", Password = "", RememberMe = true };
			LoginResponse response = emx.Login(request);


			//All done
			//
			connectionChannel.ShutdownAsync().Wait();

		}
	}
}
