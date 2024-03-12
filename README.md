Setup environment

Middleware: install c#, .net 6, and JetBrains Rider IDE.

Backend: install npm first, then install nodejs, express, and mongoDB.

Frontend: install flutter and android studio.

Beside the above thing, user also need to install an intranet penetration tool to connect the watch to middle. The tool we use is cpolar. https://www.cpolar.com/

Also, user is recommended to install vscode to run the backend and frontend.

Finally, user is required to download our 4 source code folders from the zip file. Alternatively, user can also download them from GitHub. https://github.com/Hounds-of-Tindalos

Start project

First, user need to open cpolar, login with their account, initiate a tunnel. For example, we have a tunnel called tcp-testing.

![image](https://github.com/jyao244/hounds-of-tindalos/assets/68982751/813e04f4-25e0-4906-9cfd-2388b5528c8a)
 
Then, go to state and find the public network address of it. For our example, it is tcp://1.tcp.cpolar.io:10606.

![image](https://github.com/jyao244/hounds-of-tindalos/assets/68982751/efdf71d0-5eb2-429d-aade-01206e5aa7e4)
 
Then, user need to run “npm i” to install all the required packages and then run “npm run start” to start the backend.

![image](https://github.com/jyao244/hounds-of-tindalos/assets/68982751/849f1a75-ebf1-4b27-88b7-16538d0156f2)
 
Then, user can open the JetBrains Rider IDE and press run to start the middleware.

![image](https://github.com/jyao244/hounds-of-tindalos/assets/68982751/66bc08fc-d143-4d78-94a4-2fb985123ea2)
 
Then, user can open the vscode and open the emulator by using android studio.
Or, user can press ctrl+shift+p and search “flutter lunch emulator” to start the emulator by vscode.

![image](https://github.com/jyao244/hounds-of-tindalos/assets/68982751/7e4231fa-2cb0-400e-95b2-7f8bbc8f400c)

Then, user can press “run” and choose “start debugging” to run the app.

![image](https://github.com/jyao244/hounds-of-tindalos/assets/68982751/a6405bfd-a7b5-420e-95b2-e8f953ed76dd)

If user want to use it on their phone, they need to go to tracker/lib/utils/global_variables.dart, change line 14 to the public network address described above. For our example, is change to

![image](https://github.com/jyao244/hounds-of-tindalos/assets/68982751/8d92bf9f-13d2-4111-a6ed-982b4f340317)

Then, user can connect their mobile phone to computer, click “run” as shown above and choose “start debugging” to install the application on their phone.

Artitecture

![artitecture](https://github.com/jyao244/hounds-of-tindalos/assets/68982751/ed9227f2-c852-4c9f-9097-4370c36793a7)
