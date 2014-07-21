using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Script.Serialization;

namespace ServiceInvoker
{
    public class ServiceProcesser
    {
        
        Dictionary<string, object> parameters =new Dictionary<string,object>();
        string _methodName = "";
        Type _type = null;

        public ServiceProcesser(Type type,string jsonParam, string methodName)
        {
            _type = type;
            _methodName = methodName;
            JavaScriptSerializer jss = new JavaScriptSerializer();
            parameters = jss.Deserialize<Dictionary<string, object>>(jsonParam);
        }

        public object Invoke()
        {
            try
            {
                if (!IsMethodExist(_methodName))
                    throw new Exception(ErrorType.METHOD_NOT_FOUND.ToString());
                var eventParams = GetParameters(_methodName);
                if (eventParams.Count() > parameters.Count)
                    throw new Exception(ErrorType.INSUFFICIENT_PARAMS.ToString());

                ArrayList param = new ArrayList();//   new Object[] { "C#.net", "Vb.net" }
                foreach (var info in eventParams)
                {
                    var x = info.Name;
                    object obj = null;
                    try
                    {
                        obj = parameters[info.Name];
                    }
                    catch (KeyNotFoundException)
                    {
                        throw new Exception(ErrorType.INVALID_PARAMETERS.ToString());
                    }
                    string paramType = info.ParameterType.FullName.ToLower();

                    //checking and handling nullable type
                    if (info.ParameterType.IsGenericType && info.ParameterType.GetGenericTypeDefinition() == typeof(Nullable<>))
                    {                       
                            paramType = info.ParameterType.GetGenericArguments()[0].FullName.ToLower();

                            if (paramType == "system.datetime" || paramType == "system.single" || paramType == "system.boolean" || paramType == "system.string" || paramType == "system.int32" || paramType == "system.int64" || paramType == "system.decimal" || paramType == "system.double" || paramType == "system.object")
                            {
                                if (obj != null)
                                {
                                    var sngleParam = Convert.ChangeType(obj, Nullable.GetUnderlyingType(info.ParameterType));
                                    param.Add(sngleParam);
                                }
                                else {
                                    param.Add(null);
                                }
                            }
                        
                    }
                    //ok for string decimal int not for class object type
                    else if (paramType == "system.datetime" || paramType == "system.single" || paramType == "system.boolean" || paramType == "system.string" || paramType == "system.int32" || paramType == "system.int64" || paramType == "system.decimal" || paramType == "system.double" || paramType == "system.object")
                    {
                        if (obj != null)
                        {
                            var sngleParam = Convert.ChangeType(obj, Type.GetType(info.ParameterType.FullName));
                            param.Add(sngleParam);
                        }
                        else
                        {
                            param.Add(null);
                        }
                    }
                    else
                    {
                        if (info.ParameterType.IsClass == true && info.ParameterType.IsGenericType == true)
                        {
                            Type objectParamType = Type.GetType(info.ParameterType.FullName);
                            var obj1 = Converter.DictionaryToList((IList)obj, objectParamType);
                            param.Add(obj1);

                        }
                        else if (paramType == "system.array")
                        {
                            //value contain arraysvalue
                            Array arrValues = Converter.DictionaryToArray((IList)obj);
                            param.Add(arrValues);

                        }
                        else if (paramType == "system.collections.arraylist")
                        {
                            //value contains array
                            ArrayList arrList = Converter.DictionaryToArrayList((IList)obj);
                            param.Add(arrList);
                        }
                        else
                        {

                            Type objectParamType = Type.GetType(info.ParameterType.FullName);

                            if (objectParamType == null)
                            {
                                if (info.ParameterType.Assembly.Location != null && info.ParameterType.Assembly.Location != string.Empty)
                                {
                                    Assembly assembly = Assembly.LoadFile(info.ParameterType.Assembly.Location);
                                    objectParamType = assembly.GetType(info.ParameterType.FullName);
                                }
                            }

                            if (objectParamType != null)
                            {
                                var objParam = Converter.DictionaryToObj((Dictionary<string, object>)obj, objectParamType);
                                param.Add(objParam);
                            }
                            else
                            {
                                throw new Exception(ErrorType.INVALID_CLASS.ToString());
                            }
                        }
                    }
                }
                Object MyObj = Activator.CreateInstance(_type);
                MethodInfo method = _type.GetMethod(_methodName, BindingFlags.Default | BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static);
                if (method == null)
                    throw new Exception(ErrorType.METHOD_NOT_FOUND.ToString());
                var response = method.Invoke(MyObj, param.ToArray());
                return response;
            }
            catch (TargetInvocationException ex)
            {
               throw ex.InnerException; // ex now stores the original exception
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private ParameterInfo[] GetParameters(string methodName)
        {
            return _type.GetMethod(methodName).GetParameters();
        }

        private bool IsMethodExist(string methodName)
        {
            try
            {
                if (_type.GetMethod(methodName) != null)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}
