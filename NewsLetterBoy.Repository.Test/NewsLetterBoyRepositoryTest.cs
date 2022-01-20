using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using NewsLetterBoy.Model;
using NewsLetterBoy.Model.NewsLetter;
using Xunit;

namespace NewsLetterBoy.Repository.Test
{
    public class NewsLetterRepositoryTest
    {
        private NewsLetterRepository _sut;

        public NewsLetterRepositoryTest()
        {
            var dbOptionsBuilder = new DbContextOptionsBuilder<NewsLetterDbContext>().UseInMemoryDatabase("db");

            var context = new NewsLetterDbContext(dbOptionsBuilder.Options);
            _sut = new NewsLetterRepository(context, NullLogger<NewsLetterRepository>.Instance); 
        }
        
        [Fact]
        public async Task InsertAsync_Of_NewsLetterRepository_Should_Add_Item()
        {
            //arrange
            var expected = new NewsLetter("Tech news", "description");
            
            //act
            var id = await _sut.InsertAsync(expected);
            var actual = _sut.GetByIdAsync(id);
            
            //assert
            expected.Should().BeEquivalentTo(expected); 
        }
        [Fact]
        public async Task UpdateAsync_Of_NewsLetterRepository_Should_Modify_Item()
        {
            //arrange
            var newTitle = "Politic news";
            var newDescription = "changed description"; 
            var original = new NewsLetter("Tech news", "description");
            await _sut.InsertAsync(original);

            var modified = ObjectCloner.Clone<NewsLetter>(original);
            modified.Modify(newTitle, newDescription);

            //act
            await _sut.UpdateAsync(modified); 
            
            //assert
            var actual = await _sut.GetByIdAsync(modified.Id); 
            actual.Description.Should().Be(newDescription);
            actual.Title.Should().Be(newTitle);
            actual.ModifyDate.Should().NotBeNull();
        }
        
        [Fact]
        public async Task GetAllAsync_Of_NewsLetterRepository_Should_Return_Query_Item()
        {
            //arrange
            var first = new NewsLetter("Tech news", "description");
            await _sut.InsertAsync(first);
            
            var another = new NewsLetter("Fintech news", "description");
            await _sut.InsertAsync(another);
            
            var third = new NewsLetter("Economy news", "description");
            await _sut.InsertAsync(third);
            
            //act
            var result = await _sut.GetAllAsync(x => x.Title.ToLower().Contains("tech"));

            //assert
            result.Count().Should().Be(2);
            result.Should().ContainEquivalentOf(first);
            result.Should().ContainEquivalentOf(another);
        }
        
        [Fact]
        public async Task GetAllAsync_Of_NewsLetterRepository_Should_Return_NotDeleted_Items()
        {
            //arrange
            var first = new NewsLetter("Tech news", "description");
            await _sut.InsertAsync(first);
            
            var another = new NewsLetter("Fintech news", "description");
            await _sut.InsertAsync(another);
            
            var third = new NewsLetter("HighTech news", "description");
            third.SetAsRemoved();
            await _sut.InsertAsync(third);
            
            //act
            var result = await _sut.GetAllAsync(x => x.Title.ToLower().Contains("tech"));

            //assert
            result.Count().Should().Be(2);
            result.Should().ContainEquivalentOf(first);
            result.Should().ContainEquivalentOf(another);
        }
    }
}