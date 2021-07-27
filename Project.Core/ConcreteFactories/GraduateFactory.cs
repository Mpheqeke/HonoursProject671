using Project.Core.AbstractFactories;
using Project.Core.ConcreteClasses;
using Project.Core.Interfaces;
using Project.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Core.ConcreteFactories
{
    public class GraduateFactory : BaseUserFactory
    {
        public GraduateFactory(User user) : base(user)
        {
        }

        public override IUser Create()
        {
            ConcreteGraduate grad = new ConcreteGraduate();

            _user.UUID = grad.getUUID();

            return grad;
        }
    }
}
