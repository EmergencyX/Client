using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmergencyX_Client
{
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

		public NotSuccessFullLoggedInException(string message, Exception inner)
			: base(message, inner)
		{

		}
	}
}
