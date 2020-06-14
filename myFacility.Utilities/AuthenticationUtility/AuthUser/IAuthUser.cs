using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace myFacility.Utilities.AuthenticationUtility.AuthUser
{
    public interface IAuthUser
    {
        string Name { get; }
        string EmailAddress { get; }
        string UserId { get; }
        string TenantId { get; }
        //long BranchID { get;  }
        string IPAddress { get; }
        string UserCategory { get; }
        string Browser { get; }
        bool Authenticated { get; }
        bool IsAuthenticated();
        IEnumerable<Claim> GetClaimsIdentity();
    }
}
