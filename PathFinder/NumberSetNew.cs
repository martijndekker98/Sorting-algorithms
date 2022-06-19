using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace PathFinder
{
    class NumberSetNew : IEnumerable<DataWrapper>
    {
        //private List<IntegralWrapper> numberListIntegral;
        //private List<FloatingPointWrapper> numberListFloating;
        protected List<DataWrapper> LijstBase;
        public delegate void SwitchTest(int a, int b);
        public SwitchTest SwitchNew;

        public WrapperType Type { get; protected set; }
        public bool IsSorted { get; private set; }
        public bool HasRectangles { get; protected set; }
        public int  ResumeIndex { get; protected set; }

        protected int Accessed = 0;
        protected int Switched = 0;

        public virtual int Count => LijstBase.Count;
        public int TimesAccessed => Accessed;
        public int TimesSwitched => Switched;

        public virtual DataWrapper this[int i]
        {
            get {
                Accessed++;
                return LijstBase[i];
            }
        }

        public virtual Rectangle GetCorrespondingRectangle(int i) { return LijstBase[i].CorrespondingRectangle; }

        //TEMP TEST
        public virtual DataWrapper GetItem(int i) { return LijstBase[i]; }

        public NumberSetNew(bool hasRectangles = false, bool sorted = false)
        {
            LijstBase = new List<DataWrapper>();
            IsSorted = sorted;
            HasRectangles = hasRectangles;
            Type = WrapperType.Standard;
            ResumeIndex = 0;
            //if (hasRectangles) SwitchNew = SwitchHas;
            //else SwitchNew = SwitchHasNot;
        }
        public NumberSetNew(List<DataWrapper> lijst, bool hasRectangles, bool sorted = false)
        {
            LijstBase = lijst;
            IsSorted = sorted;
            HasRectangles = hasRectangles;
            Type = WrapperType.Standard;
            ResumeIndex = 0;
            //if (hasRectangles) SwitchNew = SwitchHas;
            //else SwitchNew = SwitchHasNot;
        }

        public void HasBeenSorted() { IsSorted = true; }

        public void UpdateResumeIndex() { ResumeIndex++; }
        public void UpdateResumeIndex(int toUpdate) { ResumeIndex += toUpdate; }
        public void SetResumeIndexTo(int newVal) { ResumeIndex = newVal; }

        // Switch the indices a and b
        public virtual void Switch(int a, int b)
        {
            Switched++;
            if (HasRectangles) SwitchHas(a, b);
            else SwitchHasNot(a, b);
        }

        private void SwitchHas(int a, int b)
        {
            if (a == b) return;
            if (a < 0 || b < 0 || a >= LijstBase.Count || b >= LijstBase.Count)
                throw new IndexOutOfRangeException($"Index out of bounds of array, {a}, {b}, Count: {LijstBase.Count}");

            DataWrapper temp = LijstBase[a];
            double leftb = Canvas.GetLeft(LijstBase[b].CorrespondingRectangle);
            LijstBase[a] = LijstBase[b];
            Canvas.SetLeft(LijstBase[a].CorrespondingRectangle, Canvas.GetLeft(temp.CorrespondingRectangle));
            LijstBase[b] = temp;
            Canvas.SetLeft(temp.CorrespondingRectangle, leftb);
        }
        private void SwitchHasNot(int a, int b)
        {
            if (a == b) return;
            if (a < 0 || b < 0 || a >= LijstBase.Count || b >= LijstBase.Count)
                throw new IndexOutOfRangeException($"Index out of bounds of array, {a}, {b}, Count: {LijstBase.Count}");

            DataWrapper temp = LijstBase[a];
            LijstBase[a] = LijstBase[b];
            LijstBase[b] = temp;
        }

        
        //public virtual void SwitchHasVar(int a, int b)
        //{
        //    if (a < 0 || b < 0 || a >= Lijst.Count || b >= Lijst.Count)
        //    {
        //        Variables.WriteLine($"ERROR, switching not possible with indices {a}, {b}. Count: {Lijst.Count}");
        //        return;
        //    }
        //    var temp = Lijst[a];
        //    double leftb = Canvas.GetLeft(Lijst[b].CorrespondingRectangle);
        //    Lijst[a] = Lijst[b];
        //    Canvas.SetLeft(Lijst[a].CorrespondingRectangle, Canvas.GetLeft(temp.CorrespondingRectangle));
        //    Lijst[b] = temp;
        //    Canvas.SetLeft(temp.CorrespondingRectangle, leftb);
        //}

        public virtual bool CheckCorrectness()
        {
            for (int i = 1; i < LijstBase.Count; i++)
            {
                if (LijstBase[i] < LijstBase[i - 1]) return false;
            }
            return true;
        }

        public IEnumerator<DataWrapper> GetEnumerator()
        {
            foreach (DataWrapper item in LijstBase)
            {
                yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public void ResetCounters()
        {
            Accessed = 0;
            Switched = 0;
        }

        //
        // Because Covariant Returns are not a thing in this .Net/C# version yet...
        //
        public virtual bool ASmallerThanB(int a, int b)
        {
            return this[a] < this[b];
        }
        public virtual bool ASmallerOrEqualB(int a, int b)
        {
            return this[a] < this[b] || this[a] == this[b];
        }

        // TEMP TEST
        public virtual void PrintName() { Variables.WriteLine("DATAWRAPPER type"); }
    }

    class NumberSetNewIntegral : NumberSetNew, IEnumerable<IntegralWrapper>
    {
        public List<IntegralWrapper> Lijst;
        //public delegate void SwitchTest(int a, int b);
        //public SwitchTest SwitchNew;

        public override int Count => Lijst.Count;
        public new IntegralWrapper this[int i]
        {
            get {
                //if (Lijst.Count == 0) { Variables.WriteLine($"In base {LijstBase.Count}"); }
                Accessed++;
                return Lijst[i];
            }
        }

        public override Rectangle GetCorrespondingRectangle(int i) { return Lijst[i].CorrespondingRectangle; }

        public NumberSetNewIntegral(List<IntegralWrapper> lijst, bool hasRectangles, bool sorted = false) : base(hasRectangles, sorted)
        {
            Lijst = lijst;
            Type = WrapperType.IntegralWrapper;
        }
        protected NumberSetNewIntegral(bool hasRectangles = false, bool sorted = false) : base(hasRectangles, sorted) { }


        public override void Switch(int a, int b)
        {
            if (a == b) return;
            if (a < 0 || b < 0 || a >= Lijst.Count || b >= Lijst.Count)
                throw new IndexOutOfRangeException($"Index out of bounds of array, {a}, {b}, Count: {Lijst.Count}");

            Switched++;

            IntegralWrapper temp = Lijst[a];
            Lijst[a] = Lijst[b];
            Lijst[b] = temp;
            //Accessed += 4;
            if (HasRectangles)
            {
                double leftb = Canvas.GetLeft(Lijst[b].CorrespondingRectangle);
                Canvas.SetLeft(Lijst[a].CorrespondingRectangle, Canvas.GetLeft(Lijst[b].CorrespondingRectangle));
                Canvas.SetLeft(Lijst[a].CorrespondingRectangle, leftb);
            }
        }
        // Using 'var temp' instead of 'IntegralWrapper temp' speeds up the code but the difference is small 7029 ms vs 7086 (0.8% faster)

        public override bool CheckCorrectness()
        {
            for (int i = 1; i < Lijst.Count; i++)
            {
                if (Lijst[i] < Lijst[i - 1]) return false;
            }
            return true;
        }

        public new IEnumerator<IntegralWrapper> GetEnumerator()
        {
            foreach (IntegralWrapper item in Lijst)
            {
                yield return item;
            }
        }

        public string ListToString()
        {
            string ans = "(";
            for (int i = 0; i < Lijst.Count; i++)
            {
                if (ResumeIndex == i) ans += "), (";
                ans += (i < Lijst.Count - 1) ? $"{Lijst[i].Value}, ": $"{Lijst[i].Value})";
            }
            return ans;
        }


        // Because Covariant Returns are not a thing in this .Net/C# version yet...
        public override bool ASmallerThanB(int a, int b)
        {
            return this[a] < this[b];
        }
        public override bool ASmallerOrEqualB(int a, int b)
        {
            return this[a] < this[b] || this[a] == this[b];
        }

        public override void PrintName() { Variables.WriteLine("Integral type"); }
    }
    

    class NumberSetNewInt : NumberSetNewIntegral, IEnumerable<IntWrapper>
    {

        public new List<IntWrapper> Lijst;
        //public delegate void SwitchTest(int a, int b);
        //public SwitchTest SwitchNew;

        public NumberSetNewInt(List<IntWrapper> lijst, bool hasRectangles, bool sorted = false) : base(hasRectangles, sorted)
        {
            Lijst = lijst;
            //Type = WrapperType.IntWrapper;
        }

        public override int Count => Lijst.Count;
        public new IntWrapper this[int i]
        {
            get
            {
                Accessed++;
                return Lijst[i];
            }
        }

        public override Rectangle GetCorrespondingRectangle(int i) { return Lijst[i].CorrespondingRectangle; }

        // TEMP METHODS
        public override DataWrapper GetItem(int i) { return Lijst[i]; }

        
        public override void Switch(int a, int b)
        {
            if (a == b) return;
            if (a < 0 || b < 0 || a >= Lijst.Count || b >= Lijst.Count)
                throw new IndexOutOfRangeException($"Index out of bounds of array, {a}, {b}, Count: {Lijst.Count}");

            Switched++;

            IntWrapper temp = Lijst[a];
            Lijst[a] = Lijst[b];
            Lijst[b] = temp;
            Accessed += 4;
            if (HasRectangles)
            {
                double leftb = Canvas.GetLeft(Lijst[b].CorrespondingRectangle);
                Canvas.SetLeft(Lijst[b].CorrespondingRectangle, Canvas.GetLeft(Lijst[a].CorrespondingRectangle));
                Canvas.SetLeft(Lijst[a].CorrespondingRectangle, leftb);
            }
        }
        // Using 'var temp' instead of 'IntegralWrapper temp' speeds up the code but the difference is small 7029 ms vs 7086 (0.8% faster)

        public override bool CheckCorrectness()
        {
            for (int i = 1; i < Lijst.Count; i++)
            {
                if (Lijst[i] < Lijst[i - 1]) return false;
            }
            return true;
        }

        public new IEnumerator<IntWrapper> GetEnumerator()
        {
            foreach (IntWrapper item in Lijst)
            {
                yield return item;
            }
        }

        public new string ListToString()
        {
            string ans = "(";
            for (int i = 0; i < Lijst.Count; i++)
            {
                if (ResumeIndex == i) ans += "), (";
                ans += (i < Lijst.Count - 1) ? $"{Lijst[i].Value}, " : $"{Lijst[i].Value})";
            }
            return ans;
        }

        // Because Covariant Returns are not a thing in this .Net/C# version yet...
        public override bool ASmallerThanB(int a, int b)
        {
            return this[a] < this[b];
        }
        public override bool ASmallerOrEqualB(int a, int b)
        {
            return this[a] <= this[b];
        }

        // TEST TEMP
        public override void PrintName() { Variables.WriteLine("Int type"); }
    }


    class NumberSetNewUint : NumberSetNewIntegral, IEnumerable<UintWrapper>
    {

        public new List<UintWrapper> Lijst;
        //public delegate void SwitchTest(int a, int b);
        //public SwitchTest SwitchNew;

        public NumberSetNewUint(List<UintWrapper> lijst, bool hasRectangles, bool sorted = false) : base(hasRectangles, sorted)
        {
            Lijst = lijst;
            //Type = WrapperType.IntWrapper;
        }

        public override int Count => Lijst.Count;
        public new UintWrapper this[int i]
        {
            get
            {
                Accessed++;
                return Lijst[i];
            }
        }

        public override Rectangle GetCorrespondingRectangle(int i) { return Lijst[i].CorrespondingRectangle; }

        public override void Switch(int a, int b)
        {
            if (a == b) return;
            if (a < 0 || b < 0 || a >= Lijst.Count || b >= Lijst.Count)
                throw new IndexOutOfRangeException($"Index out of bounds of array, {a}, {b}, Count: {Lijst.Count}");

            Switched++;

            UintWrapper temp = Lijst[a];
            Lijst[a] = Lijst[b];
            Lijst[b] = temp;
            Accessed += 4;
            if (HasRectangles)
            {
                double leftb = Canvas.GetLeft(Lijst[b].CorrespondingRectangle);
                Canvas.SetLeft(Lijst[b].CorrespondingRectangle, Canvas.GetLeft(Lijst[a].CorrespondingRectangle));
                Canvas.SetLeft(Lijst[a].CorrespondingRectangle, leftb);
            }
        }
        // Using 'var temp' instead of 'IntegralWrapper temp' speeds up the code but the difference is small 7029 ms vs 7086 (0.8% faster)

        public override bool CheckCorrectness()
        {
            for (int i = 1; i < Lijst.Count; i++)
            {
                if (Lijst[i] < Lijst[i - 1]) return false;
            }
            return true;
        }

        public new IEnumerator<UintWrapper> GetEnumerator()
        {
            foreach (UintWrapper item in Lijst)
            {
                yield return item;
            }
        }

        public new string ListToString()
        {
            string ans = "(";
            for (int i = 0; i < Lijst.Count; i++)
            {
                if (ResumeIndex == i) ans += "), (";
                ans += (i < Lijst.Count - 1) ? $"{Lijst[i].Value}, " : $"{Lijst[i].Value})";
            }
            return ans;
        }

        // Because Covariant Returns are not a thing in this .Net/C# version yet...
        public override bool ASmallerThanB(int a, int b)
        {
            return this[a] < this[b];
        }
        public override bool ASmallerOrEqualB(int a, int b)
        {
            return this[a] <= this[b];
        }
    }


    class NumberSetNewLong : NumberSetNewIntegral, IEnumerable<LongWrapper>
    {

        public new List<LongWrapper> Lijst;
        //public delegate void SwitchTest(int a, int b);
        //public SwitchTest SwitchNew;

        public NumberSetNewLong(List<LongWrapper> lijst, bool hasRectangles, bool sorted = false) : base(hasRectangles, sorted)
        {
            Lijst = lijst;
            //Type = WrapperType.IntWrapper;
        }

        public override int Count => Lijst.Count;
        public new LongWrapper this[int i]
        {
            get
            {
                Accessed++;
                return Lijst[i];
            }
        }

        public override Rectangle GetCorrespondingRectangle(int i) { return Lijst[i].CorrespondingRectangle; }

        public override void Switch(int a, int b)
        {
            if (a == b) return;
            if (a < 0 || b < 0 || a >= Lijst.Count || b >= Lijst.Count)
                throw new IndexOutOfRangeException($"Index out of bounds of array, {a}, {b}, Count: {Lijst.Count}");

            Switched++;

            LongWrapper temp = Lijst[a];
            Lijst[a] = Lijst[b];
            Lijst[b] = temp;
            Accessed += 4;
            if (HasRectangles)
            {
                double leftb = Canvas.GetLeft(Lijst[b].CorrespondingRectangle);
                Canvas.SetLeft(Lijst[b].CorrespondingRectangle, Canvas.GetLeft(Lijst[a].CorrespondingRectangle));
                Canvas.SetLeft(Lijst[a].CorrespondingRectangle, leftb);
            }
        }
        // Using 'var temp' instead of 'IntegralWrapper temp' speeds up the code but the difference is small 7029 ms vs 7086 (0.8% faster)

        public override bool CheckCorrectness()
        {
            for (int i = 1; i < Lijst.Count; i++)
            {
                if (Lijst[i] < Lijst[i - 1]) return false;
            }
            return true;
        }

        public new IEnumerator<LongWrapper> GetEnumerator()
        {
            foreach (LongWrapper item in Lijst)
            {
                yield return item;
            }
        }

        public new string ListToString()
        {
            string ans = "(";
            for (int i = 0; i < Lijst.Count; i++)
            {
                if (ResumeIndex == i) ans += "), (";
                ans += (i < Lijst.Count - 1) ? $"{Lijst[i].Value}, " : $"{Lijst[i].Value})";
            }
            return ans;
        }

        // Because Covariant Returns are not a thing in this .Net/C# version yet...
        public override bool ASmallerThanB(int a, int b)
        {
            return this[a] < this[b];
        }
        public override bool ASmallerOrEqualB(int a, int b)
        {
            return this[a] <= this[b];
        }
    }

    
    class NumberSetNewUlong : NumberSetNewIntegral, IEnumerable<UlongWrapper>
    {

        public new List<UlongWrapper> Lijst;
        //public delegate void SwitchTest(int a, int b);
        //public SwitchTest SwitchNew;

        public NumberSetNewUlong(List<UlongWrapper> lijst, bool hasRectangles, bool sorted = false) : base(hasRectangles, sorted)
        {
            Lijst = lijst;
            //Type = WrapperType.IntWrapper;
        }

        public override int Count => Lijst.Count;
        public new UlongWrapper this[int i]
        {
            get
            {
                Accessed++;
                return Lijst[i];
            }
        }

        public override Rectangle GetCorrespondingRectangle(int i) { return Lijst[i].CorrespondingRectangle; }

        public override void Switch(int a, int b)
        {
            if (a == b) return;
            if (a < 0 || b < 0 || a >= Lijst.Count || b >= Lijst.Count)
                throw new IndexOutOfRangeException($"Index out of bounds of array, {a}, {b}, Count: {Lijst.Count}");

            Switched++;

            UlongWrapper temp = Lijst[a];
            Lijst[a] = Lijst[b];
            Lijst[b] = temp;
            Accessed += 4;
            if (HasRectangles)
            {
                double leftb = Canvas.GetLeft(Lijst[b].CorrespondingRectangle);
                Canvas.SetLeft(Lijst[b].CorrespondingRectangle, Canvas.GetLeft(Lijst[a].CorrespondingRectangle));
                Canvas.SetLeft(Lijst[a].CorrespondingRectangle, leftb);
            }
        }
        // Using 'var temp' instead of 'IntegralWrapper temp' speeds up the code but the difference is small 7029 ms vs 7086 (0.8% faster)

        public override bool CheckCorrectness()
        {
            for (int i = 1; i < Lijst.Count; i++)
            {
                if (Lijst[i] < Lijst[i - 1]) return false;
            }
            return true;
        }

        public new IEnumerator<UlongWrapper> GetEnumerator()
        {
            foreach (UlongWrapper item in Lijst)
            {
                yield return item;
            }
        }

        public new string ListToString()
        {
            string ans = "(";
            for (int i = 0; i < Lijst.Count; i++)
            {
                if (ResumeIndex == i) ans += "), (";
                ans += (i < Lijst.Count - 1) ? $"{Lijst[i].Value}, " : $"{Lijst[i].Value})";
            }
            return ans;
        }

        // Because Covariant Returns are not a thing in this .Net/C# version yet...
        public override bool ASmallerThanB(int a, int b)
        {
            return this[a] < this[b];
        }
        public override bool ASmallerOrEqualB(int a, int b)
        {
            return this[a] <= this[b];
        }
    }


    class NumberSetNewShort : NumberSetNewIntegral, IEnumerable<ShortWrapper>
    {
        public new List<ShortWrapper> Lijst;
        //public delegate void SwitchTest(int a, int b);
        //public SwitchTest SwitchNew;

        public NumberSetNewShort(List<ShortWrapper> lijst, bool hasRectangles, bool sorted = false) : base(hasRectangles, sorted)
        {
            Lijst = lijst;
            //Type = WrapperType.IntWrapper;
        }

        public override int Count => Lijst.Count;
        public new ShortWrapper this[int i]
        {
            get
            {
                Accessed++;
                return Lijst[i];
            }
        }

        public override Rectangle GetCorrespondingRectangle(int i) { return Lijst[i].CorrespondingRectangle; }

        public override void Switch(int a, int b)
        {
            if (a == b) return;
            if (a < 0 || b < 0 || a >= Lijst.Count || b >= Lijst.Count)
                throw new IndexOutOfRangeException($"Index out of bounds of array, {a}, {b}, Count: {Lijst.Count}");

            Switched++;

            ShortWrapper temp = Lijst[a];
            Lijst[a] = Lijst[b];
            Lijst[b] = temp;
            Accessed += 4;
            if (HasRectangles)
            {
                double leftb = Canvas.GetLeft(Lijst[b].CorrespondingRectangle);
                Canvas.SetLeft(Lijst[b].CorrespondingRectangle, Canvas.GetLeft(Lijst[a].CorrespondingRectangle));
                Canvas.SetLeft(Lijst[a].CorrespondingRectangle, leftb);
            }
        }
        // Using 'var temp' instead of 'IntegralWrapper temp' speeds up the code but the difference is small 7029 ms vs 7086 (0.8% faster)

        public override bool CheckCorrectness()
        {
            for (int i = 1; i < Lijst.Count; i++)
            {
                if (Lijst[i] < Lijst[i - 1]) return false;
            }
            return true;
        }

        public new IEnumerator<ShortWrapper> GetEnumerator()
        {
            foreach (ShortWrapper item in Lijst)
            {
                yield return item;
            }
        }

        public new string ListToString()
        {
            string ans = "(";
            for (int i = 0; i < Lijst.Count; i++)
            {
                if (ResumeIndex == i) ans += "), (";
                ans += (i < Lijst.Count - 1) ? $"{Lijst[i].Value}, " : $"{Lijst[i].Value})";
            }
            return ans;
        }

        // Because Covariant Returns are not a thing in this .Net/C# version yet...
        public override bool ASmallerThanB(int a, int b)
        {
            return this[a] < this[b];
        }
        public override bool ASmallerOrEqualB(int a, int b)
        {
            return this[a] <= this[b];
        }
    }


    class NumberSetNewUshort : NumberSetNewIntegral, IEnumerable<UshortWrapper>
    {
        public new List<UshortWrapper> Lijst;
        //public delegate void SwitchTest(int a, int b);
        //public SwitchTest SwitchNew;

        public NumberSetNewUshort(List<UshortWrapper> lijst, bool hasRectangles, bool sorted = false) : base(hasRectangles, sorted)
        {
            Lijst = lijst;
            //Type = WrapperType.IntWrapper;
        }

        public override int Count => Lijst.Count;
        public new UshortWrapper this[int i]
        {
            get
            {
                Accessed++;
                return Lijst[i];
            }
        }

        public override Rectangle GetCorrespondingRectangle(int i) { return Lijst[i].CorrespondingRectangle; }

        public override void Switch(int a, int b)
        {
            if (a == b) return;
            if (a < 0 || b < 0 || a >= Lijst.Count || b >= Lijst.Count)
                throw new IndexOutOfRangeException($"Index out of bounds of array, {a}, {b}, Count: {Lijst.Count}");

            Switched++;

            UshortWrapper temp = Lijst[a];
            Lijst[a] = Lijst[b];
            Lijst[b] = temp;
            Accessed += 4;
            if (HasRectangles)
            {
                double leftb = Canvas.GetLeft(Lijst[b].CorrespondingRectangle);
                Canvas.SetLeft(Lijst[b].CorrespondingRectangle, Canvas.GetLeft(Lijst[a].CorrespondingRectangle));
                Canvas.SetLeft(Lijst[a].CorrespondingRectangle, leftb);
            }
        }
        // Using 'var temp' instead of 'IntegralWrapper temp' speeds up the code but the difference is small 7029 ms vs 7086 (0.8% faster)

        public override bool CheckCorrectness()
        {
            for (int i = 1; i < Lijst.Count; i++)
            {
                if (Lijst[i] < Lijst[i - 1]) return false;
            }
            return true;
        }

        public new IEnumerator<UshortWrapper> GetEnumerator()
        {
            foreach (UshortWrapper item in Lijst)
            {
                yield return item;
            }
        }

        public new string ListToString()
        {
            string ans = "(";
            for (int i = 0; i < Lijst.Count; i++)
            {
                if (ResumeIndex == i) ans += "), (";
                ans += (i < Lijst.Count - 1) ? $"{Lijst[i].Value}, " : $"{Lijst[i].Value})";
            }
            return ans;
        }

        // Because Covariant Returns are not a thing in this .Net/C# version yet...
        public override bool ASmallerThanB(int a, int b)
        {
            return this[a] < this[b];
        }
        public override bool ASmallerOrEqualB(int a, int b)
        {
            return this[a] <= this[b];
        }
    }


    class NumberSetNewSbyte : NumberSetNewIntegral, IEnumerable<SbyteWrapper>
    {
        public new List<SbyteWrapper> Lijst;
        //public delegate void SwitchTest(int a, int b);
        //public SwitchTest SwitchNew;

        public NumberSetNewSbyte(List<SbyteWrapper> lijst, bool hasRectangles, bool sorted = false) : base(hasRectangles, sorted)
        {
            Lijst = lijst;
            //Type = WrapperType.IntWrapper;
        }

        public override int Count => Lijst.Count;
        public new SbyteWrapper this[int i]
        {
            get
            {
                Accessed++;
                return Lijst[i];
            }
        }

        public override Rectangle GetCorrespondingRectangle(int i) { return Lijst[i].CorrespondingRectangle; }

        public override void Switch(int a, int b)
        {
            if (a == b) return;
            if (a < 0 || b < 0 || a >= Lijst.Count || b >= Lijst.Count)
                throw new IndexOutOfRangeException($"Index out of bounds of array, {a}, {b}, Count: {Lijst.Count}");

            Switched++;

            SbyteWrapper temp = Lijst[a];
            Lijst[a] = Lijst[b];
            Lijst[b] = temp;
            Accessed += 4;
            if (HasRectangles)
            {
                double leftb = Canvas.GetLeft(Lijst[b].CorrespondingRectangle);
                Canvas.SetLeft(Lijst[b].CorrespondingRectangle, Canvas.GetLeft(Lijst[a].CorrespondingRectangle));
                Canvas.SetLeft(Lijst[a].CorrespondingRectangle, leftb);
            }
        }
        // Using 'var temp' instead of 'IntegralWrapper temp' speeds up the code but the difference is small 7029 ms vs 7086 (0.8% faster)

        public override bool CheckCorrectness()
        {
            for (int i = 1; i < Lijst.Count; i++)
            {
                if (Lijst[i] < Lijst[i - 1]) return false;
            }
            return true;
        }

        public new IEnumerator<SbyteWrapper> GetEnumerator()
        {
            foreach (SbyteWrapper item in Lijst)
            {
                yield return item;
            }
        }

        public new string ListToString()
        {
            string ans = "(";
            for (int i = 0; i < Lijst.Count; i++)
            {
                if (ResumeIndex == i) ans += "), (";
                ans += (i < Lijst.Count - 1) ? $"{Lijst[i].Value}, " : $"{Lijst[i].Value})";
            }
            return ans;
        }

        // Because Covariant Returns are not a thing in this .Net/C# version yet...
        public override bool ASmallerThanB(int a, int b)
        {
            return this[a] < this[b];
        }
        public override bool ASmallerOrEqualB(int a, int b)
        {
            return this[a] <= this[b];
        }
    }


    class NumberSetNewByte : NumberSetNewIntegral, IEnumerable<ByteWrapper>
    {
        public new List<ByteWrapper> Lijst;
        //public delegate void SwitchTest(int a, int b);
        //public SwitchTest SwitchNew;

        public NumberSetNewByte(List<ByteWrapper> lijst, bool hasRectangles, bool sorted = false) : base(hasRectangles, sorted)
        {
            Lijst = lijst;
            //Type = WrapperType.IntWrapper;
        }

        public override int Count => Lijst.Count;
        public new ByteWrapper this[int i]
        {
            get
            {
                Accessed++;
                return Lijst[i];
            }
        }

        public override Rectangle GetCorrespondingRectangle(int i) { return Lijst[i].CorrespondingRectangle; }

        public override void Switch(int a, int b)
        {
            if (a == b) return;
            if (a < 0 || b < 0 || a >= Lijst.Count || b >= Lijst.Count)
                throw new IndexOutOfRangeException($"Index out of bounds of array, {a}, {b}, Count: {Lijst.Count}");

            Switched++;

            ByteWrapper temp = Lijst[a];
            Lijst[a] = Lijst[b];
            Lijst[b] = temp;
            Accessed += 4;
            if (HasRectangles)
            {
                double leftb = Canvas.GetLeft(Lijst[b].CorrespondingRectangle);
                Canvas.SetLeft(Lijst[b].CorrespondingRectangle, Canvas.GetLeft(Lijst[a].CorrespondingRectangle));
                Canvas.SetLeft(Lijst[a].CorrespondingRectangle, leftb);
            }
        }
        // Using 'var temp' instead of 'IntegralWrapper temp' speeds up the code but the difference is small 7029 ms vs 7086 (0.8% faster)

        public override bool CheckCorrectness()
        {
            for (int i = 1; i < Lijst.Count; i++)
            {
                if (Lijst[i] < Lijst[i - 1]) return false;
            }
            return true;
        }

        public new IEnumerator<ByteWrapper> GetEnumerator()
        {
            foreach (ByteWrapper item in Lijst)
            {
                yield return item;
            }
        }

        public new string ListToString()
        {
            string ans = "(";
            for (int i = 0; i < Lijst.Count; i++)
            {
                if (ResumeIndex == i) ans += "), (";
                ans += (i < Lijst.Count - 1) ? $"{Lijst[i].Value}, " : $"{Lijst[i].Value})";
            }
            return ans;
        }

        // Because Covariant Returns are not a thing in this .Net/C# version yet...
        public override bool ASmallerThanB(int a, int b)
        {
            return this[a] < this[b];
        }
        public override bool ASmallerOrEqualB(int a, int b)
        {
            return this[a] <= this[b];
        }
    }


    ///
    /// FLOATING POINT 
    ///

    class NumberSetNewFloatingPoint : NumberSetNew, IEnumerable<FloatingPointWrapper>
    {
        public List<FloatingPointWrapper> Lijst;
        //public delegate void SwitchTest(int a, int b);
        //public SwitchTest SwitchNew;

        public NumberSetNewFloatingPoint(List<FloatingPointWrapper> lijst, bool hasRectangles, bool sorted = false) : base(hasRectangles, sorted)
        {
            Lijst = lijst;
            Type = WrapperType.FloatingWrapper;
        }
        protected NumberSetNewFloatingPoint(bool hasRectangles = false, bool sorted = false) : base(hasRectangles, sorted) { }

        public override int Count => Lijst.Count;
        public new FloatingPointWrapper this[int i]
        {
            get
            {
                //if (Lijst.Count == 0) { Variables.WriteLine($"In base {LijstBase.Count}"); }
                Accessed++;
                return Lijst[i];
            }
        }

        public override Rectangle GetCorrespondingRectangle(int i) { return Lijst[i].CorrespondingRectangle; }
        

        public override void Switch(int a, int b)
        {
            if (a == b) return;
            if (a < 0 || b < 0 || a >= Lijst.Count || b >= Lijst.Count)
                throw new IndexOutOfRangeException($"Index out of bounds of array, {a}, {b}, Count: {Lijst.Count}");

            Switched++;

            FloatingPointWrapper temp = Lijst[a];
            Lijst[a] = Lijst[b];
            Lijst[b] = temp;
            //Accessed += 4;
            if (HasRectangles)
            {
                double leftb = Canvas.GetLeft(Lijst[b].CorrespondingRectangle);
                Canvas.SetLeft(Lijst[b].CorrespondingRectangle, Canvas.GetLeft(Lijst[a].CorrespondingRectangle));
                Canvas.SetLeft(Lijst[a].CorrespondingRectangle, leftb);
            }
        }
        // Using 'var temp' instead of 'IntegralWrapper temp' speeds up the code but the difference is small 7029 ms vs 7086 (0.8% faster)

        public override bool CheckCorrectness()
        {
            for (int i = 1; i < Lijst.Count; i++)
            {
                if (Lijst[i] < Lijst[i - 1]) return false;
            }
            return true;
        }

        public new IEnumerator<FloatingPointWrapper> GetEnumerator()
        {
            foreach (FloatingPointWrapper item in Lijst)
            {
                yield return item;
            }
        }

        public string ListToString()
        {
            string ans = "(";
            for (int i = 0; i < Lijst.Count; i++)
            {
                if (ResumeIndex == i) ans += "), (";
                ans += (i < Lijst.Count - 1) ? $"{Lijst[i].Value}, " : $"{Lijst[i].Value})";
            }
            return ans;
        }

        // Because Covariant Returns are not a thing in this .Net/C# version yet...
        public override bool ASmallerThanB(int a, int b)
        {
            return this[a] < this[b];
        }
        public override bool ASmallerOrEqualB(int a, int b)
        {
            return this[a] < this[b] || this[a] == this[b];
        }
    }


    class NumberSetNewFloat : NumberSetNewFloatingPoint, IEnumerable<FloatWrapper>
    {

        public new List<FloatWrapper> Lijst;
        //public delegate void SwitchTest(int a, int b);
        //public SwitchTest SwitchNew;

        public NumberSetNewFloat(List<FloatWrapper> lijst, bool hasRectangles, bool sorted = false) : base(hasRectangles, sorted)
        {
            Lijst = lijst;
            //Type = WrapperType.IntWrapper;
        }

        public override int Count => Lijst.Count;
        public new FloatWrapper this[int i]
        {
            get
            {
                Accessed++;
                return Lijst[i];
            }
        }

        public override Rectangle GetCorrespondingRectangle(int i) { return Lijst[i].CorrespondingRectangle; }

        public override void Switch(int a, int b)
        {
            if (a == b) return;
            if (a < 0 || b < 0 || a >= Lijst.Count || b >= Lijst.Count)
                throw new IndexOutOfRangeException($"Index out of bounds of array, {a}, {b}, Count: {Lijst.Count}");

            Switched++;

            FloatWrapper temp = Lijst[a];
            Lijst[a] = Lijst[b];
            Lijst[b] = temp;
            Accessed += 4;
            if (HasRectangles)
            {
                double leftb = Canvas.GetLeft(Lijst[b].CorrespondingRectangle);
                Canvas.SetLeft(Lijst[b].CorrespondingRectangle, Canvas.GetLeft(Lijst[a].CorrespondingRectangle));
                Canvas.SetLeft(Lijst[a].CorrespondingRectangle, leftb);
            }
        }
        // Using 'var temp' instead of 'IntegralWrapper temp' speeds up the code but the difference is small 7029 ms vs 7086 (0.8% faster)

        public override bool CheckCorrectness()
        {
            for (int i = 1; i < Lijst.Count; i++)
            {
                if (Lijst[i] < Lijst[i - 1]) return false;
            }
            return true;
        }

        public new IEnumerator<FloatWrapper> GetEnumerator()
        {
            foreach (FloatWrapper item in Lijst)
            {
                yield return item;
            }
        }

        public new string ListToString()
        {
            string ans = "(";
            for (int i = 0; i < Lijst.Count; i++)
            {
                if (ResumeIndex == i) ans += "), (";
                ans += (i < Lijst.Count - 1) ? $"{Lijst[i].Value}, " : $"{Lijst[i].Value})";
            }
            return ans;
        }

        // Because Covariant Returns are not a thing in this .Net/C# version yet...
        public override bool ASmallerThanB(int a, int b)
        {
            return this[a] < this[b];
        }
        public override bool ASmallerOrEqualB(int a, int b)
        {
            return this[a] <= this[b];
        }
    }


    class NumberSetNewDouble : NumberSetNewFloatingPoint, IEnumerable<DoubleWrapper>
    {

        public new List<DoubleWrapper> Lijst;
        //public delegate void SwitchTest(int a, int b);
        //public SwitchTest SwitchNew;

        public NumberSetNewDouble(List<DoubleWrapper> lijst, bool hasRectangles, bool sorted = false) : base(hasRectangles, sorted)
        {
            Lijst = lijst;
            //Type = WrapperType.IntWrapper;
        }

        public override int Count => Lijst.Count;
        public new DoubleWrapper this[int i]
        {
            get
            {
                Accessed++;
                return Lijst[i];
            }
        }

        public override Rectangle GetCorrespondingRectangle(int i) { return Lijst[i].CorrespondingRectangle; }

        public override void Switch(int a, int b)
        {
            if (a == b) return;
            if (a < 0 || b < 0 || a >= Lijst.Count || b >= Lijst.Count)
                throw new IndexOutOfRangeException($"Index out of bounds of array, {a}, {b}, Count: {Lijst.Count}");

            Switched++;

            DoubleWrapper temp = Lijst[a];
            Lijst[a] = Lijst[b];
            Lijst[b] = temp;
            Accessed += 4;
            if (HasRectangles)
            {
                double leftb = Canvas.GetLeft(Lijst[b].CorrespondingRectangle);
                Canvas.SetLeft(Lijst[b].CorrespondingRectangle, Canvas.GetLeft(Lijst[a].CorrespondingRectangle));
                Canvas.SetLeft(Lijst[a].CorrespondingRectangle, leftb);
            }
        }
        // Using 'var temp' instead of 'IntegralWrapper temp' speeds up the code but the difference is small 7029 ms vs 7086 (0.8% faster)

        public override bool CheckCorrectness()
        {
            for (int i = 1; i < Lijst.Count; i++)
            {
                if (Lijst[i] < Lijst[i - 1]) return false;
            }
            return true;
        }

        public new IEnumerator<DoubleWrapper> GetEnumerator()
        {
            foreach (DoubleWrapper item in Lijst)
            {
                yield return item;
            }
        }

        public new string ListToString()
        {
            string ans = "(";
            for (int i = 0; i < Lijst.Count; i++)
            {
                if (ResumeIndex == i) ans += "), (";
                ans += (i < Lijst.Count - 1) ? $"{Lijst[i].Value}, " : $"{Lijst[i].Value})";
            }
            return ans;
        }

        // Because Covariant Returns are not a thing in this .Net/C# version yet...
        public override bool ASmallerThanB(int a, int b)
        {
            return this[a] < this[b];
        }
        public override bool ASmallerOrEqualB(int a, int b)
        {
            return this[a] <= this[b];
        }
    }



}
