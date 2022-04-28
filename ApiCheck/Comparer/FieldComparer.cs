﻿using ApiCheck.Result;
using ApiCheck.Utility;
using System;
using System.Reflection;

namespace ApiCheck.Comparer
{
  internal class FieldComparer : ComparerBase<FieldInfo>
  {
    public FieldComparer(FieldInfo referenceType, FieldInfo newType, IComparerContext comparerContext)
      : base(referenceType, newType, comparerContext, ResultContext.Field, referenceType.ToString())
    {
    }

    public override IComparerResult Compare()
    {
      ComparerContext.LogDetail(string.Format("Comparing field '{0}'", ReferenceType));
      if (ReferenceType.FieldType.GetCompareableName() != NewType.FieldType.GetCompareableName())
      {
        ComparerResult.AddChangedProperty("Type", ReferenceType.FieldType.GetCompareableName(), NewType.FieldType.GetCompareableName(), Severities.FieldTypeChanged);
      }
      if (ReferenceType.IsStatic != NewType.IsStatic)
      {
        ComparerResult.AddChangedFlag("Static", ReferenceType.IsStatic, Severities.StaticFieldChanged);
      }
      if (ReferenceType.IsStatic && NewType.IsStatic && ReferenceType.FieldType.IsEnum)
      {
        // compare numeric enum values
        object referenceValue = ReferenceType.GetRawConstantValue();
        object newValue = NewType.GetRawConstantValue();
        Type enumBaseType = ReferenceType.FieldType.GetEnumUnderlyingType();

        if (!Convert.ChangeType(referenceValue, enumBaseType).Equals(Convert.ChangeType(newValue, enumBaseType)))
        {
          ComparerResult.AddChangedProperty("Value", referenceValue.ToString(), newValue.ToString(), Severities.ConstEnumValueChanged);
        }
      }
      return ComparerResult;
    }
  }
}
