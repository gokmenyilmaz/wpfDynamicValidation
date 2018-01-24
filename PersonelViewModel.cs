using Prism.Commands;
using System.ComponentModel.DataAnnotations;

namespace DynamicValidation
{
    public class PersonelViewModel
    {
        public Personel Kullanici { get; set; }

        public DelegateCommand KaydetCommand => new DelegateCommand(Kaydet);

        public PersonelViewModel()
        {
            Kullanici = new Personel();
        }

        public void Kaydet()
        {
            var c = new ValidationContext(Kullanici);
            Kullanici.Validate(c);

        }
    }
}