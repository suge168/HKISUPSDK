#ifndef _EHOMEDEMO_LOGTYPE_H_
#define _EHOMEDEMO_LOGTYPE_H_

#ifndef _HC_NET_SDK_H_

/* ���� */
//������
#define MAJOR_ALARM                        0x1
//������
#define MINOR_ALARM_IN                    0x1        /* �������� */
#define MINOR_ALARM_OUT                    0x2        /* ������� */
#define MINOR_MOTDET_START                0x3        /* �ƶ���ⱨ����ʼ */
#define MINOR_MOTDET_STOP                0x4        /* �ƶ���ⱨ������ */
#define MINOR_HIDE_ALARM_START            0x5        /* �ڵ�������ʼ */
#define MINOR_HIDE_ALARM_STOP            0x6        /* �ڵ��������� */
#define MINOR_VCA_ALARM_START            0x7        /*���ܱ�����ʼ*/
#define MINOR_VCA_ALARM_STOP            0x8        /*���ܱ���ֹͣ*/
#define MINOR_ITS_ALARM_START           0x09    // ��ͨ�¼�������ʼ
#define MINOR_ITS_ALARM_STOP            0x0A    // ��ͨ�¼���������
//2010-11-10 ���籨����־
#define MINOR_NETALARM_START            0x0b    /*���籨����ʼ*/
#define MINOR_NETALARM_STOP             0x0c    /*���籨������*/
//2010-12-16 ��������־����"MINOR_ALARM_IN"���ʹ��
#define MINOR_NETALARM_RESUME            0x0d    /*���籨���ָ�*/
//2012-4-5 IPC PIR�����ߡ����ȱ���
#define MINOR_WIRELESS_ALARM_START      0x0e  /* ���߱�����ʼ */
#define MINOR_WIRELESS_ALARM_STOP          0x0f /* ���߱������� */
#define MINOR_PIR_ALARM_START           0x10  /* �����Ӧ������ʼ */
#define MINOR_PIR_ALARM_STOP               0x11  /* �����Ӧ�������� */
#define MINOR_CALLHELP_ALARM_START      0x12  /* ���ȱ�����ʼ */
#define MINOR_CALLHELP_ALARM_STOP          0x13  /* ���ȱ������� */
#define MINOR_IPCHANNEL_ALARMIN_START   0x14  //����ͨ���������뿪ʼ��PCNVR�ڽ��յ�����ͨ����MINOR_ALARM_IN����������ͨ���������뿪ʼ����10s�����ղ���MINOR_ALARM_IN������������ͨ���������������
#define MINOR_IPCHANNEL_ALARMIN_STOP    0x15  //����ͨ���������뿪ʼ��ͬ��
#define MINOR_DETECTFACE_ALARM_START    0x16  /* ������ⱨ����ʼ */
#define MINOR_DETECTFACE_ALARM_STOP      0x17  /* ������ⱨ������ */
#define MINOR_VQD_ALARM_START           0x18  //VQD����
#define MINOR_VQD_ALARM_STOP            0x19  //VQD�������� 
#define MINOR_VCA_SECNECHANGE_DETECTION 0x1a  //������ⱨ�� 2013-07-16

#define MINOR_SMART_REGION_EXITING_BEGIN            0x1b  //�뿪������⿪ʼ
#define MINOR_SMART_REGION_EXITING_END              0x1c  //�뿪����������
#define MINOR_SMART_LOITERING_BEGIN                 0x1d  //�ǻ���⿪ʼ
#define MINOR_SMART_LOITERING_END                   0x1e  //�ǻ�������

#define MINOR_VCA_ALARM_LINE_DETECTION_BEGIN        0x20  
#define MINOR_VCA_ALARM_LINE_DETECTION_END          0x21  
#define MINOR_VCA_ALARM_INTRUDE_BEGIN                0x22  //������⿪ʼ    
#define MINOR_VCA_ALARM_INTRUDE_END                       0x23  //����������    
#define MINOR_VCA_ALARM_AUDIOINPUT                    0x24   //��Ƶ�쳣����
#define MINOR_VCA_ALARM_AUDIOABNORMAL                 0x25   //��ǿͻ��
#define MINOR_VCA_DEFOCUS_DETECTION_BEGIN           0x26  //�齹��⿪ʼ
#define MINOR_VCA_DEFOCUS_DETECTION_END                0x27  //�齹������

//����NVR
#define MINOR_EXT_ALARM                             0x28/*IPC�ⲿ����*/
#define MINOR_VCA_FACE_ALARM_BEGIN                    0x29    /*������⿪ʼ*/
#define MINOR_SMART_REGION_ENTRANCE_BEGIN           0x2a  //����������⿪ʼ
#define MINOR_SMART_REGION_ENTRANCE_END             0x2b  //��������������
#define MINOR_SMART_PEOPLE_GATHERING_BEGIN          0x2c  //��Ա�ۼ���⿪ʼ
#define MINOR_SMART_PEOPLE_GATHERING_END            0x2d  //��Ա�ۼ�������
#define MINOR_SMART_FAST_MOVING_BEGIN               0x2e  //�����˶���⿪ʼ
#define MINOR_SMART_FAST_MOVING_END                 0x2f  //�����˶�������

#define MINOR_VCA_FACE_ALARM_END                    0x30    /*����������*/
#define MINOR_VCA_SCENE_CHANGE_ALARM_BEGIN            0x31   /*���������⿪ʼ*/
#define MINOR_VCA_SCENE_CHANGE_ALARM_END            0x32   /*�������������*/
#define MINOR_VCA_ALARM_AUDIOINPUT_BEGIN            0x33   /*��Ƶ�쳣���뿪ʼ*/
#define MINOR_VCA_ALARM_AUDIOINPUT_END                0x34   /*��Ƶ�쳣�������*/
#define MINOR_VCA_ALARM_AUDIOABNORMAL_BEGIN            0x35  /*��ǿͻ����⿪ʼ*/
#define MINOR_VCA_ALARM_AUDIOABNORMAL_END            0x36  /*��ǿͻ��������*/

#define MINOR_VCA_LECTURE_DETECTION_BEGIN           0x37  //�ڿ���⿪ʼ����
#define MINOR_VCA_LECTURE_DETECTION_END             0x38  //�ڿ�����������
#define MINOR_VCA_ALARM_AUDIOSTEEPDROP              0x39  //��ǿ���� 2014-03-21
#define MINOR_VCA_ANSWER_DETECTION_BEGIN            0x3a  //�ش�������⿪ʼ����
#define MINOR_VCA_ANSWER_DETECTION_END              0x3b  //�ش���������������

#define MINOR_SMART_PARKING_BEGIN                   0x3c   //ͣ����⿪ʼ
#define MINOR_SMART_PARKING_END                     0x3d   //ͣ��������
#define MINOR_SMART_UNATTENDED_BAGGAGE_BEGIN        0x3e   //��Ʒ������⿪ʼ
#define MINOR_SMART_UNATTENDED_BAGGAGE_END          0x3f   //��Ʒ����������
#define MINOR_SMART_OBJECT_REMOVAL_BEGIN            0x40   //��Ʒ��ȡ��⿪ʼ
#define MINOR_SMART_OBJECT_REMOVAL_END              0x41   //��Ʒ��ȡ������
#define MINOR_SMART_VEHICLE_ALARM_START             0x46   //���Ƽ�⿪ʼ
#define MINOR_SMART_VEHICLE_ALARM_STOP              0x47   //���Ƽ�����
#define MINOR_THERMAL_FIREDETECTION                 0x48   //�ȳ���������⿪ʼ
#define MINOR_THERMAL_FIREDETECTION_END             0x49   //�ȳ�������������
#define MINOR_SMART_VANDALPROOF_BEGIN               0x50   //���ƻ���⿪ʼ
#define MINOR_SMART_VANDALPROOF_END                 0x51   //���ƻ�������

#define MINOR_FACESNAP_MATCH_ALARM_START            0x55  /*�����ȶԱ�����ʼ*/
#define MINOR_FACESNAP_MATCH_ALARM_STOP             0x56  /*�����ȶԱ�������*/
#define MINOR_WHITELIST_FACESNAP_MATCH_ALARM_START  0x57  /*��Ȩ���������ȶԣ�İ���ˣ�������ʼ*/
#define MINOR_WHITELIST_FACESNAP_MATCH_ALARM_STOP   0x58  /*��Ȩ���������ȶԣ�İ���ˣ���������*/

#define MINOR_THERMAL_SHIPSDETECTION                0x5a   //�ȳ���ֻ������
#define MINOR_THERMAL_THERMOMETRY_EARLYWARNING_BEGIN 0x5b  //�ȳ������Ԥ����ʼ
#define MINOR_THERMAL_THERMOMETRY_EARLYWARNING_END  0x5c   //�ȳ������Ԥ������
#define MINOR_THERMAL_THERMOMETRY_ALARM_BEGIN       0x5d   //�ȳ�����±�����ʼ
#define MINOR_THERMAL_THERMOMETRY_ALARM_END         0x5e   //�ȳ�����±�������
#define MINOR_THERMAL_THERMOMETRY_DIFF_ALARM_BEGIN  0x5f   //�ȳ����²����ʼ
#define MINOR_THERMAL_THERMOMETRY_DIFF_ALARM_END    0x60   //�ȳ����²������
#define MINOR_FACE_THERMOMETRY_ALARM                0x63   //�������±���
#define MINOR_SMART_DENSEFOGDETECTION_BEGIN         0x6e   //������⿪ʼ
#define MINOR_SMART_DENSEFOGDETECTION_END           0x6f   //����������
#define MINOR_RUNNING_ALARM                         0x70   //���ܼ��
#define MINOR_RETENTION_ALARM                       0x71   //�������
#define MINOR_SAFETY_HELMET_ALARM_START             0x72  /*δ�����ȫñ��ⱨ����ʼ*/
#define MINOR_SAFETY_HELMET_ALARM_STOP              0x73  /*δ�����ȫñ��ⱨ������*/
#define MINOR_HFPD_ALARM_START                      0x74  /*��Ƶ��Ա��ⱨ����ʼ*/
#define MINOR_HFPD_ALARM_STOP                       0x75  /*��Ƶ��Ա��ⱨ������*/
#define MINOR_MIXED_TARGET_ALARM_START              0x76  /*���Ŀ���ⱨ����ʼ*/
#define MINOR_MIXED_TARGET_ALARM_STOP               0x77  /*���Ŀ���ⱨ������*/
#define MINOR_VCA_GET_UP_ALARM_BEGIN                0x80   //�𴲼�ⱨ����ʼ
#define MINOR_VCA_GET_UP_ALARM_END                  0x81   //�𴲼�ⱨ������
#define MINOR_VCA_ADV_REACH_HEIGHT_ALARM_BEGIN      0x82   //�����ʸ߱�����ʼ
#define MINOR_VCA_ADV_REACH_HEIGHT_ALARM_END        0x83   //�����ʸ߱�������
#define MINOR_VCA_TOILET_TARRY_ALARM_BEGIN          0x84   //��ʱ������ʼ
#define MINOR_VCA_TOILET_TARRY_ALARM_END            0x85   //��ʱ��������
#define MINOR_HUMAN_RECOGNITION_ALARM_BEGIN         0x86   //����ʶ�𱨾���ʼ
#define MINOR_HUMAN_RECOGNITION_ALARM_END           0x87   //����ʶ�𱨾�����
#define MINOR_STUDENTS_STOODUP_ALARM_BEGIN          0x88   //ѧ������������ʼ
#define MINOR_STUDENTS_STOODUP_ALARM_END            0x89   //ѧ��������������
#define MINOR_FRAMES_PEOPLE_COUNTING_ALARM          0x8a   //��������ͳ�Ʊ���
#define MINOR_FACE_SNAP_ALARM_BEGIN                 0x8b   //����ץ�ı�����ʼ
#define MINOR_FACE_SNAP_ALARM_END                   0x8c   //����ץ�ı�������
#define MINOR_TEACHER_BEHAVIOR_DETECT_ALARM_BEGIN   0x8d   //��ʦ��Ϊ��ⱨ����ʼ
#define MINOR_TEACHER_BEHAVIOR_DETECT_ALARM_END     0x8e   //��ʦ��Ϊ��ⱨ������
#define MINOR_PERIMETER_CAPTURE_ALARM_BEGIN         0x8f   //�ܽ�ץ�ı�����ʼ
#define MINOR_PERIMETER_CAPTURE_ALARM_END           0x90   //�ܽ�ץ�ı�������
#define MINOR_UNREGISTERED_STREET_VENDOR_ALARM      0x91  //�Ƿ���̯����

#define MINOR_PERSON_QUEUE_TIME_ALARM_BEGIN         0x92   //�Ŷ�ʱ����ⱨ����ʼ
#define MINOR_PERSON_QUEUE_TIME_ALARM_END           0x93   //�Ŷ�ʱ����ⱨ������
#define MINOR_PERSON_QUEUE_COUNTING_ALARM_BEGIN     0x94   //�Ŷ�������ⱨ����ʼ
#define MINOR_PERSON_QUEUE_COUNTING_ALARM_END       0x95   //�Ŷ�������ⱨ������


#define MINOR_ACCESS_CONTROLLER_EVENT               0x100  //�Ž������¼�
#define MINOR_VIDEO_INTERCOM_EVENT                  0x101  //���ӶԽ��豸�¼�
#define MINOR_GJD_EVENT                             0x102  //GJD���������¼�
#define MINOR_LUMINITE_EVENT                        0x103  // LUMINITE���������¼�
#define MINOR_OPTEX_EVENT                           0x104  // OPTEX���������¼�
#define MINOR_CAMERA_DETECTOR_EVENT                 0x105  // �������¼�
#define MINOR_SECURITY_CONTROL_PANEL_EVENT          0x106  //�������������¼�

#define MINOR_VCA_SPACE_CHANGE_START                0x10c //����쳣��⿪ʼ
#define MINOR_VCA_SPACE_CHANGE_STOP                 0x10d //����쳣������
#define MINOR_MANUAL_ALARM                          0x10e //�ֶ�����
#define MINOR_DETECTOR_ALARM                        0x10f //̽��������
#define MINOR_LINKAGE_ALARM                         0x110 //��������
#define MINOR_VCA_SITUATION_ANALYSIS_START          0x111 //̬�Ʒ�����⿪ʼ
#define MINOR_VCA_SITUATION_ANALYSIS_STOP           0x112 //̬�Ʒ���������

//0x400-0x1000 �Ž�����
#define MINOR_ALARMIN_SHORT_CIRCUIT                 0x400  //������·����
#define MINOR_ALARMIN_BROKEN_CIRCUIT                0x401  //������·����
#define MINOR_ALARMIN_EXCEPTION                     0x402  //�����쳣����
#define MINOR_ALARMIN_RESUME                        0x403  //���������ָ�
#define MINOR_HOST_DESMANTLE_ALARM                  0x404  //�豸���𱨾�
#define MINOR_HOST_DESMANTLE_RESUME                 0x405  //�豸����ָ�
#define MINOR_CARD_READER_DESMANTLE_ALARM           0x406  //���������𱨾�
#define MINOR_CARD_READER_DESMANTLE_RESUME          0x407  //����������ָ�
#define MINOR_CASE_SENSOR_ALARM                     0x408  //�¼����뱨��
#define MINOR_CASE_SENSOR_RESUME                    0x409  //�¼�����ָ�
#define MINOR_STRESS_ALARM                          0x40a  //в�ȱ���
#define MINOR_OFFLINE_ECENT_NEARLY_FULL             0x40b  //�����¼���90%����
#define MINOR_CARD_MAX_AUTHENTICATE_FAIL            0x40c  //������֤ʧ�ܳ��α���
#define MINOR_SD_CARD_FULL                          0x40d  //SD���洢������
#define MINOR_LINKAGE_CAPTURE_PIC                   0x40e  //����ץ���¼�����
#define MINOR_SECURITY_MODULE_DESMANTLE_ALARM        0x40f  //�ſذ�ȫģ����𱨾�
#define MINOR_SECURITY_MODULE_DESMANTLE_RESUME        0x410  //�ſذ�ȫģ�����ָ�

#define MINOR_POS_START_ALARM                       0x411  //POS����
#define MINOR_POS_END_ALARM                         0x412  //POS����
#define MINOR_FACE_IMAGE_QUALITY_LOW                0x413  //����ͼ���ʵ�
#define MINOR_FINGE_RPRINT_QUALITY_LOW              0x414  //ָ��ͼ���ʵ�
#define MINOR_FIRE_IMPORT_SHORT_CIRCUIT             0x415  //���������·����
#define MINOR_FIRE_IMPORT_BROKEN_CIRCUIT            0x416  //���������·����
#define MINOR_FIRE_IMPORT_RESUME                    0x417  //��������ָ�
#define MINOR_FIRE_BUTTON_TRIGGER                   0x418  //������ť����
#define MINOR_FIRE_BUTTON_RESUME                    0x419  //������ť�ָ�
#define MINOR_MAINTENANCE_BUTTON_TRIGGER            0x41a  //ά����ť����
#define MINOR_MAINTENANCE_BUTTON_RESUME             0x41b  //ά����ť�ָ�
#define MINOR_EMERGENCY_BUTTON_TRIGGER              0x41c  //������ť����
#define MINOR_EMERGENCY_BUTTON_RESUME               0x41d  //������ť�ָ�
#define MINOR_DISTRACT_CONTROLLER_ALARM             0x41e  //�ֿ������𱨾�
#define MINOR_DISTRACT_CONTROLLER_RESUME            0x41f  //�ֿ������𱨾��ָ�

#define MINOR_PERSON_DENSITY_DETECTION_START        0x420  //��Ա�ܶȳ���ֵ������ʼ
#define MINOR_PERSON_DENSITY_DETECTION_END          0x421  //��Ա�ܶȳ���ֵ��������

#define MINOR_CHANNEL_CONTROLLER_DESMANTLE_ALARM    0x422  //ͨ�����������𱨾�
#define MINOR_CHANNEL_CONTROLLER_DESMANTLE_RESUME   0x423  //ͨ�����������𱨾��ָ�
#define MINOR_CHANNEL_CONTROLLER_FIRE_IMPORT_ALARM  0x424  //ͨ���������������뱨��
#define MINOR_CHANNEL_CONTROLLER_FIRE_IMPORT_RESUME 0x425  //ͨ���������������뱨���ָ�

#define MINOR_HEART_RATE_ABNORMAL_BEGIN             0x426    //�����쳣������ʼ
#define MINOR_HEART_RATE_ABNORMAL_END               0x427    //�����쳣��������
#define MINOR_BLOOD_OXYGEN_ABNORMAL_BEGIN           0x428    //Ѫ���쳣������ʼ
#define MINOR_BLOOD_OXYGEN_ABNORMAL_END             0x429    //Ѫ���쳣��������
#define MINOR_SYSTOLIC_BLOOD_PRESSURE_ABNORMAL_BEGIN   0x42a    //Ѫѹ����ѹ�쳣������ʼ
#define MINOR_SYSTOLIC_BLOOD_PRESSURE_ABNORMAL_END     0x42b    //Ѫѹ����ѹ�쳣��������
#define MINOR_DIASTOLIC_BLOOD_PRESSURE_ABNORMAL_BEGIN  0x42c    //Ѫѹ����ѹ�쳣������ʼ
#define MINOR_DIASTOLIC_BLOOD_PRESSURE_ABNORMAL_END    0x42d    //Ѫѹ����ѹ�쳣��������
#define MINOR_VCA_LEAVE_POSITION_START              0x42e//��ڼ�⿪ʼ
#define MINOR_VCA_LEAVE_POSITION_STOP               0x42f//��ڼ�����
#define MINOR_VCA_STOOODUP_START                    0x430//������⿪ʼ
#define MINOR_VCA_STOOODUP_STOP                     0x431//����������
#define MINOR_VCA_PEOPLENUM_CHANGE_START            0x434//�����仯��ʼ
#define MINOR_VCA_PEOPLENUM_CHANGE_STOP             0x435//�����仯����
#define MINOR_VCA_RUNNING_START                     0x438//��Ա���ܿ�ʼ
#define MINOR_VCA_RUNNING_STOP                      0x439//��Ա���ܽ���
#define MINOR_VCA_VIOLENT_MOTION_START              0x43a//�����˶���ʼ
#define MINOR_VCA_VIOLENT_MOTION_STOP               0x43b//�����˶�����
#define MINOR_VCA_FAIL_DOWN_START                   0x43c//��Ա���ؿ�ʼ
#define MINOR_VCA_FAIL_DOWN_STOP                    0x43d//��Ա���ؽ���
#define MINOR_VCA_RETENTION_START                   0x43e//��Ա������ʼ
#define MINOR_VCA_RETENTION_STOP                    0x43f//��Ա��������

#define MINOR_PRINTER_OUT_OF_PAPER               0x440   //��ӡ��ȱֽ����
#define MINOR_LEGAL_EVENT_NEARLY_FULL            0x442   //���ߺϷ��¼���90%����

