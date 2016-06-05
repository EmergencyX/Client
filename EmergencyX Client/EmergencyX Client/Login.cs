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

		public async void FullLogin(string username, string password, bool remember)
		{
			
			// SSL Crt (should been placed in Solution Dir with Build Option Copy always)
			//
			SslCredentials cred = new SslCredentials(File.ReadAllText("server.crt"));
			
			// new Channel and then a new client based on that Channel
			//
			var connectionChannel = new Channel("beta.emergencyx.de:50051", cred);
			var emx = EmergencyExplorerService.NewClient(connectionChannel);

			// For testing only hardcoded login informations
			//
			LoginRequest request = new LoginRequest { Username = "ciajoe", Password = "fu", RememberMe = remember };
			LoginResponse response = await emx.LoginAsync(request);

			//Save the responded date to re-login the user every program session until he logs out
			//
			AppConfig.writeToAppConfig("rememberMe",remember.ToString());
			AppConfig.writeToAppConfig("userId", response.UserId.ToString());
			AppConfig.writeToAppConfig("token", response.Token); 

			//All done
			//
			connectionChannel.ShutdownAsync().Wait();

			if(response.Success != true)
			{
				throw new NotSuccessFullLoggedInException();
			}
			
		}

		public async void TokenLogin()
		{
			// SSL Crt (should been placed in Solution Dir with Build Option Copy always)
			//
			SslCredentials cred = new SslCredentials(File.ReadAllText("server.crt"));

			// new Channel and then a new client based on that Channel
			//
			var connectionChannel = new Channel("beta.emergencyx.de:50051", cred);
			var emx = EmergencyExplorerService.NewClient(connectionChannel);

			LoginWithTokenRequest request = new LoginWithTokenRequest { UserId = Convert.ToUInt32(AppConfig.readFromAppConfig("userId")), Token = AppConfig.readFromAppConfig("token") };
			LoginResponse response = await emx.LoginWithTokenAsync(request);
			MessageBox.Show(response.Success + " | " + response.UserId + " | " + response.Token);


			connectionChannel.ShutdownAsync().Wait();

			if(response.Success != true)
			{
				throw new NotSuccessFullLoggedInException();
			}

		}
	}

	/// <summary>
	/// Basic Exception to check wheater login was successfull or not
	/// </summary>
	public class NotSuccessFullLoggedInException : Exception
	{
		public NotSuccessFullLoggedInException()
		{

		}
		
		public NotSuccessFullLoggedInException(string message)
			: base(message)
		{

		}

		public NotSuccessFullLoggedInException(string message,Exception inner)
			: base (message, inner)
		{

		}
	}
}
