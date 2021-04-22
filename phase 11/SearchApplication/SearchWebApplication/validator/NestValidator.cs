using Nest;

namespace SearchWebApplication.validator
{
    public static class NestValidator
    {
        public static T Validate<T>(this T response) where T : IResponse
        {
            if (response.IsValid)
            {
                return response;
            }

            if (response.OriginalException == null)
            {
                throw new NestException();
            }

            if (response.ServerError != null)
            {
                throw new ServerException(response.ServerError, response.OriginalException);
            }

            throw new NestException(response.OriginalException.Message);
        }
    }
}