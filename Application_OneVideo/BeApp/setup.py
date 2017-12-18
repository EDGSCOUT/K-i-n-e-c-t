#__author__ = 'zhangzhan'
# -*- coding: utf-8 -*-
# data 2015-9-15
#用于打包python程序成可执行文件


from cx_Freeze import setup, Executable

# Dependencies are automatically detected, but it might need
# fine tuning.
# buildOptions = dict(
#   packages = [], excludes = [],
#   include_files = ['image', 'Release','nlpir','textmind','PyQt4','ltp32.dll','test.py','wenxin_5.py'],
# )
#
# name = 'wenxin'
#
# if sys.platform == 'win32':
#   name = name + '.exe'
#
# base = None
# if sys.platform == "win32":
#     base = "Win32GUI"
#
# executables = [
#   Executable('wenxin_form5.py', base = base, targetName = name,
#              compress = True,
#             )
# ]
#
# setup(name='wenxin-32',
#       version = '1.0',
#       description = 'An example program',
#       options = dict(build_exe = buildOptions),
#       executables = executables)
#
# import sys
#
# from cx_Freeze import setup, Executable
#
# # Dependencies are automatically detected, but it might need
# # fine tuning.
# buildOptions = dict(
#   packages = [], excludes = [],
#   include_files = [],
# )
#
# name = "YYBetaApp"
#
# if sys.platform == 'win32':
#   name = name + '.exe'
#
# base = None
# if sys.platform == "win32":
#     base = "Win32GUI"
#
# executables = [
#   Executable('FeatureExtraction.py', base = base, targetName = name,
#              compress = True,
#             )
# ]
#
# setup(name='YYBeta',
#       version = '1.0',
#       description = 'An example program',
#       options = dict(build_exe = buildOptions),
#       executables = executables)
import sys
import numpy as np
import codecs
import csv
import os
from numpy import zeros
from numpy import genfromtxt
from scipy import stats
from scipy import special
from distutils.core import setup
import py2exe

setup(
    console=['FeatureExtraction.py'],
    options={
        'py2exe': {
            r'includes': [r'scipy.sparse.csgraph._validation',
                          r'scipy.special._ufuncs_cxx']
        }
    }
)