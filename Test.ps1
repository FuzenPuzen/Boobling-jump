$MSBUILD_PATH = 'C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe'
$SOLUTION_FILE = 'C:\Users\Ruslan\Documents\MLALib'

& "$MSBUILD_PATH" "$SOLUTION_FILE\MLALib.csproj" /p:Configuration=Release


xcopy /y /s /i "$SOLUTION_FILE\bin\Release\*.*" "Assets\Plugins\MLALib"

Read-Host