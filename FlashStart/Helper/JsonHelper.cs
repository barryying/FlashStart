using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashStart
{
    class JsonHelper
    {
        private static string jsonPath = Environment.CurrentDirectory + "\\resourse.json";
        /// <summary>
        /// Read the other json file value add to local json
        /// </summary>
        public static void addJson()
        {
            //read the other json
            var sourceContent = File.ReadAllText(jsonPath);
            var sourceobjects = JArray.Parse("[" + sourceContent + "]"); // parse as array  
            JObject source = JObject.Parse(sourceContent);

            //read the local json
            //string p = jsonPath;
            var content = File.ReadAllText(jsonPath, Encoding.Default);
            var objects = JArray.Parse("[" + content + "]"); // parse as array  
            JObject o = JObject.Parse(content);

            //foreach the JObject add the value
            foreach (JToken child in source.Children())
            {
                var prop = child as JProperty;
                string jsonText = prop.Value.ToString();
                JObject jo = (JObject)JsonConvert.DeserializeObject(jsonText);
                if (prop.Name == "AppInfo")
                {
                    //add the json value to local JOnbject
                    o.Add(prop.Name, new JObject(jo));
                }
            }

            //found the file exist 
            if (!File.Exists(jsonPath))
            {
                FileStream fs1 = new FileStream(jsonPath, FileMode.Create, FileAccess.ReadWrite);
                fs1.Close();
            }

            //write the json to file 
            File.WriteAllText(jsonPath, o.ToString());
            content = File.ReadAllText(jsonPath);
        }

        public static void tranversJToken(JToken token, string propName, ref Dictionary<string, string> stringsList)
        {
            var prop = token as JProperty;
            if (prop != null)
            {
                propName = propName + "_" + prop.Name;
            }
            if (prop != null && prop.Value.GetType().Name.ToLower().Equals("jvalue"))
            {
                string _propName = propName.Substring(1);
                string _prop = prop.Value.ToString();
                stringsList[_propName] = _prop;
                return;
            }

            foreach (JToken child in token.Children())
            {
                tranversJToken(child, propName, ref stringsList);
            }
        }

        public static void CreateJson(string defaultcontent)
        {
            //string p = @"..\..\NewJson\Create.json";
            //found the file exist 
            if (!File.Exists(jsonPath))
            {
                FileStream fs1 = new FileStream(jsonPath, FileMode.Create, FileAccess.ReadWrite);
                fs1.Close();
                //JObject jobject = new JObject();
                //jobject.Add("Name", "yanzhiyi");

                //write the json to file
                File.WriteAllText(jsonPath, defaultcontent);
            }
        }

        public static void addListviewJson(string SelectedTab, string apptype, string filePath)
        {
            try
            {
                // 读取json文件
                //string jsonPath = Environment.CurrentDirectory + "\\resourse.json";
                string jsonString = System.IO.File.ReadAllText(jsonPath);
                JObject jobject = JObject.Parse(jsonString);

                // 添加到json文件
                //AppInfoModel appinfomodel = new AppInfoModel();
                //appinfomodel.apptag = "常用";
                //appinfomodel.apptitle = "BaiduNetdisk";
                //appinfomodel.appiconpath = file;

                JObject jobject1 = new JObject();
                jobject1.Add(new JProperty("AppInfo", new JObject(new JProperty("apptag", SelectedTab), new JProperty("apptype", apptype), new JProperty("apptitle", Path.GetFileNameWithoutExtension(filePath)), new JProperty("appiconpath", filePath.Replace("\\", "/")))));
                //jobject1.Add("apptag", "常用");
                //jobject1.Add("apptitle", Path.GetFileNameWithoutExtension(filePath));
                //jobject1.Add("appiconpath", filePath.Replace("\\", "/"));
                //JObject jobject2 = new JObject();
                //jobject2.Add("AppInfo", jobject1);
                jobject["AppListModel"].Last.AddAfterSelf(jobject1);
                Console.WriteLine(jobject.ToString());
                System.IO.File.WriteAllText(jsonPath, jobject.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine("e.Message： " + e.Message + "；e.StackTrace： " + e.StackTrace);
            }
        }

        public static bool DeleteListviewJson(JObject jobject, string id, int isFirst)
        {
            try
            {
                JEnumerable<JToken> jToken = new JEnumerable<JToken>();
                string apptitle = "";
                if (isFirst == 0)
                    jToken = jobject["AppListModel"].Children();
                else
                {
                    jToken = jobject["AppInfo"].Children();
                    apptitle = GetJsonValue(jToken, "apptitle");
                }

                IEnumerator enumerator = jToken.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    JToken jc = (JToken)enumerator.Current;
                    if (jc is JObject || ((JProperty)jc).Value is JObject)
                    {
                        DeleteListviewJson((JObject)jc, id, 1);
                    }
                    else
                    {
                        if (apptitle == id)
                        {
                            jc.Parent.Parent.Remove();
                            break;
                        }
                    }
                }

                Console.WriteLine(jobject.ToString().Replace(",\r\n    {}", ""));
                File.WriteAllText(jsonPath, jobject.ToString().Replace(",\r\n    {}", ""));
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("e.Message： " + e.Message + "；e.StackTrace： " + e.StackTrace);
                return false;
            }
        }
        public static bool SetListviewJson(JObject jobject, string id, string key, string value, int isFirst)
        {
            try
            {
                JEnumerable<JToken> jToken = new JEnumerable<JToken>();
                string apptitle = "";
                if (isFirst == 0)
                    jToken = jobject["AppListModel"].Children();
                else
                {
                    jToken = jobject["AppInfo"].Children();
                    apptitle = GetJsonValue(jToken, "apptitle");
                }

                IEnumerator enumerator = jToken.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    JToken jc = (JToken)enumerator.Current;
                    if (jc is JObject || ((JProperty)jc).Value is JObject)
                    {
                        SetListviewJson((JObject)jc, id, key, value, 1);
                    }
                    else
                    {
                        if (apptitle == id)
                        {
                            if (((JProperty)jc).Name == key)
                            {
                                ((JProperty)jc).Value = value;
                            }
                        }
                    }
                }

                Console.WriteLine(jobject.ToString());
                File.WriteAllText(jsonPath, jobject.ToString());
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("e.Message： " + e.Message + "；e.StackTrace： " + e.StackTrace);
                return false;
            }
        }

        public static JObject readJson()
        {
            // 读取json文件
            string jsonString = System.IO.File.ReadAllText(jsonPath);
            JObject jobject = JObject.Parse(jsonString);
            return jobject;
        }

        public static string GetJsonValue(JEnumerable<JToken> jToken, string key)
        {
            IEnumerator enumerator = jToken.GetEnumerator();
            while (enumerator.MoveNext())
            {
                JToken jc = (JToken)enumerator.Current;
                if (jc is JObject || ((JProperty)jc).Value is JObject)
                {
                    return GetJsonValue(jc.Children(), key);
                }
                else
                {
                    if (((JProperty)jc).Name == key)
                    {
                        return ((JProperty)jc).Value.ToString();
                    }
                }
            }
            return null;
        }

        public static List<TabPages> GetJsonList(JEnumerable<JToken> jToken, string key)
        {
            JObject jsonObj = JsonHelper.readJson();
            //foreach the JObject add the value
            List<TabPages> tabpageindexlist = new List<TabPages>() {};
            //List<string> tabpageindexlist = new List<string>();
            foreach (JToken child in jToken)
            {
                if (child.Count() != 0)
                {
                    string tabpageid = JsonHelper.GetJsonValue(child.Children(), "tabpageid");
                    string tabpagename = JsonHelper.GetJsonValue(child.Children(), "tabpagename");
                    int tabpageindex = int.Parse(JsonHelper.GetJsonValue(child.Children(), "tabpageindex"));
                    tabpageindexlist.Add(new TabPages() { tabpageid = tabpageid, tabpagename = tabpagename, tabpageindex = tabpageindex });
                    //tabpageindexlist.Add(JsonHelper.GetJsonValue(child.Children(), "tabpageindex"));
                }
            }
            return tabpageindexlist;
        }

        public static bool SetShortcutKeyJsonValue(JObject jobject, string key, string value)
        {
            try
            {
                JEnumerable<JToken> jToken = jobject["Settings"].Children();
        
                IEnumerator enumerator = jToken.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    JToken jc = (JToken)enumerator.Current;
                    if (jc is JObject || ((JProperty)jc).Value is JObject)
                    {
                        SetShortcutKeyJsonValue((JObject)jc, key, value);
                    }
                    else
                    {
                        if (((JProperty)jc).Name == key)
                        {
                            ((JProperty)jc).Value = value;
                        }
                    }
                }
                Console.WriteLine(jobject.ToString());
                File.WriteAllText(jsonPath, jobject.ToString());
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("e.Message： " + e.Message + "；e.StackTrace： " + e.StackTrace);
                return false;
            }
        }
        public static void UpdateTabPagename(string action, JObject jobject, string key, string oldvalue, string newvalue, int isFirst)
        {
            JEnumerable<JToken> jToken = new JEnumerable<JToken>();
            string apptag = "";
            if (isFirst == 0)
                jToken = jobject["AppListModel"].Children();
            else
            {
                jToken = jobject["AppInfo"].Children();
                apptag = GetJsonValue(jToken, "apptag");
            }

            IEnumerator enumerator = jToken.GetEnumerator();
            while (enumerator.MoveNext())
            {
                JToken jc = (JToken)enumerator.Current;
                if (jc is JObject || ((JProperty)jc).Value is JObject)
                {
                    if(jc.Count() > 0)
                        UpdateTabPagename(action, (JObject)jc, key, oldvalue, newvalue, 1);
                }
                else
                {
                    if (apptag == oldvalue)
                    {
                        if (action == "edit")
                        {
                            if (((JProperty)jc).Name == key)
                            {
                                ((JProperty)jc).Value = newvalue;                            
                            }
                        }
                        else
                        {
                            jc.Parent.Parent.Remove();
                            break;
                        }
                    }
                }
            }

            Console.WriteLine(jobject.ToString().Replace(",\r\n    {}", "").Replace(",\r\n      {}", ""));
            File.WriteAllText(jsonPath, jobject.ToString().Replace(",\r\n    {}", "").Replace(",\r\n      {}", ""));
        }

        public static bool SetTabPageJsonValue(JObject jobject, string id, string key, string value, int isFirst)
        {
            try
            {
                JEnumerable<JToken> jToken = new JEnumerable<JToken>();
                string tabpageid = "";
                if (isFirst == 0)
                    jToken = jobject["Settings"]["tabControl_Main"].Children();
                else
                {
                    jToken = jobject["TabPages"].Children();
                    tabpageid = GetJsonValue(jToken, "tabpageid");
                }

                IEnumerator enumerator = jToken.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    JToken jc = (JToken)enumerator.Current;
                    if (jc is JObject || ((JProperty)jc).Value is JObject)
                    {
                        SetTabPageJsonValue((JObject)jc, id, key, value, 1);
                    }
                    else
                    {
                        if (tabpageid == id)
                        {
                            if (((JProperty)jc).Name == key)
                            {
                                ((JProperty)jc).Value = value;
                            }
                        }
                    }
                }

                Console.WriteLine(jobject.ToString().Replace(",\r\n      {}", ""));
                File.WriteAllText(jsonPath, jobject.ToString().Replace(",\r\n      {}", ""));
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("e.Message： " + e.Message + "；e.StackTrace： " + e.StackTrace);
                return false;
            }
        }
        public static bool AddTabPageJsonValue(string tabpagename)
        {
            try
            {
                // 读取json文件
                string jsonString = System.IO.File.ReadAllText(jsonPath);
                JObject jobject = JObject.Parse(jsonString);
                JObject jobject1 = new JObject();
                List<TabPages> tabpageindexlist = GetJsonList(jobject["Settings"]["tabControl_Main"].Children(), "tabpageindex");
                //tabpageindexlist.Sort()
                tabpageindexlist.Sort((a, b) => a.tabpageindex.CompareTo(b.tabpageindex));
                string index = (tabpageindexlist[tabpageindexlist.Count - 1].tabpageindex + 1).ToString();
                jobject1.Add(new JProperty("TabPages", new JObject(new JProperty("tabpageid", "TabPage" + index), new JProperty("tabpagename", tabpagename), new JProperty("tabpageindex", index))));
                jobject["Settings"]["tabControl_Main"].Last.AddAfterSelf(jobject1);
                Console.WriteLine(jobject.ToString().Replace(",\r\n      {}", ""));
                System.IO.File.WriteAllText(jsonPath, jobject.ToString().Replace(",\r\n      {}", ""));
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("e.Message： " + e.Message + "；e.StackTrace： " + e.StackTrace);
                return false;
            }
        }
        public static bool DeleteTabPageJsonValue(JObject jobject, string id, int isFirst)
        {
            try
            {
                JEnumerable<JToken> jToken = new JEnumerable<JToken>();
                string tabpageid = "";
                if (isFirst == 0)
                    jToken = jobject["Settings"]["tabControl_Main"].Children();
                else
                {
                    jToken = jobject["TabPages"].Children();
                    tabpageid = GetJsonValue(jToken, "tabpageid");
                }

                IEnumerator enumerator = jToken.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    JToken jc = (JToken)enumerator.Current;
                    if (jc is JObject || ((JProperty)jc).Value is JObject)
                    {
                        DeleteTabPageJsonValue((JObject)jc, id, 1);
                    }
                    else
                    {
                        if (tabpageid == id)
                        {
                            jc.Parent.Parent.Remove();
                            break;
                        }
                    }
                }

                Console.WriteLine(jobject.ToString().Replace(",\r\n      {}", ""));
                File.WriteAllText(jsonPath, jobject.ToString().Replace(",\r\n      {}", ""));
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("e.Message： " + e.Message + "；e.StackTrace： " + e.StackTrace);
                return false;
            }
        }
    }
}
