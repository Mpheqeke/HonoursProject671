using Project.Core.Interfaces;
using Project.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Core.AbstractFactories
{
    public abstract class BaseUserFactory
    {
        protected User _user;

        public BaseUserFactory(User user)
        {
            _user = user;
        }

        public User ApplyUUID()
        {

            IUser manager = this.Create();
            _user.UUID = manager.getUUID();
            return _user;
        }

        public abstract IUser Create();
    }
}
