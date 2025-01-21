using System.Text.RegularExpressions;

public class GoogleDriveHelper
{
    public static string GetDownloadLink(string googleDriveUrl)
    {
        var match = Regex.Match(googleDriveUrl, @"(?:drive\.google\.com\/.*?\/d\/)([\w\-]+)");

        if (match.Success)
        {
            string fileId = match.Groups[1].Value;

            return $"https://drive.google.com/uc?export=download&id={fileId}";
        }
        else
        {
            throw new ArgumentException("Невалидная ссылка на Google Drive");
        }
    }
}