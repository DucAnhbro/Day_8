namespace Day_8.Middleware
{
    public class RequestMiddleware
    {
        private readonly RequestDelegate _nextMiddleware;

        public RequestMiddleware(RequestDelegate next)
        {
            _nextMiddleware = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            httpContext.Items["EdgeBrowser"] = httpContext.Request.Headers["User-Agent"].Any(v => v.ToLower().Contains("edge"));

            await _nextMiddleware.Invoke(httpContext);
        }
    }
}
