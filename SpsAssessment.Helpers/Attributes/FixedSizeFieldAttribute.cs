namespace SpsAssessment.Helpers.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class FixedSizeFieldAttribute : Attribute
    {
        private int _index;
        private int _length;

        public int index
        {
            get { return _index; }
        }

        public int length
        {
            get { return _length; }
        }

        public FixedSizeFieldAttribute(int index, int length)
        {
            this._index = index;
            this._length = length;
        }
    }
}
