using System;
using System.Threading.Tasks;
using Castle.Core.Logging;
using FluentAssertions;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using NewsLetterBoy.Model.NewsLetter;
using NewsLetterBoy.Service;
using Xunit;

namespace NewsLetterBoy.Services.Test
{
    public class NewsLetterServiceTest
    {
        private Mock<INewsLetterRepository> newsLetterRepository;
        private INewsLetterService sut;
        public NewsLetterServiceTest()
        {
            newsLetterRepository = new Mock<INewsLetterRepository>(); 
            sut = new NewsLetterService(newsLetterRepository.Object,  NullLogger<NewsLetterService>.Instance);
        }
        [Fact]
        public async void Create_NewsLetterService()
        {
            //arrange
            var title = "someTitle";
            var description = "someDescription";
            
            //act
            await sut.CreateAsync(title, description); 
            
            //assert
            newsLetterRepository.Verify(x=>x.InsertAsync(It.IsAny<NewsLetter>()),Times.Once);
        }
        
        [Fact]
        public async void Update_NewsLetterService_Valid_Id()
        {
            //arrange
            var id = 1;
            var title = "someTitle";
            var description = "someDescription";
            newsLetterRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .Returns(Task.FromResult<NewsLetter>(new NewsLetter(title, description)));
            
            //act
            await sut.UpdateAsync(id, title, description); 
            
            //assert
            newsLetterRepository.Verify(x=>x.UpdateAsync(It.IsAny<NewsLetter>()),Times.Once);
        }
        
        [Fact]
        public async void Update_NewsLetterService_Invalid_Id()
        {
            //arrange
            var id = 1;
            var title = "someTitle";
            var description = "someDescription";
            newsLetterRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .Returns(Task.FromResult<NewsLetter>(null));
            
            //act
            Func<Task> action = async () => await sut.UpdateAsync(id, title, description); 
            
            //assert
            await action.Should().ThrowAsync<ApplicationException>(); 
        }
    }
}