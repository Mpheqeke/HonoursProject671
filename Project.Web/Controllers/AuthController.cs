using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Core.DTOs;
using Project.Core.Interfaces;

namespace Project.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExceptionFilter]
    public class AuthController : ControllerBase
    {
        private readonly IFireBaseAuth _fireBaseAuth;

        public AuthController(IFireBaseAuth fireBaseAuth)
        {
            _fireBaseAuth = fireBaseAuth;
        }

        [HttpPost]
        [Route("~/api/Auth/SingUp")]
        public SignUpDTO SingUp(FireBaseAuthDTO fireBaseAuthDTO)
        {
            return _fireBaseAuth.SingUp(fireBaseAuthDTO);
        }

        [HttpPost]
        [Route("~/api/Auth/SingIn")]
        public SignUpDTO SingIn(FireBaseAuthDTO fireBaseAuthDTO)
        {
            return _fireBaseAuth.SingIn(fireBaseAuthDTO);
        }
    }
}
