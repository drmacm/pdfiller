using System.Collections.Specialized;

namespace PDFiller.Domain
{
    public class FormField
    {
        public string FieldName { get; }
   
        public FormFieldType FieldType { get; }
        
        public bool IsRequired { get; private set; }

        public string ProcessedFieldName { get; }
        
        public FormField(string fieldName, FormFieldType fieldType)
        {
            FieldName = fieldName;
            FieldType = fieldType;
            IsRequired = true;

            ProcessedFieldName = FieldNameCleaner.Clean(FieldName);
        }

        public void MakeOptional()
        {
            IsRequired = false;
        }
    }
}
