using System.Collections.Generic;

namespace Wx.MessageManagement.Template.Models
{
    public class TemplateListResult : WxError
    {
        public List<TemplateItem> template_list { get; set; }

        public TemplateListResult()
        {
            template_list = new List<TemplateItem>( );
        }
    }


    public class TemplateItem
    {
        public string template_id { get; set; }
        public string title { get; set; }
        public string primary_industry { get; set; }
        public string deputy_industry { get; set; }
        public string content { get; set; }
        public string example { get; set; }
    }
}
