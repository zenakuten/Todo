using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Todo.Api.Mappers
{
    using Todo.Domain.Models;
    using Todo.Api.Models;
    public static class UserMapper
    {
        public static UserApiModel ToApiModel(this UserModel domainModel)
        {
            if (domainModel == null)
                return null;

            var retval = new UserApiModel
            {
                Id = domainModel.Id,
                UserGuid = domainModel.UserGuid.ToString(),
                Username = domainModel.Username
            };

            return retval;
        }

        public static UserModel ToDomainModel(this UserApiModel apiModel)
        {
            if (apiModel == null)
                return null;

            Guid guid = Guid.Empty;
            Guid.TryParse(apiModel.UserGuid, out guid);

            var retval = new UserModel
            {
                Id = apiModel.Id,
                UserGuid = guid,
                Username = apiModel.Username
            };

            return retval;
        }
    }
}