using AccountModule.ApiResponse;
using AccountModule.Controllers;
using AccountModule.Data;
using AccountModule.Model;
using AccountModule.Repository;
using AccountModule.ResponseModels;
using AccountModule.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Transaction = AccountModule.Model.Transaction;

namespace BankServiceUnitTest
{
    public class UnitTest1
    {
        private readonly AccountContext _dbContext;
        private readonly IAccountService _account;
        private readonly IAccountRepository _accountRepository;
        private readonly ILogger<AccountController> _logger;
       
        public UnitTest1()
        {
            
        }
        [Fact]
        public async Task Get_AccountDetail_ListData()
        {
            //Arrange
            AccountController controller = new AccountController(_account, _logger);
            //  AccountDetailResponse Acc= new AccountDetailResponse();

            var loggerMock = new Mock<ILogger<AccountController>>();
            var accountServiceMock = new Mock<IAccountService>();
            var Data = new AccountController(accountServiceMock.Object, loggerMock.Object);

            var AccountDetailData = new List<AccountDetailResponse> {
          new AccountDetailResponse
          {
              AccountId=1,
              AccountTypeId=1,
              CurrentBalance=190,
              Status=1,
              OpenDate=DateTime.Now,
              ClosedDate=DateTime.Now,
              AccountTypeName="Current",
              UserId=1,
              UserName="Shree"
          }
     };
            accountServiceMock.Setup(service => service.GetAccountDetailList()).ReturnsAsync(AccountDetailData);
            // Act
            var result = await Data.Get();
            // Assert
            Assert.IsType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            var response = okResult.Value as ApiResponse<dynamic>;
            Assert.NotNull(response);
            //Assert.Equal(, response.Status);
        }
        [Fact]
        public void Test1()
        {
            AccountController account = new (_account, _logger);
            account.Get();
        }
        [Theory]
        [InlineData(5)]
        [InlineData(10)]
        [InlineData(-1)]
        public void GetAccountDetailById(long id)
        {
            //arrange
            AccountController account = new(_account, _logger);
            //act
            account.GetAccountDetailByid(id);

            //assert
            //as
        }
        [Fact]
        public void PositiveResult_GetAccountDetailByid()
        {
            //arrange
            AccountController account = new(_account, _logger);
            long id = 2;
            //act
            account.GetAccountDetailByid(id);
           
            //assert
            //as
        }
        [Fact]
        public void AddAccountDetail()
        {
            AccountDetails accountDetails =new AccountDetails();
            AccountController account = new(_account, _logger);
            account.AddAccountDetail(accountDetails);
        }
        [Fact]
        public void UpdateAccountDetail()
        {
            AccountDetails accountDetails = new AccountDetails();
            AccountController account = new(_account, _logger);
            account.UpdateAccountDetail(accountDetails);
        }
        [Fact]
        public void GetTransactionList()
        {
            AccountDetails accountDetails = new AccountDetails();
            AccountController account = new(_account, _logger);
            account.TransactionList();
        }
        [Fact]
        public void GetTransaction()
        {
            AccountDetails accountDetails = new AccountDetails();
            AccountController account = new(_account, _logger);
            account.GetTransaction(4);
        }
        [Fact]
        public void AddTransaction()
        {
            Transaction transaction = new Transaction();
            AccountController account = new(_account, _logger);
            account.AddTransaction(transaction);
        }
        [Fact]
        public void UpdateTransaction()
        {
            Transaction transaction = new Transaction();
            AccountController account = new(_account, _logger);
            account.UpdateTransaction(transaction);
        }
    }
}