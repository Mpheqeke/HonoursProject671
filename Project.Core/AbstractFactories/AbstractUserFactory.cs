using Project.Core.ConcreteFactories;
using Project.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Core.AbstractFactories
{
    public class AbstractUserFactory
    {
        public BaseUserFactory CreateFactory(User user)
        {
            BaseUserFactory returnVal = null;

            if (user.RoleId == 1)
            {
                returnVal = new GraduateFactory(user);
            }
            else if (user.RoleId == 2)
            {
                returnVal = new RecruiterFactory(user);
            }

            return returnVal;
        }
    }
}
