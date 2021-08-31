REM --- 変数定義 ---
SET NAME_OBJ="obj"
SET NAME_BIN="bin"
SET PATH_OUT=".\\%NAME_BIN%\Release"

REM --- 出力フォルダ削除 ---
RMDIR /S /Q .\%NAME_BIN%

REM --- ビルド実行 ---
FOR /R %%1 IN (*.sln) DO (
  "C:\Program Files (x86)\MSBuild\14.0\Bin\MSBuild.exe" ^
  "%%1" ^
  /t:Rebuild ^
  /p:Configuration=Release;OutputPath=.%PATH_OUT%
)

REM --- クリーンアップ ---
IF EXIST %TARGET%  GOTO START_CLEAN_UP ^
ELSE GOTO END_CLEAN_UP
:START_CLEAN_UP
  CD %PATH_OUT%
  DEL /Q *.xml
  DEL /Q *.config
:END_CLEAN_UP
  TIMEOUT 1

REM --- 中間ファイル削除 ---
CD ..\..\
RMDIR /S /Q .\Azuki\%NAME_OBJ%
RMDIR /S /Q .\AcroBat\%NAME_OBJ%
RMDIR /S /Q .\AcroPad\%NAME_OBJ%

TIMEOUT 1
REM PAUSE
