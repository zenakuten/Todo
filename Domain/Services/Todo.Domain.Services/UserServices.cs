using System;
using System.Collections.Generic;
using System.Text;

namespace Todo.Domain.Services
{
    using Todo.Domain.Interfaces;
    using Todo.Domain.Models;
    using Todo.Data.Interfaces;
    public class UserServices : CrudServices<UserModel>, IUserServices
    {
        private IListServices _listServices;
        public UserServices(IUserRepository userRepository, IListServices listServices) : base(userRepository)
        {
            _listServices = listServices;
        }

        private IUserRepository _userRepository => Repository as IUserRepository;

        public override UserModel Create(UserModel model)
        {
            if (string.IsNullOrEmpty(model.Username))
                return null;

            if (GetByUsername(model.Username) != null)
                return null;

            return base.Create(model);
        }

        public UserModel GetByUsername(string username)
        {
            var retval = _userRepository.GetByUsername(username);
            return retval;
        }

        public UserModel Login(string username, string password)
        {
            //TODO real auth...
            var retval = _userRepository.GetByUsername(username);
            return retval;
        }
    }
}
