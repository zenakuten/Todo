using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Todo.Api.Mappers
{
    using Todo.Domain.Models;
    using Todo.Api.Models;

    public static class ListItemMapper
    {
        public static ListItemApiModel ToApiModel(this ListItemModel domainModel)
        {
            if (domainModel == null)
                return null;

            var retval = new ListItemApiModel
            {
                Id = domainModel.Id,
                ListId = domainModel.ListId,
                Deadline = domainModel.Deadline,
                Completed = domainModel.Completed,
                Value = domainModel.Value,
                Details = domainModel.Details
            };

            return retval;
        }

        public static ListItemModel ToDomainModel(this ListItemApiModel apiModel)
        {
            if (apiModel == null)
                return null;

            var retval = new ListItemModel
            {
                Id = apiModel.Id,
                ListId = apiModel.ListId,
                Deadline = apiModel.Deadline,
                Completed = apiModel.Completed,
                Value = apiModel.Value,
                Details = apiModel.Details
            };

            return retval;
        }
    }
}