using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

using TINYXMLTRANS;
using ISUPDemo;
namespace ISUPDemo.Public
{
    public class ConvertModel
    {

        public enum XML_NODE_TYPE
        {
            NODE_STRING_TO_BOOL = 0,    //string转bool(0,1)
            NODE_STRING_TO_INT = 1,    //string转int(HPR_UINT32)
            NODE_STRING_TO_ARRAY = 2,   //string转数组(HPR_UINT8[],char[])
            NODE_STRING_TO_BYTE = 3,    //string转HPR_UINT8,HPR_UINT8仅为数字时
            NODE_STRING_TO_WORD = 4,    //string转HPR_UINT16
            NODE_STRING_TO_FLOAT = 5,    //string转FLOAT
            NODE_TYPE_REVERSE = 64,   //类型反转,用于区分转换方向   
            NODE_BOOL_TO_STRING = 65,  //bool(0,1)转string
            NODE_INT_TO_STRING = 66,  //int(HPR_UINT32)转string
            NODE_ARRAY_TO_STRING = 67,  //数组(HPR_UINT8[],char[])转string
            NODE_BYTE_TO_STRING = 68,   //HPR_UINT8转string,HPR_UINT8仅为数字时
            NODE_WORD_TO_STRING = 69    //HPR_UINT16转string
        }



        public static int IpToInt(string ip)
        {
            char[] separator = new char[] { '.' };
            string[] items = ip.Split(separator);
            return int.Parse(items[0]) << 24
                    | int.Parse(items[1]) << 16
                    | int.Parse(items[2]) << 8
                    | int.Parse(items[3]);
        }


        /*********************************************************
        函数名:	UTF82A
        函数描述:	UTF8转多字节
        输入参数:	cont - UTF8编码字符串                       
        输出参数:	
        返回值:	    多字节字符串
        **********************************************************/
        public bool UTF82A(byte[] pCont, byte[] pOut, int dwOutSize, ref int pOutLen)
        {
            
            String strCont = System.Text.Encoding.Default.GetString(pCont);
            String strOut = System.Text.Encoding.Default.GetString(pOut);
            int iNum = EhomeSDK.MultiByteToWideChar(EhomeSDK.CP_UTF8, 0, strCont, -1, null, 0);
            ushort[] pBuffw = new ushort[(ushort) iNum];
            if (pBuffw == null)
            {
                return false;
            }
            string strBuffw = string.Format("%s", pBuffw);
            EhomeSDK.MultiByteToWideChar(EhomeSDK.CP_UTF8, 0, strCont, -1, strOut, iNum);
            int iLen = EhomeSDK.WideCharToMultiByte(EhomeSDK.CP_ACP, 0, strBuffw, iNum - 1, null, 0, IntPtr.Zero, IntPtr.Zero);
            ushort[] pLpsz = new ushort[(ushort)iLen + 1];
            if (pLpsz == null)
            {
                return false;
            }

            StringBuilder strLpsz = new StringBuilder();
            strLpsz.Append(pLpsz);
            EhomeSDK.WideCharToMultiByte(EhomeSDK.CP_ACP, 0, strBuffw, iNum - 1, strLpsz, iLen, IntPtr.Zero, IntPtr.Zero);
            if (dwOutSize < iLen)
            {
                return false;
            }
            else
            {
                pOutLen = iLen;
                //pOut = strLpsz.ToString();
               
                char[] chTemp = new char[pOutLen+1];
                strOut.CopyTo(0, chTemp, 0, pOutLen);
                Encoding.ASCII.GetBytes(chTemp, 0, pOutLen, pOut, 0);
                return true;
            }
        }

