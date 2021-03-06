﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Security;

namespace accounts.tac.local.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class PasswordMinLengthAttribute : MinLengthAttribute
    {
        public PasswordMinLengthAttribute() : base(Membership.MinRequiredPasswordLength)
        {
        }
    }
}