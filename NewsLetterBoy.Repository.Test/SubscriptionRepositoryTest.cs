using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using NewsLetterBoy.Model.NewsLetter;
using NewsLetterBoy.Model.Subscription;
using Xunit;

namespace NewsLetterBoy.Repository.Test
{
    public class SubscriptionRepositoryTest
    {
        private SubscriptionRepository _sut;
        private Mock<ISubscriptionDomainService> _domainService; 
        public SubscriptionRepositoryTest()
        {
            var dbOptionsBuilder = new DbContextOptionsBuilder<NewsLetterDbContext>().UseInMemoryDatabase("db");
            _domainService = new Mock<ISubscriptionDomainService>();
            _domainService.Setup(x => x.IsUserRegisteredBefore(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(false);
            var context = new NewsLetterDbContext(dbOptionsBuilder.Options);
            _sut = new SubscriptionRepository(context, NullLogger<SubscriptionRepository>.Instance); 
        }
        
        [Fact]
        public async Task InsertAsync_Of_SubscriptionRepository_Should_Add_Item()
        {
            //arrange
            var expirationDate = DateTime.Now.AddDays(10);
            var expected = new Subscription(4, 14,_domainService.Object, expirationDate);
            
            //act
            var id = await _sut.InsertAsync(expected);
            var actual = _sut.GetByIdAsync(id);
            
            //assert
            expected.Should().BeEquivalentTo(expected); 
        }
        [Fact]
        public async Task UpdateAsync_Of_SubscriptionRepository_Should_Modify_Item()
        {
            //arrange
            var expireDate = DateTime.Now.AddDays(10);
            var original = new Subscription(4, 5, _domainService.Object, expireDate);
            await _sut.InsertAsync(original);

            var modified = ObjectCloner.Clone<Subscription>(original);
            modified.Unsubscribe();

            //act
            await _sut.UpdateAsync(modified); 
            
            //assert
            var actual = await _sut.GetByIdAsync(modified.Id); 
            
            actual.Should().BeNull();
        }
        
        [Fact]
        public async Task GetAllAsync_Of_SubscriptionRepository_Should_Return_Query_Item()
        {
            //arrange
            var expireDate = DateTime.Now.AddDays(10);
            var first = new Subscription(13, 15, _domainService.Object, null);
            await _sut.InsertAsync(first);
            
            var another = new Subscription(17, 7, _domainService.Object, expireDate);
            await _sut.InsertAsync(another);
            
            var third = new Subscription(17, 15, _domainService.Object, expireDate.AddDays(5));
            await _sut.InsertAsync(third);
            
            //act
            var result = await _sut.GetAllAsync(x => x.NewsLetterId == 17);

            //assert
            result.Count().Should().Be(2);
            result.Should().ContainEquivalentOf(third);
            result.Should().ContainEquivalentOf(another);
        }
        
        [Fact]
        public async Task GetAllAsync_Of_SubscriptionRepository_Should_Return_NotDeleted_Items()
        {
            //arrange
            var expireDate = DateTime.Now.AddDays(10);
            var first = new Subscription(18, 21, _domainService.Object, null);
            await _sut.InsertAsync(first);
            
            var another = new Subscription(19, 22, _domainService.Object, expireDate);
            await _sut.InsertAsync(another);
            
            var third = new Subscription(18, 23, _domainService.Object, expireDate.AddDays(5));
            third.Unsubscribe();
            await _sut.InsertAsync(third);
            
            //act
            var result = await _sut.GetAllAsync(x => x.NewsLetterId == 18);

            //assert
            result.Count().Should().Be(1);
            result.Should().ContainEquivalentOf(first);
        }
        
        [Fact]
        public async Task GetSubscriptionAsync_Of_SubscriptionRepository_Should_Return_Requested_Items()
        {
            //arrange
            var expireDate = DateTime.Now.AddDays(10);
            var first = new Subscription(25, 8, _domainService.Object, null);
            await _sut.InsertAsync(first);
            
            var another = new Subscription(26, 7, _domainService.Object, expireDate);
            await _sut.InsertAsync(another);
            
            var third = new Subscription(24, 9, _domainService.Object, expireDate.AddDays(5));
            third.Unsubscribe();
            await _sut.InsertAsync(third);
            
            //act
            var result = await _sut.GetSubscriptionAsync(7, 26 );

            //assert
            result.Should().BeEquivalentTo(another);
        }
    }
}