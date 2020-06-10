using System.ComponentModel.DataAnnotations;

namespace PDFiller.Website.Models
{
    public class FormModel
    {
        public FormModel(bool setValues = true)
        {
            if (setValues)
            {
                foreach (var prop in GetType().GetProperties())
                {
                    if (prop.PropertyType == typeof(string))
                    {
                        prop.SetValue(this, prop.Name, null);
                    }
                }
            }
        }
    }
}
