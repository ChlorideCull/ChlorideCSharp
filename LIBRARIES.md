#Libraries
This file details the different libraries available in this repo and the Chloride-namespace.

##Chloride.DIAL
Implements the DIAL protocol, version [1.6.4](http://www.dial-multiscreen.org/dial-protocol-specification/DIAL-2ndScreenProtocol-1.6.4.pdf?attredirects=0&d=1).

**Status:** InDev/Not suitable for production

###Instructions
Use DIALClient.SearchDIALDevices(). Function names are quite self explainatory.

##Chloride.XML
Implements a more humane XML implementation aptly named HumaneXML.

**Status:** InDev/Not suitable for production

###Instruction
HumaneXML.MakeNode("/our/xml/file") creates a node. You can use standard indexing (node["settings"], node[0])
or the self explainatory functions.