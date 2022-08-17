using Microsoft.AspNetCore.Mvc;
using StudentAPI.Controllers;
using StudentAPI.Models;
using System.Reflection;

namespace StudentAPI.Extensions
{
    public static class HtmlExtensions 
    {
        public static string extendedSupportingHtml(this StudentDetail studentModel)
        {
                var html = System.IO.File.ReadAllText(@"./HtmlRender/htmlpage.html");
                
                html = html.Replace("{{Id}}", studentModel.Id.ToString());
                html = html.Replace("{{Name}}", studentModel.Name);
                html = html.Replace("{{Address}}", studentModel.Address);
                
                return html;
        }
        public static string extendedSupportingHtmlForString(this string html, StudentDetail studentModel)
        {
            //var html = System.IO.File.ReadAllText(@"./HtmlRender/htmlpage.html");
            //html = html.Replace("{{Id}}", studentModel.Id.ToString());
            //html = html.Replace("{{Name}}", studentModel.Name);
            //html = html.Replace("{{Address}}", studentModel.Address);

            var fieldType = studentModel.GetType().GetProperties();
        
            foreach (var field in fieldType)
            {
                //html = html.Replace(typeof({{field.Name}}), field.GetValue(studentModel));
                //html = html.Replace(field.Name, field.GetValue(studentModel));
                if (field.GetValue(studentModel) == null)
                {
                    continue;
                }

                html = html.Replace( "{{" + field.Name + "}}" , field.GetValue(studentModel).ToString());

                //html1 = html.Replace( "{{" + field.Name + "}}" , field.GetValue(studentModel).ToString());
                //html2 += html1;
                //return html2;
            }
            return html;
        }

        public static void getClassFieldsValues<T>(this T obj)
        {
            // Printing Class Name
            string className = obj.GetType().ToString();
            Console.WriteLine(className);

            //printing field Names
            var fieldTypeis = obj.GetType().GetProperties();

            foreach (var field in fieldTypeis)
            {
                Console.WriteLine(field.Name); 
            }
            //FieldInfo[] myField = fieldTypeis.GetFields();

            //for (int i = 0; i < myField.Length; i++)
            //{
            //    Console.WriteLine(myField[i].Name);
            //}

            foreach (var field in fieldTypeis)
            {   
                Console.WriteLine(field.GetValue(obj));
            }


            //printing field values
            //List<StudentDetail> fieldNameandValues = new List<StudentDetail>();

            //fieldNameandValues.Add(obj.getClassFieldsValues());

            //foreach (var fieldsvalues in fieldNameandValues)
            //{
            //    Console.WriteLine(fieldsvalues);
            //}

            //Trying other way for field values

            //for (int i = 0; i < myField.Length; i++) {

            //    FieldInfo myFieldInfo = fieldTypeis.GetField(myField[i]);

            //    Console.WriteLine(myFieldInfo.GetValue(obj));
            //}

        }
    }
}
