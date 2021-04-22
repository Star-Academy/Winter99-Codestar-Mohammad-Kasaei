using System;
using Elasticsearch.Net;

namespace SearchWebApplication.validator
{
    public class ServerException : NestException
    {
        public ServerException()
            : base("Server exception")
        {
        }

        public ServerException(ServerError serverError, Exception original)
            : base(
                $"Server Exception :\n" +
                $"status => {serverError.Status}\n" +
                $"error => {serverError.Error}\n" +
                $"original exception message: {original.Message}"
            )
        {
        }
    }
}