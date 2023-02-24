using RandomActivity.API.Models;

namespace RandomActivity.API.Services
{
    public interface IA55ValidatePaymentInputService
    {
        Task<ValidatePaymentInputResponseDto> A55ValidatePaymentInputAsync(ValidatePaymentInputRequestDto requestDto);
    }
}
