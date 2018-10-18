using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MenuTakip.Models
{
    public class RegisterViewModel
    {
     public string Id { get; set; }
        public string name { get; set; }
        public string soyad { get; set; }
        [Required]
        [Display(Name = "Kullanıcı Adı")]
        public string UserName { get; set; }
         [Display(Name = "Maaş")]
        //[DataType(DataType.Currency)]
        public int maas { get; set; }
        [MaxLength(11)]
        public string tc { get; set; }
        [Display(Name = "Şifre")]
        public string Password { get; set; }
        [Display(Name = "Şifre Tekrar")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Şifreler uyuşmuyor")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Email")]
        [EmailAddress]
      //  [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "geçerli e-mail adres giriniz")]

        public string Email { get; set; }
        public List<SelectListItem> AvailableRoles { get; set; }
        public string RoleId { get; set; }
        [Display(Name = "Kullanıcı Rolü")]
        public string RoleName { get; set; }
        [CreditCard]
        public string kartNo { get; set; }
        [Display(Name = "Telefon")]
        [Phone]
      //  [RegularExpression(@"^(\d{10})$", ErrorMessage = "Yanlış telefon numarası")]
        public string cep { get; set; }
        public string adres { get; set; }
        [Display(Name = "Unvan")]
     public  int isUnvanId { get; set; }
      
    
        public List <SelectListItem> Unvanlar { get; set; }
        public isUnvan isUnvan
        {
            get
            {
                return (isUnvan)this.isUnvanId;
            }
            set
            {
                this.isUnvanId = (int)value;
            }
        }
        public bool IsManager { get; set; }

        public bool IsAdmin { get; set; }

      
     
       
    }
}