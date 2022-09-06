namespace Task3 
{
    public class IntMathUtils 
    {
        public static int Log2(int value) 
        {
            int result = 0;
            while((value >>= 1) != 0) 
            {
                result++;
            }
            return result;
        }

        public static int PositivePow(int value, int power) 
        {
            int result = 1;

            for (int i = 0; i < power; i++) 
            {
                result *= value;
            }

            return result;
        }

        public static int Max(int first, int second) 
        {
            return first >= second ? first : second;
        }

        public static int Min(int first, int second) 
        {
            return first <= second ? first : second;
        }
    }
}
