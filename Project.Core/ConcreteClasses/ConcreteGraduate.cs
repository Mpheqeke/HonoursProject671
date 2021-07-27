using Project.Core.Interfaces;
using Project.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Core.ConcreteClasses
{
    public class ConcreteGraduate : User, IUser
    {
        public string getUUID()
        {
            Guid g = Guid.NewGuid();
            string guid = Guid.NewGuid().ToString();

            UUID = guid;

            return UUID;
        }
    }
}
