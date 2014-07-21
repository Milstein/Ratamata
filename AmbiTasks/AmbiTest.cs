using System;

 using System.Collections.Generic;

 using System.Linq;

using System.Text;

using System.Reflection;

 using System.IO;

 using Microsoft.Build.Framework;

 using Microsoft.Build.Utilities;



namespace ClassLibrary1
{

    public class MemberCaseTask : Task
    {

        [Required]

        public string AssemblyPath { get; set; }



        public override bool Execute()
        {

            foreach (Assembly assembly in GetAllAssemblies())
            {
                try
                {

                    foreach (Type type in assembly.GetTypes())
                        foreach (FieldInfo field in type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance))

                            try
                            {

                                GetNonPrivateFieldType(type, field.Name);

                            }

                            catch (Exception)
                            {

                                Log.LogError(string.Format("{0} has a field conflict on field {1}.", type.Name, field.Name));

                                return false;

                            }

                }

                catch (System.Reflection.ReflectionTypeLoadException)

                { }

            }



            return true;

        }



        private IEnumerable<Assembly> GetAllAssemblies()
        {

            foreach (FileInfo file in new DirectoryInfo(AssemblyPath).GetFiles("*.dll", SearchOption.TopDirectoryOnly))
            {

                Assembly assembly = AppDomain.CurrentDomain.Load(AssemblyName.GetAssemblyName(file.FullName));

                yield return assembly;

            }

        }



        static Type GetNonPrivateFieldType(Type classType, string fieldName)
        {

            FieldInfo field = classType.GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);

            if ((field != null) && !field.IsPrivate)
            {
                return field.FieldType;

            }

            return null;

        }

    }
}