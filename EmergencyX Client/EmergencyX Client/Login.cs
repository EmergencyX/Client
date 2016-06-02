using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grpc.Core;
using EmergencyXService;

namespace EmergencyX_Client
{
	public class Login
	{
		public void Test() { 
			var connectionChannel = new Channel("90.211.60.220:50051", ChannelCredentials.Insecure);
			var emx = EmergencyExplorerService.NewClient(connectionChannel);

			var login = new LoginRequest();
			login.Password = "fu";
			login.Username = "ciajoe";
			login.RememberMe = true;
			var callSettings = new CallOptions();

			LoginResponse answer = new LoginResponse();
			answer = emx.Login(login, callSettings);

			//AsyncUnaryCall<LoginResponse> response = emx.LoginAsync(login,callSettings);

			//MessageBox.Show(response.GetStatus().ToString());
			MessageBox.Show(answer.ToString());

			connectionChannel.ShutdownAsync().Wait();
		}
	}
}
