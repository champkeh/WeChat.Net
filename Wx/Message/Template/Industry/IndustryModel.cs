namespace Wx.Message.Template.Industry
{
    public class IndustryModel : WxError
    {
        public InternalIndustry primary_industry { get; set; }
        public InternalIndustry secondary_industry { get; set; }
    }

    public class InternalIndustry
    {
        public string first_class { get; set; }
        public string second_class { get; set; }
    }
}
