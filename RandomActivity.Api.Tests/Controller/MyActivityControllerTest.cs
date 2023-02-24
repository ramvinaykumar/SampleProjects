using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using RandomActivity.API.Controllers;
using RandomActivity.API.Models;
using RandomActivity.API.Services;
using Xunit;

namespace RandomActivity.Api.Tests.Controller
{
    public class MyActivityControllerTest
    {
        private Mock<ILogger<MyActivityController>> _logger;
        private Mock<IMyActivityService> _myActivityService;
        private MyActivityController _myActivityController;
        private Mock<IA55ValidatePaymentInputService> _a55ValidatePaymentInputService;
        private Mock<IMapper> _mapper;

        public void Init()
        {
            _logger = new Mock<ILogger<MyActivityController>>();
            _mapper = new Mock<IMapper>();  
            _myActivityService = new Mock<IMyActivityService>();
            _a55ValidatePaymentInputService = new Mock<IA55ValidatePaymentInputService>();
            _mapper = new Mock<IMapper>();

            _myActivityController = new MyActivityController(_logger.Object, _mapper.Object, _myActivityService.Object, _a55ValidatePaymentInputService.Object);
        }

        [Fact]
        public void A55ValidatePaymentInputService_UOB_AccountPatter_ReturnTrue()
        {
            Init();
            ValidatePaymentInputRequestDto requestDto = new ValidatePaymentInputRequestDto()
            {
                BankAccountNumber = "000-000-000-0",
                BankName = "UOB"
            };

            var responseDto = InputVaidationResponse(true);

            _a55ValidatePaymentInputService.Setup(x => x.A55ValidatePaymentInputAsync(It.IsAny<ValidatePaymentInputRequestDto>())).Returns(Task.FromResult(responseDto));

            var result = _myActivityController.A55ValidatePaymentInputAsync(requestDto);
            Assert.NotNull(result);
        }

        [Fact]
        public void A55ValidatePaymentInputService_UOB_AccountPatter_ReturnFalse()
        {
            Init();
            ValidatePaymentInputRequestDto requestDto = new ValidatePaymentInputRequestDto()
            {
                BankAccountNumber = "000-000-000-00",
                BankName = "UOB"
            };

            var responseDto = InputVaidationResponse(false);

            _a55ValidatePaymentInputService.Setup(x => x.A55ValidatePaymentInputAsync(It.IsAny<ValidatePaymentInputRequestDto>())).Returns(Task.FromResult(responseDto));

            var result = _myActivityController.A55ValidatePaymentInputAsync(requestDto);
            Assert.NotNull(result);
        }

        [Fact]
        public void A55ValidatePaymentInputService_UOB_AccountPatter_Exception()
        {
            Init();
            ValidatePaymentInputRequestDto requestDto = new ValidatePaymentInputRequestDto()
            {
                BankAccountNumber = "000-000-000-0",
                BankName = "UOBE"
            };

            _a55ValidatePaymentInputService.Setup(x => x.A55ValidatePaymentInputAsync(It.IsAny<ValidatePaymentInputRequestDto>())).Throws(new Exception());

            var result = _myActivityController.A55ValidatePaymentInputAsync(requestDto);
            Assert.ThrowsAsync<Exception>(() => result);
        }

        private ValidatePaymentInputResponseDto InputVaidationResponse(bool isValid)
        {
            return new ValidatePaymentInputResponseDto()
            {
                Guid = Guid.NewGuid().ToString(),
                IsValidInput = isValid
            };
        }
    }
}
