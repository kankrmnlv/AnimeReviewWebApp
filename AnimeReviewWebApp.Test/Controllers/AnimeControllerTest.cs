using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnimeReviewWebApp.Controllers;
using AnimeReviewWebApp.Dto;
using AnimeReviewWebApp.Interfaces;
using AnimeReviewWebApp.Models;
using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace AnimeReviewWebApp.Test.Controllers
{
    public class AnimeControllerTest
    {
        private readonly IAnimeInterface _animeInterface;
        private readonly IReviewInterface _reviewInterface;
        private readonly IMapper _mapper;
        public AnimeControllerTest()
        {
            _animeInterface = A.Fake<IAnimeInterface>();
            _reviewInterface = A.Fake<IReviewInterface>();
            _mapper = A.Fake<IMapper>();
        }

        [Fact]
        public void AnimeController_GetAnimeList_ReturnsOK()
        {
            //Arrange
            var anime = A.Fake<ICollection<AnimeDto>>();
            var animeList = A.Fake<List<AnimeDto>>();
            A.CallTo(() => _mapper.Map<List<AnimeDto>>(anime)).Returns(animeList);
            var controller = new AnimeController(_animeInterface, _mapper, _reviewInterface);

            //Act
            var result = controller.GetAnimeList();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Fact]
        public void AnimeController_CreateAnime_ReturnsOK()
        {
            //Arrange
            int ownerId = 1;
            int genreId = 2;
            var anime = A.Fake<Anime>();
            var animeDto = A.Fake<AnimeDto>();
            var animes = A.Fake<ICollection<AnimeDto>>();
            var animeList = A.Fake<IList<AnimeDto>>();
            A.CallTo(() => _animeInterface.GetAnimeTrimToUpper(animeDto)).Returns(anime);
            A.CallTo(() => _mapper.Map<Anime>(animeDto)).Returns(anime);
            A.CallTo(() => _animeInterface.CreateAnime(ownerId, genreId, anime)).Returns(true);
            var controller = new AnimeController(_animeInterface, _mapper, _reviewInterface);
            
            //Act
            var result = controller.CreateAnime(ownerId, genreId, animeDto);

            //Assert
            result.Should().NotBeNull();
        }
    }
}
