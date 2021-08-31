using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Reflection;

namespace starbase_nexus_api.Helpers
{
    public static class IEnumerableExtension
    {
        public static IEnumerable<ExpandoObject> ShapeData<TSource>(this IEnumerable<TSource> source, string fields)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            List<ExpandoObject> objectList = new List<ExpandoObject>();
            List<PropertyInfo> propertyInfoList = new List<PropertyInfo>();

            if (string.IsNullOrWhiteSpace(fields))
            {
                PropertyInfo[] propertyInfos = typeof(TSource).GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                propertyInfoList.AddRange(propertyInfos);
            }
            else
            {
                string[] fieldsAfterSplit = fields.Split(",");
                foreach (string field in fieldsAfterSplit)
                {
                    string propertyName = field.Trim();
                    PropertyInfo? propertyInfo = typeof(TSource).GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);

                    if (propertyInfo == null)
                        continue;

                    propertyInfoList.Add(propertyInfo);
                }
            }

            foreach (TSource sourceObject in source)
            {
                ExpandoObject dataShapedObject = new ExpandoObject();
                foreach (PropertyInfo propertyInfo in propertyInfoList)
                {
                    object? propertyValue = propertyInfo.GetValue(sourceObject);

#pragma warning disable CS8604 // Possible null reference argument.
                    ((IDictionary<string, object>)dataShapedObject).Add(propertyInfo.Name, propertyValue);
#pragma warning restore CS8604 // Possible null reference argument.
                              // alternative way
                              // dataShapedObject.TryAdd(propertyInfo.Name, propertyValue);
                }

                objectList.Add(dataShapedObject);
            }

            return objectList;
        }
    }
}
