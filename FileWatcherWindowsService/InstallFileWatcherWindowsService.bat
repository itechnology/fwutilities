sc delete "File Watcher Windows Service"
sc create "File Watcher Windows Service" binPath= C:\FileWatcherWindowsService\FileWatcherWindowsService.exe
sc description "File Watcher Windows Service" "Watches for file system changes in the system."