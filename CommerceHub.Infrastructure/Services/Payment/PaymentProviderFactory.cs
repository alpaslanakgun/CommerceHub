using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommerceHub.Application.Interfaces.Payment;
using CommerceHub.Infrastructure.Options;

namespace CommerceHub.Infrastructure.Services.Payment
{
	public static class PaymentProviderFactory 
	{

		public static IPaymentProvider Create(string providerName="fake",IyzicoOptions? iyzicoOptions=null)
		{
			return providerName.ToLower() switch
			{
				"fake" => new FakePaymentProvider(),
				"iyzico"=> new IyzicoPaymentProvider(iyzicoOptions!),
				_ => throw new InvalidOperationException($"Bilinmeyen ödeme sağlayıcısı :{providerName} ")
			};
		}

		
	}
}
