﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Moq;
using FluentAssertions;
using Bogus;
using BulbaCourses.Youtube.Web.DataAccess;
using BulbaCourses.Youtube.Web.DataAccess.Repositories;
using BulbaCourses.Youtube.Web.DataAccess.Models;
using BulbaCourses.Youtube.Web.Logic.Services;

namespace BulbaCourses.Youtube.Web.Tests
{
    [TestFixture]
    class StoryServiceTest
    {
        Faker<SearchRequestDb> fakerRequest;
        Faker<SearchStoryDb> fakerStory;
        Faker<UserDb> fakerUser;

        [OneTimeSetUp]
        public void Init()
        {
            fakerUser = new Faker<UserDb>();
            fakerUser.RuleFor(u => u.Login, f => f.Internet.UserName())
                .RuleFor(u => u.Password, f => f.Random.String(8))
                .RuleFor(u => u.FirstName, f => f.Name.FirstName())
                .RuleFor(u => u.LastName, f => f.Name.LastName())
                .RuleFor(u => u.FullName, f => f.Name.FullName())
                .RuleFor(u => u.NumberPhone, f => f.Phone.PhoneNumber("+###(##)###-##-##"))
                .RuleFor(u => u.Email, f => f.Internet.Email())
                .RuleFor(u => u.ReserveEmail, f => f.Internet.Email());

            fakerRequest = new Faker<SearchRequestDb>();
            fakerRequest.RuleFor(r=> r.Title, f => f.Random.Word());

            fakerStory = new Faker<SearchStoryDb>();
            fakerStory.RuleFor(s => s.SearchRequest, f => fakerRequest.Generate(1).First())
                .RuleFor(s => s.SearchDate, f => f.Date.Past(1,null))
                .RuleFor(s => s.User, f => fakerUser.Generate(1).First());
        }

        [Test, Category("SearchStory")]
        public void Test_SearchStory_Save()
        {
            using (var context = new YoutubeContext())
            {
                var storyRepo = new StoryRepository(context);
                var storyService = new StoryService(storyRepo);

                var storyDb = fakerStory.Generate(1).First();
                var userName = storyDb.User.FirstName;
                var requestTitle = storyDb.SearchRequest.Title;

                storyService.Save(storyDb);

                var result = context.SearchStories.Where(r => r.User.FirstName == userName).First();
                result.Should().NotBeNull();
            }
        }

        [Test, Category("SearchStory")]
        public void Test_SearchStory_DeleteByUserId()
        {
            using (var context = new YoutubeContext())
            {
                var storyRepo = new StoryRepository(context);
                var storyService = new StoryService(storyRepo);

                var storyDb = fakerStory.Generate(1).First();

                storyService.Save(storyDb);
                var userId = storyDb.User.Id;

                storyService.Save(fakerStory.Generate(1).First());
                storyService.Save(fakerStory.Generate(1).First());

                var result = context.SearchStories.FirstOrDefault(r => r.User.Id == userId);
                result.Should().NotBeNull();

                storyService.DeleteByUserId(userId);
                result = context.SearchStories.FirstOrDefault(r => r.User.Id == userId);
                result.Should().BeNull();
            }
        }

        [Test, Category("SearchStory")]
        public void Test_SearchStory_DeleteByStoryId()
        {
            using (var context = new YoutubeContext())
            {
                var storyRepo = new StoryRepository(context);
                var storyService = new StoryService(storyRepo);

                var storyDb = fakerStory.Generate(1).First();
                storyService.Save(storyDb);
                var storyId = storyDb.Id;

                storyService.Save(fakerStory.Generate(1).First());
                storyService.Save(fakerStory.Generate(1).First());

                var result = context.SearchStories.FirstOrDefault(r => r.Id == storyId);
                result.Should().NotBeNull();

                storyService.DeleteByUserId(storyId);
                result = context.SearchStories.FirstOrDefault(r => r.Id == storyId);
                result.Should().BeNull();
            }
        }

        [Test, Category("SearchStory")]
        public void Test_SearchStory_GetAllStories()
        {
            using (var context = new YoutubeContext())
            {
                var storyRepo = new StoryRepository(context);
                var storyService = new StoryService(storyRepo);

                var count = context.SearchStories.Count();

                storyService.Save(fakerStory.Generate(1).First());
                storyService.Save(fakerStory.Generate(1).First());
                storyService.Save(fakerStory.Generate(1).First());

                var allStories = storyService.GetAllStories().ToList();

                allStories.Should().HaveCount(count+3);
            }
        }

        [Test, Category("SearchStory")]
        public void Test_SearchStory_GetStoriesByUserId()
        {
            using (var context = new YoutubeContext())
            {
                var storyRepo = new StoryRepository(context);
                var storyService = new StoryService(storyRepo);

                storyService.Save(fakerStory.Generate(1).First());
                storyService.Save(fakerStory.Generate(1).First());

                var storyDb = fakerStory.Generate(1).First();
                storyService.Save(storyDb);
                var userId = storyDb.User.Id;

                var story = storyService.GetStoriesByUserId(userId).First();

                story.UserId.Should().Be(userId);
            }
        }

        [Test, Category("SearchStory")]
        public void Test_SearchStory_GetStoriesByRequestId()
        {
            using (var context = new YoutubeContext())
            {
                var storyRepo = new StoryRepository(context);
                var storyService = new StoryService(storyRepo);

                storyService.Save(fakerStory.Generate(1).First());
                storyService.Save(fakerStory.Generate(1).First());

                var storyDb = fakerStory.Generate(1).First();
                storyService.Save(storyDb);
                var requestId = storyDb.SearchRequest.Id;

                var story = storyService.GetStoriesByRequestId(requestId).First();

                story.SearchRequestId.Should().Be(requestId);
            }
        }

        [Test, Category("SearchStory")]
        public void Test_SearchStory_GetStoriesByStoryId()
        {
            using (var context = new YoutubeContext())
            {
                var storyRepo = new StoryRepository(context);
                var storyService = new StoryService(storyRepo);

                storyService.Save(fakerStory.Generate(1).First());
                storyService.Save(fakerStory.Generate(1).First());

                var storyDb = fakerStory.Generate(1).First();
                storyService.Save(storyDb);
                var storyId = storyDb.Id;

                var story = storyService.GetStoryByStoryId(storyId);

                story.Id.Should().Be(storyId);
            }
        }
    }
}
