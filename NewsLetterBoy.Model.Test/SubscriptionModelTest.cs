using System;
using FluentAssertions;
using Moq;
using NewsLetterBoy.Model.Subscription;
using Xunit;

namespace NewsLetterBoy.Model.Test
{
    public class SubscriptionModelTest
    {
        private Mock<ISubscriptionDomainService> _domainService; 
        public SubscriptionModelTest()
        {
            _domainService = new Mock<ISubscriptionDomainService>();
        }
        [Fact]
        public void Create_Subscription_Should_Construct_New_Instance()
        {
            //arrange
            var userId = 1;
            var newsLetterId = 2;
            var expirationDate = DateTime.Now.AddDays(10);            

            _domainService.Setup(x => x.IsUserRegisteredBefore(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(false);
            //act
            var sut = new Subscription.Subscription(newsLetterId, userId, _domainService.Object,expirationDate); 
            
            //assert
            sut.UserId.Should().Be(userId);
            sut.NewsLetterId.Should().Be(newsLetterId);
            sut.CreationDate.Should().NotBe(default(DateTime));
            sut.ExpirationDateTime.Should().Be(expirationDate);
            sut.ModifyDate.Should().BeNull();
            sut.IsDeleted.Should().BeFalse();
        }
        
        
        [Fact]
        public void Create_Repetitive_Subscription_Should_Throw_Exception()
        {
            //arrange
            var userId = 4;
            var newsLetterId = 5;
            var expirationDate = DateTime.Now.AddDays(10);            

            _domainService.Setup(x => x.IsUserRegisteredBefore(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(true);
            //act
            Action action = () =>new Subscription.Subscription(newsLetterId, userId, _domainService.Object, expirationDate); 
            
            //assert
            action.Should().Throw<DomainException>(); 
        }
        
        [Fact]
        public void  Create_Subscription_With_Passed_Expiration_Should_Throw_Exception()
        {
            //arrange
            var userId = 3;
            var newsLetterId = 4;
            var expirationDate = DateTime.Now.AddDays(-10);            

            _domainService.Setup(x => x.IsUserRegisteredBefore(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(false);
            //act
            Action action = () =>new Subscription.Subscription(newsLetterId, userId, _domainService.Object, expirationDate); 
            
            //assert
            action.Should().Throw<DomainException>();
        }
        
        [Fact]
        public void Unsubscribe_Subscription_Should_Set_IsDeleted()
        {
            //arrange
            var userId = 5;
            var newsLetterId = 2;
            var expirationDate = DateTime.Now.AddDays(10);            

            _domainService.Setup(x => x.IsUserRegisteredBefore(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(false);
            var sut = new Subscription.Subscription(newsLetterId, userId, _domainService.Object,expirationDate); 

            //act
            sut.Unsubscribe();
            
            //assert
            sut.UserId.Should().Be(userId);
            sut.NewsLetterId.Should().Be(newsLetterId);
            sut.CreationDate.Should().NotBe(default(DateTime));
            sut.ExpirationDateTime.Should().Be(expirationDate);
            sut.ModifyDate.Should().NotBeNull();
            sut.IsDeleted.Should().BeTrue();
        }
    }
}