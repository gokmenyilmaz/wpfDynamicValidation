using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Prism.Commands;
using Prism.Mvvm;

namespace DynamicValidation
{
    public class Personel : BindableBase,IValidatableObject
    {
        private string adres;
        private string adSoyad;
        private int kilo;
        private int yas;

        public string Adres
        {
            get => adres;
            set => SetProperty(ref adres, value);
        }

        [Required(ErrorMessage ="Bu alan gerekli")]
        public string AdSoyad
        {
            get => adSoyad;
            set => SetProperty(ref adSoyad, value);
        }

        public int Kilo
        {
            get => kilo;
            set => SetProperty(ref kilo, value);
        }

        public int Yas
        {
            get => yas;
            set => SetProperty(ref yas, value);
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var props = new string[] { validationContext.MemberName };

            if (Yas < 10)
            {
                yield return new ValidationResult("yaş 10 dan küçük olamaz",props);
            }

            if (Kilo > 100)
            {
                yield return new ValidationResult("kilo 100 dan büyük olamaz",props);
            }


            
        }
    }
}