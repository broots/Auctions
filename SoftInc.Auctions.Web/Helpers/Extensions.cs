using AutoMapper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Web;

namespace SoftInc.Auctions.Web.Helpers
{
    public static class Extensions
    {
        public static CultureInfo CustomCultureInfo
        {
            get
            {
                var ci = new CultureInfo("en-GB");
                ci = ci.ToGhCulture();
                return ci;
            }
        }
        public static IMappingExpression<TSource, TDestination> IgnoreAllNonExisting<TSource, TDestination>
                (this IMappingExpression<TSource, TDestination> expression)
        {
            var flags = BindingFlags.Public | BindingFlags.Instance;
            var sourceType = typeof(TSource);
            var destinationProperties = typeof(TDestination).GetProperties(flags);

            foreach (var property in destinationProperties)
            {
                if (sourceType.GetProperty(property.Name, flags) == null)
                {
                    expression.ForMember(property.Name, opt => opt.Ignore());
                }
            }
            return expression;
        }

        public static CultureInfo ToGhCulture(this CultureInfo ci)
        {
            ci.NumberFormat.CurrencySymbol = "GH₵";
            return ci;
        }
    }
}