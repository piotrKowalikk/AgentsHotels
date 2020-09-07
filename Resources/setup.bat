rem script partially setting up G1ANT software
rem user still have to activate the triggers manually
rem user's previous config is called former_G1ANT.Robot.config

rem set variables
set startdir=%cd%
set robotsdir=%cd%\Robots
set giantdir=C:\Users\%username%\Documents\G1ANT.Robot
cd ..
set repodir=%cd%
set giantconfig=G1ANT.Robot.config
cd %robotsdir%

rem create config file
echo ^<?xml version="1.0" encoding="utf-8"?^> > %giantconfig%
echo ^<Settings xmlns="http://g1ant.com/XmlSchema/v3/Settings"^> >> %giantconfig%
echo   ^<Main^> >> %giantconfig%
echo     ^<StopKey^>ctrl+f12^</StopKey^> >> %giantconfig%
echo     ^<ShowStepTime^>1000^</ShowStepTime^> >> %giantconfig%
echo   ^</Main^> >> %giantconfig%
echo   ^<Studio^> >> %giantconfig%
echo     ^<Font^> >> %giantconfig%
echo       ^<Name^>Courier New^</Name^> >> %giantconfig%
echo       ^<Size^>12^</Size^> >> %giantconfig%
echo     ^</Font^> >> %giantconfig%
echo     ^<Styling^>true^</Styling^> >> %giantconfig%
echo     ^<WordWrap^>true^</WordWrap^> >> %giantconfig%
echo   ^</Studio^> >> %giantconfig%
echo   ^<Variables /^> >> %giantconfig%
echo   ^<Triggers^> >> %giantconfig%
echo     ^<Trigger Name="trigger1" TaskName="booking.G1ANT" Class="FileTrigger" StartInstance="true"^> >> %giantconfig%
echo       ^<Arguments^> >> %giantconfig%
echo         ^<Argument Key="Directory"^>%repodir%\HotelsGUI\bin\Debug\netcoreapp3.1\BookingSearch^</Argument^> >> %giantconfig%
echo       ^</Arguments^> >> %giantconfig%
echo     ^</Trigger^> >> %giantconfig%
echo     ^<Trigger Name="trigger2" TaskName="hotels.G1ANT" Class="FileTrigger" StartInstance="true"^> >> %giantconfig%
echo       ^<Arguments^> >> %giantconfig%
echo         ^<Argument Key="Directory"^>%repodir%\HotelsGUI\bin\Debug\netcoreapp3.1\HotelsSearch^</Argument^> >> %giantconfig%
echo       ^</Arguments^> >> %giantconfig%
echo     ^</Trigger^> >> %giantconfig%
echo     ^<Trigger Name="trigger3" TaskName="trivago.G1ANT" Class="FileTrigger" StartInstance="true"^> >> %giantconfig%
echo       ^<Arguments^> >> %giantconfig%
echo         ^<Argument Key="Directory"^>%repodir%\HotelsGUI\bin\Debug\netcoreapp3.1\TrivagoSearch^</Argument^> >> %giantconfig%
echo       ^</Arguments^> >> %giantconfig%
echo     ^</Trigger^> >> %giantconfig%
echo   ^</Triggers^> >> %giantconfig%
echo   ^<Databases^> >> %giantconfig%
echo   ^</Databases^> >> %giantconfig%
echo   ^<Dashboard /^> >> %giantconfig%
echo   ^<Emails^> >> %giantconfig%
echo   ^</Emails^> >> %giantconfig%
echo   ^<Subscriptions^> >> %giantconfig%
echo   ^</Subscriptions^> >> %giantconfig%
echo   ^<WebServer^> >> %giantconfig%
echo   ^</WebServer^> >> %giantconfig%
echo ^</Settings^> >> %giantconfig%

rem copy all files
ren %giantdir%\G1ANT.Robot.config former_G1ANT.Robot.config
copy /y * %giantdir%
del G1ANT.Robot.config
cd %startdir%