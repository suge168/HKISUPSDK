using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using ISUPDemo.Public;
using System.IO;

namespace ISUPDemo
{
	public class FileUtils
	{
		private static String m_sFolderPath;
		private static int m_FileNumber;
		private static JArray m_arrJsonObj = new JArray();
		private static Dictionary<string, string> m_mapFileList = new Dictionary<string, string>();

		public static JArray GetJSONArray()
		{
			InitFileList();
			return m_arrJsonObj;
		}

		private static void InitFileList()
		{
			//Traverse all json files in the postman_json folder and convert them into json objects
			m_sFolderPath = Environment.CurrentDirectory + "\\postman_json";

			//the files in this dictionary pull into the fileArray all
			DirectoryInfo root = new DirectoryInfo(m_sFolderPath);
			try
			{
				FileInfo[] files = root.GetFiles();
				if (files.Length != m_FileNumber)
				{
					m_FileNumber = files.Length;
					m_arrJsonObj = new JArray();

					if (files != null)
					{
						for (int i = 0; i < files.Length; i++)
						{
							String sJsonFullPath = files[i].FullName;
							// judge filename whether is end with 'json' or not
							if (sJsonFullPath.EndsWith("json"))
							{
								ReadJson(sJsonFullPath);
							}
						}
					}
				}
			}catch(FileNotFoundException ex)
			{
				MessageBox.Show("postman_json folder is empty!");
			}
		}

		private static void ReadJson(String sPath)
		{
			using (System.IO.StreamReader file = System.IO.File.OpenText(sPath))
			{
				using (JsonTextReader reader = new JsonTextReader(file))
				{
					JObject objJson = (JObject)JToken.ReadFrom(reader);
					m_arrJsonObj.Add(objJson);

					m_mapFileList.Add(((JObject)objJson["info"])["name"].ToString(), sPath);
				}
			}
		}
	}


    public partial class XMLConfig : Form
    {
        private int m_iDeviceIndex;
        private int m_lUserID;
        private DeviceTree g_deviceTree = DeviceTree.Instance();
        private DeviceLogList g_formList = DeviceLogList.Instance();
        private HCEHomeCMS.NET_EHOME_PTXML_PARAM struPTXML = new HCEHomeCMS.NET_EHOME_PTXML_PARAM();
        String m_strInputXml;
        String m_strOutputXml;
        private byte[] m_szInputBuffer = new byte[1500];
        private byte[] m_szOutBuffer = new byte[1024 * 10];
        private byte[] m_szUrl= new byte[1024];
		private TreeNode m_RootNode = new TreeNode("Protocol Tree");
		public Dictionary<String, JArray> m_mapProtocol = new Dictionary<string, JArray>();

        public XMLConfig()
        {
            InitializeComponent();
			treeImageListInit();
            m_iDeviceIndex = g_deviceTree.m_iCurDeviceIndex;
            m_lUserID = DeviceTree.g_struDeviceInfo[m_iDeviceIndex].iLoginID;
            rtbInputXml.Clear();
            rtbOutputXml.Clear();
            tbUrl.Text = "";
			InitProtocolTree(FileUtils.GetJSONArray());
        }

		private void InitProtocolTree(JArray arrProtocol)
		{
			int iProtocolFileNum = arrProtocol.Count;
			m_RootNode.ImageKey = "tree";
			m_RootNode.SelectedImageIndex = 0;
			this.treeProtocol.Nodes.Add(m_RootNode);

			foreach(JObject objJson in arrProtocol)
			{
				String sName = ((JObject)objJson["info"])["name"].ToString();
				m_mapProtocol.Add(sName, (JArray)objJson["item"]);

				TreeNode treeFamilyNode = new TreeNode(sName);
				treeFamilyNode.ImageKey = "folder";
				
				foreach(JObject objItem in (JArray)objJson["item"])
				{
					LispFindItem(objItem.ToString(), treeFamilyNode);
				}
				m_RootNode.Nodes.Add(treeFamilyNode);
			}
		}

		private void LispFindItem(String sObjItem, TreeNode parentNode)
		{
			if(sObjItem.StartsWith("["))
			{
				JArray objItem = (JArray)JToken.Parse(sObjItem);
				m_mapProtocol.Add(parentNode.Text, objItem);

				foreach(JObject objIndex in objItem)
				{
					if (objIndex.Property("item") != null)
					{
						TreeNode newFamilyNode = new TreeNode(objIndex["name"].ToString());
						LispFindItem(((JArray)objIndex["item"]).ToString(), newFamilyNode);
					}
					else
						createNodes(objIndex, parentNode);
				}
			}
			else if(sObjItem.StartsWith("{"))
			{
				JObject objItem = (JObject)JToken.Parse(sObjItem);

				while(objItem.Property("item") != null)
				{
					TreeNode newFamilyNode = new TreeNode(objItem["name"].ToString());
					LispFindItem(((JArray)objItem["item"]).ToString(), newFamilyNode);
					newFamilyNode.ImageKey = "folder";
					parentNode.Nodes.Add(newFamilyNode);
				}

				createNodes(objItem, parentNode);
			}
		}

