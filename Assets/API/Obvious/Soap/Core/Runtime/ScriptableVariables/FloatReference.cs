
namespace Obvious.Soap
{
    [System.Serializable]
    public class FloatReference
    {
        public bool UseLocal = false;
        public float LocalValue;
        public FloatVariable Variable;

        public FloatReference()
        {
        }

        public FloatReference(float value)
        {
            UseLocal = true;
            LocalValue = value;
        }

        public float Value
        {
            get => UseLocal ? LocalValue : Variable.Value;
            set
            {
                if (UseLocal)
                    LocalValue = value;
                else
                    Variable.Value = value;
            }
        }

        public static implicit operator float(FloatReference reference)
        {
            return reference.Value;
        }
    }
}