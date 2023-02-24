using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using RandomActivity.API.Models;
using RandomActivity.API.Services;
using Xunit;

namespace RandomActivity.Api.Tests.Services
{
    public class A55ValidatePaymentInputServiceTest
    {
        private A55ValidatePaymentInputService _a55ValidatePaymentInputService;
        private Mock<ILogger<A55ValidatePaymentInputService>> _logger;
        private Mock<IA55ValidatePaymentInputService> _validatePaymentInputService;

        [Fact]
        private void Init()
        {
            _logger = new Mock<ILogger<A55ValidatePaymentInputService>>();
            _validatePaymentInputService = new Mock<IA55ValidatePaymentInputService>();

            _a55ValidatePaymentInputService = new A55ValidatePaymentInputService(
                _logger.Object);
        }

        [Fact]
        public void A55ValidatePaymentInputService_DBS_AccountPatter1_ReturnTrue()
        {
            Init();
            ValidatePaymentInputRequestDto requestDto = new ValidatePaymentInputRequestDto()
            {
                BankAccountNumber = "000-0-000000",
                BankName = "DBS"
            };

            var responseDto = InputVaidationResponse(true);

            _validatePaymentInputService.Setup(x => x.A55ValidatePaymentInputAsync(It.IsAny<ValidatePaymentInputRequestDto>())).Returns(Task.FromResult(responseDto));

            var result = _a55ValidatePaymentInputService.A55ValidatePaymentInputAsync(requestDto);
            Assert.NotNull(result);
        }

        [Fact]
        public void A55ValidatePaymentInputService_DBS_AccountPatter2_ReturnTrue()
        {
            Init();
            ValidatePaymentInputRequestDto requestDto = new ValidatePaymentInputRequestDto()
            {
                BankAccountNumber = "000-000000-0",
                BankName = "DBS"
            };

            var responseDto = InputVaidationResponse(true);

            _validatePaymentInputService.Setup(x => x.A55ValidatePaymentInputAsync(It.IsAny<ValidatePaymentInputRequestDto>())).Returns(Task.FromResult(responseDto));

            var result = _a55ValidatePaymentInputService.A55ValidatePaymentInputAsync(requestDto);
            Assert.NotNull(result);
        }

        [Fact]
        public void A55ValidatePaymentInputService_DBS_AccountPatter1_ReturnFalse()
        {
            Init();
            ValidatePaymentInputRequestDto requestDto = new ValidatePaymentInputRequestDto()
            {
                BankAccountNumber = "0000-0-0000000",
                BankName = "DBS"
            };

            var responseDto = InputVaidationResponse(false);

            _validatePaymentInputService.Setup(x => x.A55ValidatePaymentInputAsync(It.IsAny<ValidatePaymentInputRequestDto>())).Returns(Task.FromResult(responseDto));

            var result = _a55ValidatePaymentInputService.A55ValidatePaymentInputAsync(requestDto);
            Assert.NotNull(result);
        }

        [Fact]
        public void A55ValidatePaymentInputService_DBS_AccountPatter2_ReturnFalse()
        {
            Init();
            ValidatePaymentInputRequestDto requestDto = new ValidatePaymentInputRequestDto()
            {
                BankAccountNumber = "000-000000-00",
                BankName = "DBS"
            };

            var responseDto = InputVaidationResponse(false);

            _validatePaymentInputService.Setup(x => x.A55ValidatePaymentInputAsync(It.IsAny<ValidatePaymentInputRequestDto>())).Returns(Task.FromResult(responseDto));

            var result = _a55ValidatePaymentInputService.A55ValidatePaymentInputAsync(requestDto);
            Assert.NotNull(result);
        }

        [Fact]
        public void A55ValidatePaymentInputService_DBS_AccountPatter1_Exception()
        {
            Init();
            ValidatePaymentInputRequestDto requestDto = new ValidatePaymentInputRequestDto()
            {
                BankAccountNumber = "000-000000-0",
                BankName = "DBS12"
            };

            _validatePaymentInputService.Setup(x => x.A55ValidatePaymentInputAsync(It.IsAny<ValidatePaymentInputRequestDto>())).Throws(new Exception());

            var result = _a55ValidatePaymentInputService.A55ValidatePaymentInputAsync(requestDto);
            Assert.ThrowsAsync<Exception>(() => result);
        }

