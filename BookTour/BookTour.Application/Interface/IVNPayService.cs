using BookTour.Application.Dto.Payment;

namespace BookTour.Application.Interface
{
    public interface IVNPayService
    {
        string CreatePaymentUrl(PaymentRequestDTO request);
        bool ValidateSignature(string signature, string rawData);
    }
}

