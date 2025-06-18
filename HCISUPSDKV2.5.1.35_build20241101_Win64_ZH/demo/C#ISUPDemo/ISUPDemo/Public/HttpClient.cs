using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace ISUPDemo.Public
{
    public class HttpClient
    {
        public delegate bool ProcessLongLinkData(byte[] data, object UserData, string boundary);
        public delegate bool ProcessSendDate(ref byte[] byBuffer, object UserDate);
        public delegate void SaveDownloadVideo(byte[] data, int iLength, ListViewItem temp, float fPercent);
        public enum HttpStatus
        {
            Http200 = 0,
            HttpOther,
            HttpTimeOut
        }
        public class RequestState
        {
            // This class stores the State of the request.
            const int BUFFER_SIZE = 8 * 102400;
            public StringBuilder requestData;
            public byte[] BufferRead;
            public HttpWebRequest request;
            public HttpWebResponse response;
            public Stream streamResponse;
            public ProcessLongLinkData processLongLinkData;
            public object objectUserData;
            public string strBoundary;
            public ProcessSendDate processSendData;
            public WebException eStatus;

            public RequestState()
            {
                BufferRead = new byte[BUFFER_SIZE];
                requestData = new StringBuilder("");
                request = null;
                streamResponse = null;
                processLongLinkData = null;
                objectUserData = null;
                strBoundary = string.Empty;
                processSendData = null;
                eStatus = null;
            }
        }

        private static Dictionary<string, NetworkCredential> digest = new Dictionary<string, NetworkCredential>();
        private CredentialCache _credentialCache = null;
        private string strURL = string.Empty;
        public static int m_iHttpTimeOut = 500000;
        const int BUFFER_SIZE = 1024;
        public static bool bIsStopDownLoad = false;

        public static void SetHttpTimeOut(int ms)
        {
            m_iHttpTimeOut = ms;
        }

        public static int GetHttpTimeOut()
        {
            return m_iHttpTimeOut;
        }
        private CredentialCache GetCredentialCache(string sUrl, string strUserName, string strPassword)
        {
            if (_credentialCache == null)
            {
                _credentialCache = new CredentialCache();
                _credentialCache.Add(new Uri(sUrl), "Digest", new NetworkCredential(strUserName, strPassword));
                strURL = sUrl;
            }
            if (strURL != sUrl)
            {
                _credentialCache.Add(new Uri(sUrl), "Digest", new NetworkCredential(strUserName, strPassword));
                strURL = sUrl;
            }

            return _credentialCache;
        }

        private NetworkCredential GetNetworkCredential(string sUrl, string strUserName, string strPassword, string deviceIp)
        {
            foreach (string ip in digest.Keys)
            {
                if (ip == deviceIp)
                {
                    NetworkCredential tmp = new NetworkCredential();
                    tmp = digest[ip];
                    return tmp;
                }
            }
            NetworkCredential tmp2 = new NetworkCredential(strUserName, strPassword);
            digest.Add(deviceIp, tmp2);
            return tmp2;
        }

        public int ParserResponseStatus(string httpBody, ref string statusCode, ref string statusString)
        {
            if (httpBody == "Timeout")
            {
                return 0;
            }
            try
            {
                if (httpBody != string.Empty)
                {
                    XmlDocument xml = new XmlDocument();
                    xml.LoadXml(httpBody);
                    if (xml.DocumentElement != null && xml.DocumentElement.Name == "ResponseStatus")
                    {
                        XmlNodeList childNode = xml.DocumentElement.ChildNodes;
                        foreach (XmlNode node in childNode)
                        {
                            if (node.Name == "statusCode")
                            {
                                statusCode = node.InnerText;
                            }
                            if (node.Name == "statusString")
                            {
                                statusString = node.InnerText;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                statusString = httpBody;
            }
            return 0;
        }

        public int HttpRequest2(string strUserName, string strPassword, string strUrl, string strHttpMethod, ref byte[] szResponse, ref string szContentType)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(strUrl);
            request.Credentials = GetCredentialCache(strUrl, strUserName, strPassword);
            request.Method = strHttpMethod;
            request.Timeout = m_iHttpTimeOut;
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/70.0.3538.102 Safari/537.36";
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8";
            request.Headers.Add("Accept-Language", "zh-CN,zh;q=0.9,en;q=0.8");
            request.Headers.Add("Accept-Encoding", "gzip, deflate");
            request.Headers.Add("Cache-Control", "max-age=0");
            request.Headers.Add("Upgrade-Insecure-Requests", "1");
            try
            {
                WebResponse wr = request.GetResponse();
                szContentType = wr.ContentType;
                szResponse = new byte[wr.ContentLength];
                byte[] buf = new byte[wr.ContentLength];
                int iRet = -1;
                int len = 0;
                while ((iRet = wr.GetResponseStream().Read(buf, 0, (int)wr.ContentLength - len)) > 0)
                {
                    Array.Copy(buf, 0, szResponse, len, iRet);
                    len += iRet;
                }
                return (int)HttpStatus.Http200;
            }
            catch (WebException ex)
            {
                WebResponse wr = ex.Response;
                if (wr != null)
                {
                    Stream st = wr.GetResponseStream();
                    StreamReader sr = new StreamReader(st, System.Text.Encoding.Default);
                    szResponse = System.Text.Encoding.Default.GetBytes(sr.ReadToEnd());
                    sr.Close();
                    st.Close();
                    return (int)HttpStatus.HttpOther;
                }
                else
                {
                    szResponse = System.Text.Encoding.Default.GetBytes(ex.Status.ToString());
                    return (int)HttpStatus.HttpTimeOut;
                }
            }
            finally
            {

            }
        }
    }
}
