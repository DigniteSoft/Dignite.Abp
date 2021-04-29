using System;

namespace Dignite.FieldCustomizing.SeperateValueStoring
{
    public static class FieldValueExtensions
    {
        public static object GetValue(
               this IFieldValue field)
        {
            if (field.NumberValue.HasValue)
                return field.NumberValue.Value;
            else if (field.DateTimeValue.HasValue)
                return field.DateTimeValue.Value;
            else if (field.BooleanValue.HasValue)
                return field.BooleanValue.Value;
            else if (field.DateTimeValue.HasValue)
                return field.GuidValue.Value;
            else if (field.TinyTextValue.IsNullOrEmpty())
                return field.TinyTextValue;
            else
                return field.LongTextValue;
        }


        public static void SetTinyText(
            this IFieldValue field,
            string value)
        {
            field.TinyTextValue = value;
        }

        public static void SetLongText(
            this IFieldValue field,
            string value)
        {
            field.LongTextValue = value;
        }

        public static void SetValue(
            this IFieldValue field,
            DateTime? value)
        {
            field.DateTimeValue = value;
        }

        public static void SetValue(
            this IFieldValue field,
            double? value)
        {
            field.NumberValue = value;
        }

        public static void SetValue(
            this IFieldValue field,
            bool? value)
        {
            field.BooleanValue = value;
        }

        public static void SetValue(
            this IFieldValue field,
            Guid? value)
        {
            field.GuidValue = value;
        }

    }
}
