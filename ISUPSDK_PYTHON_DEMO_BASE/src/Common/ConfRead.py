# -*- coding: utf-8 -*-
# @Time : 2023/9/13 18:39
# @Author : qiu jiahang

import json
import xml.etree.ElementTree as ET
import os

from src.Common import glo


def getConfJson():
    jsonStr = ''
    # 获取当前文件的绝对路径
    current_dir = os.path.dirname(os.path.abspath(__file__))
    # 构建配置文件的路径（从src/Common向上两级到达项目根目录，然后进入resources/conf）
    conf_path = os.path.join(os.path.dirname(os.path.dirname(current_dir)), "resources", "conf", "isupConf.json")
    
    with open(conf_path, "r", encoding="utf-8") as f:
        for line in f:
            if (line.strip().startswith("//")):
                continue
            else:
                jsonStr += line
        content = json.loads(jsonStr)
        # print(content)
    return content

def loadJsonConfToGlobal(jsonStr):
    glo._init()
    for key, value in jsonStr['ss'].items():
        # print(key, ":", value)
        glo.set_value(key, value)
    for key, value in jsonStr['cms'].items():
        # print(key, ":", value)
        glo.set_value(key, value)
    for key, value in jsonStr['ams'].items():
        # print(key, ":", value)
        glo.set_value(key, value)
    for key, value in jsonStr['sms'].items():
        # print(key, ":", value)
        glo.set_value(key, value)
    glo.set_value("ISUPKey", jsonStr["ISUPKey"])

def get_req_body_from_template(template_path, parameters):
    # 解析 XML 文件
    tree = ET.parse(template_path)
    root = tree.getroot()

    # 注册命名空间，防止 ns0 前缀
    ET.register_namespace('', 'http://www.isapi.org/ver20/XMLSchema')

    # 遍历所有元素，检查文本中是否有占位符需要替换
    for elem in root.iter():
        if elem.text and isinstance(elem.text, str):  # 检查元素是否有文本
            for key, value in parameters.items():
                # 查找并替换占位符 ${key}
                if f"${{{key}}}" in elem.text:
                    elem.text = elem.text.replace(f"${{{key}}}", value)

    # 返回修改后的 XML 字符串
    return ET.tostring(root, encoding='utf-8', xml_declaration=True)



if __name__ == '__main__':
    loadJsonConfToGlobal(getConfJson())