# wechat.net 

本项目是采用asp.net(mvc)开发微信公众号的基本框架，可以在此基础上编写业务逻辑


## 准备工作
推荐使用[ngrok](https://ngrok.com/)内网穿透工具进行开发。效果如下：<br/>

![ngrok-preview](https://raw.githubusercontent.com/champkeh/WeChat.Net/master/UI/Content/images/ngrok_preview1.png)

> 注意：如果使用vs搭配ngrok进行调试时，需要用下面这样的姿势打开ngrok
```
ngrok http [port] -host-header="localhost:[port]"
```
![ngrok-preview](https://raw.githubusercontent.com/champkeh/WeChat.Net/master/UI/Content/images/ngrok_config.png)


---


## 配置公众号
- 在微信公众号后台，开启开发者模式，并配置本地ip白名单方便调试
- 消息加解密方式可选择`明文模式`和`安全模式`，程序会自动根据传参来决定使用哪种模式进行响应

![wx-config](https://raw.githubusercontent.com/champkeh/WeChat.Net/master/UI/Content/images/wx_config.png)


---


## 微信传参说明

1. 提交/更新服务器配置时，验证token<br/>
每次修改公众号后台的服务器配置时，微信都会发送一个GET请求去验证Token。请求格式如下:

```http
GET /wx/access?signature=346deb1512deeea142c30245d7312874ab636869&echostr=11328701160039546815&timestamp=1521777845&nonce=3514821174 HTTP/1.1
Host: localhost:64961
User-Agent: Mozilla/4.0
Accept: */*
Cache-Control: no-cache
Connection: Keep-Alive
Pragma: no-cache
X-Forwarded-For: 103.7.30.105
X-Original-Host: 44a936a3.ngrok.io

```

2. 普通用户发送消息给公众号时(明文模式)：<br/>
当普通用户给微信发送消息时，微信会转发这条消息到我们的服务器，格式如下：

```http
POST /wx/access?signature=b4ec6687c3a35e78e60549bec4163b9ecea345c7&timestamp=1521777871&nonce=1494721558&openid=oGO4t07Fz_DGdbkIs1snsuHFKRRE HTTP/1.1
Host: localhost:64961
User-Agent: Mozilla/4.0
Content-Length: 274
Accept: */*
Cache-Control: no-cache
Content-Type: text/xml
Pragma: no-cache
X-Forwarded-For: 103.7.30.69
X-Original-Host: 44a936a3.ngrok.io

<xml><ToUserName><![CDATA[gh_2b7da7a439a4]]></ToUserName>
<FromUserName><![CDATA[oGO4t07Fz_DGdbkIs1snsuHFKRRE]]></FromUserName>
<CreateTime>1521777871</CreateTime>
<MsgType><![CDATA[text]]></MsgType>
<Content><![CDATA[1]]></Content>
<MsgId>6535986188354779215</MsgId>
</xml>
```

3. 普通用户发送消息给公众号时(安全模式)：<br/>
当普通用户给微信发送消息时，微信会转发这条消息到我们的服务器，格式如下：

```http
POST /wx/access?signature=c344576d342a966adb8e193f525b059d33963a04&timestamp=1521778198&nonce=862720700&openid=oGO4t07Fz_DGdbkIs1snsuHFKRRE&encrypt_type=aes&msg_signature=08c7e159f19509d6de8b547a0a1f226bfa23142f HTTP/1.1
Host: localhost:64961
User-Agent: Mozilla/4.0
Content-Length: 534
Accept: */*
Cache-Control: no-cache
Content-Type: text/xml
Pragma: no-cache
X-Forwarded-For: 103.7.30.69
X-Original-Host: 44a936a3.ngrok.io

<xml>
    <ToUserName><![CDATA[gh_2b7da7a439a4]]></ToUserName>
    <Encrypt><![CDATA[v+8lLMIY/q+65NJ+cCs2jsHik1K/ASfm1wfB9Zoovc3cvOpxHPeStQLwGwJKzEq41oX7ig+Tj7UZ4Zq2fGXePJjlpF3YzLzlLQXEPjuq8EPYG9VvitzeZ9zBW7UnV0y0s+1YYbGk5OKaRqvvQE9SQvbj9iyf22Tbx70w41CMLC+RoH1svzZSJ7iDVheJ7nfKrt3l+EqBlSekzFuVIJxHibFsZMLJ6xGFTN6bL2Fopv+KyFWBz99YXEBA9p8Il3sZK6LIJBoDOHrSXDYuvg8qXYxCSJt9JBaUKZcgQWL1D8vz8K4OHMkZxVcAJ2I3pSWwMy0lFSkX4zUp8yFxCLaTQcIVgP1s55UTIaEcVey6Psi7zgch+Y+ArObkASMr5LjPEooK0DPIr1nwQIHihG78ODULZxaFHpjQe3MlZ8jFYxY=]]></Encrypt>
</xml>
```

---


## todo

- [ ] 消息重试时的排重处理（加一个消息队列，用msgid来标识消息唯一性）
- [ ] 写测试
- [ ] 解决xml序列化/反序列化时，无法识别父类的字段问题
- [ ] 接入微信支付
- [ ] 接入jssdk
- [x] 接入网页授权
