﻿using GraphExpression.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphExpression
{
    public static class ComplexExpressionExtensions
    {
        public static Expression<object> AsExpression(this object entityRoot, ComplexBuilder builder = null, bool deep = false)
        {
            builder = builder ?? new ComplexBuilder();
            return builder.Build(entityRoot, deep);
        }
    }
}
