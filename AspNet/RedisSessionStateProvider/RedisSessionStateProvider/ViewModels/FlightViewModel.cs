using System;
using System.Runtime.Serialization;

namespace RedisSessionStateProvider.ViewModels
{
	[Serializable]
	public class FlightViewModel
	{
		[DataMember]
		public string FlightNumber { get; set; }
	}
}