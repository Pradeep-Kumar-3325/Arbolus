using Arbolus.Model.Concrete;
using Arbolus.Model.DTO;
using Arbolus.Model.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Arbolus.Service.Test
{
    public class CallServiceTest
    {
        private readonly Mock<ILogger<CallService>> logger;
        private readonly Mock<IConfiguration> config;

        private readonly Mock<IRateData> rateData;

        private readonly Mock<IData<ExpertData>> experts;

        private readonly Mock<IData<ClientData>> clients;

        CallService callService = null;

        //Initialization
        public CallServiceTest()
        {
            logger = new Mock<ILogger<CallService>>();
            config = new Mock<IConfiguration>();
            rateData = new Mock<IRateData>();
            experts = new Mock<IData<ExpertData>>();
            clients = new Mock<IData<ClientData>>();
            callService = new CallService(logger.Object, rateData.Object, experts.Object, clients.Object, config.Object);
        }

        [Fact]
        public async Task GetPrices_Return_CallPriceList_When_Return_Data_From_Firebase()
        {
            //Arrange
            this.setupData();
            //Act
            var result = await callService.GetPrices();
            //Assert
            int expectedCount = 4;
            Assert.NotNull(result);
            Assert.Equal(result.ToList().Count, expectedCount);
        }

        [Fact]
        public async Task GetPrices_Throw_Exception_When_Return_NullExperts_From_Firebase()
        {
            //Arrange
            this.setupDataWithExpertsNull();
           
            //Act and Assert
            Exception ex = await Assert.ThrowsAsync<Exception>(async () => await callService.GetPrices());
        }

        [Fact]
        public async Task GetPrices_Throw_Exception_When_Return_NullClients_From_Firebase()
        {
            //Arrange
            this.setupDataWithClientNull();

            //Act and Assert
            Exception ex = await Assert.ThrowsAsync<Exception>(async () => await callService.GetPrices());
        }

        [Fact]
        public async Task GetPrices_Throw_Exception_When_Return_NullRates_From_Firebase()
        {
            //Arrange
            this.setupDataWithRateNull();

            //Act and Assert
            Exception ex = await Assert.ThrowsAsync<Exception>(async () => await callService.GetPrices());
        }

        private void setupData()
        {
            Rate rate = new Rate();
            rate.AUD = new Dictionary<string, decimal>();
            rate.AUD.Add("USD", 0.86m);
            rate.USD = new Dictionary<string, decimal>();
            rate.USD.Add("AUD", 1.8m);
            rate.GBP = new Dictionary<string, decimal>();
            rate.GBP.Add("AUD", 1.8m);
            rateData.Setup(x => x.Get())
                .ReturnsAsync(rate);

            ExpertData expertData = new ExpertData();
            expertData.Experts = new List<Expert>();
            Expert expert = new Expert();
            expert.Calls = new List<Call>();
            Call call = new Call
            {
                Duration = 50,
                client = "client1"
            };
            expert.Calls.Add(call);
            call = new Call
            {
                Duration = 30,
                client = "client1"
            };
            expert.Calls.Add(call);
            call = new Call
            {
                Duration = 20,
                client = "client3"
            };
            expert.Calls.Add(call);
            expert.Name = "expert1";
            expert.hourlyRate = 100;
            expert.currency = "AUD";
            expertData.Experts.Add(expert);
            //Add Another Expert which is not in client list
            expert = new Expert();
            expert.Calls = new List<Call>();
            call = new Call
            {
                Duration = 40,
                client = "client2"
            };
            expert.Calls.Add(call);
            expert.Name = "expert2";
            expert.hourlyRate = 100;
            expert.currency = "GBP";
            expertData.Experts.Add(expert);

            experts.Setup(x => x.Get("Expert"))
                .ReturnsAsync(expertData);

            ClientData clientData = new ClientData();
            clientData.Clients = new List<Client>();
            Client client = new Client();
            client.Discounts = new List<string>();
            client.Discounts.Add("Follow");
            client.Name = "client1";
            clientData.Clients.Add(client);
            client = new Client();
            client.Name = "client3";
            clientData.Clients.Add(client);

            clients.Setup(x => x.Get("Client"))
                .ReturnsAsync(clientData);
        }

        private void setupDataWithClientNull()
        {
            ExpertData expertData = new ExpertData();
            expertData.Experts = new List<Expert>();
            Expert expert = new Expert();
            expert.Calls = new List<Call>();
            Call call = new Call
            {
                Duration = 50,
                client = "client1"
            };
            expert.Calls.Add(call);
            call = new Call
            {
                Duration = 30,
                client = "client1"
            };
            expert.Calls.Add(call);
            call = new Call
            {
                Duration = 20,
                client = "client3"
            };
            expert.Calls.Add(call);
            expert.Name = "expert1";
            expert.hourlyRate = 100;
            expert.currency = "AUD";
            expertData.Experts.Add(expert);
            experts.Setup(x => x.Get("Expert"))
                .ReturnsAsync(expertData);

            ClientData clientData = null;
            clients.Setup(x => x.Get("Client"))
                .ReturnsAsync(clientData);
        }

        private void setupDataWithRateNull()
        {
            Rate rate = null;
            rateData.Setup(x => x.Get())
                .ReturnsAsync(rate);

            ExpertData expertData = new ExpertData();
            expertData.Experts = new List<Expert>();
            Expert expert = new Expert();
            expert.Calls = new List<Call>();
            Call call = new Call
            {
                Duration = 50,
                client = "client1"
            };
            expert.Name = "expert1";
            expert.hourlyRate = 100;
            expert.currency = "AUD";
            expertData.Experts.Add(expert);

            experts.Setup(x => x.Get("Expert"))
                .ReturnsAsync(expertData);

            ClientData clientData = new ClientData();
            clientData.Clients = new List<Client>();
            Client client = new Client();
            client.Discounts = new List<string>();
            client.Discounts.Add("Follow");
            client.Name = "client1";
            clientData.Clients.Add(client);

            clients.Setup(x => x.Get("Client"))
                .ReturnsAsync(clientData);
        }

        private void setupDataWithExpertsNull()
        {
            ExpertData expertData = null; 
            experts.Setup(x => x.Get("Expert"))
                .ReturnsAsync(expertData);
        }
    }
}
