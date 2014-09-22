namespace Labor
{
    public class DataToken<T>
    {
        public enum TokenType
        {
            FOUND,
            NOT_FOUND,
            NEW
        }

        public T data;
        public TokenType type;

        public DataToken(T _data) { data = _data; type = TokenType.NEW; }
        public DataToken(T _data, TokenType _type) { data = _data; type = _type; }
    }

    public struct MinMaxPair<T>
    {
        public T min;
        public T max;

        public MinMaxPair(T _min, T _max)
        {
            min = _min;
            max = _max;
        }
    }
}
