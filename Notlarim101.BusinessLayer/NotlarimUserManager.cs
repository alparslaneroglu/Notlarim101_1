using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Notlarim101.BusinessLayer.Abstract;
using Notlarim101.DataAccessLayer.EntityFramework;
using Notlarim101.Entity;
using Notlarim101.Entity.ValueObject;

namespace Notlarim101.BusinessLayer
{
    public class NotlarimUserManager
    {
        //Kullanıcı Username kontrolü yapmalıyım.
        //Kullanıcı Email kontrolü yapmalıyım.
        //Kayıt işlemini gerçekleştirmeliyim.
        //Aktivasyon e-posta gönderimi 
        Repository<NotlarimUser> ruser = new Repository<NotlarimUser>();

        public BusinessLayerResult<NotlarimUser> RegisterUser(RegisterViewModel data)
        {
            NotlarimUser user = ruser.Find(s => s.Username == data.Username || s.Email == data.Email);

            BusinessLayerResult<NotlarimUser> lr = new BusinessLayerResult<NotlarimUser>();

            if (user != null)
            {
                if (user.Username == data.Username)
                {
                    lr.AddError(Entity.Messages.ErrorMessageCode.UsernameAlreadyExist, "Kullanıcı daha önceden alınmış.");
                }
                if (user.Email == data.Email)
                {
                    lr.AddError(Entity.Messages.ErrorMessageCode.EmailAlreadyExist, "E mail Adresi daha önceden kullanılmış.");
                }
                //throw new Exception("Kayıtlı kullanıcı yada e-posta adresi");
            }
            else
            {
                int dbResult = ruser.Insert(new NotlarimUser
                {
                    Username = data.Username,
                    Email = data.Email,
                    Password = data.Password,
                    ActivateGuid = Guid.NewGuid(),
                    IsActive = false,
                    IsAdmin = false,
                    //Repository e taşındı.
                    //ModifiedOn = DateTime.Now,
                    //CreatedOn = DateTime.Now,
                    //ModifiedUsername = "sysem"
                });
                if (dbResult > 0)
                {
                    lr.Result = ruser.Find(s => s.Email == data.Email && s.Username == data.Username);
                    //activasyon mail i atılacak.
                    //lr.Result.ActivateGuid;
                }

            }
            return lr;
        }
        public BusinessLayerResult<NotlarimUser> LoginUser(LoginViewModel data)
        {
            //Giris kontrolu
            //Hesap aktif edilmismi kontrolü

            BusinessLayerResult<NotlarimUser> res = new BusinessLayerResult<NotlarimUser>();
            res.Result = ruser.Find(s => s.Username == data.UserName && s.Password == data.Password);
            if (res.Result != null)
            {
                if (!res.Result.IsActive)
                {
                    res.AddError(Entity.Messages.ErrorMessageCode.UserIsNotActive, "Kullanıcı aktifleştirilmemiş!!! ");
                    res.AddError(Entity.Messages.ErrorMessageCode.CheckYourEmail, "Lütfen mailinizi kontrol edin.");
                }

            }
            else
            {
                
                res.AddError(Entity.Messages.ErrorMessageCode.UsernameOrPasswordWrong, "Kullanıcı adı yada şifre uyuşmuyor.");
                
            }
            return res;
        }
    }
}
