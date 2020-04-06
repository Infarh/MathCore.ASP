using Microsoft.AspNetCore.Mvc;

namespace MathCore.ASP.WEB.Tests.Components
{
    public class TestSectionViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke() => View();
    }
}
