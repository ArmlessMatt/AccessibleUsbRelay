# AccessibleUsbRelay

This software simply turns on and off USB relays connected to the PC. You can then connect devices to the relays and turn them on and off.

## Requirements 
* Windows 10
* A Supported USB Relay (see --> [List of relays](#relays-list))

## Installation 
Use the lastest release MSI installer. Link to releases --> [Release](https://github.com/ArmlessMatt/AccessibleUsbRelay/releases)

## How to use the software
### The Relay List
Shows a list of configured relays.

![image](https://user-images.githubusercontent.com/8997198/133917159-a513a7a6-5891-45a1-be7e-92d02e2cdefd.png)

### Adding A Relay
Click on add relay

![image](https://user-images.githubusercontent.com/8997198/133917212-654dce00-8143-4754-8d2c-bf08d46a24d1.png)

![image](https://user-images.githubusercontent.com/8997198/133917381-35384509-69b0-4ef8-87e9-50231217ae07.png)

Fill the form 
* Name (Nom) - A name so you can find the relay quickly.
* Port (Port) - The usb port for the relay. The list contains the port found in device manager --> Ports(COM & LPT).
* Relay type (Type de relais) - Relay model. See list of supported relays.
* Delay before action (délai avant action) - Time in seconds before the relay is turned on.
* Auto stop (Arrêt auto) - Checking this box will turn off the relay after a certain duration.
* Delay before stop (délai avant arrêt) - Duration before auto stop. 

Click accept(accepter)

### Using a relay

![image](https://user-images.githubusercontent.com/8997198/133917410-33d48f33-51fa-4a95-b8f4-6f433dd9f112.png)

* Click the radio button on the left side of the relays list.
* Click the shortcut button at the bottom of the app.

This will create a shortcut on your desktop to call a certain relay (the one that was selected). When using the shortcut, only the relay will be called and no app will be shown.

(#relays-list)
## List Of Supported Relays
* LCUS-1 (Unafiliated [Amazon link](https://www.amazon.ca/Intelligent-Control-Overcurrent-Protection-Freewheel/dp/B07RX7DKHW/ref=sr_1_5?dchild=1&keywords=usb+relay&qid=1632026160&sr=8-5))