        public bool ConvertSingleNodeData(IntPtr pOutVale, ref CTinyXmlTrans struXml, String pNodeName, XML_NODE_TYPE byDataType)
        {
            if (byDataType < XML_NODE_TYPE.NODE_TYPE_REVERSE)
            {
                if (struXml.FindElem(pNodeName))
                {
                    //string to
                    if (byDataType == XML_NODE_TYPE.NODE_STRING_TO_BOOL)    //bool类型
                    {
                        if (struXml.GetData().Equals("true") == true)
                        {
                            Marshal.StructureToPtr((bool)true, pOutVale, false);
                            return true;
                        }
                        else if (struXml.GetData().Equals("false") == true)
                        {
                            Marshal.StructureToPtr((bool)false, pOutVale, false);
                            return true;
                        }
                    }
                    else if (byDataType == XML_NODE_TYPE.NODE_STRING_TO_INT)   //int类型
                    {
                        if (struXml.GetData() != "")
                        {
                            Marshal.StructureToPtr((uint)Convert.ToUInt32(struXml.GetData().ToString()), pOutVale, false);
                            return true;
                        }
                    }
                    else if (byDataType == XML_NODE_TYPE.NODE_STRING_TO_ARRAY)
                    {
                        string strTmp = struXml.GetData().ToString();
                        if (strTmp != "")
                        {
                            int nLen = (int)strTmp.Length;
                            //if (nLen > iArrayLen)
                            //{
                            //    nLen = iArrayLen;
                            //}
                            pOutVale = Marshal.StringToBSTR(strTmp);
                            return true;
                        }
                    }
                    else if (byDataType == XML_NODE_TYPE.NODE_STRING_TO_BYTE)
                    {
                        if (struXml.GetData() != "")
                        {
                            Marshal.StructureToPtr((byte)Convert.ToUInt32(struXml.GetData().ToString()), pOutVale, false);
                            return true;
                        }
                    }
                    else if (byDataType == XML_NODE_TYPE.NODE_STRING_TO_WORD)
                    {
                        if (struXml.GetData() != "")
                        {
                            Marshal.StructureToPtr((UInt16)Convert.ToUInt32(struXml.GetData().ToString()), pOutVale, false);
                            return true;
                        }
                    }
                    else if (byDataType == XML_NODE_TYPE.NODE_STRING_TO_FLOAT)
                    {
                        if (struXml.GetData() != "")
                        {
                            Marshal.StructureToPtr((float)Convert.ToUInt32(struXml.GetData().ToString()), pOutVale, false);
                            return true;
                        }
                    }
                }
                else
                {
                    return false;
                }

            }
            else if (byDataType > XML_NODE_TYPE.NODE_TYPE_REVERSE)
            {
                //    if (byDataType == XML_NODE_TYPE.NODE_BOOL_TO_STRING)
                //    {
                //        bool b = (bool)Marshal.PtrToStructure(pOutVale, typeof(bool));
                //        if (b)
                //        {
                //            if (struXml.AddNode(pNodeName, "true"))
                //            {
                //                struXml.OutOfElem();
                //                return TRUE;
                //            }
                //        }
                //        else if (!(*(BOOL*)pOutVale))
                //        {
                //            if(struXml.AddNode(pNodeName, "false"))
                //            {
                //                struXml.OutOfElem();
                //                return TRUE;
                //            }                
                //        }
                //    }
                //    else if (byDataType == XML_NODE_TYPE.NODE_INT_TO_STRING)
                //    {
                //        char szBuf[16] = {0};
                //        itoa(*(int*)pOutVale, szBuf, 10);
                //        if (struXml.AddNode(pNodeName, szBuf))
                //        {
                //            struXml.OutOfElem();
                //            return TRUE;
                //        }
                //    }
                //    else if (byDataType == XML_NODE_TYPE.NODE_ARRAY_TO_STRING)
                //    {
                //        string strValue = "";
                //        if (iArrayLen == 0)
                //        {
                //            strValue = (char*)pOutVale;
                //        }
                //        else 
                //        {
                //            int nValueLen = (int)strlen((char*)pOutVale);
                //            if (nValueLen < iArrayLen) //数组没有被填满,有结束符
                //            {
                //                strValue = (char*)pOutVale;
                //            }
                //            else    
                //            {
                //                char *lpTmp = NULL;
                //                lpTmp = new char[iArrayLen + 1];
                //                if (lpTmp == NULL)
                //                {
                //                    return FALSE;
                //                }

                //                memset(lpTmp, 0, iArrayLen + 1);
                //                memcpy(lpTmp, (char*)pOutVale, (DWORD)iArrayLen);//增加一个结束符
                //                strValue = lpTmp;
                //                delete[] lpTmp;    
                //            }
                //        }
                //        if (struXml.AddNode(pNodeName, strValue.c_str()))
                //        {
                //            struXml.OutOfElem();
                //            return TRUE;
                //        }
                //    }
                //    else if (byDataType == XML_NODE_TYPE.NODE_BYTE_TO_STRING)
                //    {
                //        char szBuf[16] = {0};
                //        int nTmp = (int)*(BYTE*)pOutVale; 
                //        itoa(nTmp, szBuf, 10);
                //        if (struXml.AddNode(pNodeName, szBuf))
                //        {
                //            struXml.OutOfElem();
                //            return TRUE;
                //        }
                //    }
                //    else if (byDataType == XML_NODE_TYPE.NODE_WORD_TO_STRING)
                //    {
                //        char szBuf[16] = {0};
                //        int nTmp = (int)*(WORD*)pOutVale; 
                //        itoa(nTmp, szBuf, 10);
                //        if (struXml.AddNode(pNodeName, szBuf))
                //        {
                //            struXml.OutOfElem();
                //            return TRUE;
                //        }
                //    }
                //}

                return false;
            }
            return false;
        }
    }
}
