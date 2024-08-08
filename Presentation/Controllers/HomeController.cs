using Common.DTO;
using Core.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Presentation.Views.ViewModels;


namespace Presentation.Controllers
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

            // Kullanýcýnýn kendi tweetlerini al
            var userTweetDtos = await _tweetService.GetTweetsByUserIdsAsync(new List<int> { currentUser.Id });

            // Kullanýcýnýn takip ettiði kiþilerin ID'lerini al
            var followingIds = await _followService.GetFollowingIdsByUserIdAsync(currentUser.Id);

            // Takip edilen kullanýcýlarýn tweetlerini al
            var followingTweetDtos = await _tweetService.GetTweetsByUserIdsAsync(followingIds);

            // Kullanýcýnýn kendi tweetleri ile takip ettiði kiþilerin tweetlerini birleþtir
            var allTweetsDtos = userTweetDtos.Concat(followingTweetDtos).ToList();

            // Tweetleri entity'lere dönüþtür
            var tweets = allTweetsDtos.Select(dto => new Tweet
            {
                Id = dto.Id,
                Content = dto.Content,
                UserId = dto.UserId,
                CreatedAt = dto.CreatedAt,
                TwitterUser = new TwitterUser
                {
                    Id= dto.UserId,
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    UserName = dto.UserName,
                }
            }).ToList();

            // Recent users
            var recentUserDtos = await _userService.GetRecentUsersAsync(currentUser.Id, 5);
            var recentUsers = recentUserDtos.Select(dto => new TwitterUser
            {
                Id = dto.Id,
                UserName = dto.UserName,
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName
            }).ToList();

            

            var model = new DashboardViewModel
            {
                Tweets = tweets,
                RecentUsers = recentUsers,
                FollowingUsers = followingIds.ToList()
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
        [HttpPost]
        public async Task<IActionResult> Follow([FromBody] FollowUnfollowViewModel model)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return Challenge();
            }

            var user = await _userService.GetUserByIdAsync(model.UserId);
            if (user == null)
            {
                return NotFound("User not found.");
            }
            var entity = new UserFollow
            {
                FollowerId = currentUser.Id,
                FolloweeId = model.UserId
            };

            var result = await _followService.AddFollowAsync(entity);
            if (!result)
            {
                return StatusCode(500, "An error occurred while adding follow."); // Hata durumunda
            }

            var tweets = await _tweetService.GetTweetsByUserIdsAsync(new List<int> { model.UserId });

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

            var user = await _userService.GetUserByIdAsync(model.UserId);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            var result = await _followService.RemoveFollowAsync(currentUser.Id, model.UserId);
            if (!result)
            {
                return StatusCode(500, "An error occurred while removing follow."); // Hata durumunda
            }

            var tweets = await _tweetService.GetTweetsByUserIdsAsync(new List<int> { model.UserId });

            return Json(new { success = true, tweets });
        }

        [HttpGet]
        public ViewResult UserTweetCounts()
        {
            var userTweetCountsDto = _tweetService.GetUserTweetCountsAsync();

            var userTweetCountsViewModel = userTweetCountsDto
                .Select(dto => new UserTweetCountViewModel
                {
                    UserName = dto.UserName,
                    TweetCount = dto.TweetCount
                }).AsQueryable(); // AsQueryable kullanarak IQueryable döndürüyoruz



            return View(userTweetCountsViewModel);
        }






    }
}
