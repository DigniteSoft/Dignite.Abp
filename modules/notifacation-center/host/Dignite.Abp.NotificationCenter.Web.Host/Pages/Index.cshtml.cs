using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

namespace Dignite.Abp.NotificationCenter.Pages
{
    public class IndexModel : NotificationCenterPageModel
    {
        public void OnGet()
        {
            
        }

        public async Task OnPostLoginAsync()
        {
            await HttpContext.ChallengeAsync("oidc");
        }
    }
}