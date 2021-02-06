using System.Collections.Generic;

namespace System
{
    public class GenericCompare<T> : IEqualityComparer<T>
    {
        public GenericCompare(Func<T, object> expr)
        {
            _expr = expr;
        }

        public GenericCompare(Func<T, T, bool> expr)
        {
            _equals = expr;
        }

        private Func<T, object> _expr { get; }

        private Func<T, T, bool> _equals { get; }

        public bool Equals(T x, T y)
        {
            if (_equals != null) return _equals(x, y);
            var first = _expr.Invoke(x);
            var sec = _expr.Invoke(y);
            if (first != null && first.Equals(sec)) return true;
            return false;
        }

        public int GetHashCode(T obj)
        {
            return _expr != null ? _expr(obj).GetHashCode() : 0;
        }
    }
}