#define MINOR_ALARM_CUSTOM1                         0x900  //�Ž��Զ��屨��1
#define MINOR_ALARM_CUSTOM2                         0x901  //�Ž��Զ��屨��2
#define MINOR_ALARM_CUSTOM3                         0x902  //�Ž��Զ��屨��3
#define MINOR_ALARM_CUSTOM4                         0x903  //�Ž��Զ��屨��4
#define MINOR_ALARM_CUSTOM5                         0x904  //�Ž��Զ��屨��5
#define MINOR_ALARM_CUSTOM6                         0x905  //�Ž��Զ��屨��6
#define MINOR_ALARM_CUSTOM7                         0x906  //�Ž��Զ��屨��7
#define MINOR_ALARM_CUSTOM8                         0x907  //�Ž��Զ��屨��8
#define MINOR_ALARM_CUSTOM9                         0x908  //�Ž��Զ��屨��9
#define MINOR_ALARM_CUSTOM10                        0x909  //�Ž��Զ��屨��10
#define MINOR_ALARM_CUSTOM11                        0x90a  //�Ž��Զ��屨��11
#define MINOR_ALARM_CUSTOM12                        0x90b  //�Ž��Զ��屨��12
#define MINOR_ALARM_CUSTOM13                        0x90c  //�Ž��Զ��屨��13
#define MINOR_ALARM_CUSTOM14                        0x90d  //�Ž��Զ��屨��14
#define MINOR_ALARM_CUSTOM15                        0x90e  //�Ž��Զ��屨��15
#define MINOR_ALARM_CUSTOM16                        0x90f  //�Ž��Զ��屨��16
#define MINOR_ALARM_CUSTOM17                        0x910  //�Ž��Զ��屨��17
#define MINOR_ALARM_CUSTOM18                        0x911  //�Ž��Զ��屨��18
#define MINOR_ALARM_CUSTOM19                        0x912  //�Ž��Զ��屨��19
#define MINOR_ALARM_CUSTOM20                        0x913  //�Ž��Զ��屨��20
#define MINOR_ALARM_CUSTOM21                        0x914  //�Ž��Զ��屨��21
#define MINOR_ALARM_CUSTOM22                        0x915  //�Ž��Զ��屨��22
#define MINOR_ALARM_CUSTOM23                        0x916  //�Ž��Զ��屨��23
#define MINOR_ALARM_CUSTOM24                        0x917  //�Ž��Զ��屨��24
#define MINOR_ALARM_CUSTOM25                        0x918  //�Ž��Զ��屨��25
#define MINOR_ALARM_CUSTOM26                        0x919  //�Ž��Զ��屨��26
#define MINOR_ALARM_CUSTOM27                        0x91a  //�Ž��Զ��屨��27
#define MINOR_ALARM_CUSTOM28                        0x91b  //�Ž��Զ��屨��28
#define MINOR_ALARM_CUSTOM29                        0x91c  //�Ž��Զ��屨��29
#define MINOR_ALARM_CUSTOM30                        0x91d  //�Ž��Զ��屨��30
#define MINOR_ALARM_CUSTOM31                        0x91e  //�Ž��Զ��屨��31
#define MINOR_ALARM_CUSTOM32                        0x91f  //�Ž��Զ��屨��32
#define MINOR_ALARM_CUSTOM33                        0x920  //�Ž��Զ��屨��33
#define MINOR_ALARM_CUSTOM34                        0x921  //�Ž��Զ��屨��34
#define MINOR_ALARM_CUSTOM35                        0x922  //�Ž��Զ��屨��35
#define MINOR_ALARM_CUSTOM36                        0x923  //�Ž��Զ��屨��36
#define MINOR_ALARM_CUSTOM37                        0x924  //�Ž��Զ��屨��37
#define MINOR_ALARM_CUSTOM38                        0x925  //�Ž��Զ��屨��38
#define MINOR_ALARM_CUSTOM39                        0x926  //�Ž��Զ��屨��39
#define MINOR_ALARM_CUSTOM40                        0x927  //�Ž��Զ��屨��40
#define MINOR_ALARM_CUSTOM41                        0x928  //�Ž��Զ��屨��41
#define MINOR_ALARM_CUSTOM42                        0x929  //�Ž��Զ��屨��42
#define MINOR_ALARM_CUSTOM43                        0x92a  //�Ž��Զ��屨��43
#define MINOR_ALARM_CUSTOM44                        0x92b  //�Ž��Զ��屨��44
#define MINOR_ALARM_CUSTOM45                        0x92c  //�Ž��Զ��屨��45
#define MINOR_ALARM_CUSTOM46                        0x92d  //�Ž��Զ��屨��46
#define MINOR_ALARM_CUSTOM47                        0x92e  //�Ž��Զ��屨��47
#define MINOR_ALARM_CUSTOM48                        0x92f  //�Ž��Զ��屨��48
#define MINOR_ALARM_CUSTOM49                        0x930  //�Ž��Զ��屨��49
#define MINOR_ALARM_CUSTOM50                        0x931  //�Ž��Զ��屨��50
#define MINOR_ALARM_CUSTOM51                        0x932  //�Ž��Զ��屨��51
#define MINOR_ALARM_CUSTOM52                        0x933  //�Ž��Զ��屨��52
#define MINOR_ALARM_CUSTOM53                        0x934  //�Ž��Զ��屨��53
#define MINOR_ALARM_CUSTOM54                        0x935  //�Ž��Զ��屨��54
#define MINOR_ALARM_CUSTOM55                        0x936  //�Ž��Զ��屨��55
#define MINOR_ALARM_CUSTOM56                        0x937  //�Ž��Զ��屨��56
#define MINOR_ALARM_CUSTOM57                        0x938  //�Ž��Զ��屨��57
#define MINOR_ALARM_CUSTOM58                        0x939  //�Ž��Զ��屨��58
#define MINOR_ALARM_CUSTOM59                        0x93a  //�Ž��Զ��屨��59
#define MINOR_ALARM_CUSTOM60                        0x93b  //�Ž��Զ��屨��60
#define MINOR_ALARM_CUSTOM61                        0x93c  //�Ž��Զ��屨��61
#define MINOR_ALARM_CUSTOM62                        0x93d  //�Ž��Զ��屨��62
#define MINOR_ALARM_CUSTOM63                        0x93e  //�Ž��Զ��屨��63
#define MINOR_ALARM_CUSTOM64                        0x93f  //�Ž��Զ��屨��64
#define MINOR_LOCK_HIJIACK_FINGER_ALARM         0x950       //���������ٳ�ָ�Ʊ���
#define MINOR_LOCK_HIJIACK_PASSWORD_ALARM       0x951       //���������ٳ����뱨��
#define MINOR_LOCK_PRY_DOOR_ALARM               0x952       //���������ű���
#define MINOR_LOCK_LOCKED_ALARM                 0x953       //��������������
#define MINOR_LOCK_BATTERLOW_ALARM              0x954       //�������͵�ѹ����
#define MINOR_LOCK_BLACKLIST_DOOR_ALARM         0x955       //����������Ȩ��������
#define MINOR_LOCK_OFFLINE_ALARM                0x956       //���������߱���
#define MINOR_LOCK_UNCLOSED_ALARM               0x957       //���������ڱ���
#define MINOR_LOCK_NO_HOME_ALARM                0x958       //�������û�δ�ؼұ���
#define MINOR_LOCK_MAGNETOMETER_ALARM           0x959       //�Ŵ�̽��������
#define MINOR_LOCK_IR_DETECTOR_ALARM            0x95a       //����̽��������
#define MINOR_LOCK_FP_LOCKED_ALARM              0x95b       //ָ����������
#define MINOR_LOCK_PASSWORD_LOCKED_ALARM        0x95c       //������������
#define MINOR_LOCK_HIJIACK_ALARM              0x95d  //���������ٳֱ���

//2018-04-23 ͨ���������ر�����־����
#define MINOR_ALARMHOST_SHORT_CIRCUIT    	      0x1001      //��·����
#define MINOR_ALARMHOST_BROKEN_CIRCUIT   		  0x1002       //��·����
#define MINOR_ALARMHOST_ALARM_RESET      	      0x1003       //������λ
#define MINOR_ALARMHOST_ALARM_NORMAL			  0x1004   	//�����ָ�����
#define MINOR_ALARMHOST_PASSWORD_ERROR 		  0x1005		//�����������3�������������
#define MINOR_ALARMHOST_ID_CARD_ILLEGALLY 	  0x1006		//�Ƿ���Ӧ��ID
#define MINOR_ALARMHOST_KEYPAD_REMOVE 		  0x1007	//���̷���
#define MINOR_ALARMHOST_KEYPAD_REMOVE_RESTORE   0x1008	//���̷���λ

#define MINOR_ALARMHOST_BELOW_ALARM_LIMIT1 	  0x1011	//ģ�������ڱ�����1
#define MINOR_ALARMHOST_BELOW_ALARM_LIMIT2 	  0x1012    //ģ�������ڱ�����2
#define MINOR_ALARMHOST_BELOW_ALARM_LIMIT3 	  0x1013    //ģ�������ڱ�����3
#define MINOR_ALARMHOST_BELOW_ALARM_LIMIT4 	  0x1014    //ģ�������ڱ�����4
#define MINOR_ALARMHOST_ABOVE_ALARM_LIMIT1 	  0x1015    //ģ�������ڱ�����1
#define MINOR_ALARMHOST_ABOVE_ALARM_LIMIT2 	0x1016    //ģ�������ڱ�����2
#define MINOR_ALARMHOST_ABOVE_ALARM_LIMIT3 	0x1017     //ģ�������ڱ�����3
#define MINOR_ALARMHOST_ABOVE_ALARM_LIMIT4 	0x1018     //ģ�������ڱ�����4

#define MINOR_ALARMHOST_VIRTUAL_DEFENCE_BANDIT 	0x1021      //������˾�
#define MINOR_ALARMHOST_VIRTUAL_DEFENCE_FIRE 		0x1022       //�������
#define MINOR_ALARMHOST_VIRTUAL_DEFENCE_URGENT 		0x1023       //���������

#define MINOR_UPS_ALARM			0x1028      //UPS����
#define MINOR_ELECTRICITY_METER_ALARM 0x1029     //���ܵ����
#define MINOR_SWITCH_POWER_ALARM	  0x1030     //���ص�Դ����
#define MINOR_GAS_DETECT_SYS_ALARM	  0x1031    //������ϵͳ����
#define MINOR_TRANSFORMER_TEMPRATURE_ALARM 0x1032     //��������Ա���
#define MINOR_TEMP_HUMI_ALARM		0x1033   //��ʪ�ȴ���������
#define MINOR_UPS_ALARM_RESTORE	0x1034    //UPS�����ָ�
#define MINOR_ELECTRICITY_METER_ALARM_RESTORE 0x1035   //���ܵ�����ָ�
#define MINOR_SWITCH_POWER_ALARM_RESTORE	0x1036   //���ص�Դ�����ָ�
#define MINOR_GAS_DETECT_SYS_ALARM_RESTORE	  0x1037   //������ϵͳ�����ָ�
#define MINOR_TRANSFORMER_TEMPRATURE_ALARM_RESTORE 0x1038            //��������Ա����ָ�
#define MINOR_TEMP_HUMI_ALARM_RESTORE	0x1039     //��ʪ�ȴ����������ָ�
#define MINOR_WATER_LEVEL_SENSOR_ALARM 		0x1040     //ˮλ����������
#define MINOR_WATER_LEVEL_SENSOR_ALARM_RESTORE 	0x1041  //ˮλ�����������ָ�
#define MINOR_DUST_NOISE_ALARM 	0x1042  //�ﳾ��������������
#define MINOR_DUST_NOISE_ALARM_RESTORE 	0x1043     //�ﳾ���������������ָ�
#define MINOR_ENVIRONMENTAL_LOGGER_ALARM 	0x1044    //�����ɼ��Ǳ���
#define MINOR_ENVIRONMENTAL_LOGGER_ALARM_RESTORE 0x1045            //�����ɼ��Ǳ����ָ�

#define MINOR_TRIGGER_TAMPER    0x1046     //̽��������
#define MINOR_TRIGGER_TAMPER_RESTORE     0x1047    //̽��������ָ�
#define MINOR_EMERGENCY_CALL_HELP_ALARM    0x1048       //����������������
#define MINOR_EMERGENCY_CALL_HELP_ALARM_RESTORE 0x1049            //�����������������ָ�
#define MINOR_CONSULTING_ALARM     0x1050    //ҵ����ѯ����
#define MINOR_CONSULTING_ALARM_RESTORE   0x1051     //ҵ����ѯ�����ָ�
#define MINOR_ALARMHOST_ZONE_MODULE_REMOVE      0x1052    //����ģ�����
#define MINOR_ALARMHOST_ZONE_MODULE_RESET     0x1053       //����ģ�����λ

#define MINOR_ALARMHOST_ALARM_WIND_SPEED_ALARM      0x1054      //���ٴ������澯
#define MINOR_ALARMHOST_ALARM_WIND_SPEED_ALARM_RESTORE   0x1055      //���ٴ������澯�ָ�
#define MINOR_ALARMHOST_ALARM_GENERATE_OUTPUT_ALARM   0x1056   //ͨ����չ���ģ��澯
#define MINOR_ALARMHOST_ALARM_GENERATE_OUTPUT_RESTORE    0x1057     //ͨ����չ���ģ��澯�ָ�
#define MINOR_ALARMHOST_ALARM_SOAK_ALARM      0x1058      //ˮ���������澯
#define MINOR_ALARMHOST_ALARM_SOAK_ALARM_RESTORE   0x1059  //ˮ���������澯�ָ�
#define MINOR_ALARMHOST_ALARM_SOLAR_POWER_ALARM    0x1060    //̫���ܴ������澯
#define MINOR_ALARMHOST_ALARM_SOLAR_POWER_ALARM_RESTORE 0x1061   //̫���ܴ������澯�ָ�
#define MINOR_ALARMHOST_ALARM_SF6_ALARM   0x1062      //SF6���������澯
#define MINOR_ALARMHOST_ALARM_SF6_ALARM_RESTORE   0x1063    //SF6���������澯�ָ�
#define MINOR_ALARMHOST_ALARM_WEIGHT_ALARM    0x1064      //�����Ǹ澯
#define MINOR_ALARMHOST_ALARM_WEIGHT_ALARM_RESTORE    0x1065   //�����Ǹ澯�ָ�
#define MINOR_ALARMHOST_ALARM_WEATHER_ALARM   0x1066   //����ɼ�ϵͳ�澯
#define MINOR_ALARMHOST_ALARM_WEATHER_ALARM_RESTORE    0x1067     //����ɼ�ϵͳ�澯�ָ�
#define MINOR_ALARMHOST_ALARM_FUEL_GAS_ALARM     0x1068  //ȼ�����ϵͳ�澯
#define MINOR_ALARMHOST_ALARM_FUEL_GAS_ALARM_RESTORE  0x1069 //ȼ�����ϵͳ�澯��
#define MINOR_ALARMHOST_ALARM_FIRE_ALARM   0x1070  //���ֱ���ϵͳ�澯
#define MINOR_ALARMHOST_ALARM_FIRE_ALARM_RESTORE    0x1071   //���ֱ���ϵͳ�澯�ָ�
#define  MINOR_ALARMHOST_WIRELESS_OUTPUT_MODULE_REMOVE     0x1072   //�������ģ�����
#define  MINOR_ALARMHOST_WIRELESS_OUTPUT_MODULE_RESET    0x1073  //�������ģ�����λ
#define  MINOR_ALARMHOST_WIRELESS_REPEATER_MODULE_REMOVE   0x1074  //�����м�������
#define  MINOR_ALARMHOST_WIRELESS_REPEATER_MODULE_RESET  0x1074 //�����м�������λ
#define  MINOR_ALARMHOST_WIRELESS_SIREN_MODULE_REMOVE     0x1075  //���߾��ŷ���
#define  MINOR_ALARMHOST_WIRELESS_SIREN_MODULE_RESET     0x1076 //���߾��ŷ���λ

#define  MINOR_RS485_DEV_ALARM     0x1077      //RS485����豸����������豸����δ֪���豸��
#define  MINOR_RS485_DEV_RESTORE    0x1078     //RS485����豸�����ָ�������豸����δ֪���豸��
#define  MINOR_ALARMHOST_ALARM_HOST_ALARM     0x1079      //������������
#define  MINOR_ALARMHOST_ALARM_HOST_RESTORE    0x107a     //�������������ָ�

#define  MINOR_AIR_CONDITION_DEV_ALARM     0x107b      //�յ�����������
#define  MINOR_AIR_CONDITION_DEV_RESTORE    0x107c     //�յ������������ָ�


/* �쳣 */
//������
#define MAJOR_EXCEPTION                    0x2
//������
#define MINOR_SUBSYSTEM_ERROR           0x0a    /* ��ϵͳ�쳣 */
#define MINOR_RAID_ERROR                0x20    /* �����쳣 */
#define MINOR_VI_LOST                    0x21    /* ��Ƶ�źŶ�ʧ */
#define MINOR_ILLEGAL_ACCESS            0x22    /* �Ƿ����� */
#define MINOR_HD_FULL                    0x23    /* Ӳ���� */
#define MINOR_HD_ERROR                    0x24    /* Ӳ�̴��� */
#define MINOR_DCD_LOST                    0x25    /* MODEM ����(������ʹ��) */
#define MINOR_IP_CONFLICT                0x26    /* IP��ַ��ͻ */
#define MINOR_NET_BROKEN                0x27    /* ����Ͽ�*/
#define MINOR_REC_ERROR                 0x28    /* ¼����� */
#define MINOR_IPC_NO_LINK               0x29    /* IPC�����쳣 */
#define MINOR_VI_EXCEPTION              0x2a    /* ��Ƶ�����쳣(ֻ���ģ��ͨ��) */
#define MINOR_IPC_IP_CONFLICT           0x2b    /*ipc ip ��ַ ��ͻ*/
#define MINOR_SENCE_EXCEPTION           0x2c    // �����쳣

#define MINOR_PIC_REC_ERROR              0x2d    /* ץͼ����--��ȡͼƬ�ļ�ʧ��*/
#define MINOR_VI_MISMATCH                0x2e    /* ��Ƶ��ʽ��ƥ��*/
#define MINOR_RESOLUTION_MISMATCH        0x2f    /*ǰ��/¼��ֱ��ʲ�ƥ��  */

//2009-12-16 ������Ƶ�ۺ�ƽ̨��־����
#define MINOR_FANABNORMAL                0x31    /* ��Ƶ�ۺ�ƽ̨������״̬�쳣 */
#define MINOR_FANRESUME                    0x32    /* ��Ƶ�ۺ�ƽ̨������״̬�ָ����� */
#define MINOR_SUBSYSTEM_ABNORMALREBOOT    0x33    /* ��Ƶ�ۺ�ƽ̨��6467�쳣���� */
#define MINOR_MATRIX_STARTBUZZER        0x34    /* ��Ƶ�ۺ�ƽ̨��dm6467�쳣������������ */

//2010-01-22 ������Ƶ�ۺ�ƽ̨�쳣��־������
#define MINOR_NET_ABNORMAL                0x35    /*����״̬�쳣*/
#define MINOR_MEM_ABNORMAL                0x36    /*�ڴ�״̬�쳣*/
#define MINOR_FILE_ABNORMAL                0x37    /*�ļ�״̬�쳣*/
#define MINOR_PANEL_ABNORMAL            0x38 /*ǰ��������쳣*/
#define MINOR_PANEL_RESUME                0x39 /*ǰ���ָ�����*/    
#define MINOR_RS485_DEVICE_ABNORMAL        0x3a     /*RS485����״̬�쳣*/
#define MINOR_RS485_DEVICE_REVERT        0x3b    /*RS485����״̬�쳣�ָ�*/

//2012-2-18 ���Ӵ����������쳣��־������
#define MINOR_SCREEN_SUBSYSTEM_ABNORMALREBOOT   0x3c            //�Ӱ��쳣����
#define    MINOR_SCREEN_SUBSYSTEM_ABNORMALINSERT    0x3d            //�Ӱ����
#define    MINOR_SCREEN_SUBSYSTEM_ABNORMALPULLOUT    0x3e            //�Ӱ�γ�
#define    MINOR_SCREEN_ABNARMALTEMPERATURE        0x3f            //�¶��쳣
//2012-07-26 ��Ƶ�ۺ�ƽ̨v2.1
#define MINOR_HIGH_TEMPERATURE_PROTECT          0x40 //�Ӱ���ȱ���

//Netra 2.2.2
#define MINOR_RECORD_OVERFLOW                   0x41              /*���������*/  
#define MINOR_DSP_ABNORMAL                      0x42              //DSP�쳣 

//Netra 3.0.0
#define MINOR_ANR_RECORD_FAIED                 0x43         /*ANR¼��ʧ��*/ 
#define MINOR_SPARE_WORK_DEVICE_EXCEPT         0x44            /*�ȱ��豸�������쳣*/
#define MINOR_START_IPC_MAS_FAILED             0x45            /*����IPC MASʧ��*/ 
//������ 256·NVR
#define MINOR_IPCM_CRASH                       0x46         /*IPCM�쳣����*/
#define MINOR_POE_POWER_EXCEPTION              0x47         /*POE �����쳣*/
#define MINOR_UPLOAD_DATA_CS_EXCEPTION         0x48          //�ƴ洢�����ϴ�ʧ��/
#define MINOR_DIAL_EXCEPTION                   0x49         /*�����쳣*/
#define MINOR_DEV_EXCEPTION_OFFLINE            0x50  //�豸�쳣����
#define MINOR_UPGRADEFAIL                      0x51 //Զ�������豸ʧ��
#define MINOR_AI_LOST                           0x52    /* ��Ƶ�źŶ�ʧ */
#define MINOR_SYNC_IPC_PASSWD                   0x53    /* ͬ��IPC�����쳣 */
#define MINOR_EZVIZ_OFFLINE                        0x54    /* өʯ�����쳣*/
#define MINOR_VQD_ABNORMAL                        0x55 //VQD�쳣
#define MINOR_ACCESSORIES_PLATE                0x57 //������쳣
#define MINOR_KMS_EXPAMSION_DISK_LOST         0x58 // KMS�����̶�ʧ
#define MINOR_ABNORMAL_PORT                 0x59 // �˿��쳣
#define MINOR_CAMERA_ANGLE_ANOMALY          0x60//  ����ӽ��쳣
#define MINOR_DATA_DISK_ERROE               0x61//  �����̴���  
#define MINOR_INTELLIGENT_SYSTEM_RUNNING_ERROR     0x62//  ����ϵͳ�����쳣
#define MINOR_FACESNAP_RESOLUTION_OVERFLOW         0x63//  ����ץ�������ֱ��ʳ���
#define MINOR_SMD_RESOLUTION_OVERFLOW              0x64//  SMD�����ֱ��ʳ���
#define MINOR_AUDIO_LOSS_EXCEPTION                 0x65//  ��Ƶ��ʧ�쳣
#define MINOR_SAFETY_HELMET_EXCEPTION              0x66 //δ�����ȫñ����쳣
#define MINOR_VCA_PIC_LENGTH_OVERFLOW              0x67 // VCAͼƬ���ȹ��������糬��2M��С��ͼƬ��
#define MINOR_FACE_MODEL_EXCEPTION                 0x68//  ������ģ��ͬ���쳣
//NVR��Ⱥ
#define MINOR_CLUSTER_DEVICE_OFFLINE                 0x70 // ��Ⱥ���豸����
#define MINOR_CLUSTER_CONFIG_FAILED                  0x71 // ��Ⱥ���豸����ʧ��
#define MINOR_CLUSTER_DISASTER_TOLERANCE_EXCEPT      0x72 // ��Ⱥ�����쳣:��ȺCMѡ��ʧ��,��Ⱥ�洢���ڲ���,��Ⱥ������,��Ⱥͨ����Դ����,��Ⱥ�豸�����
#define MINOR_CLUSTER_STORFULL_EXCEPTION             0x73 //��ȺӲ����
#define MINOR_CLUSTER_VERSION_EXCEPTION              0x74  //��Ⱥ�汾�쳣
#define MINOR_CLUSTER_OFFLINENODE_EXCEPTION          0x75   //��Ⱥ����������
#define MINOR_CLUSTER_RECORDCYCLE_EXCEPTION          0x76   //��Ⱥ¼�����ڲ���
#define MINOR_CLUSTER_IPCTRANSFER_EXCEPTION          0x77   //��ȺIPCǨ��ʧ��
#define MINOR_CLUSTER_IPCONFLICT_EXCEPTION           0x78   // ��ȺIP��ͻ����¼CM��IP��ַ

#define MINOR_GET_SUB_STREAM_FAILURE                 0x79//������ȡ��ʧ��
#define MINOR_HDD_SHM_DETECT_EXCEPTION               0x7a//Ӳ��SHM����쳣
#define MINOR_DEVICE_FORTIFY_FAILURE                 0x7b//ǰ���豸��������ʧ��
#define MINOR_EVENT_UPLOAD_EXCEPTION                 0x7c //�¼������쳣���豸�ϴ��¼�ʧ�ܻ��߶����ˣ�

