using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AreaCalc
{
    class Program
    {
        static void Main(string[] args)
        {
            IList<float> sides = new List<float>();
            foreach(string str in args)
            {
                float result = 0;
                if (float.TryParse(str, out result))
                {
                    sides.Add(result);
                }
            }
            
            AreaCalc calc = new AreaCalc();
            calc.SetSides(sides.ToArray());
            //calc.SetSides(new float[] {5, 30, 8, 11.5f, 30 });
            //calc.SetSides(new float[] { 21, 24, 10, 30.5f, 16});
            //calc.SetSides(new float[] { 11.5f, 20, 13, 24 });
            //calc.SetSides(new float[] { 5.50f, 14, 9, 15 });
            //calc.SetSides(new float[] { 34, 32, 41 });

            //calc.SetSides(new float[] { 30, 5, 30, 5 });
            calc.Calculate();
            Console.WriteLine("Result  = " + calc.Area);
            Console.ReadKey();
        }
    }

    class AreaCalc
    {
        Queue<float> _sides;

        public float Area { get; private set; }
        public AreaCalc()
        {
            _sides = new Queue<float>();
        }

        public void SetSides(float[] sides)
        {
            _sides.Clear();
            foreach (float value in sides)
            {
                _sides.Enqueue(value);
            }
        }

        public void Calculate()
        {
            Area = 0;
            float val1 = _sides.Dequeue();
            while (_sides.Count > 0)
            {
                float val2 = _sides.Dequeue();
                if (_sides.Count == 1)
                {
                    float val3 = _sides.Dequeue();
                    float semi = GetTriangleSemiparameter(val1, val2, val3);
                    Area += GetAreaFromArbitraryTriangle(semi, val1, val2, val3);
                }
                else
                {
                    // suppose first and second sides as perpendicular 
                    Area += GetTriangleArea(val1, val2);
                    val1 = GetRootSquare(val1, val2);
                }
            }
        }

        private float GetRootSquare(float val1, float val2)
        {
            return (float)Math.Sqrt(val1 * val1 + val2 * val2);
        }

        private float GetTriangleArea(float basis, float height)
        {
            return .5f * basis * height;
        }

        private float GetTriangleSemiparameter(float val1, float val2, float val3)
        {
            return .5f * (val1 + val2 + val3);
        }

        private float GetAreaFromArbitraryTriangle(float semi, float val1, float val2, float val3)
        {
            return (float)Math.Sqrt(semi*(semi-val1)*(semi-val2)*(semi-val3));
        }
    }
}
