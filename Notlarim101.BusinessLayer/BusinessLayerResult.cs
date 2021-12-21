using Notlarim101.Entity.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notlarim101.BusinessLayer
{
    public class BusinessLayerResult<T> where T:class
    {
        public List<KeyValuePair<ErrorMessageCode,string>> Errors { get; set; }// Normalde List tek tip veri taşır fakat KeyValuePair ile birden fazla tiple taşıyabilir.
        public T Result { get; set; }

        public BusinessLayerResult()
        {
            Errors = new List<KeyValuePair<ErrorMessageCode,string>>(); // Listeyi içi boş birşekilde oluşturuyoruz.Çünkü liste eğer oluşmazsa program hataya düşücek bundan dolayı listeyi önden peşin peşin oluşturduk.
        }
        public void AddError(ErrorMessageCode code,string message)
        {
            Errors.Add(new KeyValuePair<ErrorMessageCode, string>(code, message));
        }
    }
}
