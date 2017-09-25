using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Todo.Api.Mappers
{
    using Todo.Domain.Models;
    using Todo.Api.Models;
    public static class ListMapper
    {
        public static ListApiModel ToApiModel(this ListModel domainModel)
        {
            if (domainModel == null)
                return null;

            var retval = new ListApiModel
            {
                Id = domainModel.Id,
                UserId = domainModel.UserId,
                Name = domainModel.Name,
                Items = domainModel.Items.Select( li => li.ToApiModel()).ToArray()
            };

            return retval;
        }

        public static ListModel ToDomainModel(this ListApiModel apiModel)
        {
            if (apiModel == null)
                return null;

            var retval = new ListModel
            {
                Id = apiModel.Id,
                UserId = apiModel.UserId,
                Name = apiModel.Name
            };

            retval.Items.AddRange(apiModel.Items.Select(li => li.ToDomainModel()));
            return retval;
        }
    }
}