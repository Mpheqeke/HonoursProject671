using Project.Core.AbstractFactories;
using Project.Core.ConcreteClasses;
using Project.Core.Interfaces;
using Project.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Core.ConcreteFactories
{
    public class RecruiterFactory : BaseUserFactory
    {
        public RecruiterFactory(User user) : base(user)
        {
        }

        public override IUser Create()
        {
            ConcreteRecruiter recruiter = new ConcreteRecruiter();

            _user.UUID = recruiter.getUUID();

            return recruiter;
        }
    }
}
