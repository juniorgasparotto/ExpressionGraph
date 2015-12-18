﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionGraph.Reflection
{
    public class PropertyReaderIndexerInDictionary : IPropertyReader
    {
        public bool CanRead(UnitReflaction obj, Type type, PropertyInfo property)
        {
            // verify if property is "this[object key]"
            var parameters = property.GetIndexParameters();
            return (obj.Object is IDictionary) && (parameters.Length == 1);
        }

        public IEnumerable<MethodValue> GetValues(UnitReflaction obj, Type type, PropertyInfo property)
        {
            var dictionary = obj.Object as IDictionary;
            var parameters = property.GetIndexParameters();

            foreach (var key in dictionary.Keys)
            {
                var parameter = new MethodValueParam(parameters[0].Name, parameters[0], key);
                yield return new MethodValue(dictionary[key], parameter);
            }
        }
    }
}