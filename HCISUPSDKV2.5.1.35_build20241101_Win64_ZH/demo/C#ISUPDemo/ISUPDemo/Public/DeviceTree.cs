using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace ISUPDemo.Public
{
    public partial class DeviceTree : UserControl
    {
        //设备信息列表
        public static GlobalDefinition.LOCAL_DEVICE_INFO[] g_struDeviceInfo = new GlobalDefinition.LOCAL_DEVICE_INFO[GlobalDefinition.MAX_DEVICES];
        public EhomeSDK.PREVIEW_IFNO[] m_strPanelInfo = new EhomeSDK.PREVIEW_IFNO[GlobalDefinition.MAX_DEVICES];
        public  Public.ConvertModel m_ConvertModel = new Public.ConvertModel();
        public static DeviceTree g_deviceTree = new DeviceTree();
        public TreeNode m_hCurDeviceItem = new TreeNode();
        public TreeNode m_hCurChanItem = new TreeNode();
        public  TreeNode m_RootNode = new TreeNode("device");
        public const int MAX_DEVICES = 1024;
        //日志实例
        public static DeviceLogList g_formList = DeviceLogList.Instance();
        //预览面板实例
        public static PreviewPanel[] m_PreviewPanel = PreviewPanel.Instance();
        public Panel[] m_CurPreviewPanel = new Panel[4];
        
        public int m_iCurWndNum;        //screen split mode 1, 4, 9, 16, 25, 36
        public int m_iCurWndIndex;      //current selected split window index, start from 0
        public int m_iCurDeviceIndex;
        public int m_iCurChanIndex;
        public IntPtr m_hWnd;
        public bool m_bPlaying;             //是否正在播放
        public TreeNodeMouseClickEventArgs m_CurNodeArgs;

        public static String m_cslocalIP;   //本机IP地址
        public static Int16  m_lport;        //本机端口号
        public static Panel  m_CurPanel;     //当前播放窗口
        public bool g_bSS_Enable = true;

        #region "dll region"
        //消息发送API  
        [DllImport("User32.dll", EntryPoint = "PostMessage")]
        public static extern int PostMessage(IntPtr hWnd, uint Msg, UIntPtr wParam, UIntPtr lParam);
        #endregion
        public void Initg_struDeviceInfo(ref  GlobalDefinition.LOCAL_DEVICE_INFO[] g_struDeviceInfo, int length)
        {
            for (int i = 0; i < length; ++i)
            {
                g_struDeviceInfo[i].Init();
            }
        }
        public  DeviceTree()
        {
            InitializeComponent();
            treeImageListInit();
            this.treeView1.Nodes.Add(m_RootNode);
            this.treeView1.Nodes[0].ImageKey = "tree";
            this.treeView1.Nodes[0].SelectedImageIndex = 0;
            Initg_struDeviceInfo(ref g_struDeviceInfo, MAX_DEVICES);
            m_bPlaying = false;
        }

        public static DeviceTree Instance()
        {
            return g_deviceTree;
        }

        public void MessageProcess(Message m)
        {
            DevStatusProcess(ref m);
        }
        public void DevStatusProcess(ref Message m) 
        { 
            if (m.Msg == ISUPDemo.WM_ADD_DEV) 
            {
                addDevice(m.LParam);
            }
            else if (m.Msg == ISUPDemo.WM_DEL_DEV)
            {
                deleteDevice(m.LParam);
            }
            else if (m.Msg == ISUPDemo.WM_CHANGE_IP)
            {
                IPAddrChanged(m.WParam);
            }

            //base.DefWndProc(ref m);  
        }

        public void treeImageListInit()
        {
            this.treeView1.ImageList.Images.Add("tree", Properties.Resources.tree);
            this.treeView1.ImageList.Images.Add("camera", Properties.Resources.camera);
            this.treeView1.ImageList.Images.Add("Alarm", Properties.Resources.Alarm);
            this.treeView1.ImageList.Images.Add("audio", Properties.Resources.audio);
            this.treeView1.ImageList.Images.Add("bitmap_p", Properties.Resources.bitmap_p);
            this.treeView1.ImageList.Images.Add("dev_alarm", Properties.Resources.dev_alarm);
            this.treeView1.ImageList.Images.Add("fortify", Properties.Resources.fortify);
            this.treeView1.ImageList.Images.Add("fortify_alarm", Properties.Resources.fortify_alarm);
            this.treeView1.ImageList.Images.Add("IPChan", Properties.Resources.IPChan);
            this.treeView1.ImageList.Images.Add("login", Properties.Resources.login);
            this.treeView1.ImageList.Images.Add("logout", Properties.Resources.logout);
            this.treeView1.ImageList.Images.Add("p_r_a", Properties.Resources.p_r_a);
            this.treeView1.ImageList.Images.Add("play", Properties.Resources.play);
            this.treeView1.ImageList.Images.Add("play_alarm", Properties.Resources.play_alarm);
            this.treeView1.ImageList.Images.Add("playAndAlarm", Properties.Resources.playAndAlarm);
            
        }
        public void deleteDevice(IntPtr pstDevInfo)
        {
            if(IntPtr.Zero == pstDevInfo)
            {
                return;
            }
            int iLoginID = (int)Marshal.PtrToStructure(pstDevInfo, typeof(int));
            HCEHomeCMS.NET_ECMS_ForceLogout(iLoginID);
            DelDev(iLoginID);
        }

        public void IPAddrChanged(IntPtr pstDevInfo)
        {
            if (IntPtr.Zero == pstDevInfo)
            {
                return;
            }
            GlobalDefinition.LOCAL_DEVICE_INFO stDevInfo = new GlobalDefinition.LOCAL_DEVICE_INFO();
            stDevInfo.Init();
            stDevInfo = (GlobalDefinition.LOCAL_DEVICE_INFO)Marshal.PtrToStructure(pstDevInfo, typeof(GlobalDefinition.LOCAL_DEVICE_INFO));
            int iLoginID = stDevInfo.iLoginID;
            DelDev(iLoginID);
            return;
        }

        public void DelDev(Int32 lLoginID)
        {
            int iDeviceIndex = -1;
            int iChanIndex = -1;
            int i = 0;
            int j = 0;
            //HTREEITEM hRoot = m_treeDeviceList.GetRootItem();
            //HTREEITEM hDev = m_treeDeviceList.GetChildItem(hRoot);
            //TreeNode curNode;
            for (i = 0; i < GlobalDefinition.MAX_DEVICES; i++)
            {
                iDeviceIndex = m_iCurDeviceIndex;
                iChanIndex = m_iCurChanIndex;
                if (g_struDeviceInfo[iDeviceIndex].iDeviceIndex != -1)
                {
                    //说明有添加设备，那么就判断一下
                    if (g_struDeviceInfo[iDeviceIndex].iLoginID == lLoginID)
                    {
                        int iPlayWndIndex = g_struDeviceInfo[iDeviceIndex].struChanInfo[iChanIndex].iPlayWndIndex;

                        if (g_struDeviceInfo[iDeviceIndex].struChanInfo[iChanIndex].bPlay)
                        {
                            if (!m_PreviewPanel[iPlayWndIndex].StopPlay())
                            {
                                g_formList.AddLog(iDeviceIndex, GlobalDefinition.OPERATION_FAIL_T, 0, "StopPlay Failed");
                            }
                            g_struDeviceInfo[iDeviceIndex].struChanInfo[iChanIndex].bPlay = false;
                            g_struDeviceInfo[iDeviceIndex].struChanInfo[iChanIndex].iPlayWndIndex = -1;
                        }
                        
                        this.Refresh();
                        m_CurPreviewPanel[iPlayWndIndex].BackColor = Color.Gray;
                        m_CurPreviewPanel[iPlayWndIndex].Refresh();
                        g_struDeviceInfo[iDeviceIndex].iDeviceIndex = -1;
                        for (j = 0; j < GlobalDefinition.MAX_CHAN_NUM_DEMO; j++)
                        {
                            g_struDeviceInfo[iDeviceIndex].struChanInfo[j].iDeviceIndex = -1;
                            g_struDeviceInfo[iDeviceIndex].struChanInfo[j].iChanIndex = -1;
                            g_struDeviceInfo[iDeviceIndex].struChanInfo[j].iChannelNO = -1;
                        }
                        break;
                    }
                }
            }

            foreach(TreeNode node in m_RootNode.Nodes)
            {
                if(node.Index == iDeviceIndex)
                {
                    node.Remove();
                }
            }

            this.Refresh();
        }

        public static void StrToByteArray(ref byte[] destination, string data)
        {
            if (data != "")
            {
                byte[] source = System.Text.Encoding.Default.GetBytes(data);
                if (source.Length > destination.Length)
                {
                    MessageBox.Show("The length of num is exceeding");
                }
                else
                {
                    for (int i = 0; i < source.Length; ++i)
                    {
                        destination[i] = source[i];
                    }
                }
            }
        }

        public static string ByteToStr(ref byte[] source,int length)
        {
            string temp = null;
            int len = 0;
            for(int i=0;i<length;++i)
            {
                if(source[i]!=0)
                {
                    ++len;
                }
                else
                {
                    break;
                }
            }
            byte[] res = new byte[len];
            Array.Copy(source, 0, res, 0, len);
            temp = System.Text.Encoding.Default.GetString(res);
            return temp;
        }

        private bool iSByteArrayEmpty(ref byte[] array,int length)
        {
            int zeronumber = 0;
            for(int i=0;i<length;++i)
            {
                if(0==array[i])
                {
                    ++zeronumber;
                }
            }
            if(length==zeronumber)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool isByteEqual(ref byte[] source,ref byte[] dst,int length)
        {
            if(length>source.Length||length>dst.Length)
            {
                return false;
            }
            bool flag = true;
            for (int i = 0; i < length; ++i)
            {
                if(source[i]!=dst[i])
                {
                    flag = false;
                    break;
                }
            }
            return flag;
        }
        public void addDevice(IntPtr pstDevInfo)
        {
            
            int i = 0;
            int j = 0;

            GlobalDefinition.LOCAL_DEVICE_INFO stDevInfo = new GlobalDefinition.LOCAL_DEVICE_INFO();
            stDevInfo.Init();
            stDevInfo = (GlobalDefinition.LOCAL_DEVICE_INFO)Marshal.PtrToStructure(pstDevInfo, typeof(GlobalDefinition.LOCAL_DEVICE_INFO));

            TreeNode deviceNode=new TreeNode();;        //设备根节点的通道节点
            TreeNode channelNode=new TreeNode();       //通道节点
            String strTemp = null;

            for(i=0; i<GlobalDefinition.MAX_DEVICES; ++i)
            {
                if (g_struDeviceInfo[i].iDeviceIndex == -1 || isByteEqual(ref stDevInfo.byDeviceID, ref g_struDeviceInfo[i].byDeviceID,256))
                {
                    //这一步走的加密流程EhomeKeyC++中传的都是NULL，所以不判断
                    bool isEhomekeyempty = iSByteArrayEmpty(ref g_struDeviceInfo[i].byEhomeKey, 32);
                    if (false == isEhomekeyempty)
                    {
                        g_struDeviceInfo[i].byEhomeKey.CopyTo(stDevInfo.byEhomeKey, 0);
                    }

                    bool isSessionKeyempty = iSByteArrayEmpty(ref g_struDeviceInfo[i].bySessionKey, 16);
                    if (false == isSessionKeyempty)
                    {
                        g_struDeviceInfo[i].bySessionKey.CopyTo(stDevInfo.bySessionKey, 0);
                    }
                    if (g_bSS_Enable)
                    {
                        IntPtr ptrDeviceID = Marshal.AllocHGlobal(256);
                        Marshal.Copy(stDevInfo.byDeviceID, 0, ptrDeviceID, 256);

                        IntPtr ptrbyEhomeKey = Marshal.AllocHGlobal(32);
                        Marshal.Copy(stDevInfo.byEhomeKey, 0, ptrbyEhomeKey, 32);

                        byte[] ClouldSecretKey = new byte[256];
                        if (HCEHomeSS.NET_ESS_HAMSHA256(ptrDeviceID, ptrbyEhomeKey, ClouldSecretKey, 255))
                        {
                            //Marshal.Copy(ptrClouldSecretKey,  stDevInfo.byClouldSecretKey,0, 64);
                            Array.Copy(ClouldSecretKey, stDevInfo.byClouldSecretKey, 64);
                        }
                        Marshal.FreeHGlobal(ptrDeviceID);
                        Marshal.FreeHGlobal(ptrbyEhomeKey);
                        //Marshal.FreeHGlobal(ptrClouldSecretKey);
                    }

                    //m_iCurDeviceIndex = i;
                    int tempindex = g_struDeviceInfo[i].iDeviceIndex;
                    g_struDeviceInfo[i] = stDevInfo;//注册成功, 拷贝设备信息
                    g_struDeviceInfo[i].iDeviceIndex = tempindex;

                    String strDevID = null;
                    if (stDevInfo.dwVersion >= 4)
                    {
                        if (g_struDeviceInfo[i].iDeviceIndex < 0)
                        {
                            if (false == iSByteArrayEmpty(ref g_struDeviceInfo[i].sDeviceSerial, 12))
                            {
                                strDevID = System.Text.Encoding.Default.GetString(g_struDeviceInfo[i].sDeviceSerial);
                                deviceNode = new TreeNode(strDevID);
                                deviceNode.ImageKey = "login";
                                deviceNode.SelectedImageIndex = 9;
                                deviceNode.Text = strDevID;
                                m_RootNode.Nodes.Add(deviceNode);
                            }
                            else
                            {
                                if (5 == stDevInfo.dwVersion && stDevInfo.iLoginID < 0)
                                {
                                    string strName = null;
                                    strName = ByteToStr(ref g_struDeviceInfo[i].byDeviceID,256) + "(offline)";
                                    deviceNode = new TreeNode(strName);
                                    deviceNode.ImageKey = "login";
                                    deviceNode.SelectedImageIndex = 9;
                                    deviceNode.Text = strName;
                                    m_RootNode.Nodes.Add(deviceNode);

                                }
                                else
                                {
                                    g_formList.AddLog(0, HCEHomePublic.OPERATION_FAIL_T, 3, "version is larger than four, serial is null，show DeviceID!");
                                    string strName = null;
                                    strName = ByteToStr(ref g_struDeviceInfo[i].byDeviceID, 256);
                                    deviceNode = new TreeNode(strName);
                                    deviceNode.ImageKey = "login";
                                    deviceNode.SelectedImageIndex = 9;
                                    deviceNode.Text = strName;
                                    m_RootNode.Nodes.Add(deviceNode);
                                }
                            }
                        }
                        else//给5.0设备未上线的上线
                        {
                            bool flag = false;
                            foreach (TreeNode node in m_RootNode.Nodes)
                            {
                                if (node.Text.Contains("offline"))
                                {
                                    flag = true;
                                    if (false == iSByteArrayEmpty(ref g_struDeviceInfo[i].sDeviceSerial, 12))
                                    {
                                        node.Text = ByteToStr(ref g_struDeviceInfo[i].sDeviceSerial,12);
                                        deviceNode = node;
                                    }
                                    else
                                    {
                                        node.Text = ByteToStr(ref g_struDeviceInfo[i].byDeviceID, 256);
                                        deviceNode = node;
                                    }
                                    break;
                                }
                            }
                            if (false == flag)
                            {
                                g_formList.AddLog(0, HCEHomePublic.OPERATION_FAIL_T, 3, "GET TREE ITEM FAIL");
                                return;
                            }
                        }
                    }
                    else
                    {
                        strDevID = System.Text.Encoding.Default.GetString(g_struDeviceInfo[i].byDeviceID);
                        deviceNode = new TreeNode(strDevID);
                        deviceNode.ImageKey = "login";
                        deviceNode.SelectedImageIndex = 9;
                        deviceNode.Text = strDevID;
                        m_RootNode.Nodes.Add(deviceNode);
                    }

                    
                    //    if (g_struDeviceInfo[i].sDeviceSerial.Length > 0)
                    //    {
                    //        strDevID = System.Text.Encoding.Default.GetString(g_struDeviceInfo[i].sDeviceSerial);
                    //        if (g_struDeviceInfo[i].sDeviceSerial[0] == 0)
                    //        {
                    //            strDevID = System.Text.Encoding.Default.GetString(g_struDeviceInfo[i].byDeviceID);
                    //        }
                    //        deviceNode = new TreeNode(strDevID);
                    //        deviceNode.ImageKey = "login";
                    //        deviceNode.SelectedImageIndex = 9;
                    //        deviceNode.Name = strDevID;
                    //        m_RootNode.Nodes.Add(deviceNode);
                    //    }
                    //    else
                    //    {
                    //        strDevID = System.Text.Encoding.Default.GetString(g_struDeviceInfo[i].byDeviceID);
                    //        deviceNode = new TreeNode(strDevID);
                    //        deviceNode.Name = strDevID;
                    //        m_RootNode.Nodes.Add(deviceNode);
                    //    }
                    //}
                    //else
                    //{
                    //    strDevID = System.Text.Encoding.Default.GetString(g_struDeviceInfo[i].byDeviceID);
                    //    deviceNode = new TreeNode(strDevID);
                    //    deviceNode.Name = strDevID;
                    //    m_RootNode.Nodes.Add(deviceNode);
                    //}

                    //获取设备信息
                    g_struDeviceInfo[i].iDeviceIndex = i;
                    if(stDevInfo.iLoginID>=0)
                    {
                        HCEHomeCMS.NET_EHOME_DEVICE_INFO struDevInfo = new HCEHomeCMS.NET_EHOME_DEVICE_INFO();
                        HCEHomeCMS.NET_EHOME_CONFIG struCfg = new HCEHomeCMS.NET_EHOME_CONFIG();
                        struDevInfo.sSerialNumber = new byte[HCEHomeCMS.MAX_SERIALNO_LEN];
                        struDevInfo.sSIMCardSN = new byte[HCEHomeCMS.MAX_SERIALNO_LEN];
                        struDevInfo.sSIMCardPhoneNum = new byte[HCEHomeCMS.MAX_PHOMENUM_LEN];
                        struDevInfo.byRes = new byte[160];
                        struDevInfo.dwSize = Marshal.SizeOf(struDevInfo);

                        IntPtr ptrDevInfo = Marshal.AllocHGlobal(struDevInfo.dwSize);
                        Marshal.StructureToPtr(struDevInfo, ptrDevInfo, false);

                        struCfg.pOutBuf = ptrDevInfo;
                        struCfg.byRes = new byte[40];
                        struCfg.dwOutSize = (uint)struDevInfo.dwSize;
                        uint dwConfigSize = (uint)Marshal.SizeOf(struCfg);
                        IntPtr ptrCfg = Marshal.AllocHGlobal(Marshal.SizeOf(struCfg));
                        if (!HCEHomeCMS.NET_ECMS_GetDevConfig(g_struDeviceInfo[i].iLoginID, EHomeExternalCommand.NET_EHOME_GET_DEVICE_INFO, ref struCfg, dwConfigSize))
                        {
                            String str = string.Format("{0}", (int)HCEHomeCMS.NET_ECMS_GetLastError());
                            Console.WriteLine("NET_ECMS_GetDevConfig failed, err={0}", (int)HCEHomeCMS.NET_ECMS_GetLastError());

                        }
                        else
                        {
                            struDevInfo = (HCEHomeCMS.NET_EHOME_DEVICE_INFO)Marshal.PtrToStructure(ptrDevInfo, typeof(HCEHomeCMS.NET_EHOME_DEVICE_INFO));

                            //获取UTF8格式数据
                            String strDevInfo;
                            strDevInfo = System.Text.Encoding.UTF8.GetString(struDevInfo.sSerialNumber);
                            int strDevInfoLen = strDevInfo.IndexOf('\0');
                            String strSIMCardSN = System.Text.Encoding.UTF8.GetString(struDevInfo.sSIMCardSN);
                            int strSIMCardSNLen = strSIMCardSN.IndexOf('\0');
                            String strSIMCardPhoneNum = System.Text.Encoding.UTF8.GetString(struDevInfo.sSIMCardPhoneNum);
                            int strSIMCardPhoneNumLen = strDevInfo.IndexOf('\0');

                            strTemp = string.Format("NET_EHOME_GET_DEVICE_INFO sSerialNumber= {0}", strDevInfo/*.Substring(0, strDevInfoLen)*/);
                            g_formList.AddLog(g_struDeviceInfo[i].iDeviceIndex, GlobalDefinition.OPERATION_SUCC_T, 1, strTemp);
                            strTemp = string.Format("NET_EHOME_GET_DEVICE_INFO sSIMCardSN={0}", strSIMCardSN/*.Substring(0, strSIMCardSNLen)*/);
                            g_formList.AddLog(g_struDeviceInfo[i].iDeviceIndex, GlobalDefinition.OPERATION_SUCC_T, 1, strTemp);
                            strTemp = string.Format("NET_EHOME_GET_DEVICE_INFO sSIMCardPhoneNum={0}", strSIMCardPhoneNum/*.Substring(0, strSIMCardPhoneNumLen)*/);
                            g_formList.AddLog(g_struDeviceInfo[i].iDeviceIndex, GlobalDefinition.OPERATION_SUCC_T, 1, strTemp);
                        }
                        Marshal.FreeHGlobal(ptrDevInfo);
                        Marshal.FreeHGlobal(ptrCfg);

                        g_struDeviceInfo[i].dwAlarmInNum = struDevInfo.dwAlarmInPortNum;
                        g_struDeviceInfo[i].dwAlarmOutNum = struDevInfo.dwAlarmOutPortNum;
                        g_struDeviceInfo[i].dwAnalogChanNum = struDevInfo.dwChannelNumber;
                        g_struDeviceInfo[i].dwDeviceChanNum = struDevInfo.dwChannelAmount;
                        g_struDeviceInfo[i].dwIPChanNum = struDevInfo.dwChannelAmount - struDevInfo.dwChannelNumber;
                        g_struDeviceInfo[i].dwAudioNum = struDevInfo.dwAudioChanNum;

                        //模拟通道
                        for (j = 0; j < struDevInfo.dwChannelNumber; j++)
                        {
                            //由于目前的通道无法判断是否可用，因此都当做可用
                            g_struDeviceInfo[i].struChanInfo[j].bEnable = true;
                            //这是模拟通道
                            g_struDeviceInfo[i].struChanInfo[j].iChannelNO = j + 1;
                            g_struDeviceInfo[i].struChanInfo[j].iChanType = HCEHomeCMS.DEMO_CHANNEL_TYPE_ANALOG;
                            strTemp = string.Format("Camera{0}", g_struDeviceInfo[i].struChanInfo[j].iChannelNO);
                            channelNode = new TreeNode(strTemp);
                            channelNode.ImageKey = "camera";
                            channelNode.SelectedImageIndex = 1;
                            deviceNode.Nodes.Add(channelNode);
                            g_struDeviceInfo[i].struChanInfo[j].iDeviceIndex = i;
                            g_struDeviceInfo[i].struChanInfo[j].iChanIndex = j;
                            //m_treeDeviceList.SetItemData(hChannel, CHANNELTYPE * 1000 + g_struDeviceInfo[i].struChanInfo[j].iChanIndex);
                        }

                        //然后添加IP通道
                        for (; j < struDevInfo.dwChannelAmount; j++)
                        {
                            //由于目前的通道无法判断是否可用，因此都当做可用
                            g_struDeviceInfo[i].struChanInfo[j].bEnable = true;
                            //这是IP通道
                            g_struDeviceInfo[i].struChanInfo[j].iChannelNO = (int)(struDevInfo.dwStartChannel + (j - struDevInfo.dwChannelNumber));
                            g_struDeviceInfo[i].struChanInfo[j].iChanType = GlobalDefinition.DEMO_CHANNEL_TYPE.DEMO_CHANNEL_TYPE_IP;
                            strTemp = string.Format("IPCamera{0}", g_struDeviceInfo[i].struChanInfo[j].iChannelNO);
                            channelNode = new TreeNode(strTemp);
                            channelNode.ImageKey = "camera";
                            channelNode.SelectedImageIndex = 1;
                            deviceNode.Nodes.Add(channelNode);
                            g_struDeviceInfo[i].struChanInfo[j].iDeviceIndex = i;
                            g_struDeviceInfo[i].struChanInfo[j].iChanIndex = j;
                            //m_treeDeviceList.SetItemData(hChannel, CHANNELTYPE * 1000 + g_struDeviceInfo[i].struChanInfo[j].iChanIndex);
                        }

                        //零通道处理逻辑
                        g_struDeviceInfo[i].dwZeroChanNum = struDevInfo.dwSupportZeroChan;// SupportZeroChan:支持零通道的个数：0-不支持，1-支持1路，2-支持2路，以此类推
                        g_struDeviceInfo[i].dwZeroChanStart = struDevInfo.dwStartZeroChan;// 零通道起始编号，默认10000
                        for (int k = 0; k < (int)struDevInfo.dwSupportZeroChan; k++)
                        {
                            int nZeroChannelIndex = j + k;
                            g_struDeviceInfo[i].struChanInfo[nZeroChannelIndex].bEnable = true;

                            //这是零通道
                            g_struDeviceInfo[i].struChanInfo[nZeroChannelIndex].iChannelNO = (int)(struDevInfo.dwStartZeroChan + k);
                            g_struDeviceInfo[i].struChanInfo[nZeroChannelIndex].iChanType = GlobalDefinition.DEMO_CHANNEL_TYPE.DEMO_CHANNEL_TYPE_ZERO;
                            strTemp = string.Format("ZeroChannel{0}", g_struDeviceInfo[i].struChanInfo[j].iChannelNO);
                            channelNode = new TreeNode(strTemp);
                            channelNode.ImageKey = "camera";
                            channelNode.SelectedImageIndex = 1;
                            deviceNode.Nodes.Add(channelNode);
                            g_struDeviceInfo[i].struChanInfo[nZeroChannelIndex].iDeviceIndex = i;
                            g_struDeviceInfo[i].struChanInfo[nZeroChannelIndex].iChanIndex = nZeroChannelIndex;
                            //m_treeDeviceList.SetItemData(hChannel, CHANNELTYPE * 1000 + g_struDeviceInfo[i].struChanInfo[nZeroChannelIndex].iChanIndex);
                        }

                        for (j = 0; j < GlobalDefinition.MAX_CHAN_NUM_DEMO; j++)
                        {
                            String strIP = m_cslocalIP;
                            System.Net.IPAddress ip = System.Net.IPAddress.Parse(strIP);
                            System.Net.IPAddress.NetworkToHostOrder(ip.Address);
                            strIP = ip.ToString();
                            strIP.CopyTo(0, g_struDeviceInfo[i].struChanInfo[j].struIP.szIP, 0, strIP.Length);
                        }
                        m_PreviewPanel[i].g_struDeviceInfo[i] = g_struDeviceInfo[i];

                    }
                    this.treeView1.ExpandAll();
                    break;
                }

            }
        }
        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            m_CurNodeArgs = e;
            if (1 == e.Node.Level)//play the Device
            {
                m_iCurDeviceIndex = e.Node.Index;
                m_iCurChanIndex = 0;                      //默认播放第一个通道
                g_struDeviceInfo[m_iCurDeviceIndex].struChanInfo[m_iCurChanIndex].iPlayWndIndex = m_iCurWndIndex;
                PlayChan(m_iCurDeviceIndex, m_iCurChanIndex, m_CurNodeArgs); 
            }
            else if (2 == e.Node.Level)  //play the Channel
            {
                m_iCurDeviceIndex = e.Node.Parent.Index;  //当前设备索引:form 0
                m_iCurChanIndex = e.Node.Index;           //当前通道索引:form 0
                PlayChan(m_iCurDeviceIndex, m_iCurChanIndex, m_CurNodeArgs);
            }
            this.Refresh();
        }

        public void PlayChan(int iDeviceIndex, int iChanIndex, TreeNodeMouseClickEventArgs e)
        {
            if(iDeviceIndex < 0)
            {
                g_formList.AddLog(iDeviceIndex, GlobalDefinition.OPERATION_FAIL_T, 0, "DeviceTree.PlayChan iDeviceIndex < 0");
                return;
            }
            int iPlayWndIndex = g_struDeviceInfo[iDeviceIndex].struChanInfo[iChanIndex].iPlayWndIndex;

            if (g_struDeviceInfo[iDeviceIndex].struChanInfo[iChanIndex].bPlay)
            {
                if (!m_PreviewPanel[iPlayWndIndex].StopPlay())
                {
                    g_formList.AddLog(iDeviceIndex, GlobalDefinition.OPERATION_FAIL_T, 0, "StopPlay Failed");
                }
                g_struDeviceInfo[iDeviceIndex].struChanInfo[iChanIndex].bPlay = false;
                g_struDeviceInfo[iDeviceIndex].struChanInfo[iChanIndex].iPlayWndIndex = -1;
                e.Node.ImageKey = "camera";
                e.Node.SelectedImageIndex = 1;
                m_CurPreviewPanel[iPlayWndIndex].BackColor = Color.Gray;
                m_CurPreviewPanel[iPlayWndIndex].Refresh();

            }
            else
            {
                g_struDeviceInfo[m_iCurDeviceIndex].struChanInfo[m_iCurChanIndex].iPlayWndIndex = m_iCurWndIndex;
                if (!m_PreviewPanel[m_iCurWndIndex].StartPlay(iDeviceIndex, iChanIndex))
                {
                    g_formList.AddLog(iDeviceIndex, GlobalDefinition.OPERATION_FAIL_T, 0, "StartPlay Failed");
                    return;
                }
                g_struDeviceInfo[iDeviceIndex].struChanInfo[iChanIndex].bPlay = true;
                e.Node.ImageKey = "play";
                e.Node.SelectedImageIndex = 12;
            }
        }


        public void PlayDevice(int iDeviceIndex, int iChannelIndex)
        {
            if (m_bPlaying)
            {
                if (!m_PreviewPanel[m_iCurWndIndex].StopPlay())
                {
                    g_formList.AddLog(0, HCEHomePublic.OPERATION_FAIL_T, 1, "StopPlay Failed");
                    return;
                }
                else
                {
                    m_bPlaying = false;
                    return;
                }
            }
            m_PreviewPanel[m_iCurWndIndex].StartPlay(iDeviceIndex, iDeviceIndex);
        }

        private void setEhomeKeyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EhomeV50Auth V50;
            if(m_iCurDeviceIndex>=0)
            {
                V50 = new EhomeV50Auth(m_iCurDeviceIndex);
            }
            else
            {
                V50 = new EhomeV50Auth();
            }
            V50.ShowDialog();
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            treeView1.SelectedNode = e.Node;
            if(e.Button==MouseButtons.Right&&1==e.Node.Level)
            {
                m_iCurDeviceIndex = e.Node.Index;
                e.Node.ContextMenuStrip = treeEhomeKeyMenuStrip;
                treeEhomeKeyMenuStrip.Show(e.Location);
            }
        }


        public delegate void DelegateDev(Message m);
        public  void ProDevStatu(Message mes)
        {
            if (this.InvokeRequired)
            {
                Delegate delegateAddLog = new DelegateDev(ProDevStatu);
                this.BeginInvoke(delegateAddLog, mes);
            }
            else
            {
                MessageProcess(mes);
            }
        }

        
        private void SetDeviceIndex(int index)
        {
            m_iCurDeviceIndex = index;
        }
    }
}
