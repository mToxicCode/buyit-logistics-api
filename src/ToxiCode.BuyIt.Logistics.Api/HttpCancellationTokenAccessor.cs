namespace ToxiCode.BuyIt.Logistics.Api
{
    /// <summary>
    /// Accessor that allows to gain cancellation token from request contest 
    /// </summary>
    public class HttpCancellationTokenAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HttpCancellationTokenAccessor(IHttpContextAccessor httpContextAccessor)
            => _httpContextAccessor = httpContextAccessor;

        public CancellationToken Token
            => _httpContextAccessor.HttpContext?.RequestAborted ?? CancellationToken.None;
    }
}