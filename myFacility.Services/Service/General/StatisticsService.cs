using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using myFacility.Infrastructure;
using myFacility.Model.DataObjects.General.User;
using myFacility.Models.DataObjects.MessageObject;
using myFacility.Utilities.AuthenticationUtility.AuthUser;
using myFacility.Utilities.DateUtility;

namespace myFacility.Services.Handler
{
    public class StatisticsService
    {
        private readonly authDbContext _context;
        private readonly IAuthUser _authUser;
        private readonly ILogger<StatisticsService> _logger;


        public StatisticsService(authDbContext context,
            IAuthUser authUser, ILogger<StatisticsService> logger)
        {
            _context = context;
            _authUser = authUser;
            _logger = logger;
           
        }

        /// <summary>
        /// item 1 = fullname
        /// item 2 = user category
        /// item 3 = email
        /// item 4 = datejoined
        /// item 5 = user category abreviation
        /// </summary>
        /// <returns></returns>
        public async Task<UserFullnameViewModel> UserFullName()
        {
            UserFullnameViewModel user = new UserFullnameViewModel();
            var fetchdata = await _context.Users.FirstOrDefaultAsync(x => x.Id == _authUser.UserId);
            user.username = fetchdata.UserName;
            //user.fullname = fetchdata.LastName + "," + fetchdata.FirstName + "  " + fetchdata.MiddleName;
            user.email = fetchdata.Email;
            user.datejoined = fetchdata.CreatedDate.ToString("MMM dd, yyyy");
            switch (fetchdata.UserCategory)
            {
                case "P":
                    user.usercategory = "Patient";
                    user.usercategoryabv = "P";
                    break;
                case "A":
                    user.usercategory = "Regulator";
                    user.usercategoryabv = "R";
                    break;
                case "H":
                    user.usercategory = "Hospital";
                    user.usercategoryabv = "H";
                    break;
                default:
                    break;
            }
            return user;
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
       
        public async Task<EmailTemplateViewModel> PatientAgreement()
        {
            var fetchtemplate = await _context.TEmailtemplate.FirstOrDefaultAsync(x => x.Code == "PAT_AGRT");
            if (fetchtemplate != null)
            {
                return new EmailTemplateViewModel
                {
                    templateid = fetchtemplate.EtemplateId,
                    body = fetchtemplate.Body,
                    code = fetchtemplate.Code
                };
            }
            else
            {
                return null;
            }
        }

    

     

     
        

        

        

        

      
    }
}
