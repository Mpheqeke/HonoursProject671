using Project.Core.DTOs;
using Project.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Core.Services
{
    public class FireBaseAuth : IFireBaseAuth
    {
        private readonly IProjectUnitOfWork _unitOfWork;
        private readonly IFireBaseHelper _fireBaseHelper;

        public FireBaseAuth(IProjectUnitOfWork unitOfWork, IFireBaseHelper fireBaseHelper)
        {
            _unitOfWork = unitOfWork;
            _fireBaseHelper = fireBaseHelper;
        }

        //Make allow anon "[AllowAnonymous]"
        public SignUpDTO SingUp(FireBaseAuthDTO fireBaseAuthDTO)
        {
            var authResponse = _fireBaseHelper.SingUp(fireBaseAuthDTO);
            
            //Register User
            var signUp = new SignUpDTO
            {
                UserFirebaseId = authResponse.LocalId,
                JwtToken = authResponse.idToken
            };

            return signUp;
        }

        //Make allow anon "[AllowAnonymous]"
        public SignUpDTO SingIn(FireBaseAuthDTO fireBaseAuthDTO)
        {
            var authResponse = _fireBaseHelper.SingIn(fireBaseAuthDTO);

            //Register User
            var signIn = new SignUpDTO
            {
                UserFirebaseId = authResponse.LocalId,
                JwtToken = authResponse.idToken
            };

            return signIn;
        }
    }
}
