# -*- mode: python -*-

block_cipher = None


a = Analysis(['C:\\Users\\ad\\Documents\\Visual Studio 2012\\Projects\\Application_OneVideo\\BeApp\\FeatureExtraction.py'],
             pathex=['C:\\Users\\ad\\Documents\\Visual Studio 2012\\Projects\\Application_OneVideo\\BeApp'],
             binaries=[],
             datas=[],
             hiddenimports=[],
             hookspath=[],
             runtime_hooks=[],
             excludes=[],
             win_no_prefer_redirects=False,
             win_private_assemblies=False,
             cipher=block_cipher)
pyz = PYZ(a.pure, a.zipped_data,
             cipher=block_cipher)
exe = EXE(pyz,
          a.scripts,
          a.binaries,
          a.zipfiles,
          a.datas,
          name='FeatureExtraction',
          debug=False,
          strip=False,
          upx=True,
          console=True )
