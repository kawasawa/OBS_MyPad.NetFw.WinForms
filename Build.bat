REM --- �ϐ���` ---
SET NAME_OBJ="obj"
SET NAME_BIN="bin"
SET PATH_OUT=".\\%NAME_BIN%\Release"

REM --- �o�̓t�H���_�폜 ---
RMDIR /S /Q .\%NAME_BIN%

REM --- �r���h���s ---
FOR /R %%1 IN (*.sln) DO (
  "C:\Program Files (x86)\MSBuild\14.0\Bin\MSBuild.exe" ^
  "%%1" ^
  /t:Rebuild ^
  /p:Configuration=Release;OutputPath=.%PATH_OUT%
)

REM --- �N���[���A�b�v ---
IF EXIST %TARGET%  GOTO START_CLEAN_UP ^
ELSE GOTO END_CLEAN_UP
:START_CLEAN_UP
  CD %PATH_OUT%
  DEL /Q *.xml
  DEL /Q *.config
:END_CLEAN_UP
  TIMEOUT 1

REM --- ���ԃt�@�C���폜 ---
CD ..\..\
RMDIR /S /Q .\Azuki\%NAME_OBJ%
RMDIR /S /Q .\AcroBat\%NAME_OBJ%
RMDIR /S /Q .\AcroPad\%NAME_OBJ%

TIMEOUT 1
REM PAUSE
