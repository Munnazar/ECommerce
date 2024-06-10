using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Common.Utilities
{
    public static class Utility
    {

        public static DateTime UnixTimeStampToDateTime(long unixTimeStamp)
        {
            var dateTimeVal = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTimeVal = dateTimeVal.AddSeconds(unixTimeStamp).ToUniversalTime();

            return dateTimeVal;
        }

        public static string RandomString(int length)
        {
            var random = new Random();
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(x => x[random.Next(x.Length)]).ToArray());
        }

        public static string FileUpload(IFormFile file, string uploadDirectory)
        {
            try
            {
                if (file.Length > 0)
                {
                    string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), uploadDirectory);
                    if (!Directory.Exists(uploadPath))
                    {
                        Directory.CreateDirectory(uploadPath);
                    }
                    string filePath = Path.Combine(uploadPath, file.FileName);
                    using (FileStream fileStream = System.IO.File.Create(filePath))
                    {
                        file.CopyTo(fileStream);
                        fileStream.Flush();
                        return Path.Combine(uploadDirectory, file.FileName);
                    }
                }
                else
                {
                    return "Failed";
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions appropriately
                return "Failed";
            }
        }

        public static string GetResourcesFolderPath()
        {
            return Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "ServiceImages");


        }
    }
}
