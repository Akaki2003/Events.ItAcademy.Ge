using System.Globalization;

namespace Events.ItAcademy.API.Infrastructure.Middlewares.CultureMiddleware
{
    public class Culturemiddleware
    {
        private readonly RequestDelegate _next;

        public Culturemiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var cultureName = "en-US";

            var queryCulture = context.Request.Headers["Accept-Language"].ToString();

            if (!string.IsNullOrWhiteSpace(queryCulture))
                cultureName = queryCulture.Split(',')[0];

            var culture = new CultureInfo(cultureName);

            CultureInfo.CurrentCulture = culture;
            CultureInfo.CurrentUICulture = culture;

            await _next(context);
        }
    }
}
