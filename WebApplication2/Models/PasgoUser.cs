﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class PasgoUser
    {
        public int PasgoID { get; set; }
        [Required(ErrorMessage = "Tên không được bỏ trống!")]
        [DataType(DataType.Text, ErrorMessage = "Tên không hợp lệ!")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Số điện thoại không được bỏ trống!")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Số điện thoại không hợp lệ!")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Email không được bỏ trống!")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ!")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email không hợp lệ!")]
        public string Email { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Thời gian không hợp lệ!")]
        public Nullable<System.DateTime> DOB { get; set; }
        [Required(ErrorMessage = "Số điện thoại không được bỏ trống!")]
        public bool Gender_ { get; set; }
        public bool PhoneConfrimed { get; set; }
        public bool EmailConfirmed { get; set; }
        public string Salt { get; set; }
        public string PasswordHashed { get; set; }
        [Required(ErrorMessage = "Mật khẩu không được bỏ trống!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Mật khẩu không được bỏ trống!")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Mật khẩu không trùng khớp!")]
        public string RetypePassword { get; set; }
        public string LoginMessage { get; set; }
        public string DetailMessage { get; set; }
        [Required(ErrorMessage = "Mật khẩu mới không được bỏ trống!")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "Mật khẩu không được bỏ trống!")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Mật khẩu không trùng khớp!")]
        public string RetypeNewPassword { get; set; }
        public int Level { get; set; }
        public Nullable<System.DateTime> Locked { get; set; }
        public string ConnectionIds_ { get; set; }
        public string Avatar { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Conversation> Conversations { get; set; }
    }
}
