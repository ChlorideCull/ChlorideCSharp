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

##Chloride.ReCaptcha
Implements a simple way to deal with ReCaptchas, seen from a client's point of view.

**Status:** InDev/Not suitable for production

###Instruction
Create the class with Captcha(key). Use .GetData() to retrieve the captcha image and the `recaptcha_challenge_field` (Challenge).
Put the answer in `recaptcha_response_field` when submitting the form. It should pass validation.

##Chloride.SOCKS
Implements SOCKS server-side (client-side is built into .NET/Mono).

**Status:** Useable - Actively used by creator

###Instructions
Create a SOCKS4 class, run with .Listen(proxyMethod, authMethod), or without arguments to use the default ones implemented in ProxyCommon.