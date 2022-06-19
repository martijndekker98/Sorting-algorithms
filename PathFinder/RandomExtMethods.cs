using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinder
{
    public static class RandomExtMethods
    {
        /// <summary>
        /// Methods for returning a random Uint
        /// </summary>
        
        // Return a random uint with both min and max included
        public static uint NextUint(this Random rand, uint minIncluded, uint maxIncluded)
        {
            if (maxIncluded <= minIncluded) throw new ArgumentOutOfRangeException("maxIncluded", "maxIncluded must be larger than minIncluded");

            uint randuint;
            // If all possible values are allowed, then any combination of 4 random bytes will be acceptable
            if (minIncluded == uint.MinValue && maxIncluded == uint.MaxValue)
            {
                byte[] bytes = new byte[4];
                rand.NextBytes(bytes);
                randuint = (uint)BitConverter.ToInt32(bytes, 0);
                return randuint;
            }
            // Should happen at least once, so do while. The while is there to prevent modulo bias
            uint range = maxIncluded - minIncluded + 1;
            do
            {
                byte[] bytes = new byte[4];
                rand.NextBytes(bytes);
                randuint = (uint)BitConverter.ToInt32(bytes, 0);
            }
            while (randuint > uint.MaxValue - (((uint.MaxValue % range) + 1) % range));

            return randuint;
        }

        // Return a random uint from 0 to maxInc (both included)
        public static uint NextUint(this Random rand, uint maxInc)
        {
            return NextUint(rand, 0, maxInc);
        }

        // Return a random uint from MinValue to MaxValue (both included)
        public static uint NextUint(this Random rand)
        {
            return NextUint(rand, uint.MinValue, uint.MaxValue);
        }


        /// <summary>
        /// Methods for returning a random Int
        /// </summary>

        // Return a random int with both min and max included
        public static int NextInt(this Random rand, int minIncluded, int maxIncluded)
        {
            if (maxIncluded <= minIncluded) throw new ArgumentOutOfRangeException("maxIncluded", "maxIncluded must be larger than minIncluded");

            // If all possible values are allowed, then any combination of 4 random bytes will be acceptable
            if (minIncluded == int.MinValue && maxIncluded == int.MaxValue)
            {
                byte[] bytes = new byte[4];
                rand.NextBytes(bytes);
                return BitConverter.ToInt32(bytes, 0);
            }

            // Should happen at least once, so do while. The while is there to prevent modulo bias
            uint randuint;
            uint range = (uint)(maxIncluded - minIncluded) + 1;
            do
            {
                byte[] bytes = new byte[4];
                rand.NextBytes(bytes);
                randuint = (uint)BitConverter.ToInt32(bytes, 0);
            }
            while (randuint > uint.MaxValue - (((uint.MaxValue % range) + 1) % range));

            return (int)(randuint % range) + minIncluded;
        }

        // Return a random uint from 0 to maxInc (both included)
        public static int NextInt(this Random rand, int maxInc)
        {
            return NextInt(rand, 0, maxInc);
        }

        // Return a random uint from MinValue to MaxValue (both included)
        public static int NextInt(this Random rand)
        {
            return NextInt(rand, int.MinValue, int.MaxValue);
        }



        /// <summary>
        /// Methods for returning a random Ulong
        /// </summary>

        // Return a random ulong with both min and max included
        public static ulong NextUlong(this Random rand, ulong minIncluded, ulong maxIncluded)
        {
            if (maxIncluded <= minIncluded) throw new ArgumentOutOfRangeException("maxIncluded", "maxIncluded must be larger than minIncluded");

            ulong randulong;
            // If all possible values are allowed, then any combination of 4 random bytes will be acceptable
            if (minIncluded == ulong.MinValue && maxIncluded == ulong.MaxValue)
            {
                byte[] bytes = new byte[8];
                rand.NextBytes(bytes);
                randulong = (ulong)BitConverter.ToInt64(bytes, 0);
                return randulong;
            }
            // Should happen at least once, so do while. The while is there to prevent modulo bias
            ulong range = maxIncluded - minIncluded + 1;
            do
            {
                byte[] bytes = new byte[8];
                rand.NextBytes(bytes);
                randulong = (ulong)BitConverter.ToInt64(bytes, 0);
            }
            while (randulong > ulong.MaxValue - (((ulong.MaxValue % range) + 1) % range));

            return randulong;
        }

        // Return a random ulong from 0 to maxInc (both included)
        public static ulong NextUlong(this Random rand, ulong maxInc)
        {
            return NextUlong(rand, 0, maxInc);
        }

        // Return a random ulong from MinValue to MaxValue (both included)
        public static ulong NextUlong(this Random rand)
        {
            return NextUlong(rand, ulong.MinValue, ulong.MaxValue);
        }


        /// <summary>
        /// Methods for returning a random Long
        /// </summary>

        // Return a random long with both min and max included
        public static long NextLong(this Random rand, long minIncluded, long maxIncluded)
        {
            if (maxIncluded <= minIncluded) throw new ArgumentOutOfRangeException("maxIncluded", "maxIncluded must be larger than minIncluded");

            // If all possible values are allowed, then any combination of 4 random bytes will be acceptable
            if (minIncluded == long.MinValue && maxIncluded == long.MaxValue)
            {
                byte[] bytes = new byte[8];
                rand.NextBytes(bytes);
                return BitConverter.ToInt64(bytes, 0);
            }

            // Should happen at least once, so do while. The while is there to prevent modulo bias
            ulong randulong;
            ulong range = (ulong)(maxIncluded - minIncluded) + 1;
            do
            {
                byte[] bytes = new byte[8];
                rand.NextBytes(bytes);
                randulong = (ulong)BitConverter.ToInt64(bytes, 0);
            }
            while (randulong > ulong.MaxValue - (((ulong.MaxValue % range) + 1) % range));

            return (long)(randulong % range) + minIncluded;
        }

        // Return a random long from 0 to maxInc (both included)
        public static long NextLong(this Random rand, long maxInc)
        {
            return NextLong(rand, 0, maxInc);
        }

        // Return a random long from MinValue to MaxValue (both included)
        public static long NextLong(this Random rand)
        {
            return NextLong(rand, long.MinValue, long.MaxValue);
        }


        







        public static ushort NextUshort(this Random rand, ushort minIncluded, ushort maxIncluded)
        {
            if (maxIncluded <= minIncluded) throw new ArgumentOutOfRangeException("maxIncluded", "maxIncluded must be larger than minIncluded");

            ushort randushort;
            // If all possible values are allowed, then any combination of 4 random bytes will be acceptable
            if (minIncluded == ushort.MinValue && maxIncluded == ushort.MaxValue)
            {
                byte[] bytes = new byte[2];
                rand.NextBytes(bytes);
                randushort = (ushort)BitConverter.ToInt16(bytes, 0);
                return randushort;
            }
            // Should happen at least once, so do while
            ushort range = (ushort)(maxIncluded - minIncluded);
            do
            {
                byte[] bytes = new byte[2];
                rand.NextBytes(bytes);
                randushort = (ushort)BitConverter.ToInt16(bytes, 0);
            }
            while (randushort > ushort.MaxValue - ((ushort.MaxValue % range) + 1) % range);

            return randushort;
        }
    }
}
