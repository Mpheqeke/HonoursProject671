using Project.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Core.Interfaces
{
    public interface IFireBaseAuth
    {
        SignUpDTO SingUp(FireBaseAuthDTO fireBaseAuthDTO);
        SignUpDTO SingIn(FireBaseAuthDTO fireBaseAuthDTO);

    }
}