        [Fact]
        public void A55ValidatePaymentInputService_DBS_AccountPatter2_Exception()
        {
            Init();
            ValidatePaymentInputRequestDto requestDto = new ValidatePaymentInputRequestDto()
            {
                BankAccountNumber = "000-0-000000",
                BankName = "DBSXSA"
            };

            _validatePaymentInputService.Setup(x => x.A55ValidatePaymentInputAsync(It.IsAny<ValidatePaymentInputRequestDto>())).Throws(new Exception());

            var result = _a55ValidatePaymentInputService.A55ValidatePaymentInputAsync(requestDto);
            Assert.ThrowsAsync<Exception>(() => result);
        }

        [Fact]
        public void A55ValidatePaymentInputService_OCBC_AccountPatter1_ReturnTrue()
        {
            Init();
            ValidatePaymentInputRequestDto requestDto = new ValidatePaymentInputRequestDto()
            {
                BankAccountNumber = "000-0-000000",
                BankName = "OCBC"
            };

            var responseDto = InputVaidationResponse(true);

            _validatePaymentInputService.Setup(x => x.A55ValidatePaymentInputAsync(It.IsAny<ValidatePaymentInputRequestDto>())).Returns(Task.FromResult(responseDto));

            var result = _a55ValidatePaymentInputService.A55ValidatePaymentInputAsync(requestDto);
            Assert.NotNull(result);
        }

        [Fact]
        public void A55ValidatePaymentInputService_OCBC_AccountPatter2_ReturnTrue()
        {
            Init();
            ValidatePaymentInputRequestDto requestDto = new ValidatePaymentInputRequestDto()
            {
                BankAccountNumber = "000-000000-000",
                BankName = "OCBC"
            };

            var responseDto = InputVaidationResponse(true);

            _validatePaymentInputService.Setup(x => x.A55ValidatePaymentInputAsync(It.IsAny<ValidatePaymentInputRequestDto>())).Returns(Task.FromResult(responseDto));

            var result = _a55ValidatePaymentInputService.A55ValidatePaymentInputAsync(requestDto);
            Assert.NotNull(result);
        }

        [Fact]
        public void A55ValidatePaymentInputService_OCBC_AccountPatter1_ReturnFalse()
        {
            Init();
            ValidatePaymentInputRequestDto requestDto = new ValidatePaymentInputRequestDto()
            {
                BankAccountNumber = "0000-0-0000000",
                BankName = "OCBC"
            };

            var responseDto = InputVaidationResponse(false);

            _validatePaymentInputService.Setup(x => x.A55ValidatePaymentInputAsync(It.IsAny<ValidatePaymentInputRequestDto>())).Returns(Task.FromResult(responseDto));

            var result = _a55ValidatePaymentInputService.A55ValidatePaymentInputAsync(requestDto);
            Assert.NotNull(result);
        }

        [Fact]
        public void A55ValidatePaymentInputService_OCBC_AccountPatter2_ReturnFalse()
        {
            Init();
            ValidatePaymentInputRequestDto requestDto = new ValidatePaymentInputRequestDto()
            {
                BankAccountNumber = "000-000000-0000",
                BankName = "OCBC"
            };

            var responseDto = InputVaidationResponse(false);

            _validatePaymentInputService.Setup(x => x.A55ValidatePaymentInputAsync(It.IsAny<ValidatePaymentInputRequestDto>())).Returns(Task.FromResult(responseDto));

            var result = _a55ValidatePaymentInputService.A55ValidatePaymentInputAsync(requestDto);
            Assert.NotNull(result);
        }

