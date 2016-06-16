using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using EmergencyX_Client;

namespace EmergencyX_Client
{
	/// <summary>
	/// Interaktionslogik für LoginWindow.xaml
	/// </summary>
	public partial class LoginWindow : Window
	{
		DataPool dataContext {
			get { return DataContext as DataPool; }
		}
		
		public LoginWindow()
		{
			InitializeComponent();
		}

		private void btnLogin_Click(object sender, RoutedEventArgs e)
		{
			dataContext.loginWithUsernameUpdate(txbUsername.Text, txbPassword.Password, cbxRememberMe.IsChecked.Value);
			this.Close();
		}
	}
}
