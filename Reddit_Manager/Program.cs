using Reddit_Manager;

namespace RedditOAuthExample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var redditClient = new RedditClient();
            string accessToken = await redditClient.GetAccessTokenAsync("username", "password", "secret_id", "secret");

            if (!string.IsNullOrEmpty(accessToken))
            {
                var userInfo = await redditClient.GetUserInfoAsync(accessToken);
                Console.WriteLine(userInfo);
            }
            else
            {
                Console.WriteLine("Failed to get access token.");
            }
        }
    }
}