using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Evie
{
    public static class DebugUtil
    {
        public const string INDENT_STRING = "    ";

        /// <summary>
        /// Return a string representation of the object graph.  Useful for dumping DomainObjects.
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static string ObjectToString(object o)
        {
            return ObjectToString(o, 0, new List<object>());
        }

        private static string ObjectToString(object o, int indentLevel, List<object> stack)
        {
            if (stack.Contains(o))
            {
                return String.Format("[RECURSION]{0}", Environment.NewLine);
            }

            stack.Add(o);

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < indentLevel; i++)
            {
                sb.Append(INDENT_STRING);
            }
            string indent = sb.ToString();
            string indentPlus = indent + INDENT_STRING;

            sb = new StringBuilder();

            if (o == null)
            {
                sb.Append("[NULL]");
            }
            else if (o.GetType().IsArray)
            {
                sb.Append(Environment.NewLine);

                Array array = (Array)(o);
                int arrayLength = array.Length;

                for (int i = 0; i < arrayLength; i++)
                {
                    object elementValue = array.GetValue(i);
                    string valueStr = ObjectToString(elementValue, indentLevel + 1, stack);
                    sb.AppendFormat("{0}{1}[{2}]:  {3}", indent, INDENT_STRING, i, valueStr);
                }
            }
            else
            {
                Type t = o.GetType();
                if (t.Name.EndsWith("Record") ||
                    (t.IsValueType == false &&
                    t.FullName != "System.String")
                    )
                {
                    if (t != typeof(string) && t.GetProperties().Length > 0)
                    {
                        sb.AppendFormat("{1}{0}{2}{{{0}", Environment.NewLine, o.GetType().Name, indent);

                        foreach (PropertyInfo pi in o.GetType().GetProperties())
                        {
                            string propName = pi.Name;
                            string typeDesc = pi.PropertyType.Name;
                            ParameterInfo[] p = pi.GetIndexParameters();
                            if (p.Length > 0)
                            {
                                continue;
                            }
                            object value = pi.GetValue(o, null);
                            sb.AppendFormat("{0}{1} ({2}):  ", indentPlus, propName, typeDesc);

                            string valueStr = ObjectToString(value, indentLevel + 1, stack);
                            sb.AppendFormat("{0}", valueStr);
                        }

                        sb.AppendFormat("{0}}}", indent);
                    }
                }
                else
                {
                    sb.Append(o.ToString());
                }
            }

            stack.Remove(o);
            sb.AppendLine();
            return sb.ToString();
        }
    }
}
