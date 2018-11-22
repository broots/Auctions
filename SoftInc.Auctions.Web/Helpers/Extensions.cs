using AutoMapper;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
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

        public static string ImgToBase64String(this Stream inputStream)
        {
            var imgStr = string.Empty;
            using (MemoryStream ms = new MemoryStream())
            {
                inputStream.Seek(0, SeekOrigin.Begin);
                inputStream.CopyTo(ms);
                var array = ms.GetBuffer();
                imgStr = Convert.ToBase64String(array);
            }

            return imgStr;
        }

        public static string ImgToThumbBase64String(this Stream inputStream)
        {
            var thumbImgStr = string.Empty;
            using (MemoryStream ms = new MemoryStream())
            {
                inputStream.Seek(0, SeekOrigin.Begin);

                var img = Image.FromStream(inputStream);
                var imgThumb = img.GetThumbnailImage(120, 120, () => false, IntPtr.Zero);
                imgThumb.Save(ms, ImageFormat.Png);
                var array = ms.GetBuffer();
                thumbImgStr = Convert.ToBase64String(array);
            }

            return thumbImgStr;
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