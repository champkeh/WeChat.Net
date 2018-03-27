namespace BIZService.Models
{
    class WxNotifyParam
    {
        public string touser { get; set; }
        public string template_id { get; set; }
        public string url { get; set; }
        public NotifyParams data { get; set; }
    }

    public class NotifyParams
    {
        public NotifyParamItem first { get; set; }
        public NotifyParamItem keyword1 { get; set; }
        public NotifyParamItem keyword2 { get; set; }
        public NotifyParamItem keyword3 { get; set; }
        public NotifyParamItem remark { get; set; }
    }

    public class NotifyParamItem
    {
        public string value { get; set; }
        public string color { get; set; }
    }
}
