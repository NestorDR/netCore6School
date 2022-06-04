using Microsoft.AspNetCore.Mvc;

namespace SchoolWeb.Components
{
    public class HelloWorldViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(int count)
        {
            object someThing = string.Concat(Enumerable.Repeat("Hello world. ", count));
            // someThing = await GetSomething(...);

            return View(someThing);
        }
    }
}
