﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityGraph
{
    public class ExpressionItemOpenParenthesis<T> : ExpressionItem<T>
    {
        internal ExpressionItemOpenParenthesis(int level, int levelInExpression, int index)
            : base(default(T), level, levelInExpression, index)
        {
        }

        public override string ToString()
        {
            return "(";
        }
    }
}
