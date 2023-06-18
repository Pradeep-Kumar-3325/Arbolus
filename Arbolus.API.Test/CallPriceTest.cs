using Arbolus.API.Controllers;
using Arbolus.Model;
using Arbolus.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http.Results;
using Xunit;

namespace Arbolus.API.Test
{
    public class CallPriceTest
    {
        private readonly Mock<ILogger<CallPrice>> logger;

        private readonly Mock<ICallService> callService;

        CallPrice callPrice = null;

        //Initialization
        public CallPriceTest()
        {
            logger = new Mock<ILogger<CallPrice>>();
            callService = new Mock<ICallService>();

            callPrice = new CallPrice(logger.Object, callService.Object);
        }

        [Fact]
        public async Task Get_Return_CallPriceList_When_Return_Data_From_CallService()
        {
            //Arrange
            List<CallPriceDetails> prices = new List<CallPriceDetails>();
            prices.Add(
                           new CallPriceDetails
                           {
                               Expert = "expert1",
                               Client = "client1",
                               Duration = 40,
                               Price = 100.29m,
                               Currency = "JPY"
                           }
                           );
            callService.Setup(x => x.GetPrices())
               .ReturnsAsync(prices);
           
            //Act
            IActionResult actionResult = await callPrice.Get();
            var contentResult = actionResult as ObjectResult;
           
            //Assert
            Assert.NotNull(contentResult);
            Assert.NotNull(contentResult.Value);
            int exceptedStatusCode = 200;
            Assert.Equal(contentResult.StatusCode, exceptedStatusCode);
        }

        [Fact]
        public async Task GetPrices_Throw_Exception_When_CallService_Throw_Exception()
        {
            //Arrange
            callService.Setup(x => x.GetPrices())
               .ThrowsAsync(new Exception());

            //Act
            IActionResult actionResult = await callPrice.Get();
            var contentResult = actionResult as ObjectResult;
            
            //Assert
            Assert.NotNull(contentResult);
            Assert.NotNull(contentResult.Value);
            int exceptedStatusCode = 500;
            string exception = "Error while processing data!";
            Assert.Equal(contentResult.Value.ToString(), exception);
            Assert.Equal(contentResult.StatusCode, exceptedStatusCode);
        }

    }
}
