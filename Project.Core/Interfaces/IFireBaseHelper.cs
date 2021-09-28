using Project.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Core.Interfaces
{
    public interface IFireBaseHelper
    {
        AuthResponseDTO SingUp(FireBaseAuthDTO signUp);
        AuthResponseDTO SingIn(FireBaseAuthDTO signIn);
    }
}
