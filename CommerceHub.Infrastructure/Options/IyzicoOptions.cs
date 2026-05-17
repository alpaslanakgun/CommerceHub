using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommerceHub.Infrastructure.Options
{
	public class IyzicoOptions:Iyzipay.Options
	{
		public const string SectionName = "Iyzico";
		public string  ApiKey { get; set; }=string.Empty;
		public string  SecretKey { get; set; }= string.Empty;
		public string BaseUrl { get; set; } = "https://sandbox-api.iyzipay.com\r\n";

	}
}
