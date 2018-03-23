using System.Collections.Generic;

namespace Wx.Menu.Models
{
    public class MenuModel : WxError
    {
        public Menu menu { get; set; }
    }


    public class Menu
    {
        public List<Button> button { get; set; }

        public Menu()
        {
            button = new List<Button>( );
        }
    }

    public class Button
    {
        public string type { get; set; }
        public string name { get; set; }
        public string key { get; set; }
        public string url { get; set; }
        public string appid { get; set; }
        public string pagepath { get; set; }
        public List<Button> sub_button { get; set; }

        public Button()
        {
            sub_button = new List<Button>( );
        }
    }

}
