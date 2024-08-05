// DOM yüklendiğinde çalışacak fonksiyonlar
document.addEventListener("DOMContentLoaded", function () {
    // Tweet box'a kaydırma fonksiyonu
    function scrollToTweetBox() {
        document.querySelector('.tweet-box textarea').scrollIntoView({ behavior: 'smooth' });
    }

    // Takip et veya takibi bırak fonksiyonu
    window.toggleFollow = function (userId, button) {
        var action = $(button).text().trim() === "Follow" ? "Follow" : "Unfollow";
        var url = action === "Follow" ? "/Home/Follow" : "/Home/Unfollow";

        $.ajax({
            url: url,
            type: "POST",
            contentType: "application/json",
            data: JSON.stringify({ UserId: userId }),
            success: function (response) {
                debugger;
                if (response.success) {
                    if (action === "Follow") {
                        $(button).text("Unfollow");
                        // Yeni tweetleri ana sayfaya ekle
                        response.tweets.forEach(function (tweet) {
                            $('#tweet-container').prepend('<div data-tweet-id="' + tweet.Id + '">' + tweet.Content + '</div>');
                        });
                    } else {
                        $(button).text("Follow");
                        // Tweetleri ana sayfadan kaldır
                        response.tweets.forEach(function (tweet) {
                            $('#tweet-container div[data-tweet-id="' + tweet.Id + '"]').remove();
                        });
                    }
                } else {
                    // Başarılı yanıt ancak success false ise error işlemleri
                    console.error("Operation was not successful.");
                    alert("An error occurred while processing your request.");
                }
            },
            
        });
    }


    // Tweet gönderme fonksiyonu
    window.postTweet = function () {
        var content = $('#tweetContent').val();

        if (!content) {
            alert('Tweet content cannot be empty!');
            return;
        }

        $.ajax({
            url: '/Home/PostTweet',
            type: 'POST',
            data: { content: content },
            success: function (response) {
                // Başarılı olursa kullanıcıyı Dashboard sayfasına yönlendirecek.
                window.location.href = '/Home/Dashboard';
            },
            error: function (xhr, status, error) {
                console.error('An error occurred: ' + error);
            }
        });
    }

    // Fonksiyonları global erişilebilir hale getirme
    window.scrollToTweetBox = scrollToTweetBox;

    window.toggleFollow = toggleFollow;
    window.postTweet = postTweet;
});
