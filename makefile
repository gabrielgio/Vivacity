CSC=xbuild
CSR=mono
J=java -jar
RM=rm -rf
PUML=plantuml.jar

library-release:
	$(CSC) /p:Configuration=Release ./Cities.Library/Cities.Library.csproj

view:
	$(MAKE) view-release
	$(CSR) ./Cities.View/bin/Release/Cities.View.exe

library-debug:
	$(CSC) /p:Configuration=Debug ./Cities.Library/Cities.Library.csproj

view-release:
	$(CSC) /p:Configuration=Release ./Cities.View/Cities.View.csproj

view-debug:
	$(CSC) /p:Configuration=Debug ./Cities.View/Cities.View.csproj

diagram:
	$(J) $(PUML) Cities.puml "Flow Cities.puml"

clear:
	$(RM) ./Cities.Library/bin
	$(RM) ./Cities.Library/obj
	$(RM) ./Cities.View/bin
	$(RM) ./Cities.View/obj
