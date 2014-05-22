[dev_stable_full]: http://i.imgur.com/Rx1SP2T.png
[dev_stable_sec]: http://i.imgur.com/nI1WO9y.png
[dev_in]: http://i.imgur.com/t80MnXb.png
[prod_notready]: http://i.imgur.com/toxWRXv.png
[prod_ready]: http://i.imgur.com/APTBLA0.png
[api_notready]: http://i.imgur.com/RbSD0rW.png
[api_ready]: http://i.imgur.com/ygZ3YzK.png

#Libraries
This file details the different libraries available in this repo and the Chloride-namespace.

##Chloride.DIAL
Implements the DIAL protocol, version [1.6.4](http://www.dial-multiscreen.org/dial-protocol-specification/DIAL-2ndScreenProtocol-1.6.4.pdf?attredirects=0&d=1).

<div style="display: flex;flex-direction: column;width: 128px;">
<img src="http://i.imgur.com/toxWRXv.png" />
<img src="http://i.imgur.com/t80MnXb.png" />
<img src="http://i.imgur.com/RbSD0rW.png" />
</div>

###Instructions
Use DIALClient.SearchDIALDevices(). Function names are quite self explainatory.

##Chloride.XML
Implements a more humane XML implementation aptly named HumaneXML.

<div style="display: flex;flex-direction: column;width: 128px;">
<img src="http://i.imgur.com/toxWRXv.png" />
<img src="http://i.imgur.com/t80MnXb.png" />
<img src="http://i.imgur.com/RbSD0rW.png" />
</div>

###Instruction
HumaneXML.MakeNode("/our/xml/file") creates a node. You can use standard indexing (node["settings"], node[0])
or the self explainatory functions.

##Chloride.ReCaptcha
Implements a simple way to deal with ReCaptchas, seen from a client's point of view.

<div style="display: flex;flex-direction: column;width: 128px;">
<img src="http://i.imgur.com/toxWRXv.png" />
<img src="http://i.imgur.com/t80MnXb.png" />
<img src="http://i.imgur.com/RbSD0rW.png" />
</div>

###Instruction
Create the class with Captcha(key). Use .GetData() to retrieve the captcha image and the `recaptcha_challenge_field` (Challenge).
Put the answer in `recaptcha_response_field` when submitting the form. It should pass validation.

##Chloride.SOCKS
Implements SOCKS server-side (client-side is built into .NET/Mono).

<div style="display: flex;flex-direction: column;width: 128px;">
<img src="http://i.imgur.com/APTBLA0.png" />
<img src="http://i.imgur.com/Rx1SP2T.png" />
<img src="http://i.imgur.com/ygZ3YzK.png" />
</div>

###Instructions
Create a SOCKS4 class, run with .Listen(proxyMethod, authMethod), or without arguments to use the default ones implemented in ProxyCommon.
