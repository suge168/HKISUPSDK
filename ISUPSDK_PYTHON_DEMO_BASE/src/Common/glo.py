# -*- coding: utf-8 -*-
# @Time : 2023/9/13 12:36
# @Author : qiu jiahang

'''用于保存全局共享字段'''


def _init():  # 初始化
    global _global_dict
    _global_dict = {}


def set_value(key, value):
    # 定义一个全局变量
    _global_dict[key] = value


def get_value(key, defValue=None):
    # 获得全局变量，不存在则返回默认值
    try:
        return _global_dict[key]
    except KeyError:
        return defValue
