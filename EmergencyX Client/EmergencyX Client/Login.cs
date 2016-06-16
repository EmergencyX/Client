using System;
using System.Windows;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.NetworkInformation;
using System.Net;
using System.Runtime.CompilerServices;
using Grpc.Core;
using EmergencyXService;

namespace EmergencyX_Client
{

	public class Login : INotifyPropertyChanged
	{


		//implement Property Event
		//
		public event PropertyChangedEventHandler PropertyChanged;

		// static member stuff
		//
		private int userId;
		private string token;
		private string userName;

		// Property Stuff...
		//
		public string Token
		{
			get
			{
				return token;
			}

			set
			{
				token = value;
				NotifyPropertyChanged();
			}
		}
		public int UserId
		{
			get
			{
				return userId;
			}

			set
			{
				userId = value;
				NotifyPropertyChanged();
			}
		}
		public string UserName
		{
			get
			{
				return userName;
			}

			set
			{
				userName = value;
				NotifyPropertyChanged();
			}
		}


		/// <summary>
		/// Logs the user with the given data in
		/// </summary>
		/// <param name="username">User provided username</param>
		/// <param name="password">User's password</param>
		/// <param name="remember">Remain logged in or not</param>
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
			LoginRequest request = new LoginRequest { Username = username, Password = password, RememberMe = remember };
			LoginResponse response = await emx.LoginAsync(request);

			//Save the responded date to re-login the user every program session until he logs out
			//
			AppConfig.writeToAppConfig("rememberMe",remember.ToString());
			AppConfig.writeToAppConfig("userId", response.UserId.ToString());
			AppConfig.writeToAppConfig("token", response.Token);
			AppConfig.writeToAppConfig("username", request.Username);

			//All done
			//
			connectionChannel.ShutdownAsync().Wait();
			if(response.Success != true)
			{
				throw new NotSuccessFullLoggedInException();
			}
			
		}

		/// <summary>
		/// Logs the user asyncrouns in. No parmaeter needed because the needed data is stored in app.config
		/// </summary>
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

			connectionChannel.ShutdownAsync().Wait();

			if(response.Success != true)
			{
				throw new NotSuccessFullLoggedInException();
			}

		}


		// Implement NotifyPropertyChanged
		//
		public void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
