if "%1" == "Release" (	
	%3Infrastructure\ScriptBuilder\bin\%1\ScriptBuilder %1 %2 %3 %2App\build-system\script-builder.js
	del /F /Q %2App\build\*.* 
	node %2Scripts\libs\r.js -o %2App\build-system\modules.js
	node %2Scripts\libs\r.js -o %2App\build-system\main.js
)