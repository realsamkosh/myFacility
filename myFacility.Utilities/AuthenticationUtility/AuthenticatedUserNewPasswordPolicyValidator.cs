using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;

namespace myFacility.Utilities.AuthenticationUtility
{
    public class AuthenticatedUsernewpasswordPolicyValidator
    {
        /// <summary>
        /// This method is used to validate Authenticated user Password Reset.
        /// </summary>
        /// <param name="newpassword">The new inputed password from authenticated user</param>
        /// <param name="passwrule">The Password Policy active on the system</param>
        /// <param name="username">The Authenticated Username</param>
        /// <param name="verificationResult">The Result of Password Verification</param>
        /// <param name="email">The email of the authenticated user</param>
        /// <returns></returns>
        public static List<string> ValidatePasswordPolicy(string newpassword, PasswordOptions passwrule, 
                                                          string username, PasswordVerificationResult verificationResult,string email)
        {
            List<string> result = new List<string>();
                
            //Default 
            if (newpassword == username)
            {
                result.Add($"Sorry, You cannot use your username: {username} as password. Please Input another Password");
            }

            //Check if password is equal to user email
            if (newpassword == email)
            {
                result.Add($"Sorry, You cannot use your email: {email} as password. Please Input another Password");
            }
            //Check if the new inputed password is the same as the the current password
            else if (verificationResult == PasswordVerificationResult.Success)
            {
                result.Add($"{username} active password cannot be set as new password!. Please Input another Password");
            }

            else if (newpassword.Length <= passwrule.RequiredLength)
            {
                result.Add($"The inputed Password Length should be minimum {passwrule.RequiredLength}");
            }
            else if (passwrule.RequireUppercase == true && passwrule.RequireLowercase == true && passwrule.RequireDigit == true)
            {
                if (!newpassword.Any(ch => char.IsUpper(ch)))
                {
                    result.Add("The inputed password must contain Upper case Letter");
                }
                else if (!newpassword.Any(ch => char.IsLower(ch)))
                {
                    result.Add("The inputed password must contain Lower case Letter");
                }
                else if (!newpassword.Any(ch => char.IsDigit(ch)))
                {
                    result.Add("The inputed password must contain Digits");
                }
            }
            else if (passwrule.RequireUppercase == false && passwrule.RequireLowercase == true && passwrule.RequireDigit == true)
            {
                if (!newpassword.Any(ch => char.IsDigit(ch)))
                {
                    result.Add("The inputed password must contain Digits");
                }
                else if (!newpassword.Any(ch => char.IsLower(ch)))
                {
                    result.Add("The inputed password must contain Lower case Letter");
                }
            }
            else if (passwrule.RequireUppercase == false && passwrule.RequireLowercase == false && passwrule.RequireDigit == true)
            {
                if (!newpassword.Any(ch => char.IsDigit(ch)))
                {
                    result.Add("The inputed password must contain Digits");
                }
            }
            else if (passwrule.RequireUppercase == true && passwrule.RequireLowercase == false && passwrule.RequireDigit == false)
            {
                if (!newpassword.Any(ch => char.IsUpper(ch)))
                {
                    result.Add("The inputed password must contain Upper case Letter");
                }
            }
            else if (passwrule.RequireUppercase == false && passwrule.RequireLowercase == true && passwrule.RequireDigit == false)
            {
                if (!newpassword.Any(ch => char.IsLower(ch)))
                {
                    result.Add("The inputed password must contain Lower case Letter");
                }
            }
            else if (passwrule.RequireUppercase == true && passwrule.RequireLowercase == true && passwrule.RequireDigit == false)
            {
                if (!newpassword.Any(ch => char.IsUpper(ch)))
                {
                    result.Add("The inputed password must contain Upper case Letter");
                }
                else if (!newpassword.Any(ch => char.IsLower(ch)))
                {
                    result.Add("The inputed password must contain Lower case Letter");
                }
            }
            return result;
        }
    }
}
