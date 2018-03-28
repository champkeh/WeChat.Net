using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WxTest.WxAccess
{
    [TestClass]
    public class WxAccessTest
    {
        static string domain = "http://localhost:64961/";
        /// <summary>
        /// 测试开发者接入
        /// </summary>
        [TestMethod]
        public void TestAccessGet()
        {
            var echostr = "10332107084865994711";

            var url = domain + "/wx/access?signature=3b9c1c2ef31e01893b0b9aea14d2ae9cf5f57d0b&echostr=" + echostr + "&timestamp=1522203234&nonce=2600898831";
            var res = new AppUtils.HttpUtil( ).Get( url );

            Assert.AreEqual( res, echostr );
        }



        [TestMethod]
        public void TestAccessPostEnc()
        {
            // 正常请求
            var url = domain + "/wx/access?signature=21b6da0d9473e4c42f66f675e67dad927b6ae90e&timestamp=1522208384&nonce=1529393385&openid=oGO4t07Fz_DGdbkIs1snsuHFKRRE&encrypt_type=aes&msg_signature=a17d442e011a768f4ac7613b53bbcad096677311";

            var data = @"<xml>
                            <ToUserName><![CDATA[gh_2b7da7a439a4]]></ToUserName>
                            <Encrypt><![CDATA[4YZKVwmA47vn4fU4Pa/nByOykucX3vhXmevsqtw5I+tYCfw2csEn8ihP1Gvg7QMHGePV/DTxPkE3XSqjU/Q+h9GSp+qzpZSwB04PmclMtirxxMDs4XpvvGdVVoN1jY72B/VkOyn1xPg2CczizJW3+2Bu5XFbO5STIotOTL30pRTq5zGzETQKXGPQbjB+zJ9z6cqSXeRsG8+l2ysmxHwbLRz6osw2LLuTiM1sIPrkAmCAgITKrGD1UpGG3TV7K1ZVLtu7SWvjkHwT9NgpCUDlfEEVe3LtZm4et3FfB5Fr5SfNwoegf9n/AnRhfv4BXTrDnRG53FjzjXT4PP47YFUZC9kOFCJT42/hyJkkdxlTlyEMAZK9KY3JHlYflzWSGh8R7PJ7zpRhFVdD8veStNsL4d3hE9KxMsHZCRGueJlTLZI=]]></Encrypt>
                        </xml>";

            var res = new AppUtils.HttpUtil( ).PostXml( url, data );

            Assert.AreEqual( res, "success" );
        }

        [TestMethod]
        public void TestAccessPostEnc1()
        {
            // token签名失败
            var url = domain + "/wx/access?signature=21b6da0d9473e4c42f66f675e67dad927b6ae9e&timestamp=1522208384&nonce=1529393385&openid=oGO4t07Fz_DGdbkIs1snsuHFKRRE&encrypt_type=aes&msg_signature=a17d442e011a768f4ac7613b53bbcad096677311";

            var data = @"<xml>
                            <ToUserName><![CDATA[gh_2b7da7a439a4]]></ToUserName>
                            <Encrypt><![CDATA[4YZKVwmA47vn4fU4Pa/nByOykucX3vhXmevsqtw5I+tYCfw2csEn8ihP1Gvg7QMHGePV/DTxPkE3XSqjU/Q+h9GSp+qzpZSwB04PmclMtirxxMDs4XpvvGdVVoN1jY72B/VkOyn1xPg2CczizJW3+2Bu5XFbO5STIotOTL30pRTq5zGzETQKXGPQbjB+zJ9z6cqSXeRsG8+l2ysmxHwbLRz6osw2LLuTiM1sIPrkAmCAgITKrGD1UpGG3TV7K1ZVLtu7SWvjkHwT9NgpCUDlfEEVe3LtZm4et3FfB5Fr5SfNwoegf9n/AnRhfv4BXTrDnRG53FjzjXT4PP47YFUZC9kOFCJT42/hyJkkdxlTlyEMAZK9KY3JHlYflzWSGh8R7PJ7zpRhFVdD8veStNsL4d3hE9KxMsHZCRGueJlTLZI=]]></Encrypt>
                        </xml>";

            var res = new AppUtils.HttpUtil( ).PostXml( url, data );

            Assert.AreEqual( res, "你的ip已被记录，警察稍候会请你喝茶，好自为之~" );
        }


        [TestMethod]
        public void TestAccessPostEnc2()
        {
            // 消息签名失败
            var url = domain + "/wx/access?signature=21b6da0d9473e4c42f66f675e67dad927b6ae90e&timestamp=1522208384&nonce=1529393385&openid=oGO4t07Fz_DGdbkIs1snsuHFKRRE&encrypt_type=aes&msg_signature=a17d442e011a768f4ac7613b53bbcad0966773";

            var data = @"<xml>
                            <ToUserName><![CDATA[gh_2b7da7a439a4]]></ToUserName>
                            <Encrypt><![CDATA[4YZKVwmA47vn4fU4Pa/nByOykucX3vhXmevsqtw5I+tYCfw2csEn8ihP1Gvg7QMHGePV/DTxPkE3XSqjU/Q+h9GSp+qzpZSwB04PmclMtirxxMDs4XpvvGdVVoN1jY72B/VkOyn1xPg2CczizJW3+2Bu5XFbO5STIotOTL30pRTq5zGzETQKXGPQbjB+zJ9z6cqSXeRsG8+l2ysmxHwbLRz6osw2LLuTiM1sIPrkAmCAgITKrGD1UpGG3TV7K1ZVLtu7SWvjkHwT9NgpCUDlfEEVe3LtZm4et3FfB5Fr5SfNwoegf9n/AnRhfv4BXTrDnRG53FjzjXT4PP47YFUZC9kOFCJT42/hyJkkdxlTlyEMAZK9KY3JHlYflzWSGh8R7PJ7zpRhFVdD8veStNsL4d3hE9KxMsHZCRGueJlTLZI=]]></Encrypt>
                        </xml>";

            var res = new AppUtils.HttpUtil( ).PostXml( url, data );

            Assert.AreEqual( res, "消息解密失败" );
        }

    }
}
