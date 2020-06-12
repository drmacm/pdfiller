using System.Collections.Specialized;

namespace PDFiller.Models
{
    public class FormField
    {
        public string FieldName { get; }
   
        public FormFieldType FieldType { get; }
        
        public bool IsRequired { get; private set; }

        public string HtmlFieldName { get; }

        public string CSharpFieldName { get; }

        public FormField(string fieldName, FormFieldType fieldType)
        {
            FieldName = fieldName;
            HtmlFieldName = fieldName;
            CSharpFieldName = fieldName;
            FieldType = fieldType;
            IsRequired = true;
        }

        public FormField(
            string fieldName, 
            string htmlFieldName,
            string cSharpFieldName,
            FormFieldType fieldType)
        {
            FieldName = fieldName;
            HtmlFieldName = htmlFieldName;
            CSharpFieldName = cSharpFieldName;
            FieldType = fieldType;
            IsRequired = true;
        }

        public void MakeOptional()
        {
            IsRequired = false;
        }
    }
}
