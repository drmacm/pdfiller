using PDFiller.Website.Models;

namespace PDFiller.Website.Services
{
    public static class FormModelExtensions
    {
        public static FormModel SetPropertyValuesToPropertyNames(this FormModel formModel)
        {
            foreach (var prop in formModel.GetType().GetProperties())
            {
                if (prop.PropertyType == typeof(string))
                {
                    prop.SetValue(formModel, prop.Name, null);
                }
            }

            return formModel;
        }
    }
}
