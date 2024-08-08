document.addEventListener("DOMContentLoaded", function () {
    // Tweet box'a kaydırma fonksiyonu
    function scrollToTweetBox() {
        $('html, body').animate({
            scrollTop: $('.tweet-box textarea').offset().top
        }, 'smooth');
    }

    // Takip et veya takibi bırak fonksiyonu
    document.addEventListener('click', function (event) {
        if (event.target.classList.contains('follow-btn')) {
            var userId = event.target.getAttribute('data-userid');
            var followBtn = event.target;
           // debugger;

            $.ajax({
                type: "POST",
                url: '/Home/Follow', // Follow action'a gönderilen URL
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ UserId: userId }), // FollowUnfollowViewModel modelinde UserId propertysi
                success: function (response) {
                   // debugger;
                    //console.log(response.success, response.success);
                    
                    if (response.success) {
                        // Gelen tweetleri ekrana ekle
                        response.tweets.forEach(function (tweet) {
                            //debugger;
                            
                            $('#tweets-container').append(
                                `<div class="tweet" id="tweet-${tweet.id}">
                            <time>${new Date(tweet.createdAt).toLocaleString()}</time>
                            <p>
                                <strong>${tweet.firstName} ${tweet.lastName}</strong>
            <span class="text-muted">${tweet.userName}</span>
                            </p>
                            <p>${tweet.content}</p>
                            <div class="tweet-actions">
                                <i class="fas fa-comment"></i>
                                <i class="fas fa-retweet"></i>
                                <i class="fas fa-heart"></i>
                            </div>
                        </div>`
                            );
                        });

                        // Follow butonunu unfollow butonuna çevir
                        followBtn.classList.remove('follow-btn');
                        followBtn.classList.add('unfollow-btn');
                        followBtn.textContent = 'Unfollow';
                    }
                },
                error: function (e) {
                   console.log(e)
                }
            });

        }

        if (event.target.classList.contains('unfollow-btn')) {
            var userId = event.target.getAttribute('data-userid');
            var unfollowBtn = event.target;

            $.ajax({
                type: "POST",
                url: '/Home/Unfollow', // Unfollow action'a gönderilen URL
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ UserId: userId }), // FollowUnfollowViewModel modelinde UserId propertysi
                success: function (response) {
                    if (response.success) {
                        // Unfollow edilen kullanıcının tweetlerini ekrandan kaldır
                        response.tweets.forEach(function (tweet) {
                            //debugger;
                            document.getElementById(`tweet-${tweet.id}`).remove();
                        });

                        // Unfollow butonunu follow butonuna çevir
                        unfollowBtn.classList.remove('unfollow-btn');
                        unfollowBtn.classList.add('follow-btn');
                        unfollowBtn.textContent = 'Follow';
                    }
                },
                error: function () {
                  
                }
            });
        }
    });


    // Tweet gönderme fonksiyonu
    window.postTweet = function () {
        var content = document.getElementById('tweetContent').value;

        if (!content) {
            alert('Tweet content cannot be empty!');
            return;
        }

        $.ajax({
            url: '/Home/PostTweet',
            type: 'POST',
            data: { content: content },
            success: function () {
                // Başarılı olursa kullanıcıyı Dashboard sayfasına yönlendirecek.
                window.location.href = '/Home/Dashboard';
            },
            error: function (xhr, status, error) {
                console.error('An error occurred: ' + error);
            }
        });
    }

});




//// DOM yüklendiğinde çalışacak fonksiyonlar
//document.addEventListener("DOMContentLoaded", function () {
//    // Tweet box'a kaydırma fonksiyonu
//    function scrollToTweetBox() {
//        document.querySelector('.tweet-box textarea').scrollIntoView({ behavior: 'smooth' });
//    }

//    // Takip et veya takibi bırak fonksiyonu
//    window.toggleFollow = function (userId, button) {
//        var action = $(button).text().trim() === "Follow" ? "Follow" : "Unfollow";
//        var url = action === "Follow" ? "/Home/Follow" : "/Home/Unfollow";

//        $.ajax({
//            url: url,
//            type: "POST",
//            contentType: "application/json",
//            data: JSON.stringify({ UserId: userId }),
//            success: function (response) {
//                debugger;
//                if (response.success) {
//                    if (action === "Follow") {
//                        $(button).text("Unfollow");
//                        // Yeni tweetleri ana sayfaya ekle
//                        response.tweets.forEach(function (tweet) {
//                            $('#tweet-container').prepend('<div data-tweet-id="' + tweet.Id + '">' + tweet.Content + '</div>');
//                        });
//                    } else {
//                        $(button).text("Follow");
//                        // Tweetleri ana sayfadan kaldır
//                        response.tweets.forEach(function (tweet) {
//                            $('#tweet-container div[data-tweet-id="' + tweet.Id + '"]').remove();
//                        });
//                    }
//                } else {
//                    // Başarılı yanıt ancak success false ise error işlemleri
//                    console.error("Operation was not successful.");
//                    alert("An error occurred while processing your request.");
//                }
//            },
            
//        });
//    }


//    // Tweet gönderme fonksiyonu
//    window.postTweet = function () {
//        var content = $('#tweetContent').val();

//        if (!content) {
//            alert('Tweet content cannot be empty!');
//            return;
//        }

//        $.ajax({
//            url: '/Home/PostTweet',
//            type: 'POST',
//            data: { content: content },
//            success: function (response) {
//                // Başarılı olursa kullanıcıyı Dashboard sayfasına yönlendirecek.
//                window.location.href = '/Home/Dashboard';
//            },
//            error: function (xhr, status, error) {
//                console.error('An error occurred: ' + error);
//            }
//        });
//    }

//    // Fonksiyonları global erişilebilir hale getirme
//    window.scrollToTweetBox = scrollToTweetBox;

//    window.toggleFollow = toggleFollow;
//    window.postTweet = postTweet;
//});
