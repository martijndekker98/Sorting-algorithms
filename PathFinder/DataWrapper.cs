using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace PathFinder
{
    enum WrapperType
    {
        Standard,
        IntegralWrapper,
        IntWrapper,
        FloatingWrapper,
    }

    public class DataWrapper : IComparable<DataWrapper>
    {
        public object Value;
        public Rectangle CorrespondingRectangle { get; private set; }
        public DataWrapper(object val) { Value = val; }

        public void SetRectangle(Rectangle rect)
        {
            CorrespondingRectangle = rect;
        }

        public int CompareTo(DataWrapper other)
        {
            Variables.WriteLine($"Datawrapper compare to");
            if (other == null) return 1;
            else return 0;
        }

        // The less than and greater than operators
        public static bool operator <(DataWrapper lhs, DataWrapper rhs)
        {
            return lhs.SmallerThan(rhs);
        }
        public static bool operator >(DataWrapper lhs, DataWrapper rhs)
        {
            return (int)lhs.Value > (int)rhs.Value;
        }

        public virtual bool SmallerThan(DataWrapper rhs)
        {
            //Variables.WriteLine("DataWrapper smaller than Datawrapper");
            return Convert.ToDecimal(Value) < Convert.ToDecimal(rhs.Value);
        }


        //public static bool operator <=(DataWrapper lhs, DataWrapper rhs)
        //{
        //    return lhs.SmallerThan(rhs) || lhs == rhs;
        //}
        //public static bool operator >=(DataWrapper lhs, DataWrapper rhs)
        //{
        //    return lhs.GreaterThan(rhs) || lhs == rhs;
        //}
        //public virtual bool GreaterThan(DataWrapper rhs)
        //{
        //    return false;
        //}

        public override string ToString() { return $"DataWrapper({Value})"; }

        public virtual bool PositiveOnly() { return false; }
        public virtual void Print() { Variables.WriteLine("PRINT"); }

        // TEMP METHODS
        public virtual void TestComp(DataWrapper dw) { Variables.WriteLine("Data - Data"); }
        public virtual void TestComp(IntWrapper dw) { Variables.WriteLine("Data - int"); }
    }

    // For the integral number types: 
    // sbyte, byte, short, ushort, int, uint, long, uint, ulong
    public class IntegralWrapper : DataWrapper, IComparable<IntegralWrapper>
    {
        public IntegralWrapper(object val) : base(val) { }
        
        public int CompareTo(IntegralWrapper other)
        {
            Variables.WriteLine($"IntegralWrapper compare to");
            if (other == null) return 1;
            else return 0;
        }

        public override void Print() { Console.WriteLine("PRINT integral"); }

        public static bool operator <(IntegralWrapper lhs, IntegralWrapper rhs)
        {
            //Variables.WriteLine("IntegralWrapper < IntegralWrapper");
            return lhs.SmallerThan(rhs);
        }
        public static bool operator >(IntegralWrapper lhs, IntegralWrapper rhs)
        {
            return (int)lhs.Value > (int)rhs.Value;
        }

        public virtual bool SmallerThan(IntegralWrapper rhs)
        {
            if (rhs.PositiveOnly()) return Convert.ToUInt64(Value) < Convert.ToUInt64(rhs.Value);
            else return Convert.ToInt64(Value) < Convert.ToInt64(rhs.Value);
        }
        //public override bool SmallerThan(DataWrapper rhs)
        //{
        //    Variables.WriteLine("IntegralWrapper smaller than Datawrapper");
        //    return (decimal)Value < (decimal)rhs.Value;
        //}

        public static bool operator <=(IntegralWrapper lhs, IntegralWrapper rhs)
        {
            return lhs.SmallerThan(rhs) || lhs == rhs;
        }
        public static bool operator >=(IntegralWrapper lhs, IntegralWrapper rhs)
        {
            return lhs.GreaterThan(rhs) || lhs == rhs;
        }
        public virtual bool GreaterThan(IntegralWrapper rhs)
        {
            if (rhs.PositiveOnly()) return Convert.ToUInt64(Value) > Convert.ToUInt64(rhs.Value);
            else return Convert.ToInt64(Value) > Convert.ToInt64(rhs.Value);
        }

        public override string ToString() { return $"IntegralWrapper({Value})"; }
    }

    // For the floating point number types: 
    // float, double, decimal
    public class FloatingPointWrapper : DataWrapper, IComparable<FloatingPointWrapper>
    {
        public FloatingPointWrapper(object val) : base(val) { }

        public int CompareTo(FloatingPointWrapper other)
        {
            Variables.WriteLine($"FloatingPointWrapper compare to");
            if (other == null) return 1;
            else return 0;
        }
        
        public override string ToString() { return $"FloatingPointWrapper({Value})"; }

    }


    public class FloatWrapper : FloatingPointWrapper, IComparable<FloatWrapper>, IEquatable<FloatWrapper>
    {
        public new float Value;
        public FloatWrapper(float val) : base(val) { Value = val; }

        public int CompareTo(FloatWrapper other)
        {
            if (other == null) return 1;
            else return Value.CompareTo(other.Value);
        }

        public override bool Equals(object obj) => this.Equals(obj as FloatWrapper);
        public bool Equals(FloatWrapper other)
        {
            if (other == null) return false;
            //if (this.GetType() != other.GetType()) return false;
            return this.Value == other.Value;
        }

        public override int GetHashCode() => Value.GetHashCode();

        public static bool operator ==(FloatWrapper lhs, FloatWrapper rhs)
        {
            if (lhs is null)
            {
                if (rhs is null) return true;
                return false;
            }
            // Equals handles case of null on right side.
            return lhs.Equals(rhs);
        }

        public static bool operator !=(FloatWrapper lhs, FloatWrapper rhs) => !(lhs == rhs);


        public static bool operator <(FloatWrapper lhs, FloatWrapper rhs)
        {
            return lhs.SmallerThan(rhs);
        }
        public static bool operator >(FloatWrapper lhs, FloatWrapper rhs)
        {
            return lhs.GreaterThan(rhs);
        }

        public static bool operator <=(FloatWrapper lhs, FloatWrapper rhs)
        {
            return lhs.SmallerThan(rhs) || lhs == rhs;
        }
        public static bool operator >=(FloatWrapper lhs, FloatWrapper rhs)
        {
            return lhs.GreaterThan(rhs) || lhs == rhs;
        }

        public bool SmallerThan(FloatWrapper rhs)
        {
            return Value < rhs.Value;
        }
        public bool GreaterThan(FloatWrapper rhs)
        {
            return Value > rhs.Value;
        }

        public override void Print() { Console.WriteLine("PRINT UPDATED"); }

        public override string ToString() { return $"FloatWrapper({Value})"; }
    }

    public class DoubleWrapper : FloatingPointWrapper, IComparable<DoubleWrapper>, IEquatable<DoubleWrapper>
    {
        public new double Value;
        public DoubleWrapper(double val) : base(val) { Value = val; }

        public int CompareTo(DoubleWrapper other)
        {
            if (other == null) return 1;
            else return Value.CompareTo(other.Value);
        }

        public override bool Equals(object obj) => this.Equals(obj as DoubleWrapper);
        public bool Equals(DoubleWrapper other)
        {
            if (other == null) return false;
            //if (this.GetType() != other.GetType()) return false;
            return this.Value == other.Value;
        }

        public override int GetHashCode() => Value.GetHashCode();

        public static bool operator ==(DoubleWrapper lhs, DoubleWrapper rhs)
        {
            if (lhs is null)
            {
                if (rhs is null) return true;
                return false;
            }
            // Equals handles case of null on right side.
            return lhs.Equals(rhs);
        }

        public static bool operator !=(DoubleWrapper lhs, DoubleWrapper rhs) => !(lhs == rhs);


        public static bool operator <(DoubleWrapper lhs, DoubleWrapper rhs)
        {
            return lhs.SmallerThan(rhs);
        }
        public static bool operator >(DoubleWrapper lhs, DoubleWrapper rhs)
        {
            return lhs.GreaterThan(rhs);
        }

        public static bool operator <=(DoubleWrapper lhs, DoubleWrapper rhs)
        {
            return lhs.SmallerThan(rhs) || lhs == rhs;
        }
        public static bool operator >=(DoubleWrapper lhs, DoubleWrapper rhs)
        {
            return lhs.GreaterThan(rhs) || lhs == rhs;
        }

        public bool SmallerThan(DoubleWrapper rhs)
        {
            return Value < rhs.Value;
        }
        public bool GreaterThan(DoubleWrapper rhs)
        {
            return Value > rhs.Value;
        }

        public override void Print() { Console.WriteLine("PRINT UPDATED"); }

        public override string ToString() { return $"DoubleWrapper({Value})"; }
    }


    // Integral wrappers

    public class IntWrapper : IntegralWrapper, IComparable<IntWrapper>, IEquatable<IntWrapper>
    {
        public new int Value;
        public IntWrapper(int val) : base(val) { Value = val; }

        public int CompareTo(IntWrapper other)
        {
            //Variables.WriteLine($"Intwrapper compare to");
            if (other == null) return 1;
            else return Value.CompareTo(other.Value);
            //else return 0;
        }

        public override bool Equals(object obj) => this.Equals(obj as IntWrapper);
        public bool Equals(IntWrapper other)
        {
            if (other == null) return false;
            //if (this.GetType() != other.GetType()) return false;
            return this.Value == other.Value;
        }

        public override int GetHashCode() => Value.GetHashCode();

        public static bool operator ==(IntWrapper lhs, IntWrapper rhs)
        {
            if (lhs is null)
            {
                if (rhs is null) return true;
                return false;
            }
            // Equals handles case of null on right side.
            return lhs.Equals(rhs);
        }

        public static bool operator !=(IntWrapper lhs, IntWrapper rhs) => !(lhs == rhs);


        public static bool operator <(IntWrapper lhs, IntWrapper rhs)
        {
            //Variables.WriteLine("IntWrapper < IntWrapper");
            return lhs.SmallerThan(rhs);
        }
        public static bool operator >(IntWrapper lhs, IntWrapper rhs)
        {
            return lhs.GreaterThan(rhs);
        }
        
        public static bool operator <=(IntWrapper lhs, IntWrapper rhs)
        {
            return lhs.SmallerThan(rhs) || lhs == rhs;
        }
        public static bool operator >=(IntWrapper lhs, IntWrapper rhs)
        {
            return lhs.GreaterThan(rhs) || lhs == rhs;
        }
        
        public bool SmallerThan(IntWrapper rhs)
        {
            //Variables.WriteLine($"Intwrapper smaller than IntWrapper");
            return Value < rhs.Value;
        }
        public bool GreaterThan(IntWrapper rhs)
        {
            return Value > rhs.Value;
        }

        public override void Print() { Console.WriteLine("PRINT UPDATED"); }

        public override string ToString() { return $"IntWrapper({Value})"; }

        // TEMP METHODS
        public override void TestComp(DataWrapper dw) { Variables.WriteLine("int - Data"); }
        public override void TestComp(IntWrapper dw) { Variables.WriteLine("int - int"); }
    }


    public class UintWrapper : IntegralWrapper, IComparable<UintWrapper>, IEquatable<UintWrapper>
    {
        public new uint Value;
        public UintWrapper(uint val) : base(val) { Value = val; }

        public int CompareTo(UintWrapper other)
        {
            if (other == null) return 1;
            else return Value.CompareTo(other.Value);
        }

        public override bool Equals(object obj) => this.Equals(obj as UintWrapper);
        public bool Equals(UintWrapper other)
        {
            if (other == null) return false;
            //if (this.GetType() != other.GetType()) return false;
            return this.Value == other.Value;
        }

        public override int GetHashCode() => Value.GetHashCode();

        public static bool operator ==(UintWrapper lhs, UintWrapper rhs)
        {
            if (lhs is null)
            {
                if (rhs is null) return true;
                return false;
            }
            // Equals handles case of null on right side.
            return lhs.Equals(rhs);
        }

        public static bool operator !=(UintWrapper lhs, UintWrapper rhs) => !(lhs == rhs);


        public static bool operator <(UintWrapper lhs, UintWrapper rhs)
        {
            //Variables.WriteLine("IntWrapper < IntWrapper");
            return lhs.SmallerThan(rhs);
        }
        public static bool operator >(UintWrapper lhs, UintWrapper rhs)
        {
            return lhs.GreaterThan(rhs);
        }

        public static bool operator <=(UintWrapper lhs, UintWrapper rhs)
        {
            return lhs.SmallerThan(rhs) || lhs == rhs;
        }
        public static bool operator >=(UintWrapper lhs, UintWrapper rhs)
        {
            return lhs.GreaterThan(rhs) || lhs == rhs;
        }

        public bool SmallerThan(UintWrapper rhs)
        {
            return Value < rhs.Value;
        }
        public bool GreaterThan(UintWrapper rhs)
        {
            return Value > rhs.Value;
        }

        public override void Print() { Console.WriteLine("PRINT UPDATED"); }

        public override string ToString() { return $"UintWrapper({Value})"; }

        public override bool PositiveOnly() { return true; }
    }


    public class LongWrapper : IntegralWrapper, IComparable<LongWrapper>, IEquatable<LongWrapper>
    {
        public new long Value;
        public LongWrapper(long val) : base(val) { Value = val; }

        public int CompareTo(LongWrapper other)
        {
            if (other == null) return 1;
            else return Value.CompareTo(other.Value);
        }

        public override bool Equals(object obj) => this.Equals(obj as LongWrapper);
        public bool Equals(LongWrapper other)
        {
            if (other == null) return false;
            //if (this.GetType() != other.GetType()) return false;
            return this.Value == other.Value;
        }

        public override int GetHashCode() => Value.GetHashCode();

        public static bool operator ==(LongWrapper lhs, LongWrapper rhs)
        {
            if (lhs is null)
            {
                if (rhs is null) return true;
                return false;
            }
            // Equals handles case of null on right side.
            return lhs.Equals(rhs);
        }

        public static bool operator !=(LongWrapper lhs, LongWrapper rhs) => !(lhs == rhs);


        public static bool operator <(LongWrapper lhs, LongWrapper rhs)
        {
            return lhs.SmallerThan(rhs);
        }
        public static bool operator >(LongWrapper lhs, LongWrapper rhs)
        {
            return lhs.GreaterThan(rhs);
        }

        public static bool operator <=(LongWrapper lhs, LongWrapper rhs)
        {
            return lhs.SmallerThan(rhs) || lhs == rhs;
        }
        public static bool operator >=(LongWrapper lhs, LongWrapper rhs)
        {
            return lhs.GreaterThan(rhs) || lhs == rhs;
        }

        public bool SmallerThan(LongWrapper rhs)
        {
            return Value < rhs.Value;
        }
        public bool GreaterThan(LongWrapper rhs)
        {
            return Value > rhs.Value;
        }

        public override void Print() { Console.WriteLine("PRINT UPDATED"); }

        public override string ToString() { return $"LongWrapper({Value})"; }
    }


    public class UlongWrapper : IntegralWrapper, IComparable<UlongWrapper>, IEquatable<UlongWrapper>
    {
        public new ulong Value;
        public UlongWrapper(ulong val) : base(val) { Value = val; }

        public int CompareTo(UlongWrapper other)
        {
            if (other == null) return 1;
            else return Value.CompareTo(other.Value);
        }

        public override bool Equals(object obj) => this.Equals(obj as UlongWrapper);
        public bool Equals(UlongWrapper other)
        {
            if (other == null) return false;
            //if (this.GetType() != other.GetType()) return false;
            return this.Value == other.Value;
        }

        public override int GetHashCode() => Value.GetHashCode();

        public static bool operator ==(UlongWrapper lhs, UlongWrapper rhs)
        {
            if (lhs is null)
            {
                if (rhs is null) return true;
                return false;
            }
            // Equals handles case of null on right side.
            return lhs.Equals(rhs);
        }

        public static bool operator !=(UlongWrapper lhs, UlongWrapper rhs) => !(lhs == rhs);


        public static bool operator <(UlongWrapper lhs, UlongWrapper rhs)
        {
            return lhs.SmallerThan(rhs);
        }
        public static bool operator >(UlongWrapper lhs, UlongWrapper rhs)
        {
            return lhs.GreaterThan(rhs);
        }

        public static bool operator <=(UlongWrapper lhs, UlongWrapper rhs)
        {
            return lhs.SmallerThan(rhs) || lhs == rhs;
        }
        public static bool operator >=(UlongWrapper lhs, UlongWrapper rhs)
        {
            return lhs.GreaterThan(rhs) || lhs == rhs;
        }

        public bool SmallerThan(UlongWrapper rhs)
        {
            return Value < rhs.Value;
        }
        public bool GreaterThan(UlongWrapper rhs)
        {
            return Value > rhs.Value;
        }

        public override void Print() { Console.WriteLine("PRINT UPDATED"); }

        public override string ToString() { return $"UlongWrapper({Value})"; }

        public override bool PositiveOnly() { return true; }
    }


    public class UshortWrapper : IntegralWrapper, IComparable<UshortWrapper>, IEquatable<UshortWrapper>
    {
        public new ushort Value;
        public UshortWrapper(ushort val) : base(val) { Value = val; }

        public int CompareTo(UshortWrapper other)
        {
            if (other == null) return 1;
            else return Value.CompareTo(other.Value);
        }

        public override bool Equals(object obj) => this.Equals(obj as UshortWrapper);
        public bool Equals(UshortWrapper other)
        {
            if (other == null) return false;
            //if (this.GetType() != other.GetType()) return false;
            return this.Value == other.Value;
        }

        public override int GetHashCode() => Value.GetHashCode();

        public static bool operator ==(UshortWrapper lhs, UshortWrapper rhs)
        {
            if (lhs is null)
            {
                if (rhs is null) return true;
                return false;
            }
            // Equals handles case of null on right side.
            return lhs.Equals(rhs);
        }

        public static bool operator !=(UshortWrapper lhs, UshortWrapper rhs) => !(lhs == rhs);


        public static bool operator <(UshortWrapper lhs, UshortWrapper rhs)
        {
            return lhs.SmallerThan(rhs);
        }
        public static bool operator >(UshortWrapper lhs, UshortWrapper rhs)
        {
            return lhs.GreaterThan(rhs);
        }

        public static bool operator <=(UshortWrapper lhs, UshortWrapper rhs)
        {
            return lhs.SmallerThan(rhs) || lhs == rhs;
        }
        public static bool operator >=(UshortWrapper lhs, UshortWrapper rhs)
        {
            return lhs.GreaterThan(rhs) || lhs == rhs;
        }

        public bool SmallerThan(UshortWrapper rhs)
        {
            return Value < rhs.Value;
        }
        public bool GreaterThan(UshortWrapper rhs)
        {
            return Value > rhs.Value;
        }

        public override void Print() { Console.WriteLine("PRINT UPDATED"); }

        public override string ToString() { return $"UshortWrapper({Value})"; }

        public override bool PositiveOnly() { return true; }
    }


    public class ShortWrapper : IntegralWrapper, IComparable<ShortWrapper>, IEquatable<ShortWrapper>
    {
        public new short Value;
        public ShortWrapper(short val) : base(val) { Value = val; }

        public int CompareTo(ShortWrapper other)
        {
            if (other == null) return 1;
            else return Value.CompareTo(other.Value);
        }

        public override bool Equals(object obj) => this.Equals(obj as ShortWrapper);
        public bool Equals(ShortWrapper other)
        {
            if (other == null) return false;
            //if (this.GetType() != other.GetType()) return false;
            return this.Value == other.Value;
        }

        public override int GetHashCode() => Value.GetHashCode();

        public static bool operator ==(ShortWrapper lhs, ShortWrapper rhs)
        {
            if (lhs is null)
            {
                if (rhs is null) return true;
                return false;
            }
            // Equals handles case of null on right side.
            return lhs.Equals(rhs);
        }

        public static bool operator !=(ShortWrapper lhs, ShortWrapper rhs) => !(lhs == rhs);


        public static bool operator <(ShortWrapper lhs, ShortWrapper rhs)
        {
            return lhs.SmallerThan(rhs);
        }
        public static bool operator >(ShortWrapper lhs, ShortWrapper rhs)
        {
            return lhs.GreaterThan(rhs);
        }

        public static bool operator <=(ShortWrapper lhs, ShortWrapper rhs)
        {
            return lhs.SmallerThan(rhs) || lhs == rhs;
        }
        public static bool operator >=(ShortWrapper lhs, ShortWrapper rhs)
        {
            return lhs.GreaterThan(rhs) || lhs == rhs;
        }

        public bool SmallerThan(ShortWrapper rhs)
        {
            return Value < rhs.Value;
        }
        public bool GreaterThan(ShortWrapper rhs)
        {
            return Value > rhs.Value;
        }

        public override void Print() { Console.WriteLine("PRINT UPDATED"); }

        public override string ToString() { return $"ShortWrapper({Value})"; }
    }


    public class ByteWrapper : IntegralWrapper, IComparable<ByteWrapper>, IEquatable<ByteWrapper>
    {
        public new byte Value;
        public ByteWrapper(byte val) : base(val) { Value = val; }

        public int CompareTo(ByteWrapper other)
        {
            if (other == null) return 1;
            else return Value.CompareTo(other.Value);
        }

        public override bool Equals(object obj) => this.Equals(obj as ByteWrapper);
        public bool Equals(ByteWrapper other)
        {
            if (other == null) return false;
            //if (this.GetType() != other.GetType()) return false;
            return this.Value == other.Value;
        }

        public override int GetHashCode() => Value.GetHashCode();

        public static bool operator ==(ByteWrapper lhs, ByteWrapper rhs)
        {
            if (lhs is null)
            {
                if (rhs is null) return true;
                return false;
            }
            // Equals handles case of null on right side.
            return lhs.Equals(rhs);
        }

        public static bool operator !=(ByteWrapper lhs, ByteWrapper rhs) => !(lhs == rhs);


        public static bool operator <(ByteWrapper lhs, ByteWrapper rhs)
        {
            return lhs.SmallerThan(rhs);
        }
        public static bool operator >(ByteWrapper lhs, ByteWrapper rhs)
        {
            return lhs.GreaterThan(rhs);
        }

        public static bool operator <=(ByteWrapper lhs, ByteWrapper rhs)
        {
            return lhs.SmallerThan(rhs) || lhs == rhs;
        }
        public static bool operator >=(ByteWrapper lhs, ByteWrapper rhs)
        {
            return lhs.GreaterThan(rhs) || lhs == rhs;
        }

        public bool SmallerThan(ByteWrapper rhs)
        {
            return Value < rhs.Value;
        }
        public bool GreaterThan(ByteWrapper rhs)
        {
            return Value > rhs.Value;
        }

        public override void Print() { Console.WriteLine("PRINT UPDATED"); }

        public override string ToString() { return $"ByteWrapper({Value})"; }

        public override bool PositiveOnly() { return true; }
    }


    public class SbyteWrapper : IntegralWrapper, IComparable<SbyteWrapper>, IEquatable<SbyteWrapper>
    {
        public new sbyte Value;
        public SbyteWrapper(sbyte val) : base(val) { Value = val; }

        public int CompareTo(SbyteWrapper other)
        {
            if (other == null) return 1;
            else return Value.CompareTo(other.Value);
        }

        public override bool Equals(object obj) => this.Equals(obj as SbyteWrapper);
        public bool Equals(SbyteWrapper other)
        {
            if (other == null) return false;
            //if (this.GetType() != other.GetType()) return false;
            return this.Value == other.Value;
        }

        public override int GetHashCode() => Value.GetHashCode();

        public static bool operator ==(SbyteWrapper lhs, SbyteWrapper rhs)
        {
            if (lhs is null)
            {
                if (rhs is null) return true;
                return false;
            }
            // Equals handles case of null on right side.
            return lhs.Equals(rhs);
        }

        public static bool operator !=(SbyteWrapper lhs, SbyteWrapper rhs) => !(lhs == rhs);


        public static bool operator <(SbyteWrapper lhs, SbyteWrapper rhs)
        {
            return lhs.SmallerThan(rhs);
        }
        public static bool operator >(SbyteWrapper lhs, SbyteWrapper rhs)
        {
            return lhs.GreaterThan(rhs);
        }

        public static bool operator <=(SbyteWrapper lhs, SbyteWrapper rhs)
        {
            return lhs.SmallerThan(rhs) || lhs == rhs;
        }
        public static bool operator >=(SbyteWrapper lhs, SbyteWrapper rhs)
        {
            return lhs.GreaterThan(rhs) || lhs == rhs;
        }

        public bool SmallerThan(SbyteWrapper rhs)
        {
            return Value < rhs.Value;
        }
        public bool GreaterThan(SbyteWrapper rhs)
        {
            return Value > rhs.Value;
        }

        public override void Print() { Console.WriteLine("PRINT UPDATED"); }

        public override string ToString() { return $"SbyteWrapper({Value})"; }
    }
}
