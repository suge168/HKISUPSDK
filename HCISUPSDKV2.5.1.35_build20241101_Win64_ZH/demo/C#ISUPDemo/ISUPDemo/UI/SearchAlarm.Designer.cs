namespace EHomeDemo.UI
{
    partial class SearchAlarm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxMainType = new System.Windows.Forms.ComboBox();
            this.comboBoxSecondType = new System.Windows.Forms.ComboBox();
            this.dateTimeStart = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dateTimeEnd = new System.Windows.Forms.DateTimePicker();
            this.btnSearch = new System.Windows.Forms.Button();
            this.listViewEvent = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader12 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader13 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader14 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader15 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader16 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader17 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader18 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader19 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader20 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader21 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader22 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader23 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader24 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader25 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader26 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader27 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader28 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader29 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader30 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader31 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Consolas", 10.5F, System.Drawing.FontStyle.Bold);
            this.label5.ForeColor = System.Drawing.Color.DimGray;
            this.label5.Location = new System.Drawing.Point(101, 107);
            this.label5.Name = "label5";
            this.label5.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label5.Size = new System.Drawing.Size(88, 17);
            this.label5.TabIndex = 93;
            this.label5.Text = "Start Time";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Consolas", 10.5F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.DimGray;
            this.label1.Location = new System.Drawing.Point(101, 61);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label1.Size = new System.Drawing.Size(88, 17);
            this.label1.TabIndex = 92;
            this.label1.Text = "Major Type";
            // 
            // comboBoxMainType
            // 
            this.comboBoxMainType.FormattingEnabled = true;
            this.comboBoxMainType.Items.AddRange(new object[] {
            "All",
            "Alarm",
            "Exception",
            "Operation",
            "Event"});
            this.comboBoxMainType.Location = new System.Drawing.Point(221, 61);
            this.comboBoxMainType.Name = "comboBoxMainType";
            this.comboBoxMainType.Size = new System.Drawing.Size(121, 20);
            this.comboBoxMainType.TabIndex = 94;
            // 
            // comboBoxSecondType
            // 
            this.comboBoxSecondType.FormattingEnabled = true;
            this.comboBoxSecondType.Items.AddRange(new object[] {
            "All",
            "4G_MOUDLE_OFFLINE",
            "4G_MOUDLE_ONLINE",
            "AC_OFF",
            "AC_RESUME",
            "ALARMIN_ARM",
            "ALARMIN_BROKEN_CIRCUIT",
            "ALARMIN_DISARM",
            "ALARMIN_EXCEPTION",
            "ALARMIN_RESUME",
            "ALARMIN_SHORT_CIRCUIT",
            "ALARMOUT_OFF",
            "ALARMOUT_ON",
            "ALWAYS_CLOSE_BEGIN",
            "ALWAYS_CLOSE_END",
            "ALWAYS_OPEN_BEGIN",
            "ALWAYS_OPEN_END",
            "ANTI_SNEAK_FAIL",
            "AUTH_PLAN_DORMANT_FAIL",
            "AUTO_COMPLEMENT_NUMBER",
            "AUTO_RENUMBER",
            "BATTERY_ELECTRIC_LOW",
            "BATTERY_ELECTRIC_RESUME",
            "BATTERY_RESUME",
            "CAMERA_NOT_CONNECT",
            "CAMERA_RESUME",
            "CAN_BUS_EXCEPTION",
            "CAN_BUS_RESUME",
            "CARD_AND_PSW_FAIL",
            "CARD_AND_PSW_OVER_TIME",
            "CARD_AND_PSW_PASS",
            "CARD_AND_PSW_TIMEOUT",
            "CARD_ENCRYPT_VERIFY_FAIL",
            "CARD_FINGERPRINT_PASSWD_VERIFY_FAIL",
            "CARD_FINGERPRINT_PASSWD_VERIFY_PASS",
            "CARD_FINGERPRINT_PASSWD_VERIFY_TIMEOUT",
            "CARD_FINGERPRINT_VERIFY_FAIL",
            "CARD_FINGERPRINT_VERIFY_PASS",
            "CARD_FINGERPRINT_VERIFY_TIMEOUT",
            "CARD_INVALID_PERIOD",
            "CARD_MAX_AUTHENTICATE_FAIL",
            "CARD_NO_RIGHT",
            "CARD_OUT_OF_DATE",
            "CARD_PLATFORM_VERIFY",
            "CARD_READER_DESMANTLE_ALARM",
            "CARD_READER_DESMANTLE_RESUME",
            "CARD_READER_OFFLINE",
            "CARD_READER_RESUME",
            "CARD_RIGHT_INPUT",
            "CARD_RIGHT_OUTTPUT",
            "CASE_SENSOR_ALARM",
            "CASE_SENSOR_RESUME",
            "CHANNEL_CONTROLLER_DESMANTLE_ALARM",
            "CHANNEL_CONTROLLER_DESMANTLE_RESUME",
            "CHANNEL_CONTROLLER_FIRE_IMPORT_ALARM",
            "CHANNEL_CONTROLLER_FIRE_IMPORT_RESUME",
            "CHANNEL_CONTROLLER_OFF",
            "CHANNEL_CONTROLLER_RESUME",
            "CLIMBING_OVER_GATE",
            "COM_NOT_CONNECT",
            "COM_RESUME",
            "COMBINED_VERIFY_PASS",
            "COMBINED_VERIFY_TIMEOUT",
            "DEV_POWER_OFF",
            "DEV_POWER_ON",
            "DEVICE_NOT_AUTHORIZE",
            "DISTRACT_CONTROLLER_ALARM",
            "DISTRACT_CONTROLLER_OFFLINE",
            "DISTRACT_CONTROLLER_ONLINE",
            "DISTRACT_CONTROLLER_RESUME",
            "DOOR_BUTTON_PRESS",
            "DOOR_BUTTON_RELEASE",
            "DOOR_CLOSE_NORMAL",
            "DOOR_OPEN_ABNORMAL",
            "DOOR_OPEN_NORMAL",
            "DOOR_OPEN_OR_DORMANT_FAIL",
            "DOOR_OPEN_OR_DORMANT_LINKAGE_OPEN_FAIL",
            "DOOR_OPEN_OR_DORMANT_OPEN_FAIL",
            "DOOR_OPEN_TIMEOUT",
            "DOORBELL_RINGING",
            "DROP_ARM_BLOCK",
            "DROP_ARM_BLOCK_RESUME",
            "EMERGENCY_BUTTON_RESUME",
            "EMERGENCY_BUTTON_TRIGGER",
            "EMPLOYEE_NO_NOT_EXIST",
            "FACE_IMAGE_QUALITY_LOW",
            "FINGE_RPRINT_QUALITY_LOW",
            "FINGER_PRINT_MODULE_NOT_CONNECT",
            "FINGER_PRINT_MODULE_RESUME",
            "FINGERPRINT_COMPARE_FAIL",
            "FINGERPRINT_COMPARE_PASS",
            "FINGERPRINT_INEXISTENCE",
            "FINGERPRINT_PASSWD_VERIFY_FAIL",
            "FINGERPRINT_PASSWD_VERIFY_PASS",
            "FINGERPRINT_PASSWD_VERIFY_TIMEOUT",
            "FIRE_BUTTON_RESUME",
            "FIRE_BUTTON_TRIGGER",
            "FIRE_IMPORT_BROKEN_CIRCUIT",
            "FIRE_IMPORT_RESUME",
            "FLASH_ABNORMAL",
            "FORCE_ACCESS",
            "FREE_GATE_PASS_NOT_AUTH",
            "GATE_TEMPERATURE_OVERRUN",
            "HOST_DESMANTLE_ALARM",
            "HOST_DESMANTLE_RESUME",
            "ID_CARD_READER_NOT_CONNECT",
            "ID_CARD_READER_RESUME",
            "ILLEGAL_MESSAGE",
            "INDICATOR_LIGHT_OFF",
            "INDICATOR_LIGHT_RESUME",
            "INTERLOCK_DOOR_NOT_CLOSE",
            "INTRUSION_ALARM",
            "INVALID_CARD",
            "INVALID_MULTI_VERIFY_PERIOD",
            "IR_ADAPTOR_COMM_EXCEPTION",
            "IR_ADAPTOR_COMM_RESUME",
            "IR_EMITTER_EXCEPTION",
            "IR_EMITTER_RESUME",
            "LAMP_BOARD_COMM_EXCEPTION",
            "LAMP_BOARD_COMM_RESUME",
            "LEADER_CARD_OPEN_BEGIN",
            "LEADER_CARD_OPEN_END",
            "LEGAL_CARD_PASS",
            "LEGAL_EVENT_NEARLY_FULL",
            "LEGAL_MESSAGE",
            "LINKAGE_CAPTURE_PIC",
            "LOCAL_CONTROL_NET_BROKEN",
            "LOCAL_CONTROL_NET_RSUME",
            "LOCAL_CONTROL_OFFLINE",
            "LOCAL_CONTROL_RESUME",
            "LOCAL_DOWNSIDE_RS485_LOOPNODE_BROKEN",
            "LOCAL_DOWNSIDE_RS485_LOOPNODE_RESUME",
            "LOCAL_FACE_MODELING_FAIL",
            "LOCAL_LOGIN_LOCK",
            "LOCAL_LOGIN_UNLOCK",
            "LOCAL_RESTORE_CFG",
            "LOCAL_UPGRADE",
            "LOCAL_USB_UPGRADE",
            "LOCK_CLOSE",
            "LOCK_OPEN",
            "LOW_BATTERY",
            "MAC_DETECT",
            "MAINTENANCE_BUTTON_RESUME",
            "MAINTENANCE_BUTTON_TRIGGER",
            "MASTER_RS485_LOOPNODE_BROKEN",
            "MASTER_RS485_LOOPNODE_RESUME",
            "MINOR_REMOTE_ARM",
            "MOD_GPRS_REPORT_PARAM",
            "MOD_NET_REPORT_CFG",
            "MOD_REPORT_GROUP_PARAM",
            "MOTOR_SENSOR_EXCEPTION",
            "MULTI_VERIFY_NEED_REMOTE_OPEN",
            "MULTI_VERIFY_REMOTE_RIGHT_FAIL",
            "MULTI_VERIFY_REPEAT_VERIFY",
            "MULTI_VERIFY_SUCCESS",
            "MULTI_VERIFY_SUPER_RIGHT_FAIL",
            "MULTI_VERIFY_SUPERPASSWD_VERIFY_SUCCESS",
            "MULTI_VERIFY_TIMEOUT",
            "NET_BROKEN",
            "NET_RESUME",
            "NORMAL_CFGFILE_INPUT",
            "NORMAL_CFGFILE_OUTTPUT",
            "NOT_BELONG_MULTI_GROUP",
            "NTP_CHECK_TIME",
            "OFFLINE_ECENT_NEARLY_FULL",
            "PASSING_TIMEOUT",
            "PASSWORD_MISMATCH",
            "PEOPLE_AND_ID_CARD_DEVICE_OFFLINE",
            "PEOPLE_AND_ID_CARD_DEVICE_ONLINE",
            "POS_END_ALARM",
            "POS_START_ALARM",
            "PRINTER_OFFLINE",
            "PRINTER_ONLINE",
            "PRINTER_OUT_OF_PAPER",
            "REMOTE_ACTUAL_GUARD",
            "REMOTE_ACTUAL_UNGUARD",
            "REMOTE_ALARMOUT_CLOSE_MAN",
            "REMOTE_ALARMOUT_OPEN_MAN",
            "REMOTE_ALWAYS_CLOSE",
            "REMOTE_ALWAYS_OPEN",
            "REMOTE_CAPTURE_PIC",
            "REMOTE_CFGFILE_INTPUT",
            "REMOTE_CFGFILE_OUTPUT",
            "REMOTE_CHECK_TIME",
            "REMOTE_CLEAR_CARD",
            "REMOTE_CLOSE_DOOR",
            "REMOTE_CONTROL_ALWAYS_OPEN_DOOR",
            "REMOTE_CONTROL_CLOSE_DOOR",
            "REMOTE_CONTROL_NOT_CODE_OPER_FAILED",
            "REMOTE_CONTROL_OPEN_DOOR",
            "REMOTE_DISARM",
            "REMOTE_HOUSEHOLD_CALL_LADDER",
            "REMOTE_LOGIN",
            "REMOTE_LOGOUT",
            "REMOTE_OPEN_DOOR",
            "REMOTE_REBOOT",
            "REMOTE_RESTORE_CFG",
            "REMOTE_UPGRADE",
            "REMOTE_VISITOR_CALL_LADDER",
            "REVERSE_ACCESS",
            "RS485_DEVICE_ABNORMAL",
            "RS485_DEVICE_REVERT",
            "SD_CARD_FULL",
            "SECURITY_MODULE_DESMANTLE_ALARM",
            "SECURITY_MODULE_DESMANTLE_RESUME",
            "SECURITY_MODULE_OFF",
            "SECURITY_MODULE_RESUME",
            "STAY_EVENT",
            "STRESS_ALARM",
            "SUBMARINEBACK_COMM_BREAK",
            "SUBMARINEBACK_COMM_RESUME",
            "SUBMARINEBACK_REPLY_FAIL",
            "TRAILING",
            "UNLOCK_PASSWORD_OPEN_DOOR",
            "VERIFY_MODE_MISMATCH",
            "WATCH_DOG_RESET"});
            this.comboBoxSecondType.Location = new System.Drawing.Point(636, 61);
            this.comboBoxSecondType.Name = "comboBoxSecondType";
            this.comboBoxSecondType.Size = new System.Drawing.Size(121, 20);
            this.comboBoxSecondType.TabIndex = 95;
            // 
            // dateTimeStart
            // 
            this.dateTimeStart.Location = new System.Drawing.Point(221, 107);
            this.dateTimeStart.Name = "dateTimeStart";
            this.dateTimeStart.Size = new System.Drawing.Size(121, 21);
            this.dateTimeStart.TabIndex = 96;
            this.dateTimeStart.Value = new System.DateTime(2020, 3, 20, 10, 12, 0, 0);
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Consolas", 10.5F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.DimGray;
            this.label4.Location = new System.Drawing.Point(526, 110);
            this.label4.Name = "label4";
            this.label4.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label4.Size = new System.Drawing.Size(72, 17);
            this.label4.TabIndex = 98;
            this.label4.Text = "End Time";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Consolas", 10.5F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.DimGray;
            this.label3.Location = new System.Drawing.Point(525, 64);
            this.label3.Name = "label3";
            this.label3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label3.Size = new System.Drawing.Size(88, 17);
            this.label3.TabIndex = 97;
            this.label3.Text = "Minor Type";
            // 
            // dateTimeEnd
            // 
            this.dateTimeEnd.Location = new System.Drawing.Point(636, 110);
            this.dateTimeEnd.Name = "dateTimeEnd";
            this.dateTimeEnd.Size = new System.Drawing.Size(121, 21);
            this.dateTimeEnd.TabIndex = 99;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(708, 174);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(72, 31);
            this.btnSearch.TabIndex = 100;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // listViewEvent
            // 
            this.listViewEvent.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.listViewEvent.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader9,
            this.columnHeader10,
            this.columnHeader11,
            this.columnHeader12,
            this.columnHeader13,
            this.columnHeader14,
            this.columnHeader15,
            this.columnHeader16,
            this.columnHeader17,
            this.columnHeader18,
            this.columnHeader19,
            this.columnHeader20,
            this.columnHeader21,
            this.columnHeader22,
            this.columnHeader23,
            this.columnHeader24,
            this.columnHeader25,
            this.columnHeader26,
            this.columnHeader27,
            this.columnHeader28,
            this.columnHeader29,
            this.columnHeader30,
            this.columnHeader31});
            this.listViewEvent.FullRowSelect = true;
            this.listViewEvent.GridLines = true;
            this.listViewEvent.Location = new System.Drawing.Point(104, 239);
            this.listViewEvent.Name = "listViewEvent";
            this.listViewEvent.Size = new System.Drawing.Size(676, 383);
            this.listViewEvent.TabIndex = 101;
            this.listViewEvent.UseCompatibleStateImageBehavior = false;
            this.listViewEvent.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "ID";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "major";
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "minor";
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "time";
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "netuser";
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "remoteHostAddr";
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "CardNo";
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "CardType";
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "whiteListNo";
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "reportChannel";
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "cardReaderKind";
            // 
            // columnHeader12
            // 
            this.columnHeader12.Text = "CardReaderNo";
            // 
            // columnHeader13
            // 
            this.columnHeader13.Text = "doorNO";
            // 
            // columnHeader14
            // 
            this.columnHeader14.Text = "verifyNo";
            // 
            // columnHeader15
            // 
            this.columnHeader15.Text = "alarmInfo";
            // 
            // columnHeader16
            // 
            this.columnHeader16.Text = "alarmoutNo";
            // 
            // columnHeader17
            // 
            this.columnHeader17.Text = "caseSensorNO";
            // 
            // columnHeader18
            // 
            this.columnHeader18.Text = "RS485NO";
            // 
            // columnHeader19
            // 
            this.columnHeader19.Text = "multiCardGroupNo";
            // 
            // columnHeader20
            // 
            this.columnHeader20.Text = "accessChannel";
            // 
            // columnHeader21
            // 
            this.columnHeader21.Text = "DeviceNo";
            // 
            // columnHeader22
            // 
            this.columnHeader22.Text = "Distract Control No";
            // 
            // columnHeader23
            // 
            this.columnHeader23.Text = "employeeNoString";
            // 
            // columnHeader24
            // 
            this.columnHeader24.Text = "local Controller ID";
            // 
            // columnHeader25
            // 
            this.columnHeader25.Text = "InternetAccess";
            // 
            // columnHeader26
            // 
            this.columnHeader26.Text = "type";
            // 
            // columnHeader27
            // 
            this.columnHeader27.Text = "MACAddr";
            // 
            // columnHeader28
            // 
            this.columnHeader28.Text = "SwipeCardType";
            // 
            // columnHeader29
            // 
            this.columnHeader29.Text = "SerialNo";
            // 
            // columnHeader30
            // 
            this.columnHeader30.Text = "attendanceStatus";
            // 
            // columnHeader31
            // 
            this.columnHeader31.Text = "StatusValue";
            // 
            // SearchAlarm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(977, 672);
            this.Controls.Add(this.listViewEvent);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.dateTimeEnd);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dateTimeStart);
            this.Controls.Add(this.comboBoxSecondType);
            this.Controls.Add(this.comboBoxMainType);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label1);
            this.Name = "SearchAlarm";
            this.Text = "SearchAlarm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxMainType;
        private System.Windows.Forms.ComboBox comboBoxSecondType;
        private System.Windows.Forms.DateTimePicker dateTimeStart;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dateTimeEnd;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.ListView listViewEvent;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.ColumnHeader columnHeader12;
        private System.Windows.Forms.ColumnHeader columnHeader13;
        private System.Windows.Forms.ColumnHeader columnHeader14;
        private System.Windows.Forms.ColumnHeader columnHeader15;
        private System.Windows.Forms.ColumnHeader columnHeader16;
        private System.Windows.Forms.ColumnHeader columnHeader17;
        private System.Windows.Forms.ColumnHeader columnHeader18;
        private System.Windows.Forms.ColumnHeader columnHeader19;
        private System.Windows.Forms.ColumnHeader columnHeader20;
        private System.Windows.Forms.ColumnHeader columnHeader21;
        private System.Windows.Forms.ColumnHeader columnHeader22;
        private System.Windows.Forms.ColumnHeader columnHeader23;
        private System.Windows.Forms.ColumnHeader columnHeader24;
        private System.Windows.Forms.ColumnHeader columnHeader25;
        private System.Windows.Forms.ColumnHeader columnHeader26;
        private System.Windows.Forms.ColumnHeader columnHeader27;
        private System.Windows.Forms.ColumnHeader columnHeader28;
        private System.Windows.Forms.ColumnHeader columnHeader29;
        private System.Windows.Forms.ColumnHeader columnHeader30;
        private System.Windows.Forms.ColumnHeader columnHeader31;
    }
}