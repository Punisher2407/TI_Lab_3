using System;
using System.Numerics;
using System.Security.Cryptography;


namespace RSA
{
    public static class Calculations
    {
        const int ZERO_HASH = 100;
        public static int e = 1; // open exhibitor
        public static int p = 1; // first simple num
        public static int q = 1; // second simple num
        public static int r = 1; // product of two primes
        public static int fi = 1; // Euler's result
        public static int d = 1; // closed exhibitor
        public static long cipherNumFirst = 1;
        public static long sourceNumFirst = 1;
        public static long cipherNumSec = 1;
        public static long sourceNumSec = 1;
        public static long m = 1;
        public static long senderS = 1;
        public static long recieverS = 1;
        public static int GetP()
        {
            return p = GetSimpleNum();
        }
        public static int GetQ()
        {
            q = GetSimpleNum();
            while (q == p)
            {
                q = GetSimpleNum();
            }
            return q;
        }
        public static int GetR()
        {
            return r = p * q;
        }
        public static int GetSimpleNum()
        {
            bool IsSimple = false;
            Random rnd = new Random();
            do
            {
                int rndNum = rnd.Next(2, 10000);
                if (MillerRabinTest(rndNum, 5) == true)
                {
                    IsSimple = MillerRabinTest(rndNum, 5);
                    return rndNum;
                }
                else
                {
                    IsSimple = MillerRabinTest(rndNum, 5);
                }
            } while (IsSimple != true);
            return 0;
        }
        public static bool MillerRabinTest(BigInteger num, int k)
        {
            if (num == 2 || num == 3)
                return true;
            if (num < 2 || num % 2 == 0)
                return false;
            BigInteger t = num - 1;

            int s = 0;

            while (t % 2 == 0)
            {
                t /= 2;
                s += 1;
            }

            for (int i = 0; i < k; i++)
            {
                RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();

                byte[] _a = new byte[num.ToByteArray().LongLength];

                BigInteger a;

                do
                {
                    rng.GetBytes(_a);
                    a = new BigInteger(_a);
                }
                while (a < 2 || a >= num - 2);
                BigInteger x = BigInteger.ModPow(a, t, num);
                if (x == 1 || x == num - 1)
                    continue;
                for (int r = 1; r < s; r++)
                {
                    x = BigInteger.ModPow(x, 2, num);
                    if (x == 1)
                        return false;
                    if (x == num - 1)
                        break;
                }
                if (x != num - 1)
                    return false;
            }
            return true;
        }
        public static int EulerFunction(int p, int q)
        {
            fi = (p - 1) * (q - 1);
            return fi;
        }
        public static int OpenExhibitor(int fi)
        {
            Random rnd = new Random();
            e = rnd.Next(1, fi - 1);
            while(GCD(fi, e) != 1)
            {
                e = rnd.Next(2, fi - 1);
            }
            return e;
        }
        public static int GCD(int a, int b)
        {
            while (b != 0)
            {
                var t = b;
                b = a % b;
                a = t;
            }
            return a;
        }
        public static int Euclide(int fi, int e)
        {
            int d0 = fi, d1 = e;
            int x0 = 1, x1 = 0;
            int y0 = 0, y1 = 1;
            while(d1 > 1)
            {
                int q = d0 / d1;
                int d2 = d0 % d1;
                int x2 = x0 - q * x1;
                int y2 = y0 - q * y1;
                d0 = d1;
                d1 = d2;
                x0 = x1;
                x1 = x2;
                y0 = y1;
                y1 = y2;
            }
            if (y1 < 0)
            {
                y1 += fi;
            }
            return d = y1;
        }
        public static long cryptRSA(long sourceNum, long cipherNum, int e, int r)
        {
            cipherNum = fast_exp(sourceNum, e, r);
            return cipherNum;
        }
        public static long decryptRSA(long sourceNum, long cipherNum, int d, int r)
        {
            sourceNum = fast_exp(cipherNum, d, r);
            return sourceNum;
        }
        public static long fast_exp(long num, int degree, int r)
        {
            long num1 = num;
            int z = degree;
            long x = 1;
            while (z != 0)
            {
                while (z % 2 == 0)
                {
                    z = z / 2;
                    num1 = (num1 * num1) % r;
                }
                z = z - 1;
                x = (x * num1) % r;
            }
            return x;
        }
        ///////////////////////////////////////////
        public static long HashSum(int n)
        {
            long Hi = 0;
            Hi = (ZERO_HASH * sourceNumFirst * ZERO_HASH * sourceNumFirst) % n;
            m = Hi; // m = h(M)
            return m;
        }
        public static long digitalSign(long m, int d, int r)
        {
            //m = HashSum(r);
            senderS = fast_exp(m, d, r);
            return senderS;
        }
        public static long digitalUnsign(long senderS, int e, int r)
        {
            recieverS = fast_exp(senderS, e, r);
            return recieverS;
        }
    }
}