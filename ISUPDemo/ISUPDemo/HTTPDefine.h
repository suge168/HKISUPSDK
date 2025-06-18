#ifndef _SRC_DEFINE_HTTPDEFINE_H_
#define _SRC_DEFINE_HTTPDEFINE_H_

#define UTILS_SPACE               " "         //??????????????????????????????????????
#define UTILS_CRLF                "\r\n"      //?????????HTTP?????????????????????????
#define DOUBLE_UTILS_CRLF         "\r\n\r\n"  //??????????http????????
#define UTILS_CHUNKED_END_TAG     "0\r\n\r\n" //trunk??????????

#define MAX_UTILS_REQPARAM_NUM    15 
#define MAX_UTILS_RSPPARAM_NUM    15
#define MAX_UTILS_REQHEAD_LEN 1024 * 4
#define MAX_UTILS_RSPHEAD_LEN 5*1024
#define MAX_UTILS_HTTP_RECV_HEAD_LEN  (1024 * 10)

#define NET_UTILS_RECLUSIVE -2
#define NET_UTILS_DATA_NOT_COMPLETE -3

#define HTTP_UTILS_VALUE_UNAUTHORIZED    Http401

#define MAX_HTTP_PSW_LEN   480

#define MAX_HTTP_BODY_LEN  (64 * 1024 * 1024)      //???64M?????????????????

#define UTILS_HTTP_CHUNKED   "Transfer-Encoding: chunked"

#define MAX_HTTP_LINK_NUM  6000

#define MAX_TRANS_LINK_NUM  10000

#define MAX_WEBSOCKET_LINK_NUM 4096

#define WEBSOCKET_GUID      "258EAFA5-E914-47DA-95CA-C5AB0DC85B11"

#define HTTP_RECV_DATA_ERROR            0xff00 //??????????
#define HTTP_RECV_DATA_RESOURCE_ERROR   0xff01 //???????
#define HTTP_RECV_DATA_CUSTOM_CLOSE     0xff02 //????????????
#define HTTP_RECV_DATA_LINK_END         0xffff //???????   
#define HTTP_RECV_DATA_NEED_CONTINUE    0x1    //???????????
#define HTTP_RECV_DATA_END              0x0    //??????????
#define HTTP_RECV_DATA_MIME             0x2    //mime???????
#define HTTP_CERTIFICATE_ERROR          0x3    //??????

//???????????
#define HTTP_RECV_UPGRADE               0xff0000  //????????
#define HTTP_RECV_SETTING               0xff0001  //?????????? 

//??????????????????????????windows/linux?????????·Ú
#if defined OS_WINDOWS
#define UTILS_SNPRINTF		_snprintf
#define UTILS_VSNPRINTF		_vsnprintf
#elif defined OS_POSIX
#define UTILS_SNPRINTF		snprintf
#define UTILS_VSNPRINTF		vsnprintf
#endif


typedef enum enumNET_UTILS_HTTP_STATS
{
    // Informational 1xx
    Http100 = 100,
    Http101 = 101,

    // Success 2xx
    Http200 = 200,
    Http201 = 201,
    Http202 = 202,
    Http203 = 203,
    Http204 = 204,
    Http205 = 205,
    Http206 = 206,

    // Redirection 3xx
    Http300 = 300,
    Http301 = 301,
    Http302 = 302,
    Http303 = 303,
    Http304 = 304,
    Http305 = 305,
    // 306 is deprecated but reserved
    Http307 = 307,

    // Client Error 4xx
    Http400 = 400,
    Http401 = 401,
    Http402 = 402,
    Http403 = 403,
    Http404 = 404,
    Http405 = 405,
    Http406 = 406,
    Http407 = 407,
    Http408 = 408,
    Http409 = 409,
    Http410 = 410,
    Http411 = 411,
    Http412 = 412,
    Http413 = 413,
    Http414 = 414,
    Http415 = 415,
    Http416 = 416,
    Http417 = 417,

    // Server Error 5xx
    Http500 = 500,
    Http501 = 501,
    Http502 = 502,
    Http503 = 503,
    Http504 = 504,
    Http505 = 505,
    Http509 = 509
}NET_UTILS_HTTP_STATS;

//???????1.1
typedef enum enumNET_UTILS_HTTP_VER
{
    
    ENUM_HTTP_VER_VALUE_11 = 0,
    ENUM_HTTP_VER_VALUE_20 = 1
}NET_UTILS_HTTP_VER;

#define HTTP_VER_1_1_STRING           "HTTP/1.1"           //RFC???
#define HTTP_VER_VER_DEFAULT_STRING   HTTP_VER_1_1_STRING  //????????1.1???????????1.1???

typedef enum tagNET_UTILS_HTTP_METHOD
{
    ENUM_UTILS_HTTP_UNKNOW  = -1,
    ENUM_UTILS_HTTP_GET     = 0,
    ENUM_UTILS_HTTP_PUT     = 1,
    ENUM_UTILS_HTTP_POST    = 2,
    ENUM_UTILS_HTTP_DELETE  = 3,
}NET_UTILS_HTTP_METHOD;

typedef enum tagNET_UTILS_HTTP_AUTHEN_TYPE
{
    ENUM_NONE = 0,
    ENUM_BASIC = 1,
    ENUM_DIGEST = 2
}NET_UTILS_HTTP_AUTHEN_TYPE;

typedef enum enumNET_UTILS_LISTEN_TYPE
{
    ENUM_LISTEN_TYPE_UNKNOW = -1,  //???????
    ENUM_LISTEN_TYPE_HTTP_ALL = 0,   //????????????
    ENUM_LISTEN_TYPE_HTTP_11 = 1,   //?????1.1
}UTILS_LISTEN_TYPE;

#endif // _SRC_DEFINE_HTTPDEFINE_H_

