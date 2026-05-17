using CommerceHub.Application.Interfaces.Payment;
using CommerceHub.Infrastructure.Options;
using Iyzipay;
using Iyzipay.Model;
using Iyzipay.Request;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommerceHub.Infrastructure.Services.Payment
{
	public class IyzicoPaymentProvider : IPaymentProvider
	{

		private readonly Iyzipay.Options _iyzicoOptions;

		public IyzicoPaymentProvider(IyzicoOptions iyzicoOptions)
		{
			_iyzicoOptions = new Iyzipay.Options
			{
				ApiKey = iyzicoOptions.ApiKey,
				SecretKey = iyzicoOptions.SecretKey,
				BaseUrl = iyzicoOptions.BaseUrl,
			};
		}

		public async  Task<PaymentProviderResult> ChargeAsync(PaymentProviderRequest paymentProviderRequest)
		{
			var paymentRequest = new CreatePaymentRequest
			{
				Locale = Locale.TR.ToString(),
				ConversationId = Guid.NewGuid().ToString(),
				Price = paymentProviderRequest.Amount.ToString("F2"),
				PaidPrice = paymentProviderRequest.Amount.ToString("F2"),
				Currency = Currency.TRY.ToString(),
				Installment = 1,
				BasketId = Guid.NewGuid().ToString(),
				PaymentChannel = PaymentChannel.WEB.ToString(),
				PaymentGroup = PaymentGroup.PRODUCT.ToString(),
				PaymentCard = new PaymentCard
				{
					CardHolderName = paymentProviderRequest.CardHolderName,
					CardNumber = paymentProviderRequest.CardNumber,
					ExpireMonth = paymentProviderRequest.ExpiryMonth,
					ExpireYear = paymentProviderRequest.ExpiryYear,
					Cvc = paymentProviderRequest.Cvv,
					RegisterCard = 0
				},
				Buyer = new Buyer
				{
					Id = "B001",
					Name = paymentProviderRequest.CardHolderName.Split(',')[0],
					Surname = paymentProviderRequest.CardHolderName.Split(',').Last(),
					Email = "musteri@commercehub",
					IdentityNumber = "12345678911",
					RegistrationAddress = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1",
					City = "İstanbul",
					Country = "Turkiye",
					ZipCode = "34732",
					Ip = "127.0.0.1"
				},
				ShippingAddress = new Address
				{
					ContactName = paymentProviderRequest.CardHolderName,
					City = "İstanbul",
					Country = "Turkiye",
					Description = "TEST ADRES"

				},
				BillingAddress = new Address
				{
					ContactName = paymentProviderRequest.CardHolderName,
					City = "İstanbul",
					Country = "Turkiye",
					Description = "TEST ADRES"
				},

				BasketItems = new List<BasketItem>
				 {
					 new BasketItem
					 {
						 Id="B001",
						 Name="Sipariş Ödemesi ",
						 Category1="Genel",
						 ItemType=BasketItemType.PHYSICAL.ToString(),
						 Price=paymentProviderRequest.Amount.ToString()
					 }
				 }



			};


			var payment = await Task.Run(() =>
			   Iyzipay.Model.Payment.Create(paymentRequest,_iyzicoOptions));

			if (payment.Status == "success")
			{
				return new PaymentProviderResult(true,payment.PaymentId, null);
			}
			return new PaymentProviderResult(false, null,payment.ErrorMessage);
		}
	}
}
