Setup environment

Middleware: install c#, .net 6, and JetBrains Rider IDE.

Backend: install npm first, then install nodejs, express, and mongoDB.

Frontend: install flutter and android studio.

Beside the above thing, user also need to install an intranet penetration tool to connect the watch to middle. The tool we use is cpolar. https://www.cpolar.com/

Also, user is recommended to install vscode to run the backend and frontend.

Finally, user is required to download our 4 source code folders from the zip file. Alternatively, user can also download them from GitHub. https://github.com/Hounds-of-Tindalos

Start project

First, user need to open cpolar, login with their account, initiate a tunnel. For example, we have a tunnel called tcp-testing.

![image](https://github.com/jyao244/hounds-of-tindalos/assets/68982751/83f49ba6-1964-4485-918c-a8a3454d1e01)
 
Then, go to state and find the public network address of it. For our example, it is tcp://1.tcp.cpolar.io:10606.

![image](https://github.com/jyao244/hounds-of-tindalos/assets/68982751/70ef55ae-39ed-4166-a448-a32b2765e3f8)
 
Then, user need to run “npm i” to install all the required packages and then run “npm run start” to start the backend.

![image](https://github.com/jyao244/hounds-of-tindalos/assets/68982751/188ced2c-c79a-417a-8861-baea64cddeaf)
 
Then, user can open the JetBrains Rider IDE and press run to start the middleware.

![image](https://github.com/jyao244/hounds-of-tindalos/assets/68982751/163bcfaf-00d7-4255-8209-fc8aaf000ca1)
 
Then, user can open the vscode and open the emulator by using android studio.
Or, user can press ctrl+shift+p and search “flutter lunch emulator” to start the emulator by vscode.

![image](https://github.com/jyao244/hounds-of-tindalos/assets/68982751/91a3098f-03ef-4591-bb43-f1a43ad609c4)

Then, user can press “run” and choose “start debugging” to run the app.

![image](https://github.com/jyao244/hounds-of-tindalos/assets/68982751/cb9f861c-6d94-4e39-8f70-3a3622f719bc)

If user want to use it on their phone, they need to go to tracker/lib/utils/global_variables.dart, change line 14 to the public network address described above. For our example, is change to

![image](https://github.com/jyao244/hounds-of-tindalos/assets/68982751/5255bdd9-26da-498b-b8ea-748ed163f602)

Then, user can connect their mobile phone to computer, click “run” as shown above and choose “start debugging” to install the application on their phone.

Artitecture

![artitecture](https://github.com/jyao244/hounds-of-tindalos/assets/68982751/3374799f-34aa-432a-9f29-3f63c553d879)
