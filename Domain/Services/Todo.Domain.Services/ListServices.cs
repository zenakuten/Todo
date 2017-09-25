using System;
using System.Collections.Generic;
using System.Text;

namespace Todo.Domain.Services
{
    using System.Linq;
    using Todo.Domain.Models;
    using Todo.Domain.Interfaces;
    using Todo.Data.Interfaces;
    public class ListServices : CrudServices<ListModel>, IListServices
    {
        private IListItemServices _listItemServices;
        private IListRepository _listRepository;
        public ListServices(IListRepository listRepository, IListItemServices listItemServices) : base(listRepository)
        {
            _listRepository = listRepository;
            _listItemServices = listItemServices;
        }

        private void ReadModelItems(ListModel model)
        {
            if (model == null)
                return;

            model.Items.Clear();
            model.Items.AddRange(_listItemServices.GetByList(model));
        }

        private void CreateModelItems(ListModel model)
        {
            model?.Items.ForEach(li => { li.ListId = model.Id; _listItemServices.Create(li); });
        }

        private void DeleteModelItems(ListModel model)
        {
            model?.Items.ForEach(li => _listItemServices.Delete(li.Id));
        }

        private void UpdateModelItems(ListModel model)
        {
            if (model != null)
            {
                //get list items from services
                var listItems = _listItemServices.GetByList(model);
                var matchingItems = model.Items.Where(li => listItems.Any(i => i.Id == li.Id));
                var nonMatchingNewItems = model.Items.Where(li => !listItems.Any(i => i.Id == li.Id));
                var nonMatchingDeletedItems = listItems.Where(li => !model.Items.Any(i => i.Id == li.Id));

                matchingItems.ToList().ForEach(li => _listItemServices.Update(li));
                nonMatchingNewItems.ToList().ForEach(li => { li.ListId = model.Id; _listItemServices.Create(li); });
                nonMatchingDeletedItems.ToList().ForEach(li => _listItemServices.Delete(li));
            }
        }

        public override ListModel Create(ListModel model)
        {
            var retval = base.Create(model);
            CreateModelItems(model);
            return retval;
        }

        public override bool Delete(int Id)
        {
            var model = Read(Id);
            DeleteModelItems(model);
            return base.Delete(Id);
        }

        public override bool Delete(ListModel model)
        {
            DeleteModelItems(model);
            return base.Delete(model);
        }

        public override ListModel Read(int Id)
        {
            var retval = base.Read(Id);
            ReadModelItems(retval);
            return retval;
        }

        public override ListModel Save(ListModel model)
        {
            var repoModel = Read(model.Id);
            if (repoModel == null)
            {
                return Create(model);
            }
            else
            {
                model.Id = repoModel.Id;
                return Update(model);
            }
        }

        public List<ListModel> GetByUserId(int userId)
        {
            var retval = _listRepository.GetByUserId(userId);
            retval.ForEach(list => ReadModelItems(list));
            return retval;
        }

        public override ListModel Update(ListModel model)
        {
            UpdateModelItems(model);
            return base.Update(model);
        }
    }
}
