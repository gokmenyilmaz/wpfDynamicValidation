using System.ComponentModel.DataAnnotations;

namespace DynamicValidation
{
    public class Personel : MyBindableBase
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
    }
}