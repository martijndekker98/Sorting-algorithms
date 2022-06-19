using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace PathFinder.SortingAlgorithms
{
    class BasisSortAlgorithm
    {
        public virtual string GetName() { return "Base"; }

        public virtual void Prepare(NumberSetNew nsni) { Variables.WriteLine("BasisSortAlgo prepare"); }
        //public virtual void Prepare(NumberSetNewInt nsni) { Variables.WriteLine("BasisSortAlgo prepare"); }

        public virtual void Sort(NumberSetNew nsn) { Variables.WriteLine("BasisSortAlgo sort"); }
        //public virtual void Sort(NumberSetNewInt nsni) { Variables.WriteLine("BasisSortAlgo sort"); }

        // public virtual void Step(NumberSetNewInt nsni) { Variables.WriteLine("BasisSortAlgo step"); }
        public virtual void Step(NumberSetNew nsni) { Variables.WriteLine("BasisSortAlgo step"); }

        public virtual void AdjustIndicator(NumberSetNew nsni, Rectangle indicator) { Variables.WriteLine("BasisSortAlgo adjust indicator"); }


        public virtual bool CheckCorrectness(NumberSetNew nsn)
        {
            return nsn.CheckCorrectness();
        }
    }
}
