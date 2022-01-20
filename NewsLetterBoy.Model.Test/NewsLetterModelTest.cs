using System;
using FluentAssertions;
using Xunit;

namespace NewsLetterBoy.Model.Test
{
    public class NewsLetterModelTest
    {
        [Fact]
        public void Create_NewsLetter_Should_Construct_New_Instance()
        {
            //arrange
            var title = "title";
            var description = "description";
            //act
            var sut = new NewsLetter.NewsLetter(title, description); 

            //assert
            sut.Description.Should().Be(description);
            sut.Title.Should().Be(title);
            sut.CreationDate.Should().NotBe(default(DateTime));
            sut.ModifyDate.Should().BeNull();
            sut.IsDeleted.Should().BeFalse();
        }
        
        
        [Fact]
        public void Create_NewsLetter_With_Null_Title_Should_Throw_Exception()
        {
            //arrange
            var title = "";
            var description = "description";
            //act
            Action action = () =>
            {
                 new NewsLetter.NewsLetter(title, description);
            }; 

            //assert
            action.Should().Throw<DomainException>();
        }
        
        [Fact]
        public void Edit_NewsLetter_Should_Modify_Object()
        {
            //arrange
            var title = "title";
            var description = "description";
            var newTitle = "newTitle"; 
            var newDescription = "newDescription"; 

            var sut = new NewsLetter.NewsLetter(title, description); 

            //act
            sut.Modify(newTitle, newDescription);
            //assert
            sut.Description.Should().Be(newDescription);
            sut.Title.Should().Be(newTitle);
            sut.CreationDate.Should().NotBe(default(DateTime));
            sut.ModifyDate.Should().NotBeNull();
        }


        [Fact]
        public void Edit_NewsLetter_With_Null_Title_Should_Throw_Exception()
        {
            //arrange
            var title = "title";
            var description = "description";
            var newTitle = "";
            var newDescription = "newDescription";

            var sut = new NewsLetter.NewsLetter(title, description);

            //act
            Action action = () => sut.Modify(newTitle, newDescription);
            //assert
            action.Should().Throw<DomainException>();
        }

        [Fact]
        public void SetDeleted_NewsLetter_With_Null_Title_Should_Throw_Exception()
        {
            //arrange
            var title = "title";
            var description = "description";

            var sut = new NewsLetter.NewsLetter(title, description);

            //act
            sut.SetAsRemoved();
            //assert
            sut.IsDeleted.Should().BeTrue();
        }
    }
}