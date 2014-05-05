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
Create the class with Captcha(key), the key you can get by looking at the request URL with Javascript disabled
(an IFRAME with `//www.google.com/recaptcha/api/noscript?k=KEY`). Use .GetImage() to retrieve the captcha image,
and then .SubmitCaptcha("the words") to submit the captcha, retrieving the confirmation key. You use this key
by setting the `recaptcha_response_field` to `manual_challenge` and `recaptcha_challenge_field` to the
confirmation key.