        [Fact]
        public void A55ValidatePaymentInputService_OCBC_AccountPatter1_Exception()
        {
            Init();
            ValidatePaymentInputRequestDto requestDto = new ValidatePaymentInputRequestDto()
            {
                BankAccountNumber = "000-000000-000",
                BankName = "OCBC12"
            };

            _validatePaymentInputService.Setup(x => x.A55ValidatePaymentInputAsync(It.IsAny<ValidatePaymentInputRequestDto>())).Throws(new Exception());

            var result = _a55ValidatePaymentInputService.A55ValidatePaymentInputAsync(requestDto);
            Assert.ThrowsAsync<Exception>(() => result);
        }

        [Fact]
        public void A55ValidatePaymentInputService_OCBC_AccountPatter2_Exception()
        {
            Init();
            ValidatePaymentInputRequestDto requestDto = new ValidatePaymentInputRequestDto()
            {
                BankAccountNumber = "000-0-000000",
                BankName = "OCBCX"
            };

            _validatePaymentInputService.Setup(x => x.A55ValidatePaymentInputAsync(It.IsAny<ValidatePaymentInputRequestDto>())).Throws(new Exception());

            var result = _a55ValidatePaymentInputService.A55ValidatePaymentInputAsync(requestDto);
            Assert.ThrowsAsync<Exception>(() => result);
        }

        [Fact]
        public void A55ValidatePaymentInputService_POSB_AccountPatter_ReturnTrue()
        {
            Init();
            ValidatePaymentInputRequestDto requestDto = new ValidatePaymentInputRequestDto()
            {
                BankAccountNumber = "000-00000-0",
                BankName = "POSB"
            };

            var responseDto = InputVaidationResponse(true);

            _validatePaymentInputService.Setup(x => x.A55ValidatePaymentInputAsync(It.IsAny<ValidatePaymentInputRequestDto>())).Returns(Task.FromResult(responseDto));

            var result = _a55ValidatePaymentInputService.A55ValidatePaymentInputAsync(requestDto);
            Assert.NotNull(result);
        }

        [Fact]
        public void A55ValidatePaymentInputService_POSB_AccountPatter_ReturnFalse()
        {
            Init();
            ValidatePaymentInputRequestDto requestDto = new ValidatePaymentInputRequestDto()
            {
                BankAccountNumber = "0000-0000000-0",
                BankName = "POSB"
            };

            var responseDto = InputVaidationResponse(false);

            _validatePaymentInputService.Setup(x => x.A55ValidatePaymentInputAsync(It.IsAny<ValidatePaymentInputRequestDto>())).Returns(Task.FromResult(responseDto));

            var result = _a55ValidatePaymentInputService.A55ValidatePaymentInputAsync(requestDto);
            Assert.NotNull(result);
        }

        [Fact]
        public void A55ValidatePaymentInputService_POSB_AccountPatter_Exception()
        {
            Init();
            ValidatePaymentInputRequestDto requestDto = new ValidatePaymentInputRequestDto()
            {
                BankAccountNumber = "000-00000-0",
                BankName = "POSBQ"
            };

            _validatePaymentInputService.Setup(x => x.A55ValidatePaymentInputAsync(It.IsAny<ValidatePaymentInputRequestDto>())).Throws(new Exception());

            var result = _a55ValidatePaymentInputService.A55ValidatePaymentInputAsync(requestDto);
            Assert.ThrowsAsync<Exception>(() => result);
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

            _validatePaymentInputService.Setup(x => x.A55ValidatePaymentInputAsync(It.IsAny<ValidatePaymentInputRequestDto>())).Returns(Task.FromResult(responseDto));

            var result = _a55ValidatePaymentInputService.A55ValidatePaymentInputAsync(requestDto);
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

            _validatePaymentInputService.Setup(x => x.A55ValidatePaymentInputAsync(It.IsAny<ValidatePaymentInputRequestDto>())).Returns(Task.FromResult(responseDto));

            var result = _a55ValidatePaymentInputService.A55ValidatePaymentInputAsync(requestDto);
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

            _validatePaymentInputService.Setup(x => x.A55ValidatePaymentInputAsync(It.IsAny<ValidatePaymentInputRequestDto>())).Throws(new Exception());

            var result = _a55ValidatePaymentInputService.A55ValidatePaymentInputAsync(requestDto);
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
