using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace mcs.components
{
    public class ObjectConverter
    {
        public static List<T> ConvertDataTableToList<T>(DataTable dt)
        {
            var data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }
        // Clean up this method in to smaller functions
        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (Validation.StringsAreEqual(pro.Name, column.ColumnName))
                    {
                        if (!Convert.IsDBNull(dr[column.ColumnName]))
                        {
                            if (pro.PropertyType.Name == "Boolean")
                                pro.SetValue(obj, Convert.ToBoolean(dr[column.ColumnName]), null);
                            else
                                pro.SetValue(obj, dr[column.ColumnName], null);
                        }
                        break;
                    }
                    continue;
                }
            }
            return obj;
        }
    }
}