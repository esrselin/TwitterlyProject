using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Twitter.Models.ViewModels;
using Common.DTO;
using Domain.Entities;
using Core.Interfaces;
using System.Collections.Generic;

namespace Twitter.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ITweetService _tweetService;
        private readonly ITwitterUserService _userService;
        private readonly IUserFollowService _followService;
        private readonly UserManager<TwitterUser> _userManager;

        public HomeController(
            ITweetService tweetService,
            ITwitterUserService userService,
            IUserFollowService followService,
            UserManager<TwitterUser> userManager)
        {
            _tweetService = tweetService;
            _userService = userService;
            _followService = followService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Dashboard()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return Challenge();
            }

            // Sadece kullanýcýnýn kendi tweetlerini al
            var tweetDtos = await _tweetService.GetTweetsByUserIdsAsync(new List<string> { currentUser.Id });

            var recentUserDtos = await _userService.GetRecentUsersAsync(currentUser.Id, 5);

            var tweets = tweetDtos.Select(dto => new Tweet
            {
                Id = dto.Id,
                Content = dto.Content,
                UserId = dto.UserId,
                CreatedAt = dto.CreatedAt,
                TwitterUser = dto.TwitterUser
            }).ToList();

            var recentUsers = recentUserDtos.Select(dto => new TwitterUser
            {
                Id = dto.Id,
                UserName = dto.UserName,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                FirstName = dto.FirstName,
                LastName = dto.LastName
            }).ToList();

            var model = new DashboardViewModel
            {
                Tweets = tweets,
                RecentUsers = recentUsers
            };

            return View(model);
        }


        //Tweeti paylaþýr ve db'ye kaydeder.
        [HttpPost]
        public async Task<IActionResult> PostTweet(string content)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null || string.IsNullOrEmpty(content))
            {
                return RedirectToAction(nameof(Dashboard));
            }

            var tweetDto = new TweetDTO
            {
                Content = content,
                UserId = currentUser.Id,
                CreatedAt = DateTime.UtcNow
            };

            await _tweetService.AddTweetAsync(tweetDto);

            return RedirectToAction(nameof(Dashboard));
        }

        //ADD
        public async Task<IActionResult> Follow([FromBody] FollowUnfollowViewModel model)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return Challenge();
            }

            var user = await _followService.FindUserByIdAsync(model.UserId);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            var result = await _followService.AddFollowAsync(user);
            if (!result)
            {
                return StatusCode(500, "An error occurred while adding follow."); // Hata durumunda
            }

            var tweets = await _tweetService.GetTweetsByUserIdsAsync(new List<string> { model.UserId });

            return Json(new { success = true, tweets });
        }



        //REMOVE
        [HttpPost]
        public async Task<IActionResult> Unfollow([FromBody] FollowUnfollowViewModel model)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return Challenge();
            }

            var followDto = new UserFollowDTO
            {
                FollowerId = currentUser.Id,
                FolloweeId = model.UserId
            };

            await _followService.RemoveFollowAsync(followDto);

            var tweets = await _tweetService.GetTweetsByUserIdsAsync(new List<string> { model.UserId });

            return Json(new { success = true, tweets });
        }
    }
}
