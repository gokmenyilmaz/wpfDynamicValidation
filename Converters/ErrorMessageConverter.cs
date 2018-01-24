using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace DynamicValidation.Converters
{
    public class ErrorMessageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var data=value as ReadOnlyObservableCollection<ValidationError>;

            if (data == null) return "";

            var liste = data.Select(c => c.ErrorContent.ToString());

            var hata=String.Join(",", liste);

            return hata;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
