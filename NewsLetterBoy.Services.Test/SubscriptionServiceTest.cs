using System;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using NewsLetterBoy.Model.NewsLetter;
using NewsLetterBoy.Model.Subscription;
using NewsLetterBoy.Service;
using Xunit;

namespace NewsLetterBoy.Services.Test
{
    public class SubscriptionServiceTest
    {
        private ISubscriptionService sut; 
        private Mock<ISubscriptionRepository> _subscriptionRepository;
        private Mock<INewsLetterRepository> _newsLetterRepository;
        private Mock<ISubscriptionDomainService> _domainService; 
        public SubscriptionServiceTest()
        {
            _domainService = new Mock<ISubscriptionDomainService>();
            _subscriptionRepository = new Mock<ISubscriptionRepository>();
            _newsLetterRepository = new Mock<INewsLetterRepository>();
            sut = new SubscriptionService(_newsLetterRepository.Object, _subscriptionRepository.Object,
                _domainService.Object, NullLogger<SubscriptionService>.Instance); 

        }

        [Fact]
        public async Task SubscribeAsync_Of_SubscriptionService_With_Existing_newsLetter()
        {
            //arrange
            _newsLetterRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .Returns(Task.FromResult<NewsLetter>(new NewsLetter("test", "testDescription")));
            
            //act
            Func<Task> action =async () => await sut.SubscribeAsync(1, 5, null); 
            
            //assert
            await action.Should().NotThrowAsync();
            _subscriptionRepository.Verify(x=>x.InsertAsync(It.IsAny<Subscription>()),Times.Once);
            
        }
        
        [Fact]
        public async Task SubscribeAsync_Of_SubscriptionService_With_NotExisting_newsLetter()
        {
            //arrange
            _newsLetterRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .Returns(Task.FromResult<NewsLetter>(null));
            
            //act
            Func<Task> action =async () => await sut.SubscribeAsync(1, 5, null); 
            
            //assert
            await action.Should().ThrowAsync<ApplicationException>();

        }
        
        [Fact]
        public async Task UnsubscribeAsync_Of_SubscriptionService_With_Existing_Subscription()
        {
            //arrange
            _domainService.Setup(x => x.IsUserRegisteredBefore(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(false);
            _subscriptionRepository.Setup(x => x.GetSubscriptionAsync(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(Task.FromResult<Subscription>(new Subscription(5,10, _domainService.Object,null)));
            
            //act
            await sut.UnsubscribeAsync(1, 5); 
            
            //assert
            _subscriptionRepository.Verify(x=>x.UpdateAsync(It.IsAny<Subscription>()), Times.Once);
        }
        
        [Fact]
        public async Task UnsubscribeAsync_Of_SubscriptionService_With_NotExisting_Subscription()
        {
            //arrange
            _domainService.Setup(x => x.IsUserRegisteredBefore(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(false);
            _subscriptionRepository.Setup(x => x.GetSubscriptionAsync(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(Task.FromResult<Subscription>(null));
            
            //act
            Func<Task> action = async () => await sut.UnsubscribeAsync(1, 5); 
            
            //assert
            await action.Should().ThrowAsync<ApplicationException>();
            _subscriptionRepository.Verify(x=>x.UpdateAsync(It.IsAny<Subscription>()), Times.Never);
        }
        
        [Fact]
        public async Task GetSubscribeAsync_Of_SubscriptionService_With_Existing_Subscription()
        {
            //arrange
            _domainService.Setup(x => x.IsUserRegisteredBefore(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(false);
            _subscriptionRepository.Setup(x => x.GetSubscriptionAsync(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(Task.FromResult<Subscription>(new Subscription(5,10, _domainService.Object,null)));
            
            //act
            var isSubscribed = await sut.GetSubscriptionStatusAsync(4, 5);
            
            //assert
            isSubscribed.Should().BeTrue();
        }
        
        
        [Fact]
        public async Task GetSubscribeAsync_Of_SubscriptionService_With_NotExisting_Subscription()
        {
            //arrange
            _domainService.Setup(x => x.IsUserRegisteredBefore(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(false);
            _subscriptionRepository.Setup(x => x.GetSubscriptionAsync(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(Task.FromResult<Subscription>(null));
            
            //act
            var isSubscribed = await sut.GetSubscriptionStatusAsync(4, 5);
            
            //assert
            isSubscribed.Should().BeFalse();
        }
        
    }
}