		private void createNodes(JObject objItem, TreeNode top)
		{
			String sMethod = ((JObject)objItem["request"])["method"].ToString();
			String sReqUrl = "";
			String sInbound = "";
			JArray arrUrl = (JArray)((JObject)((JObject)objItem["request"])["url"])["path"];

			foreach(String sUrl in arrUrl)
			{
				sReqUrl += ("/" + sUrl);
			}

			if(((JObject)objItem["request"]).Property("body") != null)
			{
				sInbound = ((JObject)((JObject)objItem["request"])["body"])["raw"].ToString();
			}

			TreeNode newChildNode = new TreeNode(sMethod + " " + sReqUrl);
			newChildNode.ImageKey = "file";
			top.Nodes.Add(newChildNode);
		}

		private void treeImageListInit()
		{
			this.treeProtocol.ImageList.Images.Add("tree", Properties.Resources.tree);
			this.treeProtocol.ImageList.Images.Add("file", Properties.Resources.file);
			this.treeProtocol.ImageList.Images.Add("folder", Properties.Resources.folder);
		}

        private void buttonOperate_Click(object sender, EventArgs e)
        {
            String strTemp = tbUrl.Text;
            m_strInputXml = rtbInputXml.Text;

            m_szUrl = Encoding.UTF8.GetBytes(strTemp);
			struPTXML.pRequestUrl = Marshal.AllocHGlobal(strTemp.Length);
            Marshal.Copy(m_szUrl, 0, struPTXML.pRequestUrl,m_szUrl.Length);
            struPTXML.dwRequestUrlLen = (uint)m_szUrl.Length;

            strTemp = m_strInputXml;
            if(""==strTemp)
            {
                struPTXML.pInBuffer = IntPtr.Zero;
                struPTXML.dwInSize = 0;
            }
            else
            {
                m_szInputBuffer = Encoding.UTF8.GetBytes(strTemp);
				struPTXML.pInBuffer = Marshal.AllocHGlobal(strTemp.Length);
                Marshal.Copy(m_szInputBuffer, 0, struPTXML.pInBuffer, m_szInputBuffer.Length);
                struPTXML.dwInSize = (uint)strTemp.Length;
            }


            struPTXML.pOutBuffer = Marshal.AllocHGlobal(1024 * 10);
            for (int i = 0; i < 1024 * 10; i++)
            {
                Marshal.WriteByte(struPTXML.pOutBuffer, i, 0);
            }

            struPTXML.dwOutSize = (uint)(1024 * 10);
            struPTXML.byRes = new byte[32];

            IntPtr ptrCfg = Marshal.AllocHGlobal(Marshal.SizeOf(struPTXML));
            Marshal.StructureToPtr(struPTXML, ptrCfg, false);

            if (cbCmd.Text.Equals("GET"))
            {
                if (!HCEHomeCMS.NET_ECMS_GetPTXMLConfig(m_lUserID, ptrCfg))
                {
                    string str = string.Format("NET_ECMS_GetPTXMLConfig err = {0}", HCEHomeCMS.NET_ECMS_GetLastError());
                    g_formList.AddLog(m_iDeviceIndex, HCEHomePublic.OPERATION_FAIL_T, 1, str);
                    Marshal.FreeHGlobal(struPTXML.pRequestUrl);
                    Marshal.FreeHGlobal(struPTXML.pInBuffer);
                    Marshal.FreeHGlobal(struPTXML.pOutBuffer);
                    Marshal.FreeHGlobal(ptrCfg);
                    return;
                }
            }
            else if (cbCmd.Text.Equals("PUT"))
            {
                if (!HCEHomeCMS.NET_ECMS_PutPTXMLConfig(m_lUserID, ptrCfg))
                {
                    string str = string.Format("NET_ECMS_PutPTXMLConfig err = {0}", HCEHomeCMS.NET_ECMS_GetLastError());
                    g_formList.AddLog(m_iDeviceIndex, HCEHomePublic.OPERATION_FAIL_T, 1, str);
                    Marshal.FreeHGlobal(struPTXML.pRequestUrl);
                    Marshal.FreeHGlobal(struPTXML.pInBuffer);
                    Marshal.FreeHGlobal(struPTXML.pOutBuffer);
                    Marshal.FreeHGlobal(ptrCfg);
                    return;
                }
            }
            else if (cbCmd.Text.Equals("POST"))
            {
                if (!HCEHomeCMS.NET_ECMS_PostPTXMLConfig(m_lUserID, ptrCfg))
                {
                    string str = string.Format("NET_ECMS_PostPTXMLConfig err = {0}", HCEHomeCMS.NET_ECMS_GetLastError());
                    g_formList.AddLog(m_iDeviceIndex, HCEHomePublic.OPERATION_FAIL_T, 1, str);
                    Marshal.FreeHGlobal(struPTXML.pRequestUrl);
                    Marshal.FreeHGlobal(struPTXML.pInBuffer);
                    Marshal.FreeHGlobal(struPTXML.pOutBuffer);
                    Marshal.FreeHGlobal(ptrCfg);
                    return;
                }
            }
            else if (cbCmd.Text.Equals("DELETE"))
            {
                if (!HCEHomeCMS.NET_ECMS_DeletePTXMLConfig(m_lUserID, ptrCfg))
                {
                    string str = string.Format("NET_ECMS_DeletePTXMLConfig err = {0}", HCEHomeCMS.NET_ECMS_GetLastError());
                    g_formList.AddLog(m_iDeviceIndex, HCEHomePublic.OPERATION_FAIL_T, 1, str);
                    Marshal.FreeHGlobal(struPTXML.pRequestUrl);
                    Marshal.FreeHGlobal(struPTXML.pInBuffer);
                    Marshal.FreeHGlobal(struPTXML.pOutBuffer);
                    Marshal.FreeHGlobal(ptrCfg);
                    return;
                }
            }

            g_formList.AddLog(m_iDeviceIndex, HCEHomePublic.OPERATION_SUCC_T, 1, "NET_ECMS_XXXPTXMLConfig");
            strTemp = "";
            strTemp = Marshal.PtrToStringAnsi(struPTXML.pOutBuffer, (int)struPTXML.dwOutSize);
            rtbOutputXml.Text = strTemp;
            Marshal.FreeHGlobal(struPTXML.pRequestUrl);
            Marshal.FreeHGlobal(struPTXML.pInBuffer);
            Marshal.FreeHGlobal(struPTXML.pOutBuffer);
            Marshal.FreeHGlobal(ptrCfg);
            return;
        }

		private void treeProtocolDoubleClicked(object sender, TreeNodeMouseClickEventArgs e)
		{
			if (1 == e.Node.Level)			//protocol family
			{

			}
			else if(2 == e.Node.Level)		//protocol item
			{
				String sFamilyName = e.Node.Parent.Text;
				String sItemName = e.Node.Text;

				String[] sMethod_Url = sItemName.Split(' ');
				String sMethod = sMethod_Url[0];
				String sReqUrl = sMethod_Url[1];
				
				RenderItem(sFamilyName, sReqUrl, sMethod);
			}
			this.Refresh();
		}

		private void RenderItem(String sFamilyName, String sReqUrl, String sMethod)
		{
			if(m_mapProtocol.ContainsKey(sFamilyName))
			{
				JArray arrProtocolList = m_mapProtocol[sFamilyName];

				foreach(JObject objItem in arrProtocolList)
				{
					if(objItem.Property("request") != null)
					{
						JObject objRequest = (JObject)objItem["request"];
						String sReqUrl_json = "";
						JArray arrPath = (JArray)((JObject)objRequest["url"])["path"];
						
						foreach(String sPath in arrPath)
						{
							sReqUrl_json += ("/" + sPath);
						}

						if(sReqUrl.Equals(sReqUrl_json))
						{
							tbUrl.Text = sReqUrl;
							String sReqInbound = "";
							if(objRequest.Property("body") != null)
							{
								sReqInbound = ((JObject)objRequest["body"])["raw"].ToString();
							}
							cbCmd_SetText(sMethod, sReqInbound);
							rtbOutputXml.Text = "";
						}
					}
				}
			}
		}

        private void XMLConfig_Load(object sender, EventArgs e)
        {

        }

		private void cbCmd_SetText(String sCMD, String sReqInbound)
		{
			if (sCMD.Equals("GET"))
			{
				cbCmd.SelectedIndex = (0);
				rtbInputXml.Text = "";
			}
			else if (sCMD.Equals("POST"))
			{
				cbCmd.SelectedIndex = (1);
				rtbInputXml.Text = sReqInbound;
			}
			else if (sCMD.Equals("PUT"))
			{
				cbCmd.SelectedIndex = (2);
				rtbInputXml.Text = sReqInbound;
			}
			else
			{
				cbCmd.SelectedIndex = (3);
				rtbInputXml.Text = "";
			}
		}

        private void cbCmd_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCmd.SelectedIndex == 0)  //get
            {
                rtbInputXml.Text = "";
            }
            else if (cbCmd.SelectedIndex == 1)  //put
            {
                rtbOutputXml.Text = "";
            }
            else if (cbCmd.SelectedIndex == 2)  //post
            {

            }

        }
    }
}
