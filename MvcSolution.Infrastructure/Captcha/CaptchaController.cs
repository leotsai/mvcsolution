using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace MvcSolution.Infrastructure.Captcha
{
    public class CaptchaController : Controller
    {
        public virtual void Refresh()
        {
            var img = new ImageBuilder();
            const string chars = "1234567890qwertyuioplkjhgfdsazxcvbnmQWERTYUIOPLKJHGFDSAZXCVBNM";
            var code = string.Empty;
            var ramdon = new Random();
            for (var i = 0; i < 4; i++)
            {
                code += chars.Substring(ramdon.Next(0, chars.Length - 1), 1);
            }
            var bmp = img.Generate(code);
            Session[Config.SessionKey] = code;
            bmp.Save(HttpContext.Response.OutputStream, ImageFormat.Jpeg);
        }
    }
}
