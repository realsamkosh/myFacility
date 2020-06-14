using System;
using System.ComponentModel.DataAnnotations;

namespace myFacility.Utilities.Enums
{
    public enum myFacilityPermissions : short
    {
        NotSet = 0, //error condition

        #region Country
        [Display(GroupName = "Country", Name = "Add New", Description = "Can add a Country")]
        CanAddCountry,
        [Display(GroupName = "Country", Name = "Update One", Description = "Can update a Country")]
        CanUpdateCountry,
        [Display(GroupName = "Country", Name = "Delete One", Description = "Can delete a Country")]
        CanDeleteOneCountry,
        #endregion

        #region State
        [Display(GroupName = "State", Name = "Add New", Description = "Can add a State")]
        CanAddState,
        [Display(GroupName = "State", Name = "Update One", Description = "Can update a State")]
        CanUpdateState,
        [Display(GroupName = "State", Name = "Delete One", Description = "Can delete a State")]
        CanDeleteOneState,
        #endregion

        #region Lga
        [Display(GroupName = "Lga", Name = "Add New", Description = "Can add new Lga")]
        CanAddLga,
        [Display(GroupName = "Lga", Name = "Update One", Description = "Can update one Lga")]
        CanUpdateLga,
        [Display(GroupName = "Lga", Name = "Delete One", Description = "Can delete one Lga")]
        CanDeleteOneLga,
        #endregion


        #region EmailTemplateLkup
        [Display(GroupName = "EmailTemplateLkup", Name = "Read All", Description = "Can read all EmailTemplateLkups")]
        CanReadEmailTemplateLkups,
        [Display(GroupName = "EmailTemplateLkup", Name = "Add New", Description = "Can add new EmailTemplateLkup")]
        CanAddEmailTemplateLkup,
        [Display(GroupName = "EmailTemplateLkup", Name = "Update One", Description = "Can update one EmailTemplateLkup")]
        CanUpdateEmailTemplateLkup,
        [Display(GroupName = "EmailTemplateLkup", Name = "Delete One", Description = "Can delete one EmailTemplateLkup")]
        CanDeleteOneEmailTemplateLkup,
        #endregion



        /// <summary>
        /// All Permissions for Message Endpoints
        /// </summary>
        #region Message Permissions

        #region EmailTemplate
        [Display(GroupName = "EmailTemplate", Name = "Read All by Merchant", Description = "Can read Email Templates by merchant")]
        CanReadEmailTemplatesByMerchant,
        [Display(GroupName = "EmailTemplate", Name = "Read One", Description = "Can read one Email Template")]
        CanReadOneGnsysEmailTemplate,
        [Display(GroupName = "EmailTemplate", Name = "Update One", Description = "Can update one Email Template")]
        CanUpdateGnsysEmailTemplate,
        #endregion

        #endregion
        //========================================================//


        #region Permission Permissions
        [Display(GroupName = "UserAdmin", Name = "Read Roles", Description = "Can list Role")]
        RoleRead = 50,
        [Display(GroupName = "UserAdmin", Name = "Change Role", Description = "Can create, update or delete a Role")]
        RoleChange = 51,
        [Display(GroupName = "UserAdmin", Name = "Read All", Description = "Can read all Permissions")]
        CanReadPermissions,
        [Display(GroupName = "UserAdmin", Name = "Assign Permissions", Description = "Can Assign and Remove Permissions")]
        CanChangeRolePermissions,
        [Display(GroupName = "UserAdmin", Name = "Read All", Description = "Can read all Userss")]
        CanReadUsers,
        [Display(GroupName = "UserAdmin", Name = "Add New", Description = "Can add new Users")]
        CanAddUser,
        [Display(GroupName = "UserAdmin", Name = "Update One", Description = "Can update one Users")]
        CanUpdateUser,
        [Display(GroupName = "UserAdmin", Name = "Delete One", Description = "Can delete one Users")]
        CanDeleteOneUser,
        [Display(GroupName = "UserAdmin", Name = "Read All", Description = "Can read all Students")]
        CanReadStudents,
        [Display(GroupName = "UserAdmin", Name = "Add New", Description = "Can add new Students")]
        CanAddStudent,
        [Display(GroupName = "UserAdmin", Name = "Update One", Description = "Can update one Students")]
        CanUpdateStudent,
        [Display(GroupName = "UserAdmin", Name = "Delete One", Description = "Can delete one Students")]
        CanDeleteOneStudent,
        [Display(GroupName = "UserAdmin", Name = "Read All", Description = "Can read all Supervisor")]
        CanReadSupervisors,
        [Display(GroupName = "UserAdmin", Name = "Assign", Description = "Can add new Supervisor")]
        CanAssignSupervisors,
        [Display(GroupName = "UserAdmin", Name = "Remove", Description = "Can update one Supervisor")]
        CanRemoveSupervisor,
        #endregion

        [Display(GroupName = "Patient", Name = "Read All", Description = "Can do anything as Patient")]
        CanActAsPatient,

        [Display(GroupName = "SuperAdmin", Name = "AccessAll", Description = "This allows the user to access every feature")]
        AccessAll = Int16.MaxValue,
    }
}
