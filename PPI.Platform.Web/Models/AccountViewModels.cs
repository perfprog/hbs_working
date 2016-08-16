using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PPI.Platform.Web.Models
{
    using PPI.Platform.Core.Attributes;
    using PPI.Platform.Core.Properties;
    using PPI.Platform.Web.Properties;
    public class ExternalLoginConfirmationViewModel
    {
        [Required(ErrorMessageResourceName = "Account_Email_Error", ErrorMessageResourceType = typeof(Resources_Web))]
        [Display(Name = "Account_Email", ResourceType = typeof(Resources_Web))]        
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required(ErrorMessageResourceName = "Account_Provider_Error", ErrorMessageResourceType = typeof(Resources_Web))]
        [Display(Name = "Account_Provider", ResourceType = typeof(Resources_Web))]    
        public string Provider { get; set; }

        [Required(ErrorMessageResourceName = "Account_Code_Error", ErrorMessageResourceType = typeof(Resources_Web))]
        [Display(Name = "Account_Code", ResourceType = typeof(Resources_Web))]    
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Account_RememberBrowser", ResourceType = typeof(Resources_Web))]  
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {        
        public string Email { get; set; }

        [Required(ErrorMessageResourceName = "Account_Email_Error", ErrorMessageResourceType = typeof(Resources_Web))]
        [Display(Name = "Account_Email", ResourceType = typeof(Resources_Web))]
        public string UserName { get; set; }

        public string Link { get; set; }
    }

    public class LoginViewModel
    {
        [Required(ErrorMessageResourceName = "Account_Email_Error", ErrorMessageResourceType = typeof(Resources_Web))]
        [Display(Name = "Account_Email", ResourceType = typeof(Resources_Web))]            
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Account_Password", ResourceType = typeof(Resources_Web))]    
        public string Password { get; set; }

        [Display(Name = "Account_RememberMe", ResourceType = typeof(Resources_Web))]            
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required(ErrorMessageResourceName = "Account_Email_Error", ErrorMessageResourceType = typeof(Resources_Web))]
        [Display(Name = "Account_Id", ResourceType = typeof(Resources_Web))]            
        public string Email { get; set; }

        [Required]
        [StringLength(100,ErrorMessageResourceName="Account_StringLengthError", ErrorMessageResourceType=typeof(Resources_Web),MinimumLength = 6)]
        [DataType(DataType.Password)]
        [RegularExpressionEx("PasswordValidationRegex", typeof(Resources_Entities), ErrorMessageResourceName = "Group_DefaultPassword_RegExError", ErrorMessageResourceType = typeof(Resources_Entities))]
        [Display(Name = "Account_Password", ResourceType = typeof(Resources_Web))]   
        public string Password { get; set; }

        [DataType(DataType.Password)]        
        [Display(Name = "Account_ConfirmPassword", ResourceType = typeof(Resources_Web))]
        [Compare("Password", ErrorMessageResourceType = typeof(Resources_Web), ErrorMessageResourceName = "Account_PasswordCompareError")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessageResourceName = "Account_Role_Error", ErrorMessageResourceType = typeof(Resources_Web))]
        [Display(Name = "Account_Role", ResourceType = typeof(Resources_Web))]    
        public string Role { get; set; }
    }
    public class RegisterParticipantViewModel
    {

        [Required(ErrorMessageResourceName = "Account_Email_Error", ErrorMessageResourceType = typeof(Resources_Web))]
        [Display(Name = "Account_Id", ResourceType = typeof(Resources_Web))]
        public string UserName { get; set; }
        
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessageResourceName = "Account_StringLengthError", ErrorMessageResourceType = typeof(Resources_Web), MinimumLength = 6)]
        [RegularExpressionEx("PasswordValidationRegex", typeof(Resources_Entities), ErrorMessageResourceName = "Group_DefaultPassword_RegExError", ErrorMessageResourceType = typeof(Resources_Entities))]
        [DataType(DataType.Password)]
        [Display(Name = "Account_Password", ResourceType = typeof(Resources_Web))]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Account_ConfirmPassword", ResourceType = typeof(Resources_Web))]
        [Compare("Password", ErrorMessageResourceType = typeof(Resources_Web), ErrorMessageResourceName = "Account_PasswordCompareError")]
        public string ConfirmPassword { get; set; }
        
        [Required]
        public int ParticipantId {get;set; }
        [Required]
        public string Token { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required(ErrorMessageResourceName = "Account_Email_Error", ErrorMessageResourceType = typeof(Resources_Web))]
        [Display(Name = "Account_Email", ResourceType = typeof(Resources_Web))]        
        public string UserName { get; set; }
        
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessageResourceName = "Account_StringLengthError", ErrorMessageResourceType = typeof(Resources_Web), MinimumLength = 6)]
        [RegularExpressionEx("PasswordValidationRegex", typeof(Resources_Entities), ErrorMessageResourceName = "Group_DefaultPassword_RegExError", ErrorMessageResourceType = typeof(Resources_Entities))]
        [DataType(DataType.Password)]
        [Display(Name = "Account_Password", ResourceType = typeof(Resources_Web))]   
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Account_ConfirmPassword", ResourceType = typeof(Resources_Web))]
        [Compare("Password", ErrorMessageResourceType = typeof(Resources_Web), ErrorMessageResourceName = "Account_PasswordCompareError")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        
        public string Email { get; set; }

        [Required(ErrorMessageResourceName = "Account_Email_Error", ErrorMessageResourceType = typeof(Resources_Web))]
        [Display(Name = "Account_Email", ResourceType = typeof(Resources_Web))]        
        public string UserName { get; set; }
    }

}