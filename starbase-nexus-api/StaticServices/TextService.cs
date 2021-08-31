using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace starbase_nexus_api.StaticServices
{
    public static class TextService
    {
        public static string GenerateToken()
        {
            string tokenString;
            byte[] randomNumber = new byte[32];

            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                tokenString = Convert.ToBase64String(randomNumber);
            }
            return tokenString;
        }

        public static string GeneratePassword()
        {
            int length = 32;
            bool nonAlphanumeric = true;
            bool digit = true;
            bool lowercase = true;
            bool uppercase = true;

            StringBuilder password = new StringBuilder();
            Random random = new Random();

            while (password.Length < length)
            {
                char c = (char)random.Next(32, 126);

                password.Append(c);

                if (char.IsDigit(c))
                    digit = false;
                else if (char.IsLower(c))
                    lowercase = false;
                else if (char.IsUpper(c))
                    uppercase = false;
                else if (!char.IsLetterOrDigit(c))
                    nonAlphanumeric = false;
            }

            if (nonAlphanumeric)
                password.Append((char)random.Next(33, 48));
            if (digit)
                password.Append((char)random.Next(48, 58));
            if (lowercase)
                password.Append((char)random.Next(97, 123));
            if (uppercase)
                password.Append((char)random.Next(65, 91));

            return password.ToString();
        }

        public static JsonSerializerSettings getSnakeCaseJsonSerializerSettings()
        {
            DefaultContractResolver contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            };

            return new JsonSerializerSettings
            {
                ContractResolver = contractResolver,
                Formatting = Formatting.Indented,
            };
        }

        public static Guid[] GetGuidArray(string value, char seperator)
        {
            return value.Split(seperator)
                    .Select(g => { Guid temp; return Guid.TryParse(g, out temp) ? temp : Guid.Empty; })
                    .Where(g => g != Guid.Empty)
                    .ToArray();
        }

        public static string GetConsoleSeparator(string label, int length = 150)
        {
            string separator = $"\n##### {label} #####";
            return separator.PadRight(length, '#');
        }
    }
}
