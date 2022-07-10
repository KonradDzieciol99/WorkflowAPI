﻿using WorkflowApi.DataTransferObject;

namespace WorkflowApi.Services
{
    public interface IAccountService
    {
        void RegisterUser(RegisterUserDto dto);
        Tuple<string, DateTime> GenerateJwt(UserDto dto);
    }

}
