using System.Collections.Generic;

namespace abstract_class
{
    public abstract class Expression
    {
        public abstract double Evaluate(Dictionary<string, object> vars);
    }
}
