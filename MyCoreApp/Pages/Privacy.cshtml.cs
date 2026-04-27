using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Globalization;

namespace MyCoreApp.Pages;

public class PrivacyModel : PageModel
{
    public void OnGet()
{
   string dateTime = DateTime.Now.ToString("d", new CultureInfo("en-US"));
   ViewData["TimeStamp"] = dateTime;
}
}

