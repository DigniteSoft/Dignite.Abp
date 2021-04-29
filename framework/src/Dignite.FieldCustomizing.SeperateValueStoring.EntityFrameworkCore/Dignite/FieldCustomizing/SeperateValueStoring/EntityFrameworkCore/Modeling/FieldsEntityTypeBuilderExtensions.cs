using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Dignite.FieldCustomizing.SeperateValueStoring.EntityFrameworkCore.Modeling
{
    public static class FieldsEntityTypeBuilderExtensions
    {
        public static void ConfigureFieldVaule<T>(this EntityTypeBuilder<T> b, string foreignIdColumnName)
            where T :class, IFieldValue
        {
            b.As<EntityTypeBuilder>().TryConfigureFieldVaule(foreignIdColumnName);
        }

        public static void TryConfigureFieldVaule(this EntityTypeBuilder b,string foreignIdColumnName)
        {
            if (b.Metadata.ClrType.IsAssignableTo<IFieldValue>())
            {
                b.Property(nameof(IFieldValue.ForeignId))
                    .IsRequired()
                    .HasColumnName(foreignIdColumnName);


                b.Property(nameof(IFieldValue.FieldId)).IsRequired();

                b.Property(nameof(IFieldValue.TinyTextValue))
                    .HasMaxLength(FieldValueConsts.MaxTinyTextLength);


                //add index
                b.HasIndex(nameof(IFieldValue.ForeignId));
                b.HasIndex(nameof(IFieldValue.FieldId), nameof(IFieldValue.TinyTextValue));
                b.HasIndex(nameof(IFieldValue.FieldId), nameof(IFieldValue.NumberValue));
                b.HasIndex(nameof(IFieldValue.FieldId), nameof(IFieldValue.DateTimeValue));
                b.HasIndex(nameof(IFieldValue.FieldId), nameof(IFieldValue.BooleanValue));
                b.HasIndex(nameof(IFieldValue.FieldId), nameof(IFieldValue.GuidValue));
            }
        }
    }
}
