﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DynamicValidation
{
    public class MyBindableBase : INotifyPropertyChanged, INotifyDataErrorInfo
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        
        public bool IsChanged { get; set; }

    
        public bool HasErrors => propErrors.Values.FirstOrDefault(r => r.Count > 0) != null;

        private Dictionary<string, List<string>> propErrors = new Dictionary<string, List<string>>();

     
        public Dictionary<string, List<string>> Hatalar => propErrors;

        public bool SetProperty<T>(ref T field, T value, [CallerMemberName]string propertyName = null)
        {
            // field null gelebilir o yüzden field.equal() kullanamayız
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;

            field = value;
            OnPropertyChanged(propertyName);
            OnPropertyChanged(nameof(IsChanged));

            return true;
        }

        public void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public IEnumerable GetErrors(string propertyName)
        {
            List<string> errors = new List<string>();
            if (propertyName != null)
            {
                propErrors.TryGetValue(propertyName, out errors);
                return errors;
            }
            else
                return null;
        }

        public bool IsValidModel()
        {
            ClearErrors();

            var vcontext = new ValidationContext(this);
            var vresults = new List<ValidationResult>();

            var b = Validator.TryValidateObject(this, vcontext, vresults, true);

            if (vresults.Any())
            {
                var propertyNames = vresults.SelectMany(c => c.MemberNames).Distinct().ToList();

                foreach (var p_item in propertyNames)
                {
                    propErrors[p_item] = vresults
                        .Where(c => c.MemberNames.Contains(p_item))
                        .Select(c => c.ErrorMessage).Distinct().ToList();

                    ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(p_item));
                }
            }

            return b;
        }

        private void ClearErrors()
        {
            foreach (var pname in propErrors.Keys.ToList())
            {
                propErrors.Remove(pname);
                ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(pname));
            }
        }
    }
}
