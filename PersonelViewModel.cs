using Prism.Commands;

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
           var isValid= Kullanici.IsValidModel();

        }
    }
}