# wechat.net 

## 接入开发者模式
实现了以下功能：
1. 验证开发者Token
    GET /wx/access?signature=&echostr=&timestamp=&nonce=
2. 明文模式的消息处理
    POST /wx/access?signature=&timestamp=&nonce=&openid=
3. 安全模式的消息处理
    POST /wx/access?signature=&timestamp=&nonce=&openid=&encrypt_type=aes&msg_signature=

hello world
this is test for git 