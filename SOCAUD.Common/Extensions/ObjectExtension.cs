using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace SOCAUD.Common.Extensions
{
    public static class ObjectExtension
    {
        public static bool HasProperty(this Type obj, string propertyName)
        {
            return obj.GetProperty(propertyName) != null;
        }

        public static string GetNameField<T>(Expression<Func<T, object>> property) where T : class
        {
            if (property.Body is MemberExpression)
                return ((MemberExpression)property.Body).Member.Name;
            else if (property.Body is UnaryExpression)
                return ((MemberExpression)((UnaryExpression)property.Body).Operand).Member.Name;
            else
                return string.Empty;
        }

        public static string GetValueField(this Type obj, dynamic entity, string field)
        {
            IEnumerable<PropertyInfo> list = obj.GetProperties() as IEnumerable<PropertyInfo>;
            PropertyInfo info = list.FirstOrDefault(x => x.Name.ToUpper() == field.Trim().ToUpper());
            var value = info.GetValue(entity);

            return value != null ? value.ToString() : string.Empty;
        }
        public static string GetValueField<T>(this Type obj, dynamic entity, Expression<Func<T, object>> property) where T : class
        {
            IEnumerable<PropertyInfo> list = obj.GetProperties() as IEnumerable<PropertyInfo>;
            var name = "";

            if (property.Body is MemberExpression)
                name = ((MemberExpression)property.Body).Member.Name;
            else if (property.Body is UnaryExpression)
                name = ((MemberExpression)((UnaryExpression)property.Body).Operand).Member.Name;
            else
                name = string.Empty;

            var value = new object();

            if (!string.IsNullOrEmpty(name))
            {
                PropertyInfo info = list.FirstOrDefault(x => x.Name.ToUpper() == name.Trim().ToUpper());
                value = info.GetValue(entity);
            }
            return value != null ? value.ToString() : string.Empty;
        }

        public static object SetValueFields(this Type obj, dynamic entityDestination, dynamic entitySource, string[] fields)
        {
            IEnumerable<PropertyInfo> list = obj.GetProperties() as IEnumerable<PropertyInfo>;

            foreach (var item in fields)
            {
                PropertyInfo infoDestiny = list.FirstOrDefault(x => x.Name.ToUpper() == item.Trim().ToUpper());
                PropertyInfo infoSource = list.FirstOrDefault(x => x.Name.ToUpper() == item.Trim().ToUpper());
                infoDestiny.SetValue(entityDestination, infoSource.GetValue(entitySource), null);
            }

            return entityDestination;
        }

    }
}
