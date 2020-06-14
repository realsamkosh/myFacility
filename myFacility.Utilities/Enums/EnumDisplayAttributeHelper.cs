﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace myFacility.Utilities.Enums
{
        public static class EnumDisplayAttributeHelper
        {
            /// <summary>
            ///     A generic extension method that aids in reflecting 
            ///     and retrieving any attribute that is applied to an `Enum`.
            /// </summary>
            public static TAttribute GetAttribute<TAttribute>(this Enum enumValue)
                    where TAttribute : Attribute
            {
                return enumValue.GetType()
                                .GetMember(enumValue.ToString())
                                .First()
                                .GetCustomAttribute<TAttribute>();
            }
        }
}