//0x400-0x1000 �Ž��쳣����
#define MINOR_DEV_POWER_ON                     0x400  //�豸�ϵ�����
#define MINOR_DEV_POWER_OFF                    0x401  //�豸����ر�
#define MINOR_WATCH_DOG_RESET                  0x402  //���Ź���λ
#define MINOR_LOW_BATTERY                      0x403  //���ص�ѹ��
#define MINOR_BATTERY_RESUME                   0x404  //���ص�ѹ�ָ�����
#define MINOR_AC_OFF                           0x405  //������ϵ�
#define MINOR_AC_RESUME                        0x406  //������ָ�
#define MINOR_NET_RESUME                       0x407  //����ָ�
#define MINOR_FLASH_ABNORMAL                   0x408  //FLASH��д�쳣
#define MINOR_CARD_READER_OFFLINE              0x409  //����������
#define MINOR_CARD_READER_RESUME               0x40a  //���������߻ָ�                   
#define MINOR_INDICATOR_LIGHT_OFF              0x40b  //ָʾ�ƹر�
#define MINOR_INDICATOR_LIGHT_RESUME           0x40c  //ָʾ�ƻָ�
#define MINOR_CHANNEL_CONTROLLER_OFF           0x40d  //ͨ������������
#define MINOR_CHANNEL_CONTROLLER_RESUME        0x40e  //ͨ���������ָ�
#define MINOR_SECURITY_MODULE_OFF               0x40f  //�ſذ�ȫģ�����
#define MINOR_SECURITY_MODULE_RESUME           0x410  //�ſذ�ȫģ������
#define MINOR_BATTERY_ELECTRIC_LOW             0x411  //��ص�ѹ��(�������豸ʹ��)
#define MINOR_BATTERY_ELECTRIC_RESUME          0x412  //��ص�ѹ�ָ�����(�������豸ʹ��)
#define MINOR_LOCAL_CONTROL_NET_BROKEN           0x413  //�͵ؿ���������Ͽ�
#define MINOR_LOCAL_CONTROL_NET_RSUME           0x414  //�͵ؿ���������ָ�
#define MINOR_MASTER_RS485_LOOPNODE_BROKEN     0x415  //����RS485��·�ڵ�Ͽ�
#define MINOR_MASTER_RS485_LOOPNODE_RESUME     0x416  //����RS485��·�ڵ�ָ�
#define MINOR_LOCAL_CONTROL_OFFLINE            0x417  //�͵ؿ���������
#define MINOR_LOCAL_CONTROL_RESUME             0x418  //�͵ؿ��������߻ָ�
#define MINOR_LOCAL_DOWNSIDE_RS485_LOOPNODE_BROKEN  0x419  //�͵�����RS485��·�Ͽ�
#define MINOR_LOCAL_DOWNSIDE_RS485_LOOPNODE_RESUME  0x41a  //�͵�����RS485��·�ָ�
#define MINOR_DISTRACT_CONTROLLER_ONLINE       0x41b  //�ֿ�������
#define MINOR_DISTRACT_CONTROLLER_OFFLINE      0x41c  //�ֿ�������
#define MINOR_ID_CARD_READER_NOT_CONNECT       0x41d  //���֤�Ķ���δ���ӣ�����ר�ã�
#define MINOR_ID_CARD_READER_RESUME            0x41e  //���֤�Ķ������ӻָ�������ר�ã�
#define MINOR_FINGER_PRINT_MODULE_NOT_CONNECT  0x41f  //ָ��ģ��δ���ӣ�����ר�ã�
#define MINOR_FINGER_PRINT_MODULE_RESUME       0x420  //ָ��ģ�����ӻָ�������ר�ã�
#define MINOR_CAMERA_NOT_CONNECT               0x421  //����ͷδ����
#define MINOR_CAMERA_RESUME                    0x422  //����ͷ���ӻָ�
#define MINOR_COM_NOT_CONNECT                  0x423  //COM��δ����
#define MINOR_COM_RESUME                       0x424  //COM�����ӻָ�
#define MINOR_DEVICE_NOT_AUTHORIZE             0x425  //�豸δ��Ȩ
#define MINOR_PEOPLE_AND_ID_CARD_DEVICE_ONLINE 0x426  //��֤�豸����
#define MINOR_PEOPLE_AND_ID_CARD_DEVICE_OFFLINE 0x427 //��֤�豸����
#define MINOR_LOCAL_LOGIN_LOCK                 0x428  //���ص�¼����
#define MINOR_LOCAL_LOGIN_UNLOCK               0x429  //���ص�¼����
#define MINOR_SUBMARINEBACK_COMM_BREAK         0x42a  //�뷴Ǳ�ط�����ͨ�ŶϿ�
#define MINOR_SUBMARINEBACK_COMM_RESUME        0x42b  //�뷴Ǳ�ط�����ͨ�Żָ�
#define MINOR_MOTOR_SENSOR_EXCEPTION           0x42c  //����򴫸����쳣
#define MINOR_CAN_BUS_EXCEPTION                0x42d  //CAN�����쳣
#define MINOR_CAN_BUS_RESUME                   0x42e  //CAN���߻ָ�
#define MINOR_GATE_TEMPERATURE_OVERRUN         0x42f  //բ��ǻ���¶ȳ���
#define MINOR_IR_EMITTER_EXCEPTION             0x430  //��������쳣
#define MINOR_IR_EMITTER_RESUME                0x431  //�������ָ�
#define MINOR_LAMP_BOARD_COMM_EXCEPTION        0x432  //�ư�ͨ���쳣
#define MINOR_LAMP_BOARD_COMM_RESUME           0x433  //�ư�ͨ�Żָ�
#define MINOR_IR_ADAPTOR_COMM_EXCEPTION        0x434  //����ת�Ӱ�ͨ���쳣
#define MINOR_IR_ADAPTOR_COMM_RESUME           0x435  //����ת�Ӱ�ͨ�Żָ�
#define MINOR_PRINTER_ONLINE                    0x436  //��ӡ������
#define MINOR_PRINTER_OFFLINE                    0x437  //��ӡ������
#define MINOR_4G_MOUDLE_ONLINE       0x438  //4Gģ������
#define MINOR_4G_MOUDLE_OFFLINE         0x439  //4Gģ������

#define MINOR_EXCEPTION_CUSTOM1                 0x900  //�Ž��Զ����쳣1
#define MINOR_EXCEPTION_CUSTOM2                 0x901  //�Ž��Զ����쳣2
#define MINOR_EXCEPTION_CUSTOM3                 0x902  //�Ž��Զ����쳣3
#define MINOR_EXCEPTION_CUSTOM4                 0x903  //�Ž��Զ����쳣4
#define MINOR_EXCEPTION_CUSTOM5                 0x904  //�Ž��Զ����쳣5
#define MINOR_EXCEPTION_CUSTOM6                 0x905  //�Ž��Զ����쳣6
#define MINOR_EXCEPTION_CUSTOM7                 0x906  //�Ž��Զ����쳣7
#define MINOR_EXCEPTION_CUSTOM8                 0x907  //�Ž��Զ����쳣8
#define MINOR_EXCEPTION_CUSTOM9                 0x908  //�Ž��Զ����쳣9
#define MINOR_EXCEPTION_CUSTOM10                0x909  //�Ž��Զ����쳣10
#define MINOR_EXCEPTION_CUSTOM11                0x90a  //�Ž��Զ����쳣11
#define MINOR_EXCEPTION_CUSTOM12                0x90b  //�Ž��Զ����쳣12
#define MINOR_EXCEPTION_CUSTOM13                0x90c  //�Ž��Զ����쳣13
#define MINOR_EXCEPTION_CUSTOM14                0x90d  //�Ž��Զ����쳣14
#define MINOR_EXCEPTION_CUSTOM15                0x90e  //�Ž��Զ����쳣15
#define MINOR_EXCEPTION_CUSTOM16                0x90f  //�Ž��Զ����쳣16
#define MINOR_EXCEPTION_CUSTOM17                0x910  //�Ž��Զ����쳣17
#define MINOR_EXCEPTION_CUSTOM18                0x911  //�Ž��Զ����쳣18
#define MINOR_EXCEPTION_CUSTOM19                0x912  //�Ž��Զ����쳣19
#define MINOR_EXCEPTION_CUSTOM20                0x913  //�Ž��Զ����쳣20
#define MINOR_EXCEPTION_CUSTOM21                0x914  //�Ž��Զ����쳣21
#define MINOR_EXCEPTION_CUSTOM22                0x915  //�Ž��Զ����쳣22
#define MINOR_EXCEPTION_CUSTOM23                0x916  //�Ž��Զ����쳣23
#define MINOR_EXCEPTION_CUSTOM24                0x917  //�Ž��Զ����쳣24
#define MINOR_EXCEPTION_CUSTOM25                0x918  //�Ž��Զ����쳣25
#define MINOR_EXCEPTION_CUSTOM26                0x919  //�Ž��Զ����쳣26
#define MINOR_EXCEPTION_CUSTOM27                0x91a  //�Ž��Զ����쳣27
#define MINOR_EXCEPTION_CUSTOM28                0x91b  //�Ž��Զ����쳣28
#define MINOR_EXCEPTION_CUSTOM29                0x91c  //�Ž��Զ����쳣29
#define MINOR_EXCEPTION_CUSTOM30                0x91d  //�Ž��Զ����쳣30
#define MINOR_EXCEPTION_CUSTOM31                0x91e  //�Ž��Զ����쳣31
#define MINOR_EXCEPTION_CUSTOM32                0x91f  //�Ž��Զ����쳣32
#define MINOR_EXCEPTION_CUSTOM33                0x920  //�Ž��Զ����쳣33
#define MINOR_EXCEPTION_CUSTOM34                0x921  //�Ž��Զ����쳣34
#define MINOR_EXCEPTION_CUSTOM35                0x922  //�Ž��Զ����쳣35
#define MINOR_EXCEPTION_CUSTOM36                0x923  //�Ž��Զ����쳣36
#define MINOR_EXCEPTION_CUSTOM37                0x924  //�Ž��Զ����쳣37
#define MINOR_EXCEPTION_CUSTOM38                0x925  //�Ž��Զ����쳣38
#define MINOR_EXCEPTION_CUSTOM39                0x926  //�Ž��Զ����쳣39
#define MINOR_EXCEPTION_CUSTOM40                0x927  //�Ž��Զ����쳣40
#define MINOR_EXCEPTION_CUSTOM41                0x928  //�Ž��Զ����쳣41
#define MINOR_EXCEPTION_CUSTOM42                0x929  //�Ž��Զ����쳣42
#define MINOR_EXCEPTION_CUSTOM43                0x92a  //�Ž��Զ����쳣43
#define MINOR_EXCEPTION_CUSTOM44                0x92b  //�Ž��Զ����쳣44
#define MINOR_EXCEPTION_CUSTOM45                0x92c  //�Ž��Զ����쳣45
#define MINOR_EXCEPTION_CUSTOM46                0x92d  //�Ž��Զ����쳣46
#define MINOR_EXCEPTION_CUSTOM47                0x92e  //�Ž��Զ����쳣47
#define MINOR_EXCEPTION_CUSTOM48                0x92f  //�Ž��Զ����쳣48
#define MINOR_EXCEPTION_CUSTOM49                0x930  //�Ž��Զ����쳣49
#define MINOR_EXCEPTION_CUSTOM50                0x931  //�Ž��Զ����쳣50
#define MINOR_EXCEPTION_CUSTOM51                0x932  //�Ž��Զ����쳣51
#define MINOR_EXCEPTION_CUSTOM52                0x933  //�Ž��Զ����쳣52
#define MINOR_EXCEPTION_CUSTOM53                0x934  //�Ž��Զ����쳣53
#define MINOR_EXCEPTION_CUSTOM54                0x935  //�Ž��Զ����쳣54
#define MINOR_EXCEPTION_CUSTOM55                0x936  //�Ž��Զ����쳣55
#define MINOR_EXCEPTION_CUSTOM56                0x937  //�Ž��Զ����쳣56
#define MINOR_EXCEPTION_CUSTOM57                0x938  //�Ž��Զ����쳣57
#define MINOR_EXCEPTION_CUSTOM58                0x939  //�Ž��Զ����쳣58
#define MINOR_EXCEPTION_CUSTOM59                0x93a  //�Ž��Զ����쳣59
#define MINOR_EXCEPTION_CUSTOM60                0x93b  //�Ž��Զ����쳣60
#define MINOR_EXCEPTION_CUSTOM61                0x93c  //�Ž��Զ����쳣61
#define MINOR_EXCEPTION_CUSTOM62                0x93d  //�Ž��Զ����쳣62
#define MINOR_EXCEPTION_CUSTOM63                0x93e  //�Ž��Զ����쳣63
#define MINOR_EXCEPTION_CUSTOM64                0x93f  //�Ž��Զ����쳣64
#define MINOR_SWITCH_WIRED_NETWORK            0x950    //�л���������
#define MINOR_SWITCH_WIRELESS_NETWORK         0x951    //�л���������
#define MINOR_LOCK_ONLINE_RESUME              0x952    //�������ָ�����

//[add]by silujie 2013-3-22 14:16
//0x2000~0x3fff Ϊ�豸������־
//0x4000~0x5000 Ϊ�豸�쳣��־
#define MINOR_SUBSYSTEM_IP_CONFLICT        0x4000        //�Ӱ�IP��ͻ
#define MINOR_SUBSYSTEM_NET_BROKEN        0x4001        //�Ӱ����
#define    MINOR_FAN_ABNORMAL                0x4002        //�����쳣
#define    MINOR_BACKPANEL_TEMPERATURE_ABNORMAL        0x4003        //�����¶��쳣

#define    MINOR_SDCARD_ABNORMAL            0x4004        //SD��������
#define    MINOR_SDCARD_DAMAGE                0x4005        //SD����
#define    MINOR_POC_ABNORMAL				0x4006   //�豸POCģ���쳣
#define MINOR_MAIN_POWER_FAULT              0x4007 //�������
#define MINOR_BACK_UP_POWER_FAULT           0x4008 //�������
#define MINOR_TAMPER_FAULT                  0x4009 //�������
#define MINOR_RS232_FAULT                   0x400a //232���߹���
#define MINOR_RS485_FAULT                   0x400b //485���߹���
#define MINOR_LAN_STATUS_FAULT              0x400c //LAN���߽���״̬����
#define MINOR_LAN_LINK1_FAULT               0x400d //LAN��·1����
#define MINOR_LAN_LINK2_FAULT               0x400e //LAN��·2����
#define MINOR_SIM_CARD_STATUS_FAULT         0x400f //4G-SIM��״̬����
#define MINOR_4G_LINK1_FAULT                0x4010 //4G��·1����
#define MINOR_4G_LINK2_FAULT                0x4011 //4G��·2����
#define MINOR_OTHER_FAULT                   0x4012 //��������

//2018-04-23 ͨ�����������쳣��־����
#define MINOR_ALARMHOST_WDT_RESET 			    0x1003    //WDT ��λ
#define MINOR_ALARMHOST_RTC_EXCEPTION 			0x1007    //RTCʵʱʱ���쳣

#define MINOR_ALARMHOST_TEL_LINE_CONNECT_FAILURE 	0x100a    //�绰�����Ӷ�
#define MINOR_ALARMHOST_TEL_LINE_CONNECT_RESTORE 	0x100b    //�绰�����ӻָ�
#define MINOR_ALARMHOST_EXPANDER_BUS_LOSS 		0x100c    //��չ����ģ�����
#define MINOR_ALARMHOST_EXPANDER_BUS_RESTORE 	0x100d    //��չ����ģ����߻ָ�
#define MINOR_ALARMHOST_KEYPAD_BUS_LOSS 			0x100e    //��������ģ�����
#define MINOR_ALARMHOST_KEYPAD_BUS_RESTORE 		0x100f    //��������ģ����߻ָ�
#define MINOR_ALARMHOST_SENSOR_FAILURE 			0x1010    //ģ��������������
#define MINOR_ALARMHOST_SENSOR_RESTORE 			0x1011    //ģ�����������ָ�
#define MINOR_ALARMHOST_RS485_CONNECT_FAILURE 	0x1012    //RS485ͨ�����Ӷ�
#define MINOR_ALARMHOST_RS485_CONNECT_RESTORE 	0x1013    //RS485ͨ�����Ӷϻָ�

//�����������쳣���͡����������쳣�ָ�����������־�����������Ӷϡ����������ӻָ�����������־ʱһ���ģ���û���豸֧�֡����������쳣���͡����������쳣�ָ������������͡�
#define	MINOR_ALARMHOST_WIRED_NETWORK_ABNORMAL 	0x1015    //���������쳣
#define	MINOR_ALARMHOST_WIRED_NETWORK_RESTORE 		0x1016    //��������ָ�����
#define	MINOR_ALARMHOST_GPRS_ABNORMAL 				0x1017    //GPRSͨ���쳣
#define	MINOR_ALARMHOST_GPRS_RESTORE 					0x1018    //GPRS�ָ�����
#define	MINOR_ALARMHOST_3G_ABNORMAL 					0x1019    //3Gͨ���쳣
#define	MINOR_ALARMHOST_3G_RESTORE 					0x101a    //3G�ָ�����
#define	MINOR_ALARMHOST_SIM_CARD_ABNORMAL 			0x101b    //SIM���쳣
#define	MINOR_ALARMHOST_SIM_CARD_RESTORE 			0x101c    //SIM���ָ�����

#define	MINOR_FORMAT_HDD_ERROR  0x1026 //Զ�̸�ʽ��Ӳ��ʧ��
#define	MINOR_USB_ERROR		0x1027    //USBͨ�Ź���
#define	MINOR_USB_RESTORE		0x1028    //USBͨ�Ź��ϻָ�
#define	MINOR_PRINT_ERROR	  0x1029    //��ӡ������
#define	MINOR_PRINT_RESTORE   0x1030    //��ӡ�����ϻָ�
#define  MINOR_ALARMHOST_SUBSYSTEM_COMMUNICATION_ERROR    0x1031  //�Ӱ�ͨѶ����

#define  MINOR_MCU_RESTART    0x1035    //MCU����
#define  MINOR_GPRS_MODULE_FAULT    0x1036  //GPRSģ�����
#define  MINOR_TELEPHONE_MODULE_FAULT    0x1037     //�绰ģ�����
#define  MINOR_WIFI_ABNORMAL  0x1038    //WIFIͨ���쳣
#define  MINOR_WIFI_RESTORE    0x1039    //WIFI�ָ�����
#define  MINOR_RF_ABNORMAL   0x103a    //RF�ź��쳣
#define  MINOR_RF_RESTORE     0x103b   //RF�źŻָ�����
#define  MINOR_DETECTOR_ONLINE   0x103c    //̽��������
#define  MINOR_DETECTOR_OFFLINE    0x103d    //̽��������
#define  MINOR_DETECTOR_BATTERY_NORMAL    0x103e  //̽������������        
#define  MINOR_DETECTOR_BATTERY_LOW 0x103f  //̽��������Ƿѹ
#define  MINOR_DATA_TRAFFIC_OVERFLOW    0x1040    //��������
#define  MINOR_ALARMHOST_ZONE_MODULE_LOSS     0x1041    //����ģ�����
#define  MINOR_ALARMHOST_ZONE_MODULE_RESTORE     0x1042    //����ģ����߻ָ�
#define  MINOR_WIRELESS_OUTPUT_LOSS     0x1043    //�������ģ������
#define  MINOR_WIRELESS_OUTPUT_RESTORE    0x1044    //�������ģ��ָ�����
#define  MINOR_WIRELESS_REPEATER_LOSS    0x1045    //�����м�������
#define  MINOR_WIRELESS_REPEATER_RESTORE    0x1046    //�����м����ָ�����
#define  MINOR_ALARMHOST_TRIGGER_MODULE_LOSS     0x1047    //������ģ�����
#define  MINOR_ALARMHOST_TRIGGER_MODULE_RESTORE    0x1048    //������ģ����߻ָ�
#define  MINOR_ALARMHOST_WIRELESS_SIREN_LOSS    0x1049    //���߾�������
#define  MINOR_ALARMHOST_WIRELESS_SIREN_RESTORE     0x104a    //���߾��Żָ�����
#define  MINOR_TX1_SUB_SYSTEM_EXCEPTION             0x1050   /*TX1��ϵͳ�쳣*/
#define MINOR_TX1_REBOOT_EXCEPTION       0x1051   /*TX1ϵͳ�쳣����*/
#define MINOR_TX1_SUB_SYSTEM_LOSS      0x1052   /*������ϵͳ�쳣����*/
#define MINOR_TX1_SUB_SYSTEM_RESTORE     0x1053   /*������ϵͳ���߻ָ�*/
#define MINOR_WIRELESS_SPEED_EXCEPTION   0x1054   //���ߴ��������쳣



/* ���� */
//������
#define MAJOR_OPERATION                    0x3

//������
#define MINOR_VCA_MOTIONEXCEPTION        0x29  //��������쳣
#define MINOR_START_DVR                    0x41    /* ���� */
#define MINOR_STOP_DVR                    0x42    /* �ػ� */
#define MINOR_STOP_ABNORMAL                0x43    /* �쳣�ػ� */
#define MINOR_REBOOT_DVR                0x44    /*���������豸*/

#define MINOR_LOCAL_LOGIN                0x50    /* ���ص�½ */
#define MINOR_LOCAL_LOGOUT                0x51    /* ����ע����½ */
#define MINOR_LOCAL_CFG_PARM            0x52    /* �������ò��� */
#define MINOR_LOCAL_PLAYBYFILE          0x53    /* ���ذ��ļ��طŻ����� */
#define MINOR_LOCAL_PLAYBYTIME          0x54    /* ���ذ�ʱ��طŻ�����*/
#define MINOR_LOCAL_START_REC            0x55    /* ���ؿ�ʼ¼�� */
#define MINOR_LOCAL_STOP_REC            0x56    /* ����ֹͣ¼�� */
#define MINOR_LOCAL_PTZCTRL                0x57    /* ������̨���� */
#define MINOR_LOCAL_PREVIEW                0x58    /* ����Ԥ�� (������ʹ��)*/
#define MINOR_LOCAL_MODIFY_TIME         0x59    /* �����޸�ʱ��(������ʹ��) */
#define MINOR_LOCAL_UPGRADE             0x5a    /* �������� */
#define MINOR_LOCAL_RECFILE_OUTPUT      0x5b    /* ���ر���¼���ļ� */
#define MINOR_LOCAL_FORMAT_HDD          0x5c    /* ���س�ʼ��Ӳ�� */
#define MINOR_LOCAL_CFGFILE_OUTPUT      0x5d    /* �������������ļ� */
#define MINOR_LOCAL_CFGFILE_INPUT       0x5e    /* ���뱾�������ļ� */
#define MINOR_LOCAL_COPYFILE            0x5f    /* ���ر����ļ� */
#define MINOR_LOCAL_LOCKFILE            0x60    /* ��������¼���ļ� */
#define MINOR_LOCAL_UNLOCKFILE          0x61    /* ���ؽ���¼���ļ� */
#define MINOR_LOCAL_DVR_ALARM           0x62    /* �����ֶ�����ʹ�������*/
#define MINOR_IPC_ADD                   0x63    /* �������IPC */
#define MINOR_IPC_DEL                   0x64    /* ����ɾ��IPC */
#define MINOR_IPC_SET                   0x65    /* ��������IPC */
#define MINOR_LOCAL_START_BACKUP        0x66    /* ���ؿ�ʼ���� */
#define MINOR_LOCAL_STOP_BACKUP            0x67    /* ����ֹͣ����*/
#define MINOR_LOCAL_COPYFILE_START_TIME 0x68    /* ���ر��ݿ�ʼʱ��*/
#define MINOR_LOCAL_COPYFILE_END_TIME    0x69    /* ���ر��ݽ���ʱ��*/
#define MINOR_LOCAL_ADD_NAS             0x6a    /*�����������Ӳ�� ��nfs��iscsi��*/
#define MINOR_LOCAL_DEL_NAS             0x6b    /* ����ɾ��nas�� ��nfs��iscsi��*/
#define MINOR_LOCAL_SET_NAS             0x6c    /* ��������nas�� ��nfs��iscsi��*/
#define MINOR_LOCAL_RESET_PASSWD        0x6d    /* ���ػָ�����ԱĬ������*/ 

