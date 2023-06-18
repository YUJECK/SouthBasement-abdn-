using System;

namespace SouthBasement
{
    public class ObservableVariable<TVariable>
    {
        public event Action<TVariable> OnChanged;
        private TVariable _value;

        public TVariable Value
        {
            get => _value;
            set { _value = value; OnChanged?.Invoke(_value); }
        }

        public ObservableVariable() => _value = default;

        public ObservableVariable(TVariable value) => _value = value;

        public override string ToString() => _value.ToString();
    }
}