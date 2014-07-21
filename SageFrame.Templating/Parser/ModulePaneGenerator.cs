﻿#region "Copyright"
/*
FOR FURTHER DETAILS ABOUT LICENSING, PLEASE VISIT "LICENSE.txt" INSIDE THE SAGEFRAME FOLDER
*/
#endregion

#region "References"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SageFrame.Templating.xmlparser;
using SageFrame.Web.Utilities;
#endregion

namespace SageFrame.Templating
{
    public class ModulePaneGenerator
    {
        public string GenerateHTML(List<XmlTag> lstTags, List<XmlTag> lstWrappers, int Mode)
        {
            string markup = "";
            List<CustomWrapper> lstCustomWrappers = new List<CustomWrapper>();
            if (lstWrappers.Count > 0)
            {
                lstCustomWrappers = ProcessWrappers(lstTags, lstWrappers);
            }
            foreach (XmlTag tag in lstTags)
            {
                if (tag.TagType == XmlTagTypes.Section)
                {
                    foreach (CustomWrapper start in lstCustomWrappers)
                    {
                        if (start.Type == "block" && Utils.GetAttributeValueByName(tag, XmlAttributeTypes.NAME).Equals(start.Start))
                        {
                            string style = string.Format("sfSectionwrap {0}", start.Class);
                            int depth = start.Depth;
                            for (int i = 1; i <= depth; i++)
                            {
                                if (i == 1)
                                {
                                    style = start.Depth > 1 ? string.Format("sfSectionwrap sfWrap{0}{1}", start.Index, start.Class == "" ? "" : string.Format(" {0}", start.Class)) : string.Format("sfSectionwrap sfWrap{0}{1} clearfix", start.Index, start.Class == "" ? "" : string.Format(" {0}", start.Class)); ;
                                    markup += "<div class='" + style + "'>";
                                }
                                else
                                {
                                    style = start.Depth == i ? string.Format("sfSectionwrap sf{0}{1} clearfix", i - 1, start.Class == "" ? "" : string.Format(" {0}", start.Class)) : string.Format("sfSectionwrap sf{0}{1}", i - 1, start.Class == "" ? "" : string.Format(" {0}", start.Class));
                                    markup += "<div class='" + style + "'>";
                                }
                            }
                        }
                    }
                    markup += GenerateSectionMarkup(tag, lstCustomWrappers, Mode);

                    foreach (CustomWrapper start in lstCustomWrappers)
                    {
                        if (start.Type == "block" && Utils.GetAttributeValueByName(tag, XmlAttributeTypes.NAME).Equals(start.End))
                        {

                            for (int i = 1; i <= start.Depth; i++)
                            {
                                markup += "</div>";
                            }
                        }
                    }
                }

            }


            return (GenerateExternalWrapper(markup));
        }

        public List<CustomWrapper> ProcessWrappers(List<XmlTag> lstTags, List<XmlTag> lstWrappers)
        {
            List<CustomWrapper> lstCustomWrappers = new List<CustomWrapper>();
            int index = 0;
            foreach (XmlTag wrapper in lstWrappers[0].LSTChildNodes)
            {
                string type = Utils.GetAttributeValueByName(wrapper, XmlAttributeTypes.TYPE);
                switch (type)
                {
                    case "position":
                        foreach (XmlTag tag in lstTags)
                        {
                            foreach (XmlTag pch in tag.LSTChildNodes)
                            {
                                if (pch.InnerHtml.ToLower().Contains(wrapper.InnerHtml.ToLower()))
                                {
                                    CustomWrapper obj = new CustomWrapper();
                                    obj.Name = Utils.GetAttributeValueByName(wrapper, XmlAttributeTypes.NAME);
                                    obj.Class = Utils.GetAttributeValueByName(wrapper, XmlAttributeTypes.CLASS);
                                    obj.Depth = Utils.GetAttributeValueByName(wrapper, XmlAttributeTypes.DEPTH) == "" ? 1 : int.Parse(Utils.GetAttributeValueByName(wrapper, XmlAttributeTypes.DEPTH));
                                    obj.Start = wrapper.PositionsArr[0].ToLower();
                                    obj.End = wrapper.PositionsArr[wrapper.PositionsArr.Length - 1].ToLower();
                                    obj.LSTPositions = wrapper.PositionsArr.ToList();
                                    obj.Type = Utils.GetAttributeValueByName(wrapper, XmlAttributeTypes.TYPE);
                                    obj.Index = index;
                                    lstCustomWrappers.Add(obj);
                                    break;
                                }
                            }
                        }
                        break;
                    case "placeholder":
                        foreach (XmlTag tag in lstTags)
                        {
                            if (tag.Placeholders.ToLower().Contains(wrapper.InnerHtml.ToLower()) || wrapper.InnerHtml == "left,middle" || wrapper.InnerHtml == "middle,right")
                            {
                                CustomWrapper obj = new CustomWrapper();
                                obj.Name = Utils.GetAttributeValueByName(wrapper, XmlAttributeTypes.NAME);
                                obj.Class = Utils.GetAttributeValueByName(wrapper, XmlAttributeTypes.CLASS);
                                obj.Depth = Utils.GetAttributeValueByName(wrapper, XmlAttributeTypes.DEPTH) == "" ? 1 : int.Parse(Utils.GetAttributeValueByName(wrapper, XmlAttributeTypes.DEPTH));
                                obj.Start = wrapper.PositionsArr[0].ToLower();
                                obj.End = wrapper.PositionsArr[wrapper.PositionsArr.Length - 1].ToLower();
                                obj.LSTPositions = wrapper.PositionsArr.ToList();
                                obj.Type = Utils.GetAttributeValueByName(wrapper, XmlAttributeTypes.TYPE);
                                obj.Index = index;
                                lstCustomWrappers.Add(obj);
                                break;
                            }

                        }
                        break;
                    case "block":
                        foreach (XmlTag tag in lstTags)
                        {
                            string tagname = Utils.GetAttributeValueByName(tag, XmlAttributeTypes.NAME);
                            if (tagname.ToLower().Equals(wrapper.PositionsArr[0]))
                            {
                                CustomWrapper obj = new CustomWrapper();
                                obj.Name = Utils.GetAttributeValueByName(wrapper, XmlAttributeTypes.NAME);
                                obj.Class = Utils.GetAttributeValueByName(wrapper, XmlAttributeTypes.CLASS);
                                obj.Depth = Utils.GetAttributeValueByName(wrapper, XmlAttributeTypes.DEPTH) == "" ? 1 : int.Parse(Utils.GetAttributeValueByName(wrapper, XmlAttributeTypes.DEPTH));
                                obj.Start = wrapper.PositionsArr[0].ToLower();
                                obj.End = wrapper.PositionsArr[wrapper.PositionsArr.Length - 1].ToLower();
                                obj.LSTPositions = wrapper.PositionsArr.ToList();
                                obj.Type = Utils.GetAttributeValueByName(wrapper, XmlAttributeTypes.TYPE);
                                obj.Index = index;
                                lstCustomWrappers.Add(obj);
                            }
                        }
                        break;
                }
                index++;
            }
            return lstCustomWrappers;
        }

        public string GeneratePlaceHolderMarkup(XmlTag Section)
        {
            foreach (XmlTag tag in Section.LSTChildNodes)
            {
            }
            return "";
        }

        public string GenerateSectionMarkup(XmlTag section, List<CustomWrapper> lstWrapper, int Mode)
        {
            string markup = "";
            if (section.AttributeCount > 0)
            {
                foreach (LayoutAttribute attr in section.LSTAttributes)
                {
                    switch (attr.Type)
                    {
                        case XmlAttributeTypes.NAME:
                            markup = GetSectionMarkup(attr.Value, section, lstWrapper, Mode);
                            break;
                        case XmlAttributeTypes.TYPE:
                            break;
                        case XmlAttributeTypes.SPLIT:
                            break;
                    }
                }
            }
            return markup;
        }


        public string GetSectionMarkup(string name, XmlTag section, List<CustomWrapper> lstWrapper, int Mode)
        {
            string html = "";
            if (Enum.IsDefined(typeof(SectionTypes), name.ToUpper()))
            {
                SectionTypes _type = (SectionTypes)Enum.Parse(typeof(SectionTypes), name.ToUpper());

                try
                {
                    switch (_type)
                    {
                        case SectionTypes.SFHEADER:
                            html = GetTopMarkup(section, lstWrapper, Mode);
                            break;
                        case SectionTypes.SFCONTENT:
                            html = GetMiddleWrapper(section, lstWrapper, Mode);
                            break;
                        case SectionTypes.SFFOOTER:
                            html = GetBottomMarkup(section, lstWrapper, Mode);
                            break;
                        default:
                            html = GetBottomMarkup(section, lstWrapper, Mode);
                            break;

                    }
                }
                catch (Exception ex)
                {

                    throw ex;

                }

            }
            else
            {
                html = GetBottomMarkup(section, lstWrapper, Mode);
            }
            return html;
        }

        public string GenerateExternalWrapper(string markup)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<div id='sfOuterWrapper' class=\"sfCurve\" runat=\"server\">");
            sb.Append(markup);
            sb.Append("</div>");
            return sb.ToString();
        }

        public string GetTopMarkup(XmlTag section, List<CustomWrapper> lstWrappers, int Mode)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(BlockBuilder.GetTopBlocks(section, lstWrappers, Mode));
            return sb.ToString();
        }

        public string GetMiddleWrapper(XmlTag section, List<CustomWrapper> lstWrappers, int Mode)
        {
            StringBuilder sb = new StringBuilder();
            string layout = HtmlBuilder.GenerateMiddleBlockStart(section, lstWrappers, Mode);
            sb.Append(layout);
            return sb.ToString(); ;
        }
        public string GetBottomMarkup(XmlTag section, List<CustomWrapper> lstWrappers, int Mode)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(BlockBuilder.GetBottomBlocks(section, lstWrappers, Mode));
            return sb.ToString();
        }



        public string GetPrefix(int count)
        {
            string prefix = "";
            for (int i = 0; i < count; i++)
            {
                prefix += "--";
            }
            return prefix;
        }

        public string GetAttributeString(List<LayoutAttribute> lstAttr)
        {
            StringBuilder sb = new StringBuilder();
            foreach (LayoutAttribute attr in lstAttr)
            {
                sb.Append("[");
                sb.Append(attr.Name);
                sb.Append(":");
                sb.Append(attr.Value);
                sb.Append("],");
            }
            return sb.ToString();
        }
    }
}
