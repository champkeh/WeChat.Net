# wechat.net 

����Ŀ�ǲ���asp.net(mvc)����΢�Ź��ںŵĻ�����ܣ������ڴ˻����ϱ�дҵ���߼�


## ׼������
�Ƽ�ʹ��[ngrok](https://ngrok.com/)������͸���߽��п�����Ч�����£�<br/>

![ngrok-preview](https://raw.githubusercontent.com/champkeh/WeChat.Net/master/UI/Content/images/ngrok_preview1.png)

> ע�⣺���ʹ��vs����ngrok���е���ʱ����Ҫ���������������ƴ�ngrok
```
ngrok http [port] -host-header="localhost:[port]"
```
![ngrok-preview](https://raw.githubusercontent.com/champkeh/WeChat.Net/master/UI/Content/images/ngrok_config.png)


---


## ���ù��ں�
- ��΢�Ź��ںź�̨������������ģʽ�������ñ���ip�������������
- ��Ϣ�ӽ��ܷ�ʽ��ѡ��`����ģʽ`��`��ȫģʽ`��������Զ����ݴ���������ʹ������ģʽ������Ӧ

![wx-config](https://raw.githubusercontent.com/champkeh/WeChat.Net/master/UI/Content/images/wx_config.png)


---


## ΢�Ŵ���˵��

1. �ύ/���·���������ʱ����֤token<br/>
ÿ���޸Ĺ��ںź�̨�ķ���������ʱ��΢�Ŷ��ᷢ��һ��GET����ȥ��֤Token�������ʽ����:

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

2. ��ͨ�û�������Ϣ�����ں�ʱ(����ģʽ)��<br/>
����ͨ�û���΢�ŷ�����Ϣʱ��΢�Ż�ת��������Ϣ�����ǵķ���������ʽ���£�

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

3. ��ͨ�û�������Ϣ�����ں�ʱ(��ȫģʽ)��<br/>
����ͨ�û���΢�ŷ�����Ϣʱ��΢�Ż�ת��������Ϣ�����ǵķ���������ʽ���£�

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

- [ ] ��Ϣ����ʱ�����ش�����һ����Ϣ���У���msgid����ʶ��ϢΨһ�ԣ�
- [ ] д����
- [ ] ���xml���л�/�����л�ʱ���޷�ʶ������ֶ�����
- [ ] ����΢��֧��
- [ ] ����jssdk
- [x] ������ҳ��Ȩ
