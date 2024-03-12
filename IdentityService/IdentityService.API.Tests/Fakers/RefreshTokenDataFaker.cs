﻿namespace IdentityService.API.Tests.Fakers
{
    public static class RefreshTokenDataFaker
    {
        public const int StandartMaximumRefreshTokenLength = 15;

        private const string CharsForGeneration = "XCVBNMASDFGHJKLQWERTYUIOP123456789zxcvbnmmasdfghjklqwertyuiop_";

        public static string GetRandomRefreshToken(int length)
        {
            var random = new Random();

            return new string(Enumerable.Repeat(CharsForGeneration, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
