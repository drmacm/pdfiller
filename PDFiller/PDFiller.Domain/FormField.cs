using System;
using System.Collections.Generic;
using System.Text;

namespace PDFiller.Domain
{
    public class FormField
    {
        public string FieldName { get; }
        public FormFieldType FieldType { get; }

        public FormField(string fieldName, FormFieldType fieldType)
        {
            FieldName = fieldName;
            FieldType = fieldType;
        }
    }
}