#define MINOR_REMOTE_LOGIN                0x70    /* Զ�̵�¼ */
#define MINOR_REMOTE_LOGOUT                0x71    /* Զ��ע����½ */
#define MINOR_REMOTE_START_REC            0x72    /* Զ�̿�ʼ¼�� */
#define MINOR_REMOTE_STOP_REC            0x73    /* Զ��ֹͣ¼�� */
#define MINOR_START_TRANS_CHAN            0x74    /* ��ʼ͸������ */
#define MINOR_STOP_TRANS_CHAN            0x75    /* ֹͣ͸������ */
#define MINOR_REMOTE_GET_PARM            0x76    /* Զ�̻�ȡ���� */
#define MINOR_REMOTE_CFG_PARM            0x77    /* Զ�����ò��� */
#define MINOR_REMOTE_GET_STATUS         0x78    /* Զ�̻�ȡ״̬ */
#define MINOR_REMOTE_ARM                0x79    /* Զ�̲��� */
#define MINOR_REMOTE_DISARM                0x7a    /* Զ�̳��� */
#define MINOR_REMOTE_REBOOT                0x7b    /* Զ������ */
#define MINOR_START_VT                    0x7c    /* ��ʼ�����Խ� */
#define MINOR_STOP_VT                    0x7d    /* ֹͣ�����Խ� */
#define MINOR_REMOTE_UPGRADE            0x7e    /* Զ������ */
#define MINOR_REMOTE_PLAYBYFILE         0x7f    /* Զ�̰��ļ��ط� */
#define MINOR_REMOTE_PLAYBYTIME         0x80    /* Զ�̰�ʱ��ط� */
#define MINOR_REMOTE_PTZCTRL            0x81    /* Զ����̨���� */
#define MINOR_REMOTE_FORMAT_HDD         0x82    /* Զ�̸�ʽ��Ӳ�� */
#define MINOR_REMOTE_STOP               0x83    /* Զ�̹ػ� */
#define MINOR_REMOTE_LOCKFILE            0x84    /* Զ�������ļ� */
#define MINOR_REMOTE_UNLOCKFILE         0x85    /* Զ�̽����ļ� */
#define MINOR_REMOTE_CFGFILE_OUTPUT     0x86    /* Զ�̵��������ļ� */
#define MINOR_REMOTE_CFGFILE_INTPUT     0x87    /* Զ�̵��������ļ� */
#define MINOR_REMOTE_RECFILE_OUTPUT     0x88    /* Զ�̵���¼���ļ� */
#define MINOR_REMOTE_DVR_ALARM          0x89    /* Զ���ֶ�����ʹ�������*/
#define MINOR_REMOTE_IPC_ADD            0x8a    /* Զ�����IPC */
#define MINOR_REMOTE_IPC_DEL            0x8b    /* Զ��ɾ��IPC */
#define MINOR_REMOTE_IPC_SET            0x8c    /* Զ������IPC */
#define MINOR_REBOOT_VCA_LIB            0x8d    /*�������ܿ�*/
#define MINOR_REMOTE_ADD_NAS            0x8e   /* Զ�����nas�� ��nfs��iscsi��*/
#define MINOR_REMOTE_DEL_NAS            0x8f   /* Զ��ɾ��nas�� ��nfs��iscsi��*/
#define MINOR_REMOTE_SET_NAS            0x90   /* Զ������nas�� ��nfs��iscsi��*/
#define MINOR_LOCAL_OPERATE_LOCK        0x9d            /* ���ز�������             */
#define MINOR_LOCAL_OPERATE_UNLOCK      0x9e            /* ���ز����������         */
#define MINOR_REMOTE_DELETE_HDISK       0x9a            /* Զ��ɾ���쳣�����ڵ�Ӳ�� */
#define MINOR_REMOTE_LOAD_HDISK         0x9b            /* Զ�̼���Ӳ��             */
#define MINOR_REMOTE_UNLOAD_HDISK       0x9c            /* Զ��ж��Ӳ��   */


//2010-05-26 ������ѶDVR��־����
#define MINOR_LOCAL_START_REC_CDRW      0x91   /* ���ؿ�ʼѶ�� */
#define MINOR_LOCAL_STOP_REC_CDRW       0x92   /* ����ֹͣѶ�� */
#define MINOR_REMOTE_START_REC_CDRW     0x93   /* Զ�̿�ʼѶ�� */
#define MINOR_REMOTE_STOP_REC_CDRW      0x94   /* Զ��ֹͣѶ�� */

#define MINOR_LOCAL_PIC_OUTPUT            0x95   /* ���ر���ͼƬ�ļ� */ 
#define MINOR_REMOTE_PIC_OUTPUT            0x96   /* Զ�̱���ͼƬ�ļ� */

//2011-07-26 ����81��ѶDVR��־����
#define MINOR_LOCAL_INQUEST_RESUME      0x97   /* ���ػָ���Ѷ�¼�*/
#define MINOR_REMOTE_INQUEST_RESUME     0x98   /* Զ�ָ̻���Ѷ�¼�*/

//2013-01-23 ����86������ѶNVR������־
#define MINOR_LOCAL_ADD_FILE            0x99        /*���ص����ļ�*/
#define MINOR_LOCAL_DEL_FILE            0x9f        /*����ɾ����Ѷ*/
#define MINOR_REMOTE_INQUEST_ADD_FILE   0x100       /*Զ�̵����ļ�*/ 

//2009-12-16 ������Ƶ�ۺ�ƽ̨��־����
#define MINOR_SUBSYSTEMREBOOT           0xa0    /*��Ƶ�ۺ�ƽ̨��dm6467 ��������*/
#define MINOR_MATRIX_STARTTRANSFERVIDEO 0xa1    /*��Ƶ�ۺ�ƽ̨�������л���ʼ����ͼ��*/
#define MINOR_MATRIX_STOPTRANSFERVIDEO    0xa2    /*��Ƶ�ۺ�ƽ̨�������л�ֹͣ����ͼ��*/
#define MINOR_REMOTE_SET_ALLSUBSYSTEM   0xa3    /*��Ƶ�ۺ�ƽ̨����������6467��ϵͳ��Ϣ*/
#define MINOR_REMOTE_GET_ALLSUBSYSTEM   0xa4    /*��Ƶ�ۺ�ƽ̨����ȡ����6467��ϵͳ��Ϣ*/
#define MINOR_REMOTE_SET_PLANARRAY      0xa5    /*��Ƶ�ۺ�ƽ̨�����üƻ���Ѳ��*/
#define MINOR_REMOTE_GET_PLANARRAY      0xa6    /*��Ƶ�ۺ�ƽ̨����ȡ�ƻ���Ѳ��*/
#define MINOR_MATRIX_STARTTRANSFERAUDIO 0xa7    /*��Ƶ�ۺ�ƽ̨�������л���ʼ������Ƶ*/
#define MINOR_MATRIX_STOPRANSFERAUDIO   0xa8    /*��Ƶ�ۺ�ƽ̨�������л�ֹͣ������Ƶ*/
#define MINOR_LOGON_CODESPITTER         0xa9    /*��Ƶ�ۺ�ƽ̨����½�����*/
#define MINOR_LOGOFF_CODESPITTER        0xaa    /*��Ƶ�ۺ�ƽ̨���˳������*/

//2010-01-22 ������Ƶ�ۺ�ƽ̨�н�����������־
#define MINOR_START_DYNAMIC_DECODE         0xb0    /*��ʼ��̬����*/
#define MINOR_STOP_DYNAMIC_DECODE        0xb1    /*ֹͣ��̬����*/
#define MINOR_GET_CYC_CFG                0xb2    /*��ȡ������ͨ����Ѳ����*/
#define MINOR_SET_CYC_CFG                0xb3    /*���ý���ͨ����Ѳ����*/
#define MINOR_START_CYC_DECODE            0xb4    /*��ʼ��Ѳ����*/
#define MINOR_STOP_CYC_DECODE            0xb5    /*ֹͣ��Ѳ����*/
#define MINOR_GET_DECCHAN_STATUS        0xb6    /*��ȡ����ͨ��״̬*/
#define MINOR_GET_DECCHAN_INFO            0xb7    /*��ȡ����ͨ����ǰ��Ϣ*/
#define MINOR_START_PASSIVE_DEC            0xb8    /*��ʼ��������*/
#define MINOR_STOP_PASSIVE_DEC            0xb9    /*ֹͣ��������*/
#define MINOR_CTRL_PASSIVE_DEC            0xba    /*���Ʊ�������*/
#define MINOR_RECON_PASSIVE_DEC            0xbb    /*������������*/
#define MINOR_GET_DEC_CHAN_SW            0xbc    /*��ȡ����ͨ���ܿ���*/
#define MINOR_SET_DEC_CHAN_SW            0xbd    /*���ý���ͨ���ܿ���*/
#define MINOR_CTRL_DEC_CHAN_SCALE        0xbe    /*����ͨ�����ſ���*/
#define MINOR_SET_REMOTE_REPLAY            0xbf    /*����Զ�̻ط�*/
#define MINOR_GET_REMOTE_REPLAY            0xc0    /*��ȡԶ�̻ط�״̬*/
#define MINOR_CTRL_REMOTE_REPLAY        0xc1    /*Զ�̻طſ���*/
#define MINOR_SET_DISP_CFG                0xc2    /*������ʾͨ��*/
#define MINOR_GET_DISP_CFG                0xc3    /*��ȡ��ʾͨ������*/
#define MINOR_SET_PLANTABLE                0xc4    /*���üƻ���Ѳ��*/
#define MINOR_GET_PLANTABLE                0xc5    /*��ȡ�ƻ���Ѳ��*/
#define MINOR_START_PPPPOE                0xc6    /*��ʼPPPoE����*/
#define MINOR_STOP_PPPPOE                0xc7    /*����PPPoE����*/
#define MINOR_UPLOAD_LOGO                0xc8    /*�ϴ�LOGO*/
//��ģʽ������־
#define MINOR_LOCAL_PIN                    0xc9    /* ����PIN���ܲ��� */
#define MINOR_LOCAL_DIAL                0xca    /* �����ֶ������Ͽ����� */    
#define MINOR_SMS_CONTROL                0xcb    /* ���ſ��������� */    
#define MINOR_CALL_ONLINE                0xcc    /* ���п������� */    
#define MINOR_REMOTE_PIN                0xcd    /* Զ��PIN���ܲ��� */

//2010-12-16 ��������־
#define MINOR_REMOTE_BYPASS             0xd0    /* Զ����·*/
#define MINOR_REMOTE_UNBYPASS           0xd1    /* Զ����·�ָ�*/
#define MINOR_REMOTE_SET_ALARMIN_CFG    0xd2    /* Զ�����ñ����������*/
#define MINOR_REMOTE_GET_ALARMIN_CFG    0xd3    /* Զ�̻�ȡ�����������*/
#define MINOR_REMOTE_SET_ALARMOUT_CFG   0xd4    /* Զ�����ñ����������*/
#define MINOR_REMOTE_GET_ALARMOUT_CFG   0xd5    /* Զ�̻�ȡ�����������*/
#define MINOR_REMOTE_ALARMOUT_OPEN_MAN  0xd6    /* Զ���ֶ������������*/
#define MINOR_REMOTE_ALARMOUT_CLOSE_MAN 0xd7    /* Զ���ֶ��رձ������*/
#define MINOR_REMOTE_ALARM_ENABLE_CFG   0xd8    /* Զ�����ñ���������RS485����ʹ��״̬*/
#define MINOR_DBDATA_OUTPUT                0xd9    /* �������ݿ��¼ */
#define MINOR_DBDATA_INPUT                0xda    /* �������ݿ��¼ */
#define MINOR_MU_SWITCH                    0xdb    /* �����л� */
#define MINOR_MU_PTZ                    0xdc    /* ����PTZ���� */
#define MINOR_DELETE_LOGO               0xdd    /* ɾ��logo */
#define MINOR_REMOTE_INQUEST_DEL_FILE   0xde       /*Զ��ɾ���ļ�*/

#define MINOR_LOCAL_CONF_REB_RAID       0x101           /*���������Զ��ؽ�*/
#define MINOR_LOCAL_CONF_SPARE          0x102           /*���������ȱ�*/
#define MINOR_LOCAL_ADD_RAID            0x103           /*���ش�������*/
#define MINOR_LOCAL_DEL_RAID            0x104           /*����ɾ������*/
#define MINOR_LOCAL_MIG_RAID            0x105           /*����Ǩ������*/
#define MINOR_LOCAL_REB_RAID            0x106           /* �����ֶ��ؽ�����*/
#define MINOR_LOCAL_QUICK_CONF_RAID     0x107           /*����һ������*/
#define MINOR_LOCAL_ADD_VD              0x108           /*���ش����������*/
#define MINOR_LOCAL_DEL_VD              0x109           /*����ɾ���������*/
#define MINOR_LOCAL_RP_VD               0x10a           /*�����޸��������*/
#define MINOR_LOCAL_FORMAT_EXPANDVD     0X10b           /*������չ�����������*/
#define MINOR_LOCAL_RAID_UPGRADE        0X10c           /*����raid������*/  
#define MINOR_LOCAL_STOP_RAID           0x10d           /*������ͣRAID����(����ȫ����)*/  
#define MINOR_REMOTE_CONF_REB_RAID      0x111           /*Զ�������Զ��ؽ�*/
#define MINOR_REMOTE_CONF_SPARE         0x112            /*Զ�������ȱ�*/
#define MINOR_REMOTE_ADD_RAID           0x113           /*Զ�̴�������*/
#define MINOR_REMOTE_DEL_RAID           0x114           /*Զ��ɾ������*/
#define MINOR_REMOTE_MIG_RAID           0x115           /*Զ��Ǩ������*/
#define MINOR_REMOTE_REB_RAID           0x116           /* Զ���ֶ��ؽ�����*/
#define MINOR_REMOTE_QUICK_CONF_RAID    0x117           /*Զ��һ������*/
#define MINOR_REMOTE_ADD_VD             0x118           /*Զ�̴����������*/
#define MINOR_REMOTE_DEL_VD             0x119           /*Զ��ɾ���������*/
#define MINOR_REMOTE_RP_VD              0x11a           /*Զ���޸��������*/
#define MINOR_REMOTE_FORMAT_EXPANDVD    0X11b           /*Զ�������������*/
#define MINOR_REMOTE_RAID_UPGRADE       0X11c           /*Զ��raid������*/  
#define MINOR_REMOTE_STOP_RAID          0x11d           /*Զ����ͣRAID����(����ȫ����)*/
#define MINOR_LOCAL_START_PIC_REC       0x121          /*���ؿ�ʼץͼ*/ 
#define MINOR_LOCAL_STOP_PIC_REC        0x122          /*����ֹͣץͼ*/
#define MINOR_LOCAL_SET_SNMP            0x125           /*��������SNMP*/
#define MINOR_LOCAL_TAG_OPT             0x126          /*���ر�ǩ����*/
#define MINOR_REMOTE_START_PIC_REC      0x131          /*Զ�̿�ʼץͼ*/
#define MINOR_REMOTE_STOP_PIC_REC       0x132           /*Զ��ֹͣץͼ*/
#define MINOR_REMOTE_SET_SNMP           0x135            /*Զ������SNMP*/
#define MINOR_REMOTE_TAG_OPT            0x136             /*Զ�̱�ǩ����*/  
#define MINOR_REMOTE_LOGIN_LOCK         0x137           //Զ�̵�¼����
#define MINOR_REMOTE_LOGIN_UNLOCK       0x138          //Զ�̵�¼����
// 9000 v2.2.0
#define MINOR_LOCAL_VOUT_SWITCH         0x140   /* ����������л�����*/
#define MINOR_STREAM_CABAC                 0x141   /* ����ѹ������ѡ�����ò���*/

//Netra 3.0.0
#define MINOR_LOCAL_SPARE_OPT           0x142   /*����N+1 �ȱ���ز���*/
#define MINOR_REMOTE_SPARE_OPT            0x143   /*Զ��N+1 �ȱ���ز���*/
#define MINOR_LOCAL_IPCCFGFILE_OUTPUT    0x144      /* ���ص���ipc�����ļ�*/
#define MINOR_LOCAL_IPCCFGFILE_INPUT      0x145   /* ���ص���ipc�����ļ� */
#define MINOR_LOCAL_IPC_UPGRADE         0x146   /* ��������IPC */
#define MINOR_REMOTE_IPCCFGFILE_OUTPUT  0x147   /* Զ�̵���ipc�����ļ�*/
#define MINOR_REMOTE_IPCCFGFILE_INPUT   0x148   /* Զ�̵���ipc�����ļ�*/
#define MINOR_REMOTE_IPC_UPGRADE        0x149   /* Զ������IPC */

#define MINOR_LOCAL_UNLOAD_HDISK        0x150     /*����ж��Ӳ��*/
#define MINOR_LOCAL_AUDIO_MIX           0x151     /*����������Ƶ��������*/
#define MINOR_REMOTE_AUDIO_MIX          0x152     /*Զ��������Ƶ��������*/
#define MINOR_LOCAL_TRIAL_PAUSE         0x153     /*������ͣѶ��*/
#define MINOR_LOCAL_TRIAL_RESUME        0x154     /*���ؼ���Ѷ��*/
#define MINOR_REMOTE_TRIAL_PAUSE        0x155     /*Զ����ͣѶ��*/
#define MINOR_REMOTE_TRIAL_RESUME       0x156     /*Զ�̼���Ѷ��*/
#define MINOR_REMOTE_MODIFY_VERIFICATION_CODE   0x157 /*�޸�ƽ̨����֤��*/

#define MINOR_LOCAL_MAKECALL            0x180          /*���غ���*/
#define MINOR_LOCAL_REJECTCALL          0x181          /*���ؾܽ�*/
#define MINOR_LOCAL_ANSWERCALL          0x182          /*���ؽ���*/
#define MINOR_LOCAL_HANGUPCALL          0x183          /*���عҶ�*/

#define MINOR_REMOTE_MAKECALL           0x188          /*Զ�̺���*/
#define MINOR_REMOTE_REJECTCALL         0x189          /*Զ�̾ܽ�*/
#define MINOR_REMOTE_ANSWERCALL         0x18a          /*Զ�̽���*/
#define MINOR_REMOTE_HANGUPCALL         0x18b          /*Զ�̹Ҷ�*/

#define MINOR_SET_MULTI_MASTER          0x201    /*���ô�������*/
#define MINOR_SET_MULTI_SLAVE           0x202    /*���ô�������*/
#define MINOR_CANCEL_MULTI_MASTER       0x203    /*ȡ����������*/
#define MINOR_CANCEL_MULTI_SLAVE        0x204    /*ȡ����������*/

#define MINOR_DISPLAY_LOGO                0x205    /*��ʾLOGO*/
#define MINOR_HIDE_LOGO                 0x206    /*����LOGO*/
#define MINOR_SET_DEC_DELAY_LEVEL       0x207    /*����ͨ����ʱ��������*/
#define MINOR_SET_BIGSCREEN_DIPLAY_AREA 0x208    /*���ô�����ʾ����*/
#define MINOR_CUT_VIDEO_SOURCE          0x209    /*������ƵԴ�и�����*/
#define MINOR_SET_BASEMAP_AREA          0x210    /*������ͼ��������*/
#define MINOR_DOWNLOAD_BASEMAP          0x211    /*���ش�����ͼ*/
#define MINOR_CUT_BASEMAP               0x212    /*��ͼ�и�����*/
#define MINOR_CONTROL_ELEC_ENLARGE      0x213    /*���ӷŴ����(�Ŵ��ԭ)*/
#define MINOR_SET_OUTPUT_RESOLUTION     0x214    /*��ʾ����ֱ�������*/
#define MINOR_SET_TRANCSPARENCY         0X215    /*ͼ��͸��������*/
#define MINOR_SET_OSD                   0x216    /*��ʾOSD����*/
#define MINOR_RESTORE_DEC_STATUS        0x217    /*�ָ���ʼ״̬(�����л�ʱ������ָ���ʼ״̬)*/

//2011-11-11 ���Ӵ���������������־������
#define MINOR_SCREEN_OPEN_SCREEN        0x218   //����Ļ
#define MINOR_SCREEN_CLOSE_SCREEN       0x219   //�ر���Ļ
#define MINOR_SCREEN_SWITCH_SIGNAL      0x21a   //�ź�Դ�л�
#define MINOR_SCREEN_MODIFY_NETWORK     0x21b   //�����������
#define MINOR_SCREEN_MODIFY_LEDRES      0x21c   //���������LED�ֱ���
#define MINOR_SCREEN_SHOW_NORMAL        0x21d   //���ô�����ͨ��ʾģʽ
#define MINOR_SCREEN_SHOW_TILE          0x21e   //���ô���ƽ����ʾģʽ
#define MINOR_SCREEN_DEC_NORMAL         0x21f   //������ͨ����ģʽ
#define MINOR_SCREEN_DEC_LOWLATENCY     0x220   //���õ���ʱ����ģʽ
#define MINOR_SCREEN_MODIFY_SELFRES     0x221   //�����ź�Դ�Զ���ֱ���
#define MINOR_SCREEN_OUTPUT_POSITION    0x222   //����ڹ�����Ļ
#define MINOR_SCREEN_IMAGE_ENHANCE      0x223   //ͼ����ǿ
#define MINOR_SCREEN_JOIN_SIGNAL        0x224   //�ź�Դƴ��
#define MINOR_SCREEN_SIGNAL_OSD         0x225   //�ź�Դ�ַ�����
#define MINOR_SCREEN_ASSOCIATED_INTERACTION 0x226   //�ź�Դ������������������
#define MINOR_SCREEN_MODIFY_MATRIX      0x227   //���þ������
#define MINOR_SCREEN_WND_TOP_KEEP       0x228   //�����ö�����
#define MINOR_SCREEN_WND_OPEN_KEEP      0x229   //���ڴ򿪱���
#define MINOR_SCREEN_WALL_MIRROR        0x22a   //����ǽ������
#define MINOR_SCREEN_UPLOAD_BASEMAP     0x22b   //�ϴ���ͼ
#define MINOR_SCREEN_SHOW_BASEMAP       0x22c   //��ʾ��ͼ
#define MINOR_SCREEN_HIDE_BASEMAP       0x22d   //���ص�ͼ
#define MINOR_SCREEN_MODIFY_SERIAL      0x22e   //���ô��ڲ���

#define MINOR_SCREEN_SET_INPUT          0x251    /*�޸�����Դ*/
#define MINOR_SCREEN_SET_OUTPUT         0x252    /*�޸����ͨ��*/
#define MINOR_SCREEN_SET_OSD            0x253    /*�޸�����LED*/
#define MINOR_SCREEN_SET_LOGO           0x254    /*�޸�LOGO*/
#define MINOR_SCREEN_SET_LAYOUT         0x255    /*���ò���*/
#define MINOR_SCREEN_PICTUREPREVIEW     0x256    /*���Բ���*/


//2012-06-14 CVCS2.0, �������õȲ�����V1.0�� V1.1���Ѿ����ˣ���ʱ���豸��־��û�ж��� 
#define MINOR_SCREEN_GET_OSD            0x257   /*��ȡ����LED*/
#define MINOR_SCREEN_GET_LAYOUT            0x258   /*��ȡ����*/
#define MINOR_SCREEN_LAYOUT_CTRL        0x259   /*���ֿ���*/
#define MINOR_GET_ALL_VALID_WND            0x260    /*��ȡ������Ч����*/
#define MINOR_GET_SIGNAL_WND            0x261    /*��ȡ����������Ϣ*/
#define MINOR_WINDOW_CTRL                0x262    /*���ڿ���*/
#define MINOR_GET_LAYOUT_LIST            0x263    /*��ȡ�����б�*/
#define MINOR_LAYOUT_CTRL                0x264    /*���ֿ���*/
#define MINOR_SET_LAYOUT                0x265    /*���ò���*/
#define MINOR_GET_SIGNAL_LIST            0x266    /*��ȡ�����ź�Դ�б�*/
#define MINOR_GET_PLAN_LIST                0x267    /*��ȡԤ���б�*/
#define MINOR_SET_PLAN                    0x268    /*�޸�Ԥ��*/
#define MINOR_CTRL_PLAN                    0x269    /*����Ԥ��*/
#define MINOR_CTRL_SCREEN                0x270    /*��Ļ����*/
#define MINOR_ADD_NETSIG                0x271    /*����ź�Դ*/
#define MINOR_SET_NETSIG                0x272    /*�޸��ź�Դ*/
#define MINOR_SET_DECBDCFG                0x273    /*���ý�������*/
#define MINOR_GET_DECBDCFG                0x274    /*��ȡ��������*/
#define MINOR_GET_DEVICE_STATUS            0x275    /*��ȡ�豸��Ϣ*/
#define MINOR_UPLOAD_PICTURE            0x276    /*��ͼ�ϴ�*/
#define MINOR_SET_USERPWD                0x277    /*�����û�����*/
#define MINOR_ADD_LAYOUT                0x278    /*��Ӳ���*/
#define MINOR_DEL_LAYOUT                0x279    /*ɾ������*/
#define MINOR_DEL_NETSIG                0x280    /*ɾ���ź�Դ*/
#define MINOR_ADD_PLAN                    0x281    /*���Ԥ��*/
#define MINOR_DEL_PLAN                    0x282    /*ɾ��Ԥ��*/
#define MINOR_GET_EXTERNAL_MATRIX_CFG    0x283    //��ȡ��Ӿ�������
#define MINOR_SET_EXTERNAL_MATRIX_CFG    0x284    //������Ӿ�������
#define    MINOR_GET_USER_CFG                0x285    //��ȡ�û�����
#define    MINOR_SET_USER_CFG                0x286    //�����û�����
#define    MINOR_GET_DISPLAY_PANEL_LINK_CFG 0x287    //��ȡ��ʾǽ��������
#define    MINOR_SET_DISPLAY_PANEL_LINK_CFG 0x288    //������ʾǽ��������

#define    MINOR_GET_WALLSCENE_PARAM        0x289        //��ȡ����ǽ����
#define    MINOR_SET_WALLSCENE_PARAM        0x28a        //���õ���ǽ����
#define    MINOR_GET_CURRENT_WALLSCENE        0x28b        //��ȡ��ǰʹ�ó���
#define    MINOR_SWITCH_WALLSCENE            0x28c        //�����л�
#define    MINOR_SIP_LOGIN                    0x28d        //SIPע��ɹ�
#define MINOR_VOIP_START                0x28e        //VOIP�Խ���ʼ
#define MINOR_VOIP_STOP                    0x28f        //VOIP�Խ�ֹͣ
#define MINOR_WIN_TOP                   0x290       //����ǽ�����ö�
#define MINOR_WIN_BOTTOM                0x291       //����ǽ�����õ�

// Netra 2.2.2
#define MINOR_LOCAL_LOAD_HDISK          0x300            //���ؼ���Ӳ��             
#define MINOR_LOCAL_DELETE_HDISK        0x301            //����ɾ���쳣�����ڵ�Ӳ��

//KY2013 3.0.0
#define MINOR_LOCAL_MAIN_AUXILIARY_PORT_SWITCH    0X302 //�����������л�
#define MINOR_LOCAL_HARD_DISK_CHECK                0x303 //��������Ӳ���Լ�

//Netra3.1.0
#define MINOR_LOCAL_CFG_DEVICE_TYPE        0x310    //���������豸����
#define MINOR_REMOTE_CFG_DEVICE_TYPE    0x311    //Զ�������豸����
#define MINOR_LOCAL_CFG_WORK_HOT_SERVER    0x312    //�������ù������ȱ�������
#define MINOR_REMOTE_CFG_WORK_HOT_SERVER 0x313    //Զ�����ù������ȱ�������
#define MINOR_LOCAL_DELETE_WORK            0x314    //����ɾ��������
#define MINOR_REMOTE_DELETE_WORK        0x315    //Զ��ɾ��������
#define    MINOR_LOCAL_ADD_WORK            0x316    //������ӹ�����
#define MINOR_REMOTE_ADD_WORK            0x317    //Զ����ӹ�����
#define MINOR_LOCAL_IPCHEATMAP_OUTPUT   0x318            /* ���ص����ȶ�ͼ�ļ�      */
#define MINOR_LOCAL_IPCHEATFLOW_OUTPUT  0x319          /* ���ص����ȶ������ļ�      */
#define MINOR_REMOTE_SMS_SEND           0x350    /*Զ�̷��Ͷ���*/
#define MINOR_LOCAL_SMS_SEND            0x351   /*���ط��Ͷ���*/
#define MINOR_ALARM_SMS_SEND            0x352    /*���Ͷ��ű���*/
#define MINOR_SMS_RECV                  0x353     /*���ն���*/
//����ע��0x350��0x351��ָ�˹���GUI��IE�ؼ��ϱ༭�����Ͷ��ţ�
#define MINOR_LOCAL_SMS_SEARCH          0x354  /*������������*/
#define MINOR_REMOTE_SMS_SEARCH         0x355   /*Զ����������*/
#define MINOR_LOCAL_SMS_READ            0x356   /*���ز鿴����*/
#define MINOR_REMOTE_SMS_READ           0x357   /*Զ�̲鿴����*/
#define MINOR_REMOTE_DIAL_CONNECT       0x358   /*Զ�̿����ֶ�����*/
#define MINOR_REMOTE_DIAL_DISCONN       0x359   /*Զ��ֹͣ�ֶ�����*/
#define MINOR_LOCAL_WHITELIST_SET       0x35A   /*����������Ȩ����*/
#define MINOR_REMOTE_WHITELIST_SET      0x35B   /*Զ��������Ȩ����*/
#define MINOR_LOCAL_DIAL_PARA_SET       0x35C   /*�������ò��Ų���*/
#define MINOR_REMOTE_DIAL_PARA_SET      0x35D   /*Զ�����ò��Ų���*/
#define MINOR_LOCAL_DIAL_SCHEDULE_SET   0x35E   /*�������ò��żƻ�*/
#define MINOR_REMOTE_DIAL_SCHEDULE_SET  0x35F   /*Զ�����ò��żƻ�*/
#define MINOR_PLAT_OPER                 0x360   /* ƽ̨����*/
#define MINOR_REMOTE_CFG_POE_WORK_MODE  0x361//Զ������POE����ģʽ
#define MINOR_LOCAL_CFG_POE_WORK_MODE   0x362//��������POE����ģʽ
#define MINOR_REMOTE_CFG_FACE_CONTRAST  0x363//Զ�����������ȶ�����
#define MINOR_LOCAL_CFG_FACE_CONTRAST   0x364//�������������ȶ�����
#define MINOR_REMOTE_CFG_WHITELIST_FACE_CONTRAST   0x365//Զ��������Ȩ���������ȶ�����
#define MINOR_LOCAL_CFG_WHITELIST_FACE_CONTRAST    0x366//����������Ȩ���������ȶ�����
#define MINOR_LOCAL_CHECK_TIME          0x367   //�����ֶ�Уʱ
#define MINOR_VCA_ONEKEY_EXPORT_PICTURE 0x368 //һ������ͼƬ
#define MINOR_VCA_ONEKEY_DELETE_PICTURE 0x369 //һ��ɾ��ͼƬ
#define MINOR_VCA_ONEKEY_EXPORT_VIDEO   0x36a //һ������¼��
#define MINOR_VCA_ONEKEY_DELETE_VIDEO   0x36b //һ��ɾ��¼��


//0x400-0x1000 �Ž���������
#define MINOR_REMOTE_OPEN_DOOR          0x400   //Զ�̿���
#define MINOR_REMOTE_CLOSE_DOOR         0x401   //Զ�̹���(�ܿ�)
#define MINOR_REMOTE_ALWAYS_OPEN        0x402   //Զ�̳���(����)
#define MINOR_REMOTE_ALWAYS_CLOSE       0x403   //Զ�̳���(����)
#define MINOR_REMOTE_CHECK_TIME         0x404   //Զ���ֶ�Уʱ
#define MINOR_NTP_CHECK_TIME            0x405   //NTP�Զ�Уʱ
#define MINOR_REMOTE_CLEAR_CARD         0x406   //Զ����տ���
#define MINOR_REMOTE_RESTORE_CFG        0x407   //Զ�ָ̻�Ĭ�ϲ���
#define MINOR_ALARMIN_ARM               0x408   //��������
#define MINOR_ALARMIN_DISARM            0x409   //��������
#define MINOR_LOCAL_RESTORE_CFG         0x40a   //���ػָ�Ĭ�ϲ��� 
#define MINOR_REMOTE_CAPTURE_PIC        0x40b  //Զ��ץ��
#define MINOR_MOD_NET_REPORT_CFG        0x40c   //�޸��������Ĳ�������
#define MINOR_MOD_GPRS_REPORT_PARAM     0x40d   //�޸�GPRS���Ĳ�������
#define MINOR_MOD_REPORT_GROUP_PARAM    0x40e   //�޸��������������
#define    MINOR_UNLOCK_PASSWORD_OPEN_DOOR  0x40f  //���������
#define MINOR_AUTO_RENUMBER              0x410  //�Զ����±��
#define MINOR_AUTO_COMPLEMENT_NUMBER     0x411  //�Զ�������
#define MINOR_NORMAL_CFGFILE_INPUT          0x412   //������ͨ�����ļ�
#define MINOR_NORMAL_CFGFILE_OUTTPUT     0x413   //������ͨ�����ļ�
#define MINOR_CARD_RIGHT_INPUT              0x414   //���뿨Ȩ�޲���
#define MINOR_CARD_RIGHT_OUTTPUT          0x415   //������Ȩ�޲���
#define MINOR_LOCAL_USB_UPGRADE             0x416   //����U������
#define MINOR_REMOTE_VISITOR_CALL_LADDER     0x417  //�ÿͺ���
#define MINOR_REMOTE_HOUSEHOLD_CALL_LADDER   0x418  //ס������
#define MINOR_REMOTE_ACTUAL_GUARD            0x419  //Զ��ʵʱ����
#define MINOR_REMOTE_ACTUAL_UNGUARD          0x41a  //Զ��ʵʱ����
#define MINOR_REMOTE_CONTROL_NOT_CODE_OPER_FAILED     0x41b   //ң����δ�������ʧ��
#define MINOR_REMOTE_CONTROL_CLOSE_DOOR               0x41c   //ң��������
#define MINOR_REMOTE_CONTROL_OPEN_DOOR                0x41d   //ң��������
#define MINOR_REMOTE_CONTROL_ALWAYS_OPEN_DOOR         0x41e   //ң����������

#define MINOR_OPERATION_CUSTOM1        0x900  //�Ž��Զ������1
#define MINOR_OPERATION_CUSTOM2        0x901  //�Ž��Զ������2
#define MINOR_OPERATION_CUSTOM3        0x902  //�Ž��Զ������3
#define MINOR_OPERATION_CUSTOM4        0x903  //�Ž��Զ������4
#define MINOR_OPERATION_CUSTOM5        0x904  //�Ž��Զ������5
#define MINOR_OPERATION_CUSTOM6        0x905  //�Ž��Զ������6
#define MINOR_OPERATION_CUSTOM7        0x906  //�Ž��Զ������7
#define MINOR_OPERATION_CUSTOM8        0x907  //�Ž��Զ������8
#define MINOR_OPERATION_CUSTOM9        0x908  //�Ž��Զ������9
#define MINOR_OPERATION_CUSTOM10       0x909  //�Ž��Զ������10
#define MINOR_OPERATION_CUSTOM11       0x90a  //�Ž��Զ������11
#define MINOR_OPERATION_CUSTOM12       0x90b  //�Ž��Զ������12
#define MINOR_OPERATION_CUSTOM13       0x90c  //�Ž��Զ������13
#define MINOR_OPERATION_CUSTOM14       0x90d  //�Ž��Զ������14
#define MINOR_OPERATION_CUSTOM15       0x90e  //�Ž��Զ������15
#define MINOR_OPERATION_CUSTOM16       0x90f  //�Ž��Զ������16
#define MINOR_OPERATION_CUSTOM17       0x910  //�Ž��Զ������17
#define MINOR_OPERATION_CUSTOM18       0x911  //�Ž��Զ������18
#define MINOR_OPERATION_CUSTOM19       0x912  //�Ž��Զ������19
#define MINOR_OPERATION_CUSTOM20       0x913  //�Ž��Զ������20
#define MINOR_OPERATION_CUSTOM21       0x914  //�Ž��Զ������21
#define MINOR_OPERATION_CUSTOM22       0x915  //�Ž��Զ������22
#define MINOR_OPERATION_CUSTOM23       0x916  //�Ž��Զ������23
#define MINOR_OPERATION_CUSTOM24       0x917  //�Ž��Զ������24
#define MINOR_OPERATION_CUSTOM25       0x918  //�Ž��Զ������25
#define MINOR_OPERATION_CUSTOM26       0x919  //�Ž��Զ������26
#define MINOR_OPERATION_CUSTOM27       0x91a  //�Ž��Զ������27
#define MINOR_OPERATION_CUSTOM28       0x91b  //�Ž��Զ������28
#define MINOR_OPERATION_CUSTOM29       0x91c  //�Ž��Զ������29
#define MINOR_OPERATION_CUSTOM30       0x91d  //�Ž��Զ������30
#define MINOR_OPERATION_CUSTOM31       0x91e  //�Ž��Զ������31
#define MINOR_OPERATION_CUSTOM32       0x91f  //�Ž��Զ������32
#define MINOR_OPERATION_CUSTOM33       0x920  //�Ž��Զ������33
#define MINOR_OPERATION_CUSTOM34       0x921  //�Ž��Զ������34
#define MINOR_OPERATION_CUSTOM35       0x922  //�Ž��Զ������35
#define MINOR_OPERATION_CUSTOM36       0x923  //�Ž��Զ������36
#define MINOR_OPERATION_CUSTOM37       0x924  //�Ž��Զ������37
#define MINOR_OPERATION_CUSTOM38       0x925  //�Ž��Զ������38
#define MINOR_OPERATION_CUSTOM39       0x926  //�Ž��Զ������39
#define MINOR_OPERATION_CUSTOM40       0x927  //�Ž��Զ������40
#define MINOR_OPERATION_CUSTOM41       0x928  //�Ž��Զ������41
#define MINOR_OPERATION_CUSTOM42       0x929  //�Ž��Զ������42
#define MINOR_OPERATION_CUSTOM43       0x92a  //�Ž��Զ������43
#define MINOR_OPERATION_CUSTOM44       0x92b  //�Ž��Զ������44
#define MINOR_OPERATION_CUSTOM45       0x92c  //�Ž��Զ������45
#define MINOR_OPERATION_CUSTOM46       0x92d  //�Ž��Զ������46
#define MINOR_OPERATION_CUSTOM47       0x92e  //�Ž��Զ������47
#define MINOR_OPERATION_CUSTOM48       0x92f  //�Ž��Զ������48
#define MINOR_OPERATION_CUSTOM49       0x930  //�Ž��Զ������49
#define MINOR_OPERATION_CUSTOM50       0x931  //�Ž��Զ������50
#define MINOR_OPERATION_CUSTOM51       0x932  //�Ž��Զ������51
#define MINOR_OPERATION_CUSTOM52       0x933  //�Ž��Զ������52
#define MINOR_OPERATION_CUSTOM53       0x934  //�Ž��Զ������53
#define MINOR_OPERATION_CUSTOM54       0x935  //�Ž��Զ������54
#define MINOR_OPERATION_CUSTOM55       0x936  //�Ž��Զ������55
#define MINOR_OPERATION_CUSTOM56       0x937  //�Ž��Զ������56
#define MINOR_OPERATION_CUSTOM57       0x938  //�Ž��Զ������57
#define MINOR_OPERATION_CUSTOM58       0x939  //�Ž��Զ������58
#define MINOR_OPERATION_CUSTOM59       0x93a  //�Ž��Զ������59
#define MINOR_OPERATION_CUSTOM60       0x93b  //�Ž��Զ������60
#define MINOR_OPERATION_CUSTOM61       0x93c  //�Ž��Զ������61
#define MINOR_OPERATION_CUSTOM62       0x93d  //�Ž��Զ������62
#define MINOR_OPERATION_CUSTOM63       0x93e  //�Ž��Զ������63
#define MINOR_OPERATION_CUSTOM64       0x93f  //�Ž��Զ������64

#define MINOR_SET_WIFI_PARAMETER               0x950    //����WIFI���ò���
#define MINOR_EZVIZ_LOGIN                      0x951    //өʯ�Ƶ�½
#define MINOR_EZVIZ_LOGINOUT                   0x952    //өʯ�Ƶǳ�
#define MINOR_LOCK_ADD                         0x953    //���������
#define MINOR_LOCK_DELETE                      0x954    //������ɾ��
#define MINOR_LOCK_GET_STATUS                  0x955    //������״̬��ȡ
#define MINOR_LOCK_SET_TMP_PASSWORD            0x956    //��������ʱ�����·�
#define MINOR_LOCK_SET_SILENT_MODE             0x957    //��������������
#define MINOR_LOCK_SET_LATE_WARNING            0x958    //�����������������
#define MINOR_LOCK_IPC_ADD                     0x959    //������IPC����
#define MINOR_LOCK_IPC_REMOVE                  0x95a    //������IPC�������
#define MINOR_LOCK_DETECTOR_ADD                0x95b    //������̽��������
#define MINOR_LOCK_DETECTOR_REMOVE             0x95c    //������̽�����������
#define MINOR_LOCK_MESSAGE_REMINDING_OPEN      0x95d    //��������Ϣ���Ѵ�
#define MINOR_LOCK_MESSAGE_REMINDING_CLOSE     0x95e    //��������Ϣ���ѹر�
#define MINOR_LOCK_SET_HEART_BEAT              0x95f    //��������������
#define MINOR_LOCK_REBOOT                      0x960        //����������
#define MINOR_LOCK_CLEAR_USER                  0x961        //����������û�
#define MINOR_LOCK_FORMAT                      0x962        //��������ʽ��
#define MINOR_LOCK_FINGER_CHANGE               0x963        //������ָ�ƸĶ�
#define MINOR_LOCK_PASSWORD_CHANGE             0x964     //����������Ķ�
#define MINOR_LOCK_CARD_CHANGE                 0x965     //����������Ϣ�Ķ�
#define MINOR_LOCK_USER_CHANGE                 0x966      //�������û���Ϣ�Ķ�
#define MINOR_LOCK_SYSTEM_CHANGE               0x967     //������ϵͳ��Ϣ�Ķ�
#define MINOR_LOCK_CHANGE_ADD_UESR             0x968      //�����������û�
#define MINOR_LOCK_CHANGE_DEL_UESR             0x969     //������ɾ���û�
#define MINOR_LOCK_CHANGE_CUSTOM_USER_NAME     0x96a    //�������Զ����û��û����Ķ�
#define MINOR_LOCK_CHANGE_REMOTE_DEVICE        0x96b  //������ң������Ϣ�Ķ�
#define MINOR_LOCK_CHANGE_ADD_FP               0x96c  //����������ָ��
#define MINOR_LOCK_CHANGE_DEL_FP               0x96d  //������ɾ��ָ��
#define MINOR_LOCK_CHANGE_ADD_PASSWORD         0x96e  //��������������
#define MINOR_LOCK_CHANGE_DEL_PASSWORD         0x96f  //������ɾ������
#define MINOR_LOCK_CHANGE_ADD_CARD             0x970  //������������Ƭ
#define MINOR_LOCK_CHANGE_DEL_CARD             0x971  //������ɾ����Ƭ
#define MINOR_LOCK_NETWORK_SWITCH              0x972  //���������繦�ܿ��ظĶ�
#define MINOR_LOCK_CLEAR_NETWORK_DATA          0x973  //�����������������
#define MINOR_LOCK_CLEAR_HOST_USER             0x974  //��������������û�
#define MINOR_LOCK_CLEAR_GUEST_USER            0x975  //��������տ����û�
#define MINOR_LOCK_CLEAN_ALL_REMOTE_DEVICE     0x976   //ң�����û���Ϣ���
#define MINOR_LOCK_CLEAN_NORMAL_USER_FINGRT    0x977    //�����������ͨ�û�ָ��
#define MINOR_LOCK_CLEAN_ALL_CARD              0x978      //������������п�Ƭ
#define MINOR_LOCK_CLEAN_ALL_PASSWORD          0x979    //�����������������
#define MINOR_START_WIRELESSSERVER             0x97a    //�����豸�ȵ�
#define MINOR_STOP_WIRELESSSERVER              0x97b    //�ر��豸�ȵ�
#define MINOR_EMERGENCY_CARD_AUTH_NORMAL_CARD  0x97c    //Ӧ��������Ȩ��ͨ��
#define MINOR_CHANGE_ALWAYS_OPEN_RIGHT         0x97d    //ͨ��ģʽ�Ķ�
#define MINOR_LOCK_DOOR_BELL_EVENT             0x97e    //�����¼���������������

//2018-04-23 ͨ���������ز�����־����
#define    MINOR_ALARMHOST_GUARD     		0x1010    //��ͨ����(�������)
#define    MINOR_ALARMHOST_UNGUARD 		0x1011    //��ͨ����
#define    MINOR_ALARMHOST_BYPASS 				0x1012    //��·
#define    MINOR_ALARMHOST_DURESS_ACCESS 			0x1013    //Ю��

#define    MINOR_ALARMHOST_RS485_PARAM               0x1018   //�޸�485���ò���
#define    MINOR_ALARMHOST_ALARM_OUTPUT 				0x1019    //���ƴ�����
#define    MINOR_ALARMHOST_ACCESS_OPEN 				0x101a    //�����Ž���
#define    MINOR_ALARMHOST_ACCESS_CLOSE 				0x101b    //�����Ž���
#define    MINOR_ALARMHOST_SIREN_OPEN 				0x101c    //���ƾ��ſ�
#define    MINOR_ALARMHOST_SIREN_CLOSE 				0x101d    //���ƾ��Ź�
#define    MINOR_ALARMHOST_MOD_ZONE_CONFIG 			0x101e    //�޸ķ�������
#define    MINOR_ALARMHOST_MOD_ALARMOUT_CONIFG 	0x101f    //�޸Ĵ���������
#define    MINOR_ALARMHOST_MOD_ANALOG_CONFIG 		0x1020    //�޸�ģ��������
#define    MINOR_ALARMHOST_RS485_CONFIG 				0x1021    //�޸�485ͨ������
#define    MINOR_ALARMHOST_PHONE_CONFIG 				0x1022    //�޸Ĳ�������
#define    MINOR_ALARMHOST_ADD_ADMIN 				0x1023    //���ӹ���Ա
#define    MINOR_ALARMHOST_MOD_ADMIN_PARAM 		0x1024    //�޸Ĺ���Ա����
#define    MINOR_ALARMHOST_DEL_ADMIN 					0x1025    //ɾ������Ա
#define    MINOR_ALARMHOST_ADD_NETUSER 				0x1026    //���Ӻ�˲���Ա
#define    MINOR_ALARMHOST_MOD_NETUSER_PARAM 		0x1027    //�޸ĺ�˲���Ա����
#define    MINOR_ALARMHOST_DEL_NETUSER 				0x1028    //ɾ����˲���Ա
#define    MINOR_ALARMHOST_ADD_OPERATORUSER 		0x1029    //����ǰ�˲���Ա
#define    MINOR_ALARMHOST_MOD_OPERATORUSER_PW 	0x102a    //�޸�ǰ�˲���Ա����
#define    MINOR_ALARMHOST_DEL_OPERATORUSER 		0x102b    //ɾ��ǰ�˲���Ա
#define    MINOR_ALARMHOST_ADD_KEYPADUSER 		0x102c    //���Ӽ���/�������û�	
#define    MINOR_ALARMHOST_DEL_KEYPADUSER 		0x102d    //ɾ������/�������û�	


#define    MINOR_ALARMHOST_MOD_HOST_CONFIG          0x1032    //�޸���������
#define    MINOR_ALARMHOST_RESTORE_BYPASS 			0x1033    //��·�ָ�

#define    MINOR_ALARMHOST_ALARMOUT_OPEN 			0x1034    //����������
#define    MINOR_ALARMHOST_ALARMOUT_CLOSE 			0x1035    //�������ر�
#define    MINOR_ALARMHOST_MOD_SUBSYSTEM_PARAM 	0x1036    //�޸���ϵͳ��������
#define    MINOR_ALARMHOST_GROUP_BYPASS 				0x1037    //����·
#define    MINOR_ALARMHOST_RESTORE_GROUP_BYPASS 	0x1038    //����·�ָ�
#define    MINOR_ALARMHOST_MOD_GRPS_PARAM 			0x1039    //�޸�GPRS����

#define    MINOR_ALARMHOST_MOD_REPORT_MOD 			0x103b    //�޸��ϴ���ʽ����
#define    MINOR_ALARMHOST_MOD_GATEWAY_PARAM 		0x103c    //�޸��Ž���������

#define    MINOR_STAY_ARM 	0x104c    //���ز���
#define    MINOR_QUICK_ARM 			0x104d    //��ʱ����
#define    MINOR_AUTOMATIC_ARM 		0x104e    //�Զ�����
#define    MINOR_AUTOMATIC_DISARM 		0x104f    //�Զ�����
#define    MINOR_KEYSWITCH_ARM 	0x1050  //Կ�ײ�������������
#define    MINOR_KEYSWITCH_DISARM	0x1051  //Կ�ײ�������������
#define    MINOR_CLEAR_ALARM	0x1052    //����
#define    MINOR_MOD_FAULT_CFG		0x1053  //�޸�ϵͳ��������
#define    MINOR_MOD_EVENT_TRIGGER_ALARMOUT_CFG	0x1054  //�޸��¼���������������
#define    MINOR_SEARCH_EXTERNAL_MODULE  0x1055 //�������ģ��
#define    MINOR_REGISTER_EXTERNAL_MODULE 0x1056  //����ע�����ģ��
#define    MINOR_CLOSE_KEYBOARD_ALARM	0x1057 //�رռ��̱�����ʾ��
#define    MINOR_MOD_3G_PARAM		0x1058  //�޸�3G����
#define    MINOR_MOD_PRINT_PARAM 0x1059  //�޸Ĵ�ӡ������
#define    MINOR_ALARMHOST_SD_CARD_FORMAT		0x1060    //SD����ʽ��
#define MINOR_ALARMHOST_SUBSYSTEM_UPGRADE  0x1061 //�Ӱ�̼�����

#define MINOR_PLAN_ARM_CFG     0x1062 //�ƻ���������������
#define MINOR_PHONE_ARM         0x1063 //�ֻ�����
#define MINOR_PHONE_STAY_ARM     0x1064 //�ֻ����ز���
#define MINOR_PHONE_QUICK_ARM   0x1065 //�ֻ���ʱ����
#define MINOR_PHONE_DISARM     0x1066 //�ֻ�����
#define MINOR_PHONE_CLEAR_ALARM     0x1067 //�ֻ�����
#define MINOR_WHITELIST_CFG     0x1068 //��Ȩ��������
#define MINOR_TIME_TRIGGER_CFG    0x1069 //��ʱ���ش���������
#define MINOR_CAPTRUE_CFG     0x106a //ץͼ��������
#define MINOR_TAMPER_CFG    0x106b //���������������

#define MINOR_REMOTE_KEYPAD_UPGRADE    0x106c //Զ����������
#define MINOR_ONETOUCH_AWAY_ARMING  0x106d //һ��������� 
#define MINOR_ONETOUCH_STAY_ARMING     0x106e //һ�����ز���
#define MINOR_SINGLE_PARTITION_ARMING_OR_DISARMING  0x106f //������������ 
#define MINOR_CARD_CONFIGURATION   0x1070 //����������
#define MINOR_CARD_ARMING_OR_DISARMING   0x1071 //ˢ��������
#define MINOR_EXPENDING_NETCENTER_CONFIGURATION    0x1072 //��չ������������
#define MINOR_NETCARD_CONFIGURATION    0x1073 //��������
#define MINOR_DDNS_CONFIGURATION      0x1074 //DDNS����
#define MINOR_RS485BUS_CONFIGURATION    0x1075 // 485���߲�������
#define MINOR_RS485BUS_RE_REGISTRATION     0x1076 //485��������ע��

#define MINOR_REMOTE_OPEN_ELECTRIC_LOCK 0x1077 //Զ�̴򿪵���
#define MINOR_REMOTE_CLOSE_ELECTRIC_LOCK 0x1078 //Զ�̹رյ���
#define MINOR_LOCAL_OPEN_ELECTRIC_LOCK 0x1079 //���ش򿪵���
#define MINOR_LOCAL_CLOSE_ELECTRIC_LOCK 0x107a //���عرյ���
#define MINOR_OPEN_ALARM_LAMP      0x107b //�򿪾���(Զ��)
#define MINOR_CLOSE_ALARM_LAMP      0x107c //�رվ���(Զ��)


#define MINOR_TEMPORARY_PASSWORD   0x107d //��ʱ���������¼

#define MINOR_HIDDNS_CONFIG     0x1082 // HIDDNS����
#define MINOR_REMOTE_KEYBOARD_UPDATA   0x1083 //Զ�̼���������־
#define MINOR_ZONE_ADD_DETECTOR      0x1084 //�������̽����
#define MINOR_ZONE_DELETE_DETECTOR    0x1085 //����ɾ��̽����
#define MINOR_QUERY_DETECTOR_SIGNAL   0x1086 //������ѯ̽�����ź�ǿ��
#define MINOR_QUERY_DETECTOR_BATTERY  0x1087 //������ѯ̽��������
#define MINOR_SET_DETECTOR_GUARD    0x1088 //̽��������
#define MINOR_SET_DETECTOR_UNGUARD   0x1089 //̽��������
#define MINOR_WIRELESS_CONFIGURATION     0x108a  //���߲�������
#define MINOR_OPEN_VOICE        0x108b //������
#define MINOR_CLOSE_VOICE       0x108c //�ر�����
#define MINOR_ENABLE_FUNCTION_KEY     0x108d //���ù��ܼ�
#define MINOR_DISABLE_FUNCTION_KEY      0x108e //�رչ��ܼ�
#define MINOR_READ_CARD    0x108f //Ѳ��ˢ��
#define MINOR_START_BROADCAST     0x1090 //�������㲥
#define MINOR_STOP_BROADCAST   0x1091 //�ر������㲥
#define MINOR_REMOTE_ZONE_MODULE_UPGRADE   0x1092 //Զ����������ģ��
#define MINOR_NETWORK_MODULE_EXTEND   0x1093 //����ģ���������
#define MINOR_ADD_CONTROLLER 	0x1094 //���ң�����û�
#define MINOR_DELETE_CONTORLLER 	0x1095 //ɾ��ң�����û�
#define MINOR_REMOTE_NETWORKMODULE_UPGRADE    0x1096 //Զ����������ģ��
#define MINOR_WIRELESS_OUTPUT_ADD   0x1097 //ע���������ģ��
#define MINOR_WIRELESS_OUTPUT_DEL    0x1098 //ɾ���������ģ��
#define MINOR_WIRELESS_REPEATER_ADD   0x1099 //ע�������м���
#define MINOR_WIRELESS_REPEATER_DEL    0x109a //ɾ�������м���
#define MINOR_PHONELIST_CFG     0x109b //�绰������������
#define MINOR_RF_SIGNAL_CHECK   0x109c // RF�źŲ�ѯ
#define MINOR_USB_UPGRADE   0x109d // USB����
#define MINOR_DOOR_TIME_REMINDER_CFG 0x109f //�ŴŶ�ʱ���Ѳ�������
#define MINOR_WIRELESS_SIREN_ADD    0x1100 //ע�����߾���
#define MINOR_WIRELESS_SIREN_DEL    0x1101 //ɾ�����߾���
#define MINOR_OUT_SCALE_OPEN    0x1102 //���翪��
#define MINOR_OUT_SCALE_CLOSE    0x1103 //����ر�

#define  MINOR_ALARMHOST_4G_MODULS_START  0x1108  //4Gģ������
#define  MINOR_ALARMHOST_4G_MODULS_STOP  0x1109  //4Gģ��ͣ��

#define  MINOR_EZVIZ_CLOUD_START   0x1110   //өʯ������
#define  MINOR_EZVIZ_CLOUD_STOP    0x1111   //өʯ��ͣ��
#define  MINOR_SIPUA_GRID_START    0x1112   //����B����
#define  MINOR_SIPUA_GRID_STOP    0x1113    //����Bͣ��

#define MINOR_MODBUS_FILE_DOWNLOAD   0x1114  //����modbusЭ�������ļ�
#define MINOR_MODBUS_FILE_UPLOAD      0x1115  //����modbusЭ�������ļ�

#define MINOR_RS485_DLL_FILE_DOWNLOAD   0x1116  //����485Э����ļ�
#define MINOR_RS485_DLL_FLIE_UPLOAD   0x1117  //����485Э����ļ�
#define MINOR_TX1_REBOOT              0x1118   //TX1ϵͳ��������



//2012-03-05 ITC������־����
#define MINOR_SET_TRIGGERMODE_CFG          0x1001    /*���ô���ģʽ����*/
#define MINOR_GET_TRIGGERMODE_CFG          0x1002    /*��ȡ����ģʽ����*/
#define MINOR_SET_IOOUT_CFG                0x1003    /*����IO�������*/
#define MINOR_GET_IOOUT_CFG                0x1004    /*��ȡIO�������*/
#define MINOR_GET_TRIGGERMODE_DEFAULT      0x1005    /*��ȡ����ģʽ�Ƽ�����*/
#define MINOR_GET_ITCSTATUS                0x1006    /*��ȡ״̬������*/
#define MINOR_SET_STATUS_DETECT_CFG        0x1007    /*����״̬������*/
#define MINOR_GET_STATUS_DETECT_CFG        0x1008    /*��ȡ״̬������*/
#define MINOR_SET_VIDEO_TRIGGERMODE_CFG    0x1009  /*������Ƶ����ģʽ����*/
#define MINOR_GET_VIDEO_TRIGGERMODE_CFG    0x100a   /*��ȡ��Ƶ����ģʽ����*/

//2013-04-19 ITS������־����
#define MINOR_LOCAL_ADD_CAR_INFO            0x2001  /*������ӳ�����Ϣ*/
#define MINOR_LOCAL_MOD_CAR_INFO            0x2002  /*�����޸ĳ�����Ϣ*/
#define MINOR_LOCAL_DEL_CAR_INFO            0x2003  /*����ɾ��������Ϣ*/
#define MINOR_LOCAL_FIND_CAR_INFO           0x2004  /*���ز��ҳ�����Ϣ*/
#define MINOR_LOCAL_ADD_MONITOR_INFO        0x2005  /*������Ӳ�����Ϣ*/
#define MINOR_LOCAL_MOD_MONITOR_INFO        0x2006  /*�����޸Ĳ�����Ϣ*/
#define MINOR_LOCAL_DEL_MONITOR_INFO        0x2007  /*����ɾ��������Ϣ*/
#define MINOR_LOCAL_FIND_MONITOR_INFO       0x2008  /*���ز�ѯ������Ϣ*/
#define MINOR_LOCAL_FIND_NORMAL_PASS_INFO   0x2009  /*���ز�ѯ����ͨ����Ϣ*/
#define MINOR_LOCAL_FIND_ABNORMAL_PASS_INFO 0x200a  /*���ز�ѯ�쳣ͨ����Ϣ*/
#define MINOR_LOCAL_FIND_PEDESTRIAN_PASS_INFO   0x200b  /*���ز�ѯ����ͨ����Ϣ*/
#define MINOR_LOCAL_PIC_PREVIEW             0x200c  /*����ͼƬԤ��*/
#define MINOR_LOCAL_SET_GATE_PARM_CFG       0x200d  /*���ñ������ó���ڲ���*/
#define MINOR_LOCAL_GET_GATE_PARM_CFG       0x200e  /*��ȡ�������ó���ڲ���*/
#define MINOR_LOCAL_SET_DATAUPLOAD_PARM_CFG 0x200f  /*���ñ������������ϴ�����*/
#define MINOR_LOCAL_GET_DATAUPLOAD_PARM_CFG 0x2010  /*��ȡ�������������ϴ�����*/

//2013-11-19������־����
#define MINOR_LOCAL_DEVICE_CONTROL                         0x2011   /*�����豸����(���ؿ���բ)*/
#define MINOR_LOCAL_ADD_EXTERNAL_DEVICE_INFO               0x2012   /*�����������豸��Ϣ */
#define MINOR_LOCAL_MOD_EXTERNAL_DEVICE_INFO               0x2013   /*�����޸�����豸��Ϣ */
#define MINOR_LOCAL_DEL_EXTERNAL_DEVICE_INFO               0x2014   /*����ɾ������豸��Ϣ */
#define MINOR_LOCAL_FIND_EXTERNAL_DEVICE_INFO              0x2015   /*���ز�ѯ����豸��Ϣ */
#define MINOR_LOCAL_ADD_CHARGE_RULE                        0x2016   /*��������շѹ��� */
#define MINOR_LOCAL_MOD_CHARGE_RULE                        0x2017   /*�����޸��շѹ��� */
#define MINOR_LOCAL_DEL_CHARGE_RULE                        0x2018   /*����ɾ���շѹ��� */
#define MINOR_LOCAL_FIND_CHARGE_RULE                       0x2019   /*���ز�ѯ�շѹ��� */
#define MINOR_LOCAL_COUNT_NORMAL_CURRENTINFO               0x2020   /*����ͳ������ͨ����Ϣ */
#define MINOR_LOCAL_EXPORT_NORMAL_CURRENTINFO_REPORT       0x2021   /*���ص�������ͨ����Ϣͳ�Ʊ��� */
#define MINOR_LOCAL_COUNT_ABNORMAL_CURRENTINFO             0x2022   /*����ͳ���쳣ͨ����Ϣ */
#define MINOR_LOCAL_EXPORT_ABNORMAL_CURRENTINFO_REPORT     0x2023   /*���ص����쳣ͨ����Ϣͳ�Ʊ��� */
#define MINOR_LOCAL_COUNT_PEDESTRIAN_CURRENTINFO           0x2024   /*����ͳ������ͨ����Ϣ */
#define MINOR_LOCAL_EXPORT_PEDESTRIAN_CURRENTINFO_REPORT   0x2025   /*���ص�������ͨ����Ϣͳ�Ʊ��� */
#define MINOR_LOCAL_FIND_CAR_CHARGEINFO                    0x2026   /*���ز�ѯ�����շ���Ϣ */
#define MINOR_LOCAL_COUNT_CAR_CHARGEINFO                   0x2027   /*����ͳ�ƹ����շ���Ϣ */
#define MINOR_LOCAL_EXPORT_CAR_CHARGEINFO_REPORT           0x2028   /*���ص��������շ���Ϣͳ�Ʊ��� */
#define MINOR_LOCAL_FIND_SHIFTINFO                         0x2029   /*���ز�ѯ���Ӱ���Ϣ */
#define MINOR_LOCAL_FIND_CARDINFO                          0x2030   /*���ز�ѯ��Ƭ��Ϣ */
#define MINOR_LOCAL_ADD_RELIEF_RULE                        0x2031   /*������Ӽ������ */
#define MINOR_LOCAL_MOD_RELIEF_RULE                        0x2032   /*�����޸ļ������ */
#define MINOR_LOCAL_DEL_RELIEF_RULE                        0x2033   /*����ɾ��������� */
#define MINOR_LOCAL_FIND_RELIEF_RULE                       0x2034   /*���ز�ѯ������� */
#define MINOR_LOCAL_GET_ENDETCFG                           0x2035   /*���ػ�ȡ����ڿ��ƻ����߼������ */ 
#define MINOR_LOCAL_SET_ENDETCFG                           0x2036   /*�������ó���ڿ��ƻ����߼������*/
#define MINOR_LOCAL_SET_ENDEV_ISSUEDDATA                   0x2037   /*�������ó���ڿ��ƻ��·���Ƭ��Ϣ */
#define MINOR_LOCAL_DEL_ENDEV_ISSUEDDATA                   0x2038   /*������ճ���ڿ��ƻ��·���Ƭ��Ϣ */

#define MINOR_REMOTE_DEVICE_CONTROL                        0x2101   /*Զ���豸����*/
#define MINOR_REMOTE_SET_GATE_PARM_CFG                     0x2102   /*����Զ�����ó���ڲ���*/
#define MINOR_REMOTE_GET_GATE_PARM_CFG                     0x2103   /*��ȡԶ�����ó���ڲ���*/
#define MINOR_REMOTE_SET_DATAUPLOAD_PARM_CFG               0x2104   /*����Զ�����������ϴ�����*/
#define MINOR_REMOTE_GET_DATAUPLOAD_PARM_CFG               0x2105   /*��ȡԶ�����������ϴ�����*/
#define MINOR_REMOTE_GET_BASE_INFO                         0x2106   /*Զ�̻�ȡ�ն˻�����Ϣ*/
#define MINOR_REMOTE_GET_OVERLAP_CFG                       0x2107   /*Զ�̻�ȡ�ַ����Ӳ�������*/              
#define MINOR_REMOTE_SET_OVERLAP_CFG                       0x2108   /*Զ�������ַ����Ӳ�������*/ 
#define MINOR_REMOTE_GET_ROAD_INFO                         0x2109   /*Զ�̻�ȡ·����Ϣ*/
#define MINOR_REMOTE_START_TRANSCHAN                       0x210a   /*Զ�̽���ͬ�����ݷ�����*/
#define MINOR_REMOTE_GET_ECTWORKSTATE                      0x210b   /*Զ�̻�ȡ������ն˹���״̬*/
#define MINOR_REMOTE_GET_ECTCHANINFO                       0x210c   /*Զ�̻�ȡ������ն�ͨ��״̬*/

//Զ�̿��� 2013-11-19
#define MINOR_REMOTE_ADD_EXTERNAL_DEVICE_INFO              0x210d   /*Զ���������豸��Ϣ */
#define MINOR_REMOTE_MOD_EXTERNAL_DEVICE_INFO              0x210e   /*Զ���޸�����豸��Ϣ */
#define MINOR_REMOTE_GET_ENDETCFG                          0x210f   /*Զ�̻�ȡ����ڿ��ƻ����߼������ */ 
#define MINOR_REMOTE_SET_ENDETCFG                          0x2110   /*Զ�����ó���ڿ��ƻ����߼������*/
#define MINOR_REMOTE_ENDEV_ISSUEDDATA                      0x2111   /*Զ�����ó���ڿ��ƻ��·���Ƭ��Ϣ */
#define MINOR_REMOTE_DEL_ENDEV_ISSUEDDATA                  0x2112   /*Զ����ճ���ڿ��ƻ��·���Ƭ��Ϣ */

//ITS 0x2115~0x2120 ͣ������λ��Ŀ
#define MINOR_REMOTE_ON_CTRL_LAMP           0x2115  /*����Զ�̿��Ƴ�λָʾ��*/       
#define MINOR_REMOTE_OFF_CTRL_LAMP          0x2116  /*�ر�Զ�̿��Ƴ�λָʾ��*/
//Netra3.1.0
#define MINOR_SET_VOICE_LEVEL_PARAM         0x2117  /*����������С */
#define MINOR_SET_VOICE_INTERCOM_PARAM      0x2118  /*��������¼�� */
#define MINOR_SET_INTELLIGENT_PARAM         0x2119  /*��������*/ 
#define MINOR_LOCAL_SET_RAID_SPEED          0x211a  /*��������raid�ٶ�*/
#define MINOR_REMOTE_SET_RAID_SPEED         0x211b /*Զ������raid�ٶ�*/
//Nerta3.1.2
#define MINOR_REMOTE_CREATE_STORAGE_POOL    0x211c   //Զ����Ӵ洢��
#define MINOR_REMOTE_DEL_STORAGE_POOL       0x211d    //Զ��ɾ���洢��

#define MINOR_REMOTE_DEL_PIC                0x2120   //Զ��ɾ��ͼƬ����
#define MINOR_REMOTE_DEL_RECORD             0x2121   //Զ��ɾ��¼������
#define MINOR_REMOTE_CLOUD_ENABLE           0x2123  //Զ��������ϵͳ����
#define MINOR_REMOTE_CLOUD_DISABLE          0x2124  //Զ��������ϵͳ����
#define MINOR_REMOTE_CLOUD_MODIFY_PARAM     0x2125  //Զ���޸Ĵ洢�ز���
#define MINOR_REMOTE_CLOUD_MODIFY_VOLUME    0x2126  //Զ���޸Ĵ洢������
#define MINOR_REMOTE_GET_GB28181_SERVICE_PARAM    0x2127  //Զ�̻�ȡGB28181�������
#define MINOR_REMOTE_SET_GB28181_SERVICE_PARAM    0x2128  //Զ������GB28181�������
#define MINOR_LOCAL_GET_GB28181_SERVICE_PARAM     0x2129  //���ػ�ȡGB28181�������
#define MINOR_LOCAL_SET_GB28181_SERVICE_PARAM     0x212a  //��������B28181�������
#define MINOR_REMOTE_SET_SIP_SERVER               0x212b  //Զ������SIP SERVER
#define MINOR_LOCAL_SET_SIP_SERVER                0x212c  //��������SIP SERVER
#define MINOR_LOCAL_BLACKWHITEFILE_OUTPUT         0x212d  //���غ���Ȩ��������
#define MINOR_LOCAL_BLACKWHITEFILE_INPUT          0x212e  //���غ���Ȩ��������
#define MINOR_REMOTE_BALCKWHITECFGFILE_OUTPUT     0x212f  //Զ�̺���Ȩ��������
#define MINOR_REMOTE_BALCKWHITECFGFILE_INPUT      0x2130  //Զ�̺���Ȩ��������


#define MINOR_REMOTE_CREATE_MOD_VIEWLIB_SPACE        0x2200    /*Զ�̴���/�޸���ͼ��ռ�*/
#define MINOR_REMOTE_DELETE_VIEWLIB_FILE            0x2201    /*Զ��ɾ����ͼ���ļ�*/
#define MINOR_REMOTE_DOWNLOAD_VIEWLIB_FILE            0x2202    /*Զ��������ͼ���ļ�*/
#define MINOR_REMOTE_UPLOAD_VIEWLIB_FILE            0x2203    /*Զ���ϴ���ͼ���ļ�*/
#define MINOR_LOCAL_CREATE_MOD_VIEWLIB_SPACE        0x2204    /*���ش���/�޸���ͼ��ռ�*/

#define MINOR_LOCAL_SET_DEVICE_ACTIVE   0x3000  //���ؼ����豸
#define MINOR_REMOTE_SET_DEVICE_ACTIVE  0x3001  //Զ�̼����豸
#define MINOR_LOCAL_PARA_FACTORY_DEFAULT    0x3002  //���ػظ���������
#define MINOR_REMOTE_PARA_FACTORY_DEFAULT   0x3003  //Զ�ָ̻���������

/*��Ϣ����������������־*/
#define MINOR_UPLAOD_STATIC_MATERIAL                0x2401  //��̬�ز��ϴ�
#define MINOR_UPLOAD_DYNAMIC_MATERIAL               0x2402  //��̬�ز��ϴ�
#define MINOR_DELETE_MATERIAL                       0x2403  //ɾ���ز�
#define MINOR_DOWNLOAD_STATIC_MATERIAL              0x2404  //��̬�ز�����
#define MINOR_COVER_STATIC_MATERIAL                 0x2405  //��̬�زĸ���
#define MINOR_APPROVE_MATERIAL                      0x2406  //�ز����
#define MINOR_UPLAOD_PROGRAM                        0x2407  //�ϴ���Ŀ
#define MINOR_DOWNLOAD_PROGRAM                      0x2408  //���ؽ�Ŀ
#define MINOR_DELETE_PROGRAM                        0x2409  //ɾ����Ŀ
#define MINOR_MODIFY_PROGRAM                        0x240a  //��Ŀ�����޸�
#define MINOR_APPROVE_PRAGRAM                       0x240b  //��Ŀ���
#define MINOR_UPLAOD_SCHEDULE                       0x240c  //�ϴ��ճ�
#define MINOR_DOWNLOAD_SCHEDULE                     0x240d  //�����ճ�
#define MINOR_DELETE_SCHEDULE                       0x240e  //ɾ���ճ�
#define MINOR_MODIFY_SCHEDULE                       0x240f  //�޸��ճ�����
#define MINOR_RELEASE_SCHEDULE                      0x2410  //�����ճ�
#define MINOR_ADD_TERMINAL                          0x2411  //����ն�
#define MINOR_DELETE_TERMINAL                       0x2412  //ɾ���ն�
#define MINOR_MODIFY_TERMIANL_PARAM                 0x2413  //�޸��ն˲���            
#define MINOR_MODIFY_TERMIANL_PLAY_PARAM            0x2414  //�����ն˲��Ų���
#define MINOR_ADD_TERMIANL_GROUP                    0x2415  //����ն���
#define MINOR_MODIFY_TERMINAL_GROUP_PARAM           0x2416  //�޸��ն������
#define MINOR_DELETE_TERMIANL_GROUP                 0x2417  //ɾ���ն���
#define MINOR_TERMINAL_PLAY_CONTROL                 0x2418  //�ն˲��ſ���
#define MINOR_TERMINAL_ON_OFF_LINE                  0x2419  //�ն�������
#define MINOR_SET_SWITCH_PLAN                       0x241a  //�����ն˶�ʱ���ػ��ƻ�
#define MINOR_SET_VOLUME_PLAN                       0x241b  //�����ն˶�ʱ�����ƻ�
#define MINOR_TERMINAL_SCREENSHOT                   0x241c  //�ն˽���
#define MINOR_SYSTEM_TIME_CFG                       0x241d   //ϵͳУʱ
#define MINOR_ADD_USER_CFG                          0x241e    //����û�����
#define MINOR_DEL_USER_CFG                          0x241f    //ɾ���û�����
#define MINOR_REMOTE_MANAGE_HDD                     0x2420   //Զ�̱༭Ӳ��
#define MINOR_TERMINAL_UPDATE_START                 0x2421   //�ն�����
#define MINOR_SVR_RESTORE_DEFAULT_PARAM             0x2422   //������Զ�ָ̻�Ĭ��
#define MINOR_SVR_REMOTE_RESTORE_FACTORY            0x2423   //������Զ�ָ̻���������
#define MINOR_SVR_REMOTE_REBOOT                     0x2424   //������Զ������
#define MINOR_SVR_MODIFY_NETWORK_PARAM              0x2425   //��������������޸�
#define MINOR_SVR_SOFTWARE_UPGRADE                  0x2426     //�������������

#define MINOR_BACKUP_DATA                           0xc41  //���ݱ���
#define MINOR_TRANSFER_DATA                         0xc42  //����Ǩ��
#define MINOR_RESTORE_DATA                          0xc43  //���ݻ�ԭ
#define MINOR_SET_INPUT_PLAN                        0xc44 //�����ն˶�ʱ�����л��ƻ�
#define MINOR_TERMINAL_ADB                          0xc45//�ն�ADB����
#define MINOR_TERMINAL_VOLUME                       0xc46  //�ն���������
#define MINOR_TERMINAL_LOGO                         0xc47 //�ն�LOGO����
#define MINOR_TERMINAL_DEFAULT_SCHEDULE             0xc48 //��Ƭ�ճ�ʹ��
#define MINOR_TERMINAL_PASSWORD                     0xc49 //�����ն�����
#define MINOR_TERMINAL_IP                           0xc4a//�ն�IP����
#define MINOR_TERMINAL_RELATE_IPC                   0xc4b//�ն˹���IPC
#define MINOR_TERMINAL_SERVER                       0xc4c//�ն˹�������������
#define MINOR_TERMINAL_SADP                         0xc4d//�ն�SADP��������
#define MINOR_TERMINAL_TIMEZONE                     0xc4e//�ն�ʱ������
#define MINOR_TERMINAL_TEMP_PROTECT                 0xc4f//�ն��¶ȱ�������
#define MINOR_ADD_ORGANIZATION                      0xc50 //�����֯
#define MINOR_DELETE_ORGANIZATION                   0xc51 //ɾ����֯
#define MINOR_MODIFY_ORGANIZATION                   0xc52 //�޸���֯ 
#define MINOR_WEATHER_FACTORY                       0xc53 //������������ 
#define MINOR_SADP_ENABLE                           0xc54 //sadp�������� 
#define MINOR_SSH_ENABLE                            0xc55 //SSH��������
#define MINOR_MODIFY_MATERIAL                       0xc56 //�زĲ����޸� 
#define MINOR_INSERT_CHARACTER                      0xc57 //�岥������Ϣ
#define MINOR_TERMINAL_BACKLIGHT                    0xc58 //�ն˱�������



#define MINOR_REMOTE_CONFERENCE_CONFIG             0x2501  //MCU��������
#define MINOR_REMOTE_TERMINAL_CONFIG               0x2502  //MCU�ն�����
#define MINOR_REMOTE_GROUP_CONFIG                  0x2503  //MCU��������
#define MINOR_REMOTE_CONFERENCE_CTRL               0x2504  //MCU�������
#define MINOR_REMOTE_TERMINAL_CTRL                 0x2505  //MCU�ն˿���

//NVR���
#define MINOR_LOCAL_RESET_LOGIN_PASSWORD           0x2600    /* ��������admin��½����*/ 
#define MINOR_REMOTE_RESET_LOGIN_PASSWORD          0x2601    /* Զ������admin��¼���� */
#define MINOR_LOCAL_FACE_BASE_CREATE        0x2602    /* ���������Աȿⴴ��*/ 
#define MINOR_REMOTE_FACE_BASE_CREATE       0x2603    /* Զ�������Աȿⴴ��*/
#define MINOR_LOCAL_FACE_BASE_MODIFY        0x2604    /* ���������Աȿ��޸�*/ 
#define MINOR_REMOTE_FACE_BASE_MODIFY      0x2605    /* Զ�������Աȿ��޸�*/
#define MINOR_LOCAL_FACE_BASE_DELETE        0x2606    /* ���������Աȿ�ɾ��*/ 
#define MINOR_REMOTE_FACE_BASE_DELETE       0x2607    /* Զ�������Աȿ�ɾ��*/
#define MINOR_LOCAL_FACE_DATA_APPEND        0x2608    /* ����¼����������*/ 
#define MINOR_REMOTE_FACE_DATA_APPEND       0x2609    /* Զ��¼����������*/
#define MINOR_LOCAL_FACE_DATA_SEARCH       0x2610    /* ���������ȶ����ݲ���*/ 
#define MINOR_REMOTE_FACE_DATA_SEARCH       0x2611    /* Զ�������ȶ����ݲ���*/
#define MINOR_LOCAL_FACE_DATA_ANALYSIS        0x2612    /* ����ͼƬ��������*/ 
#define MINOR_REMOTE_FACE_DATA_ANALYSIS       0x2613    /* Զ��ͼƬ��������*/
#define MINOR_LOCAL_FACE_DATA_EDIT            0x2614    /* �������������޸�*/ 
#define MINOR_REMOTE_FACE_DATA_EDIT           0x2615    /* Զ�����������޸�*/

#define MINOR_LOCAL_FACE_DATA_DELETE          0x2616    /* ������������ɾ��*/ 
#define MINOR_REMOTE_FACE_DATA_DELET          0x2617    /* Զ����������ɾ��*/

#define MINOR_LOCAL_VCA_ANALYSIS_CFG          0x2618    /* �������ܷ�������*/ 
#define MINOR_REMOTE_VCA_ANALYSIS_CFG         0x2619    /* Զ�����ܷ�������*/

#define MINOR_LOCAL_FACE_BASE_IMPORT          0x261a    /* ���ص���������*/ 
#define MINOR_LOCAL_FACE_BASE_EXPORT          0x261b    /* ���ص���������*/
//NVR��Ⱥ
#define MINOR_REMOTE_CLUSTER_MODE_CONFIG      0x261c    /* Զ�̼�Ⱥģʽ���ò���*/
#define MINOR_LOCAL_CLUSTER_MODE_CONFIG       0x261d    /* ���ؼ�Ⱥģʽ���ò���*/
#define MINOR_REMOTE_CLUSTER_NETWORK_CONFIG   0x261e    /* Զ�̼�Ⱥ�������ò���*/
#define MINOR_LOCAL_CLUSTER_NETWORK_CONFIG    0x261f    /* ���ؼ�Ⱥ�������ò���*/
#define MINOR_REMOTE_CLUSTER_ADD_DEVICE       0x2620    /* Զ�̼�Ⱥ����豸����*/
#define MINOR_LOCAL_CLUSTER_ADD_DEVICE        0x2621    /* ���ؼ�Ⱥ����豸����*/
#define MINOR_REMOTE_CLUSTER_DEL_DEVICE       0x2622    /* Զ�̼�Ⱥɾ���豸����*/
#define MINOR_LOCAL_CLUSTER_DEL_DEVICE        0x2623    /* ���ؼ�Ⱥɾ���豸����*/
#define MINOR_REMOTE_HFPD_CFG                 0x2624  /* Զ�̸�Ƶ��Ա�������*/
#define MINOR_REMOTE_FACE_CONTRAST_TASK       0x2625 /* Զ�������ȶ��������� */

#define MINOR_REMOTE_IOTCFGFILE_INPUT         0x2627//Զ�̵���IOT�����ļ�
#define MINOR_REMOTE_IOTCFGFILE_OUTPUT        0x2628//Զ�̵���IOT�����ļ�
#define MINOR_LOCAL_IOT_ADD                   0x2629//�������IOTͨ��
#define MINOR_REMOTE_IOT_ADD                  0x262a//Զ�����IOTͨ��
#define MINOR_LOCAL_IOT_DEL                   0x262b//����ɾ��IOTͨ��
#define MINOR_REMOTE_IOT_DEL                  0x262c//Զ��ɾ��IOTͨ��
#define MINOR_LOCAL_IOT_SET                   0x262d//��������IOTͨ��
#define MINOR_REMOTE_IOT_SET                  0x262e//Զ������IOTͨ��
#define MINOR_LOCAL_IOTCFGFILE_INPUT          0x262f //���ص���IOT�����ļ�
#define MINOR_LOCAL_IOTCFGFILE_OUTPUT         0x2630//���ص���IOT�����ļ�
#define MINOR_LOCAL_VAD_CFG                   0x2631  /* ����������������*/
#define MINOR_REMOTE_VAD_CFG                  0x2632  /* Զ��������������*/
#define MINOR_LOCAL_ADDRESS_FILTER_CONFIG     0x2633    /* ���ص�ַ��������*/ 
#define MINOR_REMOTE_ADDRESS_FILTER_CONFIG    0x2634    /* Զ�̵�ַ��������*/
#define MINOR_LOCAL_POE_CFG                     0x2635    /* ����POE����*/ 
#define MINOR_REMOTE_POE_CFG                    0x2636    /* Զ��POE����*/
#define MINOR_LOCAL_RESET_CHANNEL_PASSWORD        0x2637    /* ��������ͨ������*/ 
#define MINOR_REMOTE_RESET_CHANNEL_PASSWORD       0x2638    /* Զ������ͨ������*/


/*��־������Ϣ*/
//������
#define MAJOR_INFORMATION               0x4     /*������Ϣ*/
//������
#define MINOR_HDD_INFO                  0xa1 /*Ӳ����Ϣ*/
#define MINOR_SMART_INFO                0xa2 /*SMART��Ϣ*/
#define MINOR_REC_START                 0xa3 /*��ʼ¼��*/
#define MINOR_REC_STOP                  0xa4 /*ֹͣ¼��*/
#define MINOR_REC_OVERDUE                0xa5 /*����¼��ɾ��*/
#define MINOR_LINK_START                0xa6 //����ǰ���豸
#define MINOR_LINK_STOP                    0xa7 //�Ͽ�ǰ���豸��
#define MINOR_NET_DISK_INFO                0xa8 //����Ӳ����Ϣ
#define MINOR_RAID_INFO                 0xa9 //raid�����Ϣ
#define MINOR_RUN_STATUS_INFO           0xaa /*ϵͳ����״̬��Ϣ*/

//Netra3.0.0
#define MINOR_SPARE_START_BACKUP        0xab   /*�ȱ�ϵͳ��ʼ����ָ��������*/
#define MINOR_SPARE_STOP_BACKUP            0xac   /*�ȱ�ϵͳֹͣ����ָ��������*/
#define MINOR_SPARE_CLIENT_INFO         0xad   /*�ȱ��ͻ�����Ϣ*/
#define MINOR_ANR_RECORD_START            0xae   /*ANR¼��ʼ*/
#define MINOR_ANR_RECORD_END            0xaf   /*ANR¼�����*/
#define MINOR_ANR_ADD_TIME_QUANTUM        0xb0    /*ANR���ʱ���*/
#define MINOR_ANR_DEL_TIME_QUANTUM        0xb1    /*ANRɾ��ʱ���*/

#define MINOR_PIC_REC_START             0xb3  /* ��ʼץͼ*/    
#define MINOR_PIC_REC_STOP              0xb4  /* ֹͣץͼ*/
#define MINOR_PIC_REC_OVERDUE           0xb5  /* ����ͼƬ�ļ�ɾ�� */
//Netra3.1.0
#define  MINOR_CLIENT_LOGIN             0xb6   /*��¼�������ɹ�*/
#define  MINOR_CLIENT_RELOGIN            0xb7   /*���µ�¼������*/
#define  MINOR_CLIENT_LOGOUT            0xb8   /*�˳��������ɹ�*/
#define  MINOR_CLIENT_SYNC_START        0xb9   /*¼��ͬ����ʼ*/
#define  MINOR_CLIENT_SYNC_STOP            0xba   /*¼��ͬ����ֹ*/
#define  MINOR_CLIENT_SYNC_SUCC            0xbb   /*¼��ͬ���ɹ�*/
#define  MINOR_CLIENT_SYNC_EXCP            0xbc   /*¼��ͬ���쳣*/
#define  MINOR_GLOBAL_RECORD_ERR_INFO   0xbd   /*ȫ�ִ����¼��Ϣ*/
#define  MINOR_BUFFER_STATE             0xbe   /*������״̬��־��¼*/
#define  MINOR_DISK_ERRORINFO_V2        0xbf   /*Ӳ�̴�����ϸ��ϢV2*/
#define  MINOR_CS_DATA_EXPIRED          0xc0   //�ƴ洢���ݹ���
#define  MINOR_PLAT_INFO                0xc1   //ƽ̨������Ϣ
#define  MINOR_DIAL_STAT                0xc2   /*����״̬*/

#define MINOR_UNLOCK_RECORD             0xc3   //������¼
#define MINOR_VIS_ALARM                 0xc4   //��������
#define MINOR_TALK_RECORD               0xc5   //ͨ����¼
#define MINOR_ACCESSORIES_MESSAGE       0xc6 //�������Ϣ
#define MINOR_KMS_EXPAMSION_DISK_INSERT 0xc7// KMS�����̲���
#define MINOR_IPC_CONNECT               0xc8//  IPC������Ϣ
#define MINOR_INTELLIGENT_BOARD_STATUS  0xc9//  ���ܰ�״̬
#define MINOR_IPC_CONNECT_STATUS        0xca//  IPC����״̬
#define MINOR_AUTO_TIMING               0xcb  //�Զ�Уʱ
#define MINOR_EZVIZ_OPERATION           0xcc   //өʯ����״̬
//NVR��Ⱥ
#define MINOR_CLUSTER_DEVICE_ONLINE     0xcd   //��Ⱥ�豸����
#define MINOR_CLUSTER_MGR_SERVICE_STARTUP 0xce   //��Ⱥ�����������
#define MINOR_CLUSTER_BUSINESS_TRANSFER 0xcf   //��Ⱥҵ��Ǩ��
#define MINOR_CLUSTER_STATUS            0xd0   //��Ⱥ״̬��Ϣ
#define MINOR_CLUSTER_CS_STATUS            0xd1   //��ȺCS��CM�����豸״̬ʧ�ܣ���¼CS��CM��IP��ַ
#define MINOR_CLUSTER_CM_STATUS            0xd2   //CM״̬�л�����¼CMת��Ľ�ɫ����leader��follower��candidate
#define MINOR_VOICE_START_DETECTED         0xd3  /*��⵽������ʼ*/
#define MINOR_VOICE_END_DETECTED           0xd4  /*��⵽��������*/
#define MINOR_DOUBLE_VERIFICATION_PASS     0xd5  /*������֤ͨ��*/

/*�¼�*/
//������
#define MAJOR_EVENT                             0x5     /*�¼�*/
//������
#define MINOR_LEGAL_CARD_PASS                   0x01    //�Ϸ�����֤ͨ��
#define MINOR_CARD_AND_PSW_PASS                 0x02    //ˢ����������֤ͨ��
#define MINOR_CARD_AND_PSW_FAIL                 0x03    //ˢ����������֤ʧ��
#define MINOR_CARD_AND_PSW_TIMEOUT              0x04    //������������֤��ʱ
#define MINOR_CARD_AND_PSW_OVER_TIME            0x05    //ˢ�������볬��
#define MINOR_CARD_NO_RIGHT                     0x06    //δ����Ȩ��
#define MINOR_CARD_INVALID_PERIOD               0x07    //��Чʱ��
#define MINOR_CARD_OUT_OF_DATE                  0x08    //���Ź���
#define MINOR_INVALID_CARD                      0x09    //�޴˿���
#define MINOR_ANTI_SNEAK_FAIL                   0x0a    //��Ǳ����֤ʧ��
#define MINOR_INTERLOCK_DOOR_NOT_CLOSE          0x0b    //������δ�ر�
#define MINOR_NOT_BELONG_MULTI_GROUP            0x0c    //�������ڶ�����֤Ⱥ��
#define MINOR_INVALID_MULTI_VERIFY_PERIOD       0x0d    //�����ڶ�����֤ʱ�����
#define MINOR_MULTI_VERIFY_SUPER_RIGHT_FAIL     0x0e    //������֤ģʽ����Ȩ����֤ʧ��
#define MINOR_MULTI_VERIFY_REMOTE_RIGHT_FAIL    0x0f    //������֤ģʽԶ����֤ʧ��
#define MINOR_MULTI_VERIFY_SUCCESS              0x10    //������֤�ɹ�
#define MINOR_LEADER_CARD_OPEN_BEGIN            0x11    //�׿����ſ�ʼ
#define MINOR_LEADER_CARD_OPEN_END              0x12    //�׿����Ž���
#define MINOR_ALWAYS_OPEN_BEGIN                 0x13    //����״̬��ʼ
#define MINOR_ALWAYS_OPEN_END                   0x14    //����״̬����
#define MINOR_LOCK_OPEN                         0x15    //������
#define MINOR_LOCK_CLOSE                        0x16    //�����ر�
#define MINOR_DOOR_BUTTON_PRESS                 0x17    //���Ű�ť��
#define MINOR_DOOR_BUTTON_RELEASE               0x18    //���Ű�ť�ſ�
#define MINOR_DOOR_OPEN_NORMAL                  0x19    //�������ţ��Ŵţ�
#define MINOR_DOOR_CLOSE_NORMAL                 0x1a    //�������ţ��Ŵţ�
#define MINOR_DOOR_OPEN_ABNORMAL                0x1b    //���쳣�򿪣��Ŵţ�
#define MINOR_DOOR_OPEN_TIMEOUT                 0x1c    //�Ŵ򿪳�ʱ���Ŵţ�
#define MINOR_ALARMOUT_ON                       0x1d    //���������
#define MINOR_ALARMOUT_OFF                      0x1e    //��������ر�
#define MINOR_ALWAYS_CLOSE_BEGIN                0x1f    //����״̬��ʼ
#define MINOR_ALWAYS_CLOSE_END                  0x20    //����״̬����                          
#define MINOR_MULTI_VERIFY_NEED_REMOTE_OPEN     0x21    //���ض�����֤��ҪԶ�̿���
#define MINOR_MULTI_VERIFY_SUPERPASSWD_VERIFY_SUCCESS  0x22  //������֤����������֤�ɹ��¼�
#define MINOR_MULTI_VERIFY_REPEAT_VERIFY        0x23    //������֤�ظ���֤�¼�
#define MINOR_MULTI_VERIFY_TIMEOUT               0x24    //������֤�ظ���֤�¼�
#define MINOR_DOORBELL_RINGING                  0x25    //������
#define MINOR_FINGERPRINT_COMPARE_PASS          0x26    //ָ�Ʊȶ�ͨ��
#define MINOR_FINGERPRINT_COMPARE_FAIL          0x27    //ָ�Ʊȶ�ʧ��
#define MINOR_CARD_FINGERPRINT_VERIFY_PASS              0x28    //ˢ����ָ����֤ͨ��
#define MINOR_CARD_FINGERPRINT_VERIFY_FAIL              0x29    //ˢ����ָ����֤ʧ��
#define MINOR_CARD_FINGERPRINT_VERIFY_TIMEOUT           0x2a    //ˢ����ָ����֤��ʱ
#define MINOR_CARD_FINGERPRINT_PASSWD_VERIFY_PASS       0x2b    //ˢ����ָ�Ƽ�������֤ͨ��
#define MINOR_CARD_FINGERPRINT_PASSWD_VERIFY_FAIL       0x2c    //ˢ����ָ�Ƽ�������֤ʧ��
#define MINOR_CARD_FINGERPRINT_PASSWD_VERIFY_TIMEOUT    0x2d    //ˢ����ָ�Ƽ�������֤��ʱ
#define MINOR_FINGERPRINT_PASSWD_VERIFY_PASS            0x2e    //ָ�Ƽ�������֤ͨ��
#define MINOR_FINGERPRINT_PASSWD_VERIFY_FAIL            0x2f    //ָ�Ƽ�������֤ʧ��
#define MINOR_FINGERPRINT_PASSWD_VERIFY_TIMEOUT         0x30    //ָ�Ƽ�������֤��ʱ
#define MINOR_FINGERPRINT_INEXISTENCE                   0x31    //ָ�Ʋ�����
#define MINOR_CARD_PLATFORM_VERIFY                      0x32    //ˢ��ƽ̨��֤
#define MINOR_CALL_CENTER                               0x33    //���������¼�
#define MINOR_FIRE_RELAY_TURN_ON_DOOR_ALWAYS_OPEN       0x34    //�����̵�����ͨ�����ų���
#define MINOR_FIRE_RELAY_RECOVER_DOOR_RECOVER_NORMAL    0x35   //�����̵����ָ��Żָ�����
#define MINOR_FACE_AND_FP_VERIFY_PASS                   0x36    //������ָ����֤ͨ��
#define MINOR_FACE_AND_FP_VERIFY_FAIL                   0x37    //������ָ����֤ʧ��
#define MINOR_FACE_AND_FP_VERIFY_TIMEOUT                0x38    //������ָ����֤��ʱ
#define MINOR_FACE_AND_PW_VERIFY_PASS                   0x39    //������������֤ͨ��
#define MINOR_FACE_AND_PW_VERIFY_FAIL                   0x3a    //������������֤ʧ��
#define MINOR_FACE_AND_PW_VERIFY_TIMEOUT                0x3b    //������������֤��ʱ
#define MINOR_FACE_AND_CARD_VERIFY_PASS                 0x3c    //������ˢ����֤ͨ��
#define MINOR_FACE_AND_CARD_VERIFY_FAIL                 0x3d    //������ˢ����֤ʧ��
#define MINOR_FACE_AND_CARD_VERIFY_TIMEOUT              0x3e    //������ˢ����֤��ʱ
#define MINOR_FACE_AND_PW_AND_FP_VERIFY_PASS            0x3f    //�����������ָ����֤ͨ��
#define MINOR_FACE_AND_PW_AND_FP_VERIFY_FAIL            0x40    //�����������ָ����֤ʧ��
#define MINOR_FACE_AND_PW_AND_FP_VERIFY_TIMEOUT         0x41    //�����������ָ����֤��ʱ
#define MINOR_FACE_CARD_AND_FP_VERIFY_PASS              0x42    //������ˢ����ָ����֤ͨ��
#define MINOR_FACE_CARD_AND_FP_VERIFY_FAIL              0x43    //������ˢ����ָ����֤ʧ��
#define MINOR_FACE_CARD_AND_FP_VERIFY_TIMEOUT           0x44    //������ˢ����ָ����֤��ʱ
#define MINOR_EMPLOYEENO_AND_FP_VERIFY_PASS             0x45    //���ż�ָ����֤ͨ��
#define MINOR_EMPLOYEENO_AND_FP_VERIFY_FAIL             0x46    //���ż�ָ����֤ʧ��
#define MINOR_EMPLOYEENO_AND_FP_VERIFY_TIMEOUT          0x47    //���ż�ָ����֤��ʱ
#define MINOR_EMPLOYEENO_AND_FP_AND_PW_VERIFY_PASS      0x48    //���ż�ָ�Ƽ�������֤ͨ��
#define MINOR_EMPLOYEENO_AND_FP_AND_PW_VERIFY_FAIL      0x49    //���ż�ָ�Ƽ�������֤ʧ��
#define MINOR_EMPLOYEENO_AND_FP_AND_PW_VERIFY_TIMEOUT   0x4a    //���ż�ָ�Ƽ�������֤��ʱ
#define MINOR_FACE_VERIFY_PASS                          0x4b    //������֤ͨ��
#define MINOR_FACE_VERIFY_FAIL                          0x4c    //������֤ʧ��
#define MINOR_EMPLOYEENO_AND_FACE_VERIFY_PASS           0x4d    //���ż�������֤ͨ��
#define MINOR_EMPLOYEENO_AND_FACE_VERIFY_FAIL           0x4e    //���ż�������֤ʧ��
#define MINOR_EMPLOYEENO_AND_FACE_VERIFY_TIMEOUT        0x4f    //���ż�������֤��ʱ
#define MINOR_FACE_RECOGNIZE_FAIL                       0x50    //Ŀ��ʶ��ʧ��
#define MINOR_FIRSTCARD_AUTHORIZE_BEGIN                    0x51    //�׿���Ȩ��ʼ
#define MINOR_FIRSTCARD_AUTHORIZE_END                    0x52    //�׿���Ȩ����
#define MINOR_DOORLOCK_INPUT_SHORT_CIRCUIT                0x53    //���������·����
#define MINOR_DOORLOCK_INPUT_BROKEN_CIRCUIT                0x54    //���������·����
#define MINOR_DOORLOCK_INPUT_EXCEPTION                    0x55    //���������쳣����
#define MINOR_DOORCONTACT_INPUT_SHORT_CIRCUIT            0x56    //�Ŵ������·����
#define MINOR_DOORCONTACT_INPUT_BROKEN_CIRCUIT            0x57    //�Ŵ������·����
#define MINOR_DOORCONTACT_INPUT_EXCEPTION                0x58    //�Ŵ������쳣����
#define MINOR_OPENBUTTON_INPUT_SHORT_CIRCUIT            0x59    //���Ű�ť�����·����
#define MINOR_OPENBUTTON_INPUT_BROKEN_CIRCUIT            0x5a    //���Ű�ť�����·����
#define MINOR_OPENBUTTON_INPUT_EXCEPTION                0x5b    //���Ű�ť�����쳣����
#define MINOR_DOORLOCK_OPEN_EXCEPTION                    0x5c    //�����쳣��
#define MINOR_DOORLOCK_OPEN_TIMEOUT                        0x5d    //�����򿪳�ʱ
#define MINOR_FIRSTCARD_OPEN_WITHOUT_AUTHORIZE            0x5e    //�׿�δ��Ȩ����ʧ��
#define MINOR_CALL_LADDER_RELAY_BREAK                   0x5f    //���ݼ̵����Ͽ�
#define MINOR_CALL_LADDER_RELAY_CLOSE                   0x60    //���ݼ̵����պ�
#define MINOR_AUTO_KEY_RELAY_BREAK                      0x61    //�Զ������̵����Ͽ�
#define MINOR_AUTO_KEY_RELAY_CLOSE                      0x62    //�Զ������̵����պ�
#define MINOR_KEY_CONTROL_RELAY_BREAK                   0x63    //�����ݿؼ̵����Ͽ�
#define MINOR_KEY_CONTROL_RELAY_CLOSE                   0x64    //�����ݿؼ̵����պ�
#define MINOR_EMPLOYEENO_AND_PW_PASS                    0x65    //���ż�������֤ͨ��
#define MINOR_EMPLOYEENO_AND_PW_FAIL                    0x66    //���ż�������֤ʧ��
#define MINOR_EMPLOYEENO_AND_PW_TIMEOUT                 0x67    //���ż�������֤��ʱ
#define MINOR_HUMAN_DETECT_FAIL                         0x68    //���˼��ʧ��
#define MINOR_PEOPLE_AND_ID_CARD_COMPARE_PASS           0x69    //��֤�ȶ�ͨ��
#define MINOR_PEOPLE_AND_ID_CARD_COMPARE_FAIL           0x70    //��֤�ȶ�ʧ��
#define MINOR_CERTIFICATE_BLACK_LIST                    0x71    //����Ȩ�����¼�
#define MINOR_LEGAL_MESSAGE                             0x72    //�Ϸ�����
#define MINOR_ILLEGAL_MESSAGE                           0x73    //�Ƿ�����
#define MINOR_MAC_DETECT                                0x74    //MAC���
#define MINOR_DOOR_OPEN_OR_DORMANT_FAIL                 0x75   //��״̬���ջ�����״̬��֤ʧ��
#define MINOR_AUTH_PLAN_DORMANT_FAIL                    0x76   //��֤�ƻ�����ģʽ��֤ʧ��
#define MINOR_CARD_ENCRYPT_VERIFY_FAIL                  0x77   //������У��ʧ��
#define MINOR_SUBMARINEBACK_REPLY_FAIL                  0x78   //��Ǳ�ط�����Ӧ��ʧ��
#define MINOR_DOOR_OPEN_OR_DORMANT_OPEN_FAIL            0x82   //�ų��ջ�����ʱ���Ű�ť����ʧ��
#define MINOR_HEART_BEAT                                0x83    //�����¼�
#define MINOR_DOOR_OPEN_OR_DORMANT_LINKAGE_OPEN_FAIL    0x84   //�ų��ջ�����ʱ������������ʧ��
#define MINOR_TRAILING                                  0x85   //β��ͨ��
#define MINOR_REVERSE_ACCESS                            0x86   //������
#define MINOR_FORCE_ACCESS                              0x87   //������ײ
#define MINOR_CLIMBING_OVER_GATE                        0x88   //��Խ
#define MINOR_PASSING_TIMEOUT                           0x89   //ͨ�г�ʱ
#define MINOR_INTRUSION_ALARM                           0x8a   //�󴳱���
#define MINOR_FREE_GATE_PASS_NOT_AUTH                   0x8b   //բ������ͨ��ʱδ��֤ͨ��
#define MINOR_DROP_ARM_BLOCK                            0x8c   //�ڱ۱��赲
#define MINOR_DROP_ARM_BLOCK_RESUME                     0x8d   //�ڱ��赲����
#define MINOR_LOCAL_FACE_MODELING_FAIL                  0x8e   //�豸��������������ģʧ��
#define MINOR_STAY_EVENT                                0x8f   //�����¼�
#define MINOR_PASSWORD_MISMATCH                         0x97   //���벻ƥ��
#define MINOR_EMPLOYEE_NO_NOT_EXIST                     0x98   //���Ų�����
#define MINOR_COMBINED_VERIFY_PASS                      0x99   //�����֤ͨ��
#define MINOR_COMBINED_VERIFY_TIMEOUT                   0x9a   //�����֤��ʱ
#define MINOR_VERIFY_MODE_MISMATCH                      0x9b   //��֤��ʽ��ƥ��

#define MINOR_PASSPORT_VERIFY_FAIL                       0xa1   //������ϢУ��ʧ��

#define MINOR_EVENT_CUSTOM1                         0x500  //�Ž��Զ����¼�1
#define MINOR_EVENT_CUSTOM2                         0x501  //�Ž��Զ����¼�2
#define MINOR_EVENT_CUSTOM3                         0x502  //�Ž��Զ����¼�3
#define MINOR_EVENT_CUSTOM4                         0x503  //�Ž��Զ����¼�4
#define MINOR_EVENT_CUSTOM5                         0x504  //�Ž��Զ����¼�5
#define MINOR_EVENT_CUSTOM6                         0x505  //�Ž��Զ����¼�6
#define MINOR_EVENT_CUSTOM7                         0x506  //�Ž��Զ����¼�7
#define MINOR_EVENT_CUSTOM8                         0x507  //�Ž��Զ����¼�8
#define MINOR_EVENT_CUSTOM9                         0x508  //�Ž��Զ����¼�9
#define MINOR_EVENT_CUSTOM10                        0x509  //�Ž��Զ����¼�10
#define MINOR_EVENT_CUSTOM11                        0x50a  //�Ž��Զ����¼�11
#define MINOR_EVENT_CUSTOM12                        0x50b  //�Ž��Զ����¼�12
#define MINOR_EVENT_CUSTOM13                        0x50c  //�Ž��Զ����¼�13
#define MINOR_EVENT_CUSTOM14                        0x50d  //�Ž��Զ����¼�14
#define MINOR_EVENT_CUSTOM15                        0x50e  //�Ž��Զ����¼�15
#define MINOR_EVENT_CUSTOM16                        0x50f  //�Ž��Զ����¼�16
#define MINOR_EVENT_CUSTOM17                        0x510  //�Ž��Զ����¼�17
#define MINOR_EVENT_CUSTOM18                        0x511  //�Ž��Զ����¼�18
#define MINOR_EVENT_CUSTOM19                        0x512  //�Ž��Զ����¼�19
#define MINOR_EVENT_CUSTOM20                        0x513  //�Ž��Զ����¼�20
#define MINOR_EVENT_CUSTOM21                        0x514  //�Ž��Զ����¼�21
#define MINOR_EVENT_CUSTOM22                        0x515  //�Ž��Զ����¼�22
#define MINOR_EVENT_CUSTOM23                        0x516  //�Ž��Զ����¼�23
#define MINOR_EVENT_CUSTOM24                        0x517  //�Ž��Զ����¼�24
#define MINOR_EVENT_CUSTOM25                        0x518  //�Ž��Զ����¼�25
#define MINOR_EVENT_CUSTOM26                        0x519  //�Ž��Զ����¼�26
#define MINOR_EVENT_CUSTOM27                        0x51a  //�Ž��Զ����¼�27
#define MINOR_EVENT_CUSTOM28                        0x51b  //�Ž��Զ����¼�28
#define MINOR_EVENT_CUSTOM29                        0x51c  //�Ž��Զ����¼�29
#define MINOR_EVENT_CUSTOM30                        0x51d  //�Ž��Զ����¼�30
#define MINOR_EVENT_CUSTOM31                        0x51e  //�Ž��Զ����¼�31
#define MINOR_EVENT_CUSTOM32                        0x51f  //�Ž��Զ����¼�32
#define MINOR_EVENT_CUSTOM33                        0x520  //�Ž��Զ����¼�33
#define MINOR_EVENT_CUSTOM34                        0x521  //�Ž��Զ����¼�34
#define MINOR_EVENT_CUSTOM35                        0x522  //�Ž��Զ����¼�35
#define MINOR_EVENT_CUSTOM36                        0x523  //�Ž��Զ����¼�36
#define MINOR_EVENT_CUSTOM37                        0x524  //�Ž��Զ����¼�37
#define MINOR_EVENT_CUSTOM38                        0x525  //�Ž��Զ����¼�38
#define MINOR_EVENT_CUSTOM39                        0x526  //�Ž��Զ����¼�39
#define MINOR_EVENT_CUSTOM40                        0x527  //�Ž��Զ����¼�40
#define MINOR_EVENT_CUSTOM41                        0x528  //�Ž��Զ����¼�41
#define MINOR_EVENT_CUSTOM42                        0x529  //�Ž��Զ����¼�42
#define MINOR_EVENT_CUSTOM43                        0x52a  //�Ž��Զ����¼�43
#define MINOR_EVENT_CUSTOM44                        0x52b  //�Ž��Զ����¼�44
#define MINOR_EVENT_CUSTOM45                        0x52c  //�Ž��Զ����¼�45
#define MINOR_EVENT_CUSTOM46                        0x52d  //�Ž��Զ����¼�46
#define MINOR_EVENT_CUSTOM47                        0x52e  //�Ž��Զ����¼�47
#define MINOR_EVENT_CUSTOM48                        0x52f  //�Ž��Զ����¼�48
#define MINOR_EVENT_CUSTOM49                        0x530  //�Ž��Զ����¼�49
#define MINOR_EVENT_CUSTOM50                        0x531  //�Ž��Զ����¼�50
#define MINOR_EVENT_CUSTOM51                        0x532  //�Ž��Զ����¼�51
#define MINOR_EVENT_CUSTOM52                        0x533  //�Ž��Զ����¼�52
#define MINOR_EVENT_CUSTOM53                        0x534  //�Ž��Զ����¼�53
#define MINOR_EVENT_CUSTOM54                        0x535  //�Ž��Զ����¼�54
#define MINOR_EVENT_CUSTOM55                        0x536  //�Ž��Զ����¼�55
#define MINOR_EVENT_CUSTOM56                        0x537  //�Ž��Զ����¼�56
#define MINOR_EVENT_CUSTOM57                        0x538  //�Ž��Զ����¼�57
#define MINOR_EVENT_CUSTOM58                        0x539  //�Ž��Զ����¼�58
#define MINOR_EVENT_CUSTOM59                        0x53a  //�Ž��Զ����¼�59
#define MINOR_EVENT_CUSTOM60                        0x53b  //�Ž��Զ����¼�60
#define MINOR_EVENT_CUSTOM61                        0x53c  //�Ž��Զ����¼�61
#define MINOR_EVENT_CUSTOM62                        0x53d  //�Ž��Զ����¼�62
#define MINOR_EVENT_CUSTOM63                        0x53e  //�Ž��Զ����¼�63
#define MINOR_EVENT_CUSTOM64                        0x53f  //�Ž��Զ����¼�64

#define MINOR_LOCK_FINGER_OPEN_DOOR          0x600    //������ָ�ƿ���
#define MINOR_LOCK_PASSWORD_OPEN_DOOR        0x601    //���������뿪��
#define MINOR_LOCK_CARD_OPEN_DOOR            0x602    //������ˢ������
#define MINOR_LOCK_CENTER_OPEN_DOOR          0x603    //���������Ŀ���
#define MINOR_LOCK_APP_OPEN_DOOR             0x604    //������APP����
#define MINOR_LOCK_KEY_OPEN_DOOR             0x605    //������Կ�׿���
#define MINOR_LOCK_REMOTE_DEVICE_OPEN_DOOR   0x606    //������ң��������
#define MINOR_LOCK_TMP_PASSWORD_OPEN_DOOR    0x607    //��������ʱ���뿪��
#define MINOR_LOCK_BLUETOOTH_OPEN_DOOR       0x608    //��������������
#define MINOR_LOCK_MULTI_OPEN_DOOR           0x609 //���������ؿ���

//2018-04-23 ͨ�����������¼���־����
#define   MINOR_ALARMHOST_SCHOOLTIME_IRGI_B   0x1001     //B��Уʱ
#define   MINOR_ALARMHOST_SCHOOLTIME_SDK     0x1002     //SDKУʱ
#define   MINOR_ALARMHOST_SCHOOLTIME_SELFTEST    0x1003   //�����Լ�Уʱ
#define  MINOR_ALARMHOST_SUBSYSTEM_ABNORMALINSERT   0x1004  //�Ӱ���� 
#define  MINOR_ALARMHOST_SUBSYSTEM_ABNORMALPULLOUT   0x1005  //�Ӱ�γ�

#define  MINOR_ALARMHOST_AUTO_ARM    0x1006  //�Զ�����
#define  MINOR_ALARMHOST_AUTO_DISARM    0x1007  //�Զ�����
#define  MINOR_ALARMHOST_TIME_TIGGER_ON    0x1008  //��ʱ����������
#define  MINOR_ALARMHOST_TIME_TIGGER_OFF   0x1009  //��ʱ�رմ�����
#define  MINOR_ALARMHOST_AUTO_ARM_FAILD    0x100a  //�Զ�����ʧ��
#define  MINOR_ALARMHOST_AUTO_DISARM_FAILD    0x100b  //�Զ�����ʧ��
#define  MINOR_ALARMHOST_TIME_TIGGER_ON_FAILD   0x100c  //��ʱ����������ʧ��
#define  MINOR_ALARMHOST_TIME_TIGGER_OFF_FAILD    0x100d  //��ʱ�رմ�����ʧ��
#define  MINOR_ALARMHOST_MANDATORY_ALARM    0x100e  //ǿ�Ʋ���
#define  MINOR_ALARMHOST_KEYPAD_LOCKED  0x100f  //��������
#define  MINOR_ALARMHOST_USB_INSERT   0x1010  //USB���� 
#define  MINOR_ALARMHOST_USB_PULLOUT   0x1011 //USB�γ�
#define  MINOR_ALARMHOST_4G_MODULS_ONLINE  0x1012  //4Gģ������
#define  MINOR_ALARMHOST_4G_MODULS_OFFLINE  0x1013  //4Gģ������

#define  MINOR_EZVIZ_CLOUD_ONLINE   0x1014   //өʯ������
#define  MINOR_EZVIZ_CLOUD_OFFLINE    0x1015   //өʯ������

#define  MINOR_SIPUA_GRID_ONLINE     0x1016   //����B����
#define  MINOR_SIPUA_GRID_OFFLINE    0x1017   //����B����

#define  MINOR_INTERNET_ACCESS_CONNECTED  0x1018   //��������
#define  MINOR_INTERNET_ACCESS_BREAK  0x1019  //���ڶϿ�

#define  MINOR_WIRELESS_CONNECTED   0x101a  //��������
#define  MINOR_WIRELESS_BREAK     0x101b   //���߶Ͽ�


#endif

#endif //_EHOMEDEMO_LOGTYPE_H_