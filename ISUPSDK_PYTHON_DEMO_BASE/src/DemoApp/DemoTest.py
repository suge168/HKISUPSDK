# -*- coding: utf-8 -*-
# @Time : 2023/9/8 14:55
# @Author : qiu jiahang
import os

if __name__ == '__main__':
    dir = os.path.abspath(os.path.dirname(os.path.dirname(__file__)))
    d = f"{dir}\lib"
    print